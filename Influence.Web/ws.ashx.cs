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
        public void ProcessRequest(HttpContext context) => HandleQuery(context, context.Request.Url.Query);

        private const string RxViewAllSessions = "^\\?sessions$";
        private const string RxViewSpecificSession = "^\\?session=(?<sessionid>[A-Za-z0-9\\-]+)$";
        private const string RxCreateSession = "^\\?create=?(?<sessionid>[A-Za-z0-9\\-]*)$";
        private const string RxJoinSession = "^\\?join&session=(?<sessionid>[A-Za-z0-9\\-]+)&playerid=(?<playerid>[A-Za-z0-9\\-]+)&name=(?<name>[a-zA-Z0-9]{3,20})$";
        private const string RxStartSession = "^\\?start&session=(?<sessionid>[A-Za-z0-9\\-]+)$";
        
        private static readonly RuleSet RuleSet = RuleSet.Default;

        private void HandleQuery(HttpContext context, string query)
        {
            Match match;
            context.Response.ContentType = "text/plain";

            if (Regex.IsMatch(query, RxViewAllSessions))
                GetSessions(context);

            else if ((match = Regex.Match(query, RxCreateSession)).Success)
                CreateSession(context, match);

            else if ((match = Regex.Match(query, RxViewSpecificSession)).Success)
                GetSession(context, match);

            else if ((match = Regex.Match(query, RxJoinSession)).Success)
                JoinSession(context, match);

            else if ((match = Regex.Match(query, RxStartSession)).Success)
                StartSession(context, match);

            else
                Help(context);
        }

        private void Help(HttpContext context)
        {
            Ok(context,
                "Brukerhilfe:\r\n\r\n" +
                "Vis alle sessions: ws.ashx?sessions\r\n" +
                "Vis spesifikk session: ws.ashx?session=guid\r\n" +
                "Opprett session: ws.ashx?create\r\n" +
                "Opprett spesifikk session: ws.ashx?create=guid\r\n" +
                "Join en session: ws.ashx?join&session=guid&playerid=guid&name=alphanumeric3to20chars\r\n" +
                "Start en session: ws.ashx?start&session=guid");
        }

        private void CreateSession(HttpContext context, Match match)
        {
            var sessionId = match.Groups["sessionid"].Value.ToGuid();
            if (sessionId.NotValid())
                sessionId = Guid.NewGuid();

            if (GameMaster.GetSession(sessionId) != null)
                BadRequest(context, "En session med denne id-en finnes fra før");
            else
            {
                GameMaster.CreateSession(RuleSet, sessionId);
                Ok(context, $"Session {sessionId} opprettet");
            }
        }


        private void StartSession(HttpContext context, Match match)
        {
            var sessionId = match.Groups["sessionid"].Value.ToGuid();
            var session = sessionId.IsValid() ? GameMaster.GetSession(sessionId) : null;

            if (session == null)
                BadRequest(context, "Det fins ingen session med den Id-en der");
            else if (session.Players.Count < 1)
                BadRequest(context, "Minst 1 spiller må ha joinet denne session før den kan startes");
            else
            {
                session.Start();
                Ok(context, $"Session {session.Id} startet");
            }
        }

        private void JoinSession(HttpContext context, Match match)
        {
            var name = match.Groups["name"].Value.DefaultTo(string.Empty);
            var sessionId = match.Groups["sessionid"].Value.ToGuid();
            var playerId = match.Groups["playerid"].Value.ToGuid();

            if (name.Length < 3 || name.Length > 20)
                BadRequest(context, "Navn må være 3-20 bokstaver, A-Z");

            else if (sessionId.NotValid())
                BadRequest(context, "SessionId ser ikke riktig ut");

            else if (playerId.NotValid())
                BadRequest(context, "PlayerId ser ikke riktig ut");

            else
            {
                var session = GameMaster.GetSession(sessionId);

                if (session == null)
                    BadRequest(context, "Ugyldig SessionId");

                else if (session.Id == playerId)
                    BadRequest(context, "PlayerId må være ulik SessionId");

                else
                {
                    if (session.AddPlayer(playerId, name))
                    {
                        var player = session.Players.Single(p => p.Id == playerId);
                        Ok(context, $"Velkommen til session {sessionId}, {name}. Du har fått fargen {player.ColorRgbCsv} (rgbcsv)");
                    }

                    else if (session.Players.Any(p => p.Id == playerId) || session.Players.Any(p => p.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        BadRequest(context, "Det finnes allerede en spiller med den id-en eller det navnet i oppgitt session");
                    }
                }
            }
        }

        private void GetSessions(HttpContext context) 
            => Ok(context, JsonConvert.SerializeObject(new { Sessions = GameMaster.GetSessions() }));

        private void GetSession(HttpContext context, Match match)
        {
            var sessionId = match.Groups["sessionid"].Value.ToGuid();
            var session = sessionId.IsValid() ? GameMaster.GetSession(sessionId) : null;
            if (session != null)
                Ok(context, JsonConvert.SerializeObject(new { Session = session }));
            else
                BadRequest(context, "Ugyldig SessionId");
        }

        private void Ok(HttpContext context, string message)
        {
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            context.Response.Write(message);
        }

        private void BadRequest(HttpContext context, string message)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.Write(message);
        }
    }
}