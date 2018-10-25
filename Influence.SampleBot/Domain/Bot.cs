using System;
using System.Collections.Generic;
using System.Linq;
using Influence.Domain;

namespace Influence.SampleBot.Domain
{
    public class Bot
    {
        public Guid Id { get; }
        public string Name { get; }

        public Bot(string name)
        {
            Name = name;
            Id = Guid.NewGuid();
        }

        public MoveInstruction MoveAndAttack(List<Tile> allTiles, Func<Tile, bool> playerCanMoveOrAttackFromTileFunc, Func<Tile, List<Tile>> getPossibleDestinationTiles)
        {
            var tilesICanMoveFrom = allTiles.Where(playerCanMoveOrAttackFromTileFunc);
            foreach (var sourceTile in tilesICanMoveFrom)
            {
                var nearbyTiles = getPossibleDestinationTiles(sourceTile);

                var destinationTile = nearbyTiles.FirstOrDefault(t => t.OwnerId == Guid.Empty) ?? nearbyTiles.FirstOrDefault(t => t.OwnerId != Id);
                if (destinationTile != null)
                    return new MoveInstruction(sourceTile, destinationTile);
            }

            return null;
        }

        public Tile Reinforce(List<Tile> allTiles, Func<Tile, bool> playerCanReinforceTile) 
            => allTiles.FirstOrDefault(playerCanReinforceTile);
    }
}