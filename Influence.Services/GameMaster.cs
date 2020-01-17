using System;
using System.Collections.Generic;
using System.Linq;
using Influence.Domain;

namespace Influence.Services
{
    public static class GameMaster
    {
        private static readonly Random Rand = new Random();

        private static readonly List<string> SessionNames = new List<string>{
            "Mitzi", "Darlene", "Sheryl", "Madeline", "Jeanne", "Anita", "Nettie", "Ling", "Tessa", "Shantae",  "Ardith", 
            "Rosella", "Edwina", "Berniece", "Margaretta", "Kacie", "Paulene", "Gracie", "Katrina", "Lorie", "Vicky", 
            "Diane", "Lynnette", "Sherill", "Chandra", "Lorriane", "Elisabeth", "Alma", "Santina", "Zoe"};

        private static List<Session> Sessions { get; } = new List<Session>();

        public static List<Session> GetSessions() 
            => Sessions;

        public static Session GetSession(Guid guid) 
            => Sessions.FirstOrDefault(s => s.Id == guid);

        public static Session CreateSession(RuleSet customRuleSet = null, Guid id = default(Guid), bool isTournamentSession = false, string name = null)
        {
            int numTries = 0;

            if (name == null)
            {
                do { name = SessionNames[Rand.Next(0, SessionNames.Count)]; }
                while (++numTries < 50 && Sessions.Any(s => s.Name == name));
            }

            customRuleSet = customRuleSet ?? RuleSet.Default;
            var session = new Session(customRuleSet, name, id, isTournamentSession);
            Sessions.Add(session);

            return session;
        }
    }
}
