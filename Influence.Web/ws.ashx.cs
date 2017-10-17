using System.Collections.Generic;
using System.Linq;
using System.Web;
using Influence.Domain;
using Influence.Services;
using Newtonsoft.Json;

namespace Influence.Web
{
    public class ws : IHttpHandler
    {
        private static readonly object Lock = new object();
        public bool IsReusable => false;

        public void ProcessRequest(HttpContext context)
        {
            SetupDummyStuff();
            var sessions = GameMaster.GetSessions();

            context.Response.ContentType = "text/plain";
            context.Response.Write(JsonConvert.SerializeObject(sessions));
        }

        private void SetupDummyStuff()
        {
            lock (Lock)
            {
                if (!GameMaster.GetSessions().Any())
                    GameMaster.CreateSession();
            }
        }

        public static List<Session> GetSessions() => GameMaster.GetSessions();
    }
}