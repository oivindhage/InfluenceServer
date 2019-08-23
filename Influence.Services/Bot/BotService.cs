using System;
using System.Diagnostics;
using System.IO;

namespace Influence.Services.Bot
{
    public class BotService
    {
        public static void HaveBotJoinGame(string folderName, string botName, Guid sessionId, string serviceUrl)
        {
            // todo oivindhage
            Process.Start(Path.Combine(folderName, "Influence.GameClient.exe"), $"-name {botName} -serverurl {serviceUrl} -session {sessionId}");
        }
    }
}
