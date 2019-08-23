using System;
using System.IO;
using System.IO.Compression;
using System.Web.Mvc;
using Influence.Web.Models;

namespace Influence.Web.Controllers
{
    public class UploadController : Controller
    {
        public ActionResult Index()
        {
            return View(new UploadModel());
        }

        [HttpPost]
        public ActionResult UploadFile(UploadModel model)
        {
            var file = model.File;
            if (file?.ContentLength > 0 && !string.IsNullOrEmpty(file.FileName))
            {
                var fileName = Path.GetFileName(file.FileName);

                if (Path.GetExtension(file.FileName) != ".zip")
                    return UploadResult("Uploaded file must be .zip");

                var folderToPutFile = Server.MapPath($"~/UploadedBots/{Guid.NewGuid()}");
                var dirInfo = Directory.CreateDirectory(folderToPutFile);
                var fullPath = Path.Combine(dirInfo.FullName, fileName);
                file.SaveAs(fullPath);

                ZipFile.ExtractToDirectory(fullPath, dirInfo.FullName);
                System.IO.File.Delete(fullPath);

                return UploadResult("Success!");
            }

            return UploadResult("Choose a file");
        }

        private ActionResult UploadResult(string message) 
            => View("Index", new UploadModel { Message = message });
    }
}
