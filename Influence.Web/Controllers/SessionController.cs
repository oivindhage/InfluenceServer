using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Influence.Common.Extensions;
using Influence.Domain;
using Influence.Services;
using Influence.Services.Bot;
using Influence.Web.Models;

namespace Influence.Web.Controllers
{
    public class SessionController : Controller
    {
        public ActionResult Index(Guid sessionId)
        {
            var model = new SessionModel
            {
                Session = GameMaster.GetSession(sessionId),
                AvailableBots = GetUploadedBots()
            };

            return View(model);
        }

        public ActionResult StartGame(Guid sessionId)
        {
            GameMaster.GetSessions().FirstOrDefault(s => s.Id == sessionId)?.Start();
            return null;
        }

        public ActionResult JoinBot(string folderName, Guid sessionId)
        {
            var bot = GetUploadedBots().FirstOrDefault(b => b.FolderName == folderName);
            if (bot != null)
                BotService.HaveBotJoinGame(bot.FolderName, bot.Name, sessionId, $"{Request.Url.Scheme}://{Request.Url.Authority}/{nameof(ws)}.ashx");

            return null;
        }

        private List<UploadedBot> GetUploadedBots()
        {
            var bots = new List<UploadedBot>();

            var botFolders = Directory.GetDirectories(Server.MapPath($"~/{UploadController.UploadedBotsFolderName}"));
            foreach (var folder in botFolders)
            {
                var nameFile = Path.Combine(folder, "name.txt");
                var name = System.IO.File.Exists(nameFile) ? System.IO.File.ReadAllLines(nameFile).FirstOrDefault().DefaultTo(string.Empty).Trim() : "Bot " + folder.Substring(0, 8);

                bots.Add(new UploadedBot
                {
                    FolderName = folder,
                    Name = name
                });
            }

            return bots;
        }
    }
}
