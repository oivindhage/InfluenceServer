using System;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Influence.Web.Models;
using Exception = System.Exception;

namespace Influence.Web.Controllers
{
    public class UploadController : Controller
    {
        public static readonly string UploadedBotsFolderName = "UploadedBots";

        public ActionResult Index() 
            => View(new UploadModel());

        [HttpPost]
        public ActionResult UploadFile(UploadModel model)
        {
            var file = model.File;
            if (string.IsNullOrEmpty(model.Name))
                return UploadResult("Fill out a name for your bot");
            if (!Regex.IsMatch(model.Name, @"^[a-zA-Z0-9]*$"))
                return UploadResult("The name can only contain a-Z and 0-9");
            if (file?.ContentLength > 0 && !string.IsNullOrEmpty(file.FileName))
            {
                try
                {
                    var zipFileName = Path.GetFileName(file.FileName);

                    if (Path.GetExtension(file.FileName) != ".zip")
                        return UploadResult("Uploaded file must be .zip");

                    var folderToPutFile = Server.MapPath($"~/{UploadedBotsFolderName}/{Guid.NewGuid()}");
                    var dirInfo = Directory.CreateDirectory(folderToPutFile);
                    var fullPath = Path.Combine(dirInfo.FullName, zipFileName);
                    file.SaveAs(fullPath);

                    ZipFile.ExtractToDirectory(fullPath, dirInfo.FullName);
                    System.IO.File.Delete(fullPath);
                    string nameTxtPath = Path.Combine(folderToPutFile, "name.txt");
                    System.IO.File.WriteAllText(nameTxtPath, model.Name);
                    return UploadResult("Success!");
                }
                catch (Exception ex)
                {
                    return UploadResult("Failure: " + ex);
                }
            }

            return UploadResult("Choose a file");
        }

        private ActionResult UploadResult(string message)
            => View("Index", new UploadModel { Message = message });
    }
}
