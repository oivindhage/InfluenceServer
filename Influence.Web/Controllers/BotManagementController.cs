using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Influence.Domain;
using Influence.Web.Models;
using Exception = System.Exception;

namespace Influence.Web.Controllers
{
    public class BotManagementController : ControllerBase
    {
        public ActionResult Index(string uploadResultMessage = "")
        {
            var model = new BotManagementModel
            {
                UploadedBots = GetUploadedBots(),
                UploadResultMessage = uploadResultMessage
            };

            return View(model);
        }


        [HttpPost]
        public void DeleteBot(string guid)
        {
            var bots = GetUploadedBots();

            var matchingBot = bots.FirstOrDefault(b => b.FolderName != null && b.FolderName.EndsWith(guid));
            if (matchingBot != null)
                DeleteBot(matchingBot);
        }


        [HttpPost]
        public ActionResult UploadBot(BotManagementModel model)
        {
            var file = model.File;

            if (string.IsNullOrEmpty(model.Name))
                return UploadResult("Enter a name for your bot");

            if (!Regex.IsMatch(model.Name, @"^[a-zA-Z0-9]*$"))
                return UploadResult("The name of your bot can only contain a-Z and 0-9");

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
                    return UploadResult("Bot successfully uploaded!");
                }
                catch (Exception ex) { return UploadResult("Failure: " + ex); }
            }

            return UploadResult("Choose a file");
        }


        private void DeleteBot(UploadedBot bot)
        {
            var thisBotsFolder = bot.FolderName;
            var directoryOfAllBots = Server.MapPath($"~/{UploadedBotsFolderName}");

            if (Directory.Exists(directoryOfAllBots) && Directory.GetParent(thisBotsFolder).FullName == directoryOfAllBots)
                Directory.Delete(thisBotsFolder, true);
        }


        private ActionResult UploadResult(string message)
            => RedirectToAction("Index", "BotManagement", new { uploadResultMessage = message });
    }
}