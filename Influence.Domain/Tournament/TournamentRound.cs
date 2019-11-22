using System.Collections.Generic;
using System.Linq;

namespace Influence.Domain.Tournament
{
    public class TournamentRound
    {
        public List<Session> Sessions { get; set; }

        public bool IsStarted { get; set; }
        public bool IsComplete { get; set; }

        public int RoundNumber { get; set; }
    }
}