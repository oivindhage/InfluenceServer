using System;

namespace Influence.Domain.Tournament
{
    public class TournamentParticipant
    {
        public UploadedBot Bot { get; set; }
        public Guid Guid { get; set; }
        public bool HasJoined { get; set; }
        public bool IsEliminated { get; set; }
    }
}