using System.Collections.Generic;
using Influence.Domain.Tournament;

namespace Influence.Web.Models
{
    public class TournamentModel
    {
        public string TournamentName { get; set; }
        public List<TournamentParticipant> InvitedParticipants { get; set; } = new List<TournamentParticipant>();

        public TournamentSettings Settings { get; set; } = new TournamentSettings();
        public List<TournamentRound> Rounds { get; set; } = new List<TournamentRound>();
        public bool CanSetupNextRound { get; set; }
        public bool CanPlayRound { get; set; }
    }
}

