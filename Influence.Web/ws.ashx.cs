using System.Linq;
using System.Web;
using Influence.Domain;
using Influence.Services;
using Newtonsoft.Json;

namespace Influence.Web
{
    public class ws : IHttpHandler
    {
        public bool IsReusable => false;

        private static readonly object Lock = new object();
        private static readonly RuleSet RuleSet = RuleSet.Default;

        public void ProcessRequest(HttpContext context)
        {
            SetupDummyStuff();

            context.Response.ContentType = "text/plain";
            context.Response.Write(JsonConvert.SerializeObject(new {Sessions = GameMaster.GetSessions()}));
        }

        private void SetupDummyStuff()
        {
            lock (Lock)
            {
                if (!GameMaster.GetSessions().Any())
                    GameMaster.CreateSession(RuleSet);
            }
        }
    }
}