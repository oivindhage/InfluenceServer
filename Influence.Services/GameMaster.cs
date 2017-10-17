using System.Collections.Generic;
using Influence.Domain;

namespace Influence.Services
{
    public class GameMaster
    {
        private static List<Session> Sessions { get; } = new List<Session>();

        public static List<Session> GetSessions() => Sessions;

        public static void CreateSession()
        {
            Sessions.Add(new Session
            {
                Board = new Board(6),
                GameState = new GameState { CurrentPlayer = null, GamePhase = GamePhaseEnum.NotStarted, PlayerPlayerPhase = PlayerPhaseEnum.Undefined},
                Players = new List<Player>(),
                RoundNumber = 0
            });
        }
    }
}
