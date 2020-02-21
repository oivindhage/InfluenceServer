using System.Collections.Generic;
using Influence.Domain;

namespace Influence.Web.Models
{
    public class HomeModel
    {
        public string SessionUrl { get; set; }
        public List<Session> Sessions { get; set; }
    }
}