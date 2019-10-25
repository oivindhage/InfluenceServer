using System;
using System.Collections.Generic;
using System.Linq;
using Influence.Common.Extensions;
using Influence.Domain;

namespace Influence.Services.Bot
{
    public class SampleBot : IBot
    {
        public Guid Id { get; }
        public string Name { get; }

        public SampleBot(string name, Guid id)
        {
            Name = name;
            Id = id;
        }

        public Tile GetTileToReinforce(Session session)
            => session
                .CurrentBoard
                .AllTiles
                .Where(CurrentBotOwnsTile)
                .Where(TileIsNotFull)
                .ToList()
                .Random();

        public MoveInstruction GetMoveOrAttackInstruction(Session session)
        {
            var attackers = GetPotentialAttackers(session);
            if (!attackers.Any())
                return null;

            var attacker = attackers.Random();
            var destination = AttackableDestinations(attacker).Random();
            return new MoveInstruction(attacker.from, destination);
        }

        private List<(Tile from, List<Tile> destinations)> GetPotentialAttackers(Session session)
            => session
                .CurrentBoard
                .AllTiles
                .Where(CurrentBotOwnsTile)
                .Where(TileHasMoreThanOneTroop)
                .Select(t => GetPotentialDestinationTiles(t, session.CurrentBoard.AllTiles))
                .Where(TileHasAttackableDestinations)
                .ToList();

        private bool TileHasAttackableDestinations((Tile from, List<Tile> destinations) tileContext)
            => AttackableDestinations(tileContext)
                .Any();

        private List<Tile> AttackableDestinations((Tile from, List<Tile> destinations) tileContext)
            => tileContext
                .destinations
                .Where(d => d.OwnerId != tileContext.from.OwnerId)
                .ToList();

        private bool TileHasMoreThanOneTroop(Tile t)
            => t.NumTroops > 1;

        private bool CurrentBotOwnsTile(Tile t)
            => t.OwnerId == Id;

        private (Tile from, List<Tile> destinations) GetPotentialDestinationTiles(Tile from, List<Tile> allTiles)
        {
            var destinations = allTiles
                .Where(to =>
                       (to.X == from.X && Math.Abs(to.Y - from.Y) == 1)
                    || (to.Y == from.Y && Math.Abs(to.X - from.X) == 1)
                 )
                .ToList();
            return (from, destinations);
        }

        private bool TileIsNotFull(Tile tile)
            => tile.NumTroops < RuleSet.Default.MaxNumTroopsInTile;
    }
}