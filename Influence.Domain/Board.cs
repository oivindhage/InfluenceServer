using System;
using System.Collections.Generic;
using System.Linq;
using Influence.Common.Extensions;
using Influence.Common.Utils;

namespace Influence.Domain
{
    public class Board
    {
        public int Size;
        public RuleSet RuleSet;

        private static readonly Random Rand = new Random();
        public Dictionary<Guid, List<Tile>> TilesOfPlayers = new Dictionary<Guid, List<Tile>>();
        public Dictionary<int, Tile> TilesById = new Dictionary<int, Tile>();

        public List<TileRow> TileRows { get; set; }

        public Board() { }

        public Board(RuleSet ruleSet)
        {
            Size = ruleSet.BoardSize;
            RuleSet = ruleSet;

            TileRows = new List<TileRow>();
            TilesOfPlayers.Clear();

            for (int rowNum = 0; rowNum < ruleSet.BoardSize; rowNum++)
            {
                var row = new TileRow(rowNum);

                for (int colNum = 0; colNum < ruleSet.BoardSize; colNum++)
                {
                    var tile = new Tile(colNum, rowNum, ruleSet.BoardSize);
                    row.Tiles.Add(tile);
                }

                TileRows.Add(row);
            }

            TilesById.Clear();
            foreach (var row in TileRows)
                foreach (var tile in row.Tiles)
                    TilesById.Add(tile.Id, tile);
        }

        private bool TileIsNextToTile(Tile source, Tile dest)
        {
            if (source.X == dest.X && Math.Abs(source.Y - dest.Y) == 1)
                return true;

            if (source.Y == dest.Y && Math.Abs(source.X - dest.X) == 1)
                return true;

            return false;
        }

        private string Attack(int numAttackers, int numDefenders, out bool isAttackSuccessful, out int numTroopsLeftInDestinationCell)
        {
            var log = string.Empty;

            int attackerDomination = numAttackers - numDefenders;
            int winPercent =
                attackerDomination > 1 ? 100
                    : attackerDomination == 1 ? 75
                        : attackerDomination == 0 ? 50
                            : attackerDomination == -1 ? 25
                                : 0;

            isAttackSuccessful = Rng.Chance(winPercent);

            log += $"Kamp! {numAttackers} vs {numDefenders} gir {winPercent}% vinnersjanse" + Environment.NewLine;

            if (isAttackSuccessful)
                numTroopsLeftInDestinationCell = Math.Max(1, numAttackers - numDefenders);
            else
                numTroopsLeftInDestinationCell = Math.Max(1, numDefenders - numAttackers);

            log += isAttackSuccessful
                ? $"Angriper tar over cellen med {numTroopsLeftInDestinationCell} tropper igjen"
                : $"Forsvarer holder fortet med {numTroopsLeftInDestinationCell} tropper igjen";

            return log;
        }

        private void UpdateTile(int tileX, int tileY, Player owner, int numTroops)
        {
            var tile = GetTile(tileX, tileY);

            if (tile.OwnerId.NotValid())
            {
                // Tile was not owned, give to player
                if (!TilesOfPlayers.ContainsKey(owner.Id))
                    TilesOfPlayers.Add(owner.Id, new List<Tile> { tile });
                else
                    TilesOfPlayers[owner.Id].Add(tile);
            }

            else if (tile.OwnerId != owner.Id)
            {
                // Tile changed ownership, remove from old owner
                TilesOfPlayers[tile.OwnerId].RemoveAt(TilesOfPlayers[tile.OwnerId].FindIndex(c => c.Id == tile.Id));

                // And give to new
                if (!TilesOfPlayers.ContainsKey(owner.Id))
                    TilesOfPlayers.Add(owner.Id, new List<Tile> { tile });
                else
                    TilesOfPlayers[owner.Id].Add(tile);
            }

            tile.NumTroops = Math.Min(RuleSet.MaxNumTroopsInTile, numTroops);
            tile.OwnerId = owner.Id;
            tile.OwnerName = owner.Name;
            tile.OwnerColorRgbCsv = owner.ColorRgbCsv;
        }

        private void UpdateTile(Tile tile, Player owner, int numTroops)
            => UpdateTile(tile.X, tile.Y, owner, numTroops);

        public void PlacePlayers(List<Player> players)
        {
            var startPositions = new List<Coordinate>();
            while (startPositions.Count < players.Count)
            {
                var position = new Coordinate(Rand.Next(0, Size), Rand.Next(0, Size));
                if (startPositions.All(p => p.Coordinates != position.Coordinates))
                    startPositions.Add(position);
            }

            for (var playerNum = 0; playerNum < players.Count; playerNum++)
                UpdateTile(startPositions[playerNum].X, startPositions[playerNum].Y, players[playerNum], RuleSet.NumTroopsInStartTile);
        }

        public Tile GetTile(int tileId)
            => TilesById[tileId];

        public Tile GetTile(int x, int y)
            => TileRows[y].Tiles[x];

        public List<Tile> GetTilesOfPlayer(Player player)
            => TilesOfPlayers[player.Id];

        public void GrantReinforcements(Player player) 
            => player.NumAvailableReinforcements += GetTilesOfPlayer(player).Count;

        public string Move(Player player, int fromCellId, int toCellId, List<Participant> participants, out string attackLog, out Participant deadDefender)
        {
            deadDefender = null;
            attackLog = string.Empty;

            var sourceTile = GetTilesOfPlayer(player).FirstOrDefault(c => c.Id == fromCellId);
            if (sourceTile == null)
                return $"{player.Name} eier ikke cellenummer {fromCellId}";

            if (sourceTile.NumTroops < 2)
                return "Man kan ikke flytte eller angripe fra en celle med mindre enn 2 tropper";

            Tile destTile;
            if (!TilesById.TryGetValue(toCellId, out destTile))
                return $"Det finnes ingen celle med id {toCellId}";

            if (destTile.OwnerId == player.Id)
                return "Man kan ikke flytte til eller angripe celler man allerede eier";

            if (!TileIsNextToTile(sourceTile, destTile))
                return "Man kan kun flytte ett hakk til venstre, opp, høyre eller ned";

            if (destTile.OwnerId == Guid.Empty)
            {
                UpdateTile(destTile, player, sourceTile.NumTroops - 1);
                UpdateTile(sourceTile, player, 1);
                attackLog = $"{player.Name} tar over celle {destTile.Coordinates} med {destTile.NumTroops} tropp(er)";
                return string.Empty;
            }

            Player defender = null;
            bool isAttackSuccessful;
            int numTroopsLeftInDestCell;
            attackLog = Attack(sourceTile.NumTroops, destTile.NumTroops, out isAttackSuccessful, out numTroopsLeftInDestCell);

            UpdateTile(sourceTile, player, 1);
            UpdateTile(destTile, isAttackSuccessful ? player : (defender = participants.Single(p => p.Player.Id == destTile.OwnerId).Player), numTroopsLeftInDestCell);

            if (defender != null && TilesOfPlayers[defender.Id].Count == 0)
                deadDefender = participants.First(p => p.Player.Id == defender.Id);

            return string.Empty;
        }
    }
}
