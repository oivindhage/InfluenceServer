using System;
using System.Collections.Generic;
using Influence.Domain;

namespace Influence.Services.Bot
{
    public class BotService
    {
        public static List<UploadedBot> GetAvailableBots()
        {
            return new List<UploadedBot>
            {
                new UploadedBot { UniqueId = "DummyId1", Name="Dummybot 1" },
                new UploadedBot { UniqueId = "DummyId2", Name="Dummybot 2" }
            };
        }

        public static void RequestBotToJoinGame(string botId, Guid gameGuid, string serviceUrl)
        {

        }
    }
}
