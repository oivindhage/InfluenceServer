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
            model.SessionUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/ws.ashx";

            return View(model);
        }

        public ActionResult CreateSession()
        {
            GameMaster.CreateSession();
            return RedirectToAction("Index");
        }
    }
}
