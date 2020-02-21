using System.Collections.Generic;
using System.Web;
using Influence.Domain;

namespace Influence.Web.Models
{
    public class BotManagementModel
    {
        public HttpPostedFileBase File { get; set; }
        public string UploadResultMessage { get; set; }
        public string Name { get; set; }
        public List<UploadedBot> UploadedBots { get; set; }
    }
}

