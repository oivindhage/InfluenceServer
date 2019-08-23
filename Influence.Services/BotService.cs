using System;
using System.Collections.Generic;
using Influence.Domain;

namespace Influence.Services
{
    public class BotService
    {
        public static List<UploadedBot> GetAvailableBots()
        {
            return new List<UploadedBot>
            {
                new UploadedBot { UniqueId = "dummy1", Name="Dummybot 1" },
                new UploadedBot { UniqueId = "dummy2", Name="Dummybot 2" }
            };
        }

        public static void RequestBotToJoinGame(string botId, Guid gameGuid, string serviceUrl)
        {

        }
    }
}
