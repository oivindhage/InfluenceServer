using Influence.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Influence.GameClient.Mock
{
    public class DummyService
    {
        static Random random = new Random();

        public static Session GetDummySession()
        {
            Session session = new Session(new RuleSet(6, 2, 5, 4), Guid.NewGuid());
            session.Board = new Board(session.RuleSet.BoardSize);
            AddPlayersToSession(session);
            AddStartTilesForPlayers(session);
            AddGameState(session);
            AddParticipants(session);
            return session;
        }

        private static void AddGameState(Session session)
        {
            session.GameState.CurrentPlayer = session.Players.OrderBy(x => random.NextDouble()).First();
            session.GameState.PlayerPhase = Consts.PlayerPhase.Undefined;
            session.GameState.GamePhase = Consts.GamePhase.NotStarted;
        }

        private static void AddPlayersToSession(Session session)
        {
            session.Players.AddRange(new[] {
                new Player(Guid.NewGuid(), "Archer", "255,120,120"),
                new Player(Guid.NewGuid(), "Krieger", "120,255,120"),
                new Player(Guid.NewGuid(), "Lana", "120,120,255"),
                new Player(Guid.NewGuid(), "Cheryl", "255,120,255")
            });
        }

        private static void AddParticipants(Session session)
        {
            var participants = new List<Participant>();
            foreach (var player in session.Players)
            {
                var participant = new Participant(player);
                var ownedTiles = session.Board.TileRows
                    .SelectMany(x => x.Tiles)
                    .Where(x => player.Name.Equals(x.OwnerName));
                participant.OwnedTiles.AddRange(ownedTiles);
                participant.IsAlive = true;
                participants.Add(participant);
            }
            session.GameState.Participants = participants;
        }

        private static void AddStartTilesForPlayers(Session session)
        {
            for (int i = 0; i < session.Players.Count;)
            {
                var tileRow = session.Board.TileRows.OrderBy(x => random.NextDouble()).First();
                var tile = tileRow.Tiles.OrderBy(x => random.NextDouble()).First();
                if (tile.NumTroops == 0)
                {
                    tile.OwnerName = session.Players[i].Name;
                    tile.OwnerId = session.Players[i].Id;
                    tile.NumTroops = session.RuleSet.NumTroopsInStartTile;
                    tile.OwnerColorRgbCsv = session.Players[i].ColorRgbCsv;
                    ++i;
                }
            }
        }
    }
}
