using System.Collections.Generic;

namespace Influence.Domain.Tournament
{
    public class Tournament
    {
        public string Name { get; }
        public List<TournamentRound> Rounds { get; } = new List<TournamentRound>();
        public List<TournamentParticipant> Participants { get; set; } = new List<TournamentParticipant>();
        public TournamentSettings Settings { get; set; } = new TournamentSettings();

        public Tournament(string name)
        {
            Name = name;
        }
    }
}
