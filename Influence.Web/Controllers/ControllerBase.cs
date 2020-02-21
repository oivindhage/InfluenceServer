using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Influence.Common.Extensions;
using Influence.Domain;

namespace Influence.Web.Controllers
{
    public class ControllerBase : Controller
    {
        protected const string UploadedBotsFolderName = "UploadedBots";
        
        protected List<UploadedBot> GetUploadedBots()
        {
            var bots = new List<UploadedBot>();
            var uploadedPath = Server.MapPath($"~/{UploadedBotsFolderName}");
            if (!Directory.Exists(uploadedPath))
                Directory.CreateDirectory(uploadedPath);
            var botFolders = Directory.GetDirectories(uploadedPath);
            foreach (var folder in botFolders)
            {
                var nameFile = Path.Combine(folder, "name.txt");
                var name = System.IO.File.Exists(nameFile)
                    ? System.IO.File.ReadAllLines(nameFile).FirstOrDefault().DefaultTo(string.Empty).Trim()
                    : "Bot " + folder.Substring(0, 8);
                bots.Add(new UploadedBot
                {
                    FolderName = folder,
                    Name = name
                });
            }

            return bots;
        }
    }
}