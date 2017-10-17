using System.Collections.Generic;
using Influence.Domain;

namespace Influence.Services
{
    public class GameMaster
    {
        private static List<Session> Sessions { get; set; }

        public List<Session> GetSessions()
        {
            return Sessions;
        }
    }
}
