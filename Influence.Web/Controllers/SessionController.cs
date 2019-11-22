using System;
using System.Linq;
using System.Web.Mvc;
using Influence.Services;
using Influence.Services.Bot;
using Influence.Web.Models;

namespace Influence.Web.Controllers
{
    public class SessionController : ControllerBase
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
    }
}
