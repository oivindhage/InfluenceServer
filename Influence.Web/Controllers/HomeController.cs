using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Influence.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}
