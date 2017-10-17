using System;
using System.Collections.Generic;
using System.Linq;
using Influence.Common.Extensions;

namespace Influence.Domain
{
    public class Board
    {
        private readonly int _size;
        private readonly RuleSet _ruleSet;

        private static readonly Random Rand = new Random();
        private readonly Dictionary<Guid, List<Tile>> _tilesOfPlayers = new Dictionary<Guid, List<Tile>>();
        private readonly Dictionary<int, Tile> _tilesById = new Dictionary<int, Tile>();
        private List<Tile> DirtyTiles { get; } = new List<Tile>();

        public List<TileRow> TileRows { get; }

        public Board(RuleSet ruleSet)
        {
            _size = ruleSet.BoardSize;
            _ruleSet = ruleSet;

            TileRows = new List<TileRow>();
            _tilesOfPlayers.Clear();

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

            _tilesById.Clear();
            foreach (var row in TileRows)
                foreach (var tile in row.Tiles)
                    _tilesById.Add(tile.Id, tile);
        }

        public void PlacePlayers(List<Player> players)
        {
            var startPositions = new List<Coordinate>();
            while (startPositions.Count < players.Count)
            {
                var position = new Coordinate(Rand.Next(0, _size), Rand.Next(0, _size));
                if (startPositions.All(p => p.Coordinates != position.Coordinates))
                    startPositions.Add(position);
            }

            for (var playerNum = 0; playerNum < players.Count; playerNum++)
                UpdateTile(startPositions[playerNum].X, startPositions[playerNum].Y, players[playerNum], _ruleSet.NumTroopsInStartTile);
        }

        public void UpdateTile(int tileX, int tileY, Player owner, int numTroops)
        {
            var tile = GetTile(tileX, tileY);

            if (tile.OwnerId.NotValid())
            {
                // Tile was not owned, give to player
                if (!_tilesOfPlayers.ContainsKey(owner.Id))
                    _tilesOfPlayers.Add(owner.Id, new List<Tile> { tile });
                else
                    _tilesOfPlayers[owner.Id].Add(tile);
            }

            else if (tile.OwnerId != owner.Id)
            {
                // Tile changed ownership, remove from old owner
                _tilesOfPlayers[tile.OwnerId].RemoveAt(_tilesOfPlayers[tile.OwnerId].FindIndex(c => c.Id == tile.Id));

                // And give to new
                if (!_tilesOfPlayers.ContainsKey(owner.Id))
                    _tilesOfPlayers.Add(owner.Id, new List<Tile> { tile });
                else
                    _tilesOfPlayers[owner.Id].Add(tile);
            }

            tile.NumTroops = Math.Min(_ruleSet.MaxNumTroopsInTile, numTroops);
            tile.OwnerId = owner.Id;
            tile.OwnerColorRgbCsv = owner.ColorRgbCsv;

            if (!DirtyTiles.Any(d => d.X == tileX && d.Y == tileY))
                DirtyTiles.Add(tile);
        }

        public void UpdateTile(Tile tile, Player owner, int numTroops)
            => UpdateTile(tile.X, tile.Y, owner, numTroops);

        public void ClearDirtyTileList()
            => DirtyTiles.Clear();

        public Tile GetTile(int tileId)
            => _tilesById[tileId];

        public Tile GetTile(int x, int y)
            => TileRows[y].Tiles[x];

        public List<Tile> GetTilesOfPlayer(Player player)
            => _tilesOfPlayers[player.Id];

        public void GrantReinforcements(Player player) 
            => player.NumAvailableReinforcements = GetTilesOfPlayer(player).Count;
    }
}
