using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using Influence.Common.Extensions;
using Influence.Domain;
using Influence.Services;
using Newtonsoft.Json;

namespace Influence.Web
{
    public class ws : IHttpHandler
    {
        public bool IsReusable => false;

        private const string RxViewAllSessions = "^\\?sessions$";
        private const string RxViewSpecificSession = "^\\?session=(?<sessionid>[A-Za-z0-9\\-]+)$";
        private const string RxJoinSession = "^\\?join&session=(?<sessionid>[A-Za-z0-9\\-]+)&playerid=(?<playerid>[A-Za-z0-9\\-]+)&name=(?<name>[a-zA-Z]{3,15})$";
        private const string RxStartSession = "^\\?start&session=(?<sessionid>[A-Za-z0-9\\-]+)$";

        private static readonly object Lock = new object();
        private static readonly RuleSet RuleSet = RuleSet.Default;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            lock (Lock)
                SetupDummyStuff();

            Match match;

            if (Regex.IsMatch(context.Request.Url.Query, RxViewAllSessions))
                GetSessions(context);

            else if ((match = Regex.Match(context.Request.Url.Query, RxViewSpecificSession)).Success)
                GetSession(context, match);

            else if ((match = Regex.Match(context.Request.Url.Query, RxJoinSession)).Success)
                JoinSession(context, match);

            else if ((match = Regex.Match(context.Request.Url.Query, RxStartSession)).Success)
                StartSession(context, match);

            // Default behavior: List all sessions
            else
                GetSessions(context);
        }

        private void StartSession(HttpContext context, Match match)
        {
            
        }

        private void JoinSession(HttpContext context, Match match)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            string name = match.Groups["name"].Value.DefaultTo(string.Empty);
            var sessionId = match.Groups["sessionid"].Value.ToGuid();
            var playerId = match.Groups["playerid"].Value.ToGuid();

            if (name.Length < 3 || name.Length > 15)
                context.Response.Write("Navn må være 3-15 bokstaver, A-Z");

            else if (sessionId.NotValid())
                context.Response.Write("SessionId ser ikke riktig ut");

            else if (playerId.NotValid())
                context.Response.Write("PlayerId ser ikke riktig ut");

            else
            {
                var session = GameMaster.GetSession(sessionId);

                if (session == null)
                    context.Response.Write("Ugyldig session ID");

                else
                {
                    if (session.AddPlayer(playerId, name))
                    {
                        var player = session.Players.Single(p => p.Id == playerId);

                        context.Response.StatusCode = (int) HttpStatusCode.OK;
                        context.Response.Write($"Velkommen til session {sessionId}, {name}. Du har fått fargen {player.ColorRgbCsv} (rgbcsv)");
                    }

                    else if (session.Players.Any(p => p.Id == playerId) || session.Players.Any(p => p.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        context.Response.Write("Det finnes allerede en spiller med den id-en eller det navnet i oppgitt session");
                    }
                }
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