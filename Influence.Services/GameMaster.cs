using System;
using System.Collections.Generic;
using System.Linq;
using Influence.Domain;

namespace Influence.Services
{
    public class GameMaster
    {
        private static List<Session> Sessions { get; } = new List<Session>();

        public static List<Session> GetSessions() 
            => Sessions;

        public static Session GetSession(Guid guid) 
            => Sessions.FirstOrDefault(s => s.Id == guid);

        public static void CreateSession(RuleSet ruleSet, Guid id = default(Guid)) 
            => Sessions.Add(new Session(ruleSet, id));
    }
}
