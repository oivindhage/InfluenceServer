using System.Collections.Generic;
using Influence.Domain;

namespace Influence.Services
{
    public class GameMaster
    {
        private static List<Session> Sessions { get; } = new List<Session>();

        public static List<Session> GetSessions() => Sessions;

        public static void CreateSession(RuleSet ruleSet) 
            => Sessions.Add(new Session(ruleSet));
    }
}
