using System;
using System.Collections.Generic;
using System.Linq;
using Influence.Common.Extensions;

namespace Influence.Domain
{
    public class Board
    {
        public int Size;
        public RuleSet RuleSet;

        private static readonly Random Rand = new Random();
        public Dictionary<Guid, List<Tile>> TilesOfPlayers = new Dictionary<Guid, List<Tile>>();
        public Dictionary<int, Tile> TilesById = new Dictionary<int, Tile>();
        public List<Tile> DirtyTiles { get; set; } = new List<Tile>();

        public List<TileRow> TileRows { get; set; }

        public Board()
        { }

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

        public void UpdateTile(int tileX, int tileY, Player owner, int numTroops)
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

            if (!DirtyTiles.Any(d => d.X == tileX && d.Y == tileY))
                DirtyTiles.Add(tile);
        }

        public void UpdateTile(Tile tile, Player owner, int numTroops)
            => UpdateTile(tile.X, tile.Y, owner, numTroops);

        public void ClearDirtyTileList()
            => DirtyTiles.Clear();

        public Tile GetTile(int tileId)
            => TilesById[tileId];

        public Tile GetTile(int x, int y)
            => TileRows[y].Tiles[x];

        public List<Tile> GetTilesOfPlayer(Player player)
            => TilesOfPlayers[player.Id];

        public void GrantReinforcements(Player player) 
            => player.NumAvailableReinforcements = GetTilesOfPlayer(player).Count;
    }
}
