using System.Collections.Generic;

namespace Influence.Domain
{
    public class Session
    {
        public List<Player> Players { get; set; }
        public int RoundNumber { get; set; }
        public Board Board { get; private set; }
        public GameState GameState { get; set; }
    }
}