using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Influence.Web.Controllers
{
    public class FileUploadController : Controller
    {
        public ActionResult Index(HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var dirInfo = GetPath("jonas", fileName);
                file.SaveAs(Path.Combine(dirInfo.FullName, fileName));
            }
            return RedirectToAction("Index", "Home");
        }

        private DirectoryInfo GetPath(string userName, string fileName)
            => Directory.CreateDirectory(Server.MapPath($"~/App_Data/{userName}"));
    }
}