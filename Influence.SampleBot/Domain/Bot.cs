using System;
using System.Collections.Generic;
using System.Linq;
using Influence.Common.Extensions;
using Influence.Domain;

namespace Influence.SampleBot.Domain
{
    public class Bot
    {
        public Bot(string name)
        {
            Name = name;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }
        public string Name { get; }

        public MoveInstruction MoveAndAttack(IEnumerable<Tile> myTiles, int boardSize, Func<int, int, Tile> getTileFunc)
        {
            var tilesICanMoveFrom = myTiles.Where(t => t.NumTroops > 1);
            foreach (var tile in tilesICanMoveFrom)
            {
                var nearbyTiles = new List<Tile>();

                for (int xOffset = -1; xOffset <= 1; xOffset++)
                {
                    for (int yOffset = -1; yOffset <= 1; yOffset++)
                    {
                        if (Math.Abs(xOffset) + Math.Abs(yOffset) == 1)
                        {
                            var x = tile.X + xOffset;
                            var y = tile.Y + yOffset;
                            if (x.IsBetween(0, boardSize-1) && y.IsBetween(0, boardSize-1))
                                nearbyTiles.Add(getTileFunc(x, y)); 
                        }
                    }
                }

                var destinationTile = nearbyTiles.FirstOrDefault(t => t.OwnerId == Guid.Empty) ?? nearbyTiles.FirstOrDefault(t => t.OwnerId != Id);
                if (destinationTile != null)
                    return new MoveInstruction(GetActualTileId(tile.X, tile.Y, boardSize), GetActualTileId(destinationTile.X, destinationTile.Y, boardSize));
            }

            return null;
        }

        public ReinforceInstruction Reinforce(IEnumerable<Tile> myTiles, int boardSize, int ruleSetMaxNumTroopsInTile)
        {
            var tileICanReinforce = myTiles.FirstOrDefault(t => t.NumTroops < ruleSetMaxNumTroopsInTile);
            return tileICanReinforce == null 
                ? null 
                : new ReinforceInstruction(GetActualTileId(tileICanReinforce.X, tileICanReinforce.Y, boardSize));
        }

        // Deserialization of Tile.Id is buggy at this point due to ctor not getting all params right. Re-create tile in memory to get proper Id.
        private int GetActualTileId(int x, int y, int boardSize)
            => new Tile(x, y, boardSize).Id;
    }
}