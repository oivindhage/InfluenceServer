using System;
using System.Diagnostics;
using System.IO;
using System.Web;

namespace Influence.Services.Bot
{
    public class BotService
    {
        public static void HaveBotJoinGame(string folderName, string botName, Guid sessionId, string serviceUrl)
        {
            var processInfo = new ProcessStartInfo
            {
                //UserName = "normaluser", 
                //PasswordInClearText = "hilfe",

                FileName = Path.Combine(folderName, "Influence.GameClient.exe"), 
                Arguments = $"-name {botName} -serverurl {serviceUrl} -session {sessionId}",
                UseShellExecute = false,
            };

            var process = Process.Start(processInfo);

            if (process != null)
                process.PriorityClass = ProcessPriorityClass.Idle;
        }
    }
}
