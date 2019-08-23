using System;
using System.Linq;
using System.Web.Mvc;
using Influence.Services;
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
                AvailableBots = BotService.GetAvailableBots()
            };

            return View(model);
        }

        public ActionResult StartGame(Guid sessionId)
        {
            GameMaster.GetSessions().FirstOrDefault(s => s.Id == sessionId)?.Start();
            return null;
        }
    }
}
