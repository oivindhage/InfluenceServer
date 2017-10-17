using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
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

            Match match;
            if ((match = Regex.Match(context.Request.Url.Query, "session=(?<id>[A-Za-z0-9\\-]+)", RegexOptions.IgnoreCase)).Success)
            {
                Guid id;
                if (Guid.TryParse(match.Groups["id"].Value, out id))
                    context.Response.Write(JsonConvert.SerializeObject(new { Session = GameMaster.GetSessions() }));
            }

            else
                context.Response.Write(JsonConvert.SerializeObject(new { Sessions = GameMaster.GetSessions() }));
        }

        private void SetupDummyStuff()
        {
            lock (Lock)
            {
                if (!GameMaster.GetSessions().Any())
                    GameMaster.CreateSession(RuleSet, new Guid("f041ae8c-e597-4af2-933d-77cf538e14cb"));
            }
        }
    }
}