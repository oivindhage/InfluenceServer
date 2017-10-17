using System;
using System.Collections.Generic;
using System.Linq;

namespace Influence.Domain
{
    public class Session
    {
        private static readonly List<string> PlayerColors = new List<string> { "255,0,0", "0,0,255", "0,255,0", "128,128,0" };

        public Guid Id { get; }

        public List<Player> Players { get; }
        public int RoundNumber { get; set; }
        public GameState GameState { get; }
        public RuleSet RuleSet { get; }
        public Board Board { get; set; }

        public Session(RuleSet ruleSet, Guid id = default(Guid))
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id;
            Players = new List<Player>();
            GameState = new GameState();
            RoundNumber = 0;
            RuleSet = ruleSet;
            GenerateNewBoard();
        }

        public void GenerateNewBoard()
            => new Board(RuleSet.BoardSize);

        public bool AddPlayer(Guid playerId, string playerName)
        {
            if (playerId != Guid.Empty && Players.All(p => p.Id != playerId)
                && Players.All(p => !p.Name.Equals(playerName, StringComparison.InvariantCultureIgnoreCase))
                && Players.Count < RuleSet.MaxNumPlayersInGame)
            {
                Players.Add(new Player(playerId, playerName, PlayerColors[Players.Count]));
                return true;
            }

            return false;
        }
    }
}