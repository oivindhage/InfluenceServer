using System;
using System.Collections.Generic;

namespace Influence.Domain
{
    public class Session
    {
        public Guid Id { get; }

        public List<Player> Players { get; set; }
        public int RoundNumber { get; set; }
        public GameState GameState { get; set; }
        public RuleSet RuleSet { get; set; }
        public Board Board { get; set; }

        public Session(RuleSet ruleSet)
        {
            Id = Guid.NewGuid();
            Players = new List<Player>();
            GameState = new GameState();
            RoundNumber = 0;
            RuleSet = ruleSet;
            Board = new Board(ruleSet.BoardSize);
        }
    }
}