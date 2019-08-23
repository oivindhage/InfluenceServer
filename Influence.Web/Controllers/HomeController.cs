using System.Linq;
using System.Web.Mvc;
using Influence.Services;
using Influence.Web.Models;

namespace Influence.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (!GameMaster.GetSessions().Any())
                GameMaster.CreateSession();

            var model = new HomeModel { Sessions = GameMaster.GetSessions().OrderByDescending(g => g.CreationTime).ToList() };

            return View(model);
        }

        public ActionResult CreateSession()
        {
            GameMaster.CreateSession();
            return RedirectToAction("Index");
        }
    }
}
