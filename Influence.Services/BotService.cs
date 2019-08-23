using System;
using System.Collections.Generic;
using Influence.Domain;

namespace Influence.Services
{
    public class BotService
    {
        public static List<PlayerBot> GetAvailableBots()
        {
            return new List<PlayerBot>
            {
                new PlayerBot { UniqueId = "dummy1", Name="Dummybot 1" },
                new PlayerBot { UniqueId = "dummy2", Name="Dummybot 2" }
            };
        }

        public static void RequestBotToJoinGame(string botId, Guid gameGuid)
        {

        }
    }
}
