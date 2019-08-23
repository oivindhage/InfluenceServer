using System.Web;

namespace Influence.Web.Models
{
    public class UploadModel
    {
        public HttpPostedFileBase File { get; set; }
        public string Message { get; set; }
    }
}

