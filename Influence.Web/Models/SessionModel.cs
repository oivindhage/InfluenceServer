using System.Collections.Generic;
using Influence.Domain;

namespace Influence.Web.Models
{
    public class SessionModel
    {
        public Session Session { get; set; }
        public List<PlayerBot> AvailableBots { get; set; }
    }
}