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
            lock (Lock)
                SetupDummyStuff();

            context.Response.ContentType = "text/plain";
            
            Match match;
            if ((match = Regex.Match(context.Request.Url.Query, "^\\?session=(?<sessionid>[A-Za-z0-9\\-]+)$")).Success)
                GetSession(context, match);

            else if ((match = Regex.Match(context.Request.Url.Query, 
                "^\\?join&session=(?<sessionid>[A-Za-z0-9\\-]+)&playerid=(?<playerid>[A-Za-z0-9\\-]+)&name=(?<name>[a-zA-Z]{3,15})$")).Success)
                JoinSession(context, match);

            else GetSessions(context);
        }

        private void JoinSession(HttpContext context, Match match)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            string name;
            Guid sessionId, playerId;
            if (Guid.TryParse(match.Groups["sessionid"].Value, out sessionId) 
                && Guid.TryParse(match.Groups["playerid"].Value, out playerId) 
                && !string.IsNullOrEmpty((name = match.Groups["name"].Value))
                && name.Length > 3)
            {
                var session = GameMaster.GetSession(sessionId);
                if (session != null)
                {
                    if (session.AddPlayer(playerId, name))
                    {
                        var player = session.Players.Single(p => p.Id == playerId);

                        context.Response.StatusCode = (int) HttpStatusCode.OK;
                        context.Response.Write($"Velkommen til session {sessionId}, {name}. Du har fått fargen {player.ColorRgbCsv} (rgbcsv)");
                    }

                    else if (session.Players.Any(p => p.Id == playerId))
                    {
                        context.Response.Write("Du er allerede med i denne session.");
                    }

                    else
                    {
                        context.Response.Write(
                            "Dårlig forespørsel.\r\n" +
                            "Format: sessionid=guid&playerid=guid&name=something\r\n" +
                            "Name: 3-15 bokstaver a-zA-Z\r\n" +
                            "Se was.ashx/ for liste over sessions. Finn en som kan joines, og et spillernavn som ikke er tatt");
                    }
                }
                else context.Response.Write("Ugyldig session ID");
            }
        }

        private void GetSessions(HttpContext context) => context.Response.Write(JsonConvert.SerializeObject(new { Sessions = GameMaster.GetSessions() }));

        private void GetSession(HttpContext context, Match match)
        {
            Guid sessionId;
            if (Guid.TryParse(match.Groups["sessionid"].Value, out sessionId))
                context.Response.Write(JsonConvert.SerializeObject(new { Session = GameMaster.GetSessions() }));
        }

        private void SetupDummyStuff()
        {
            if (!GameMaster.GetSessions().Any())
                GameMaster.CreateSession(RuleSet, new Guid("f041ae8c-e597-4af2-933d-77cf538e14cb"));
        }
    }
}