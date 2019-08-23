using System;
using System.Web.Mvc;
using Influence.Services;
using Influence.WebNew.Models;

namespace Influence.WebNew.Controllers
{
    public class SessionController : Controller
    {
        public ActionResult Index(Guid sessionId)
        {
            var model = new SessionModel { Session = GameMaster.GetSession(sessionId) };
            return View(model);
        }
    }
}
