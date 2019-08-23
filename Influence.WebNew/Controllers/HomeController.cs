using System.Web.Mvc;
using Influence.Domain;
using Influence.Services;
using Influence.WebNew.Models;

namespace Influence.WebNew.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new HomeModel();

            model.Sessions = GameMaster.GetSessions();

            // temp
            if (model.Sessions.Count == 0)
                GameMaster.CreateSession(RuleSet.Default);

            return View(model);
        }
    }
}
