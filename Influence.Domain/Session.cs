using System;
using System.Collections.Generic;

namespace Influence.Domain
{
    public class Session
    {
        public Guid Id { get; }

        public List<Player> Players { get; set; }
        public int RoundNumber { get; set; }
        public Board Board { get; set; }
        public GameState GameState { get; set; }

        public Session()
        {
            Id = Guid.NewGuid();
        }
    }
}