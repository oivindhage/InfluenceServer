using RestSharp;
using System.Net;
using Influence.Domain;
using Newtonsoft.Json;

namespace Influence.GameClient
{
    public class InfluenceGateway
    {
        public string SessionBaseUrl;
        public string EndReinforce(string sessionId, string playerId)
            => GetResponseOrErrorMessage($"?endreinforce&session={sessionId}&playerid={playerId}");

        public string EndAttack(string sessionId, string playerId)
           => GetResponseOrErrorMessage($"?endmove&session={sessionId}&playerid={playerId}");

        public string StartSession(string sessionId)
           => GetResponseOrErrorMessage($"?start&session={sessionId}");

        public string Create()
           => GetResponseOrErrorMessage($"?create");

        public string Move(string sessionId, string text, int attackFromTileId, int attackToTileId)
           => GetResponseOrErrorMessage($"?move&session={sessionId}&playerid={text}&from={attackFromTileId}&to={attackToTileId}");

        public string Reinforce(string sessionId, string text, int reinforceTileId)
           => GetResponseOrErrorMessage($"?reinforce&session={sessionId}&playerid={text}&tileid={reinforceTileId}");

        public string Join(string sessionId, string playerId, string name)
           => GetResponseOrErrorMessage($"?join&session={sessionId}&playerid={playerId}&name={name}");

        public Session[] GetSessions()
        {
            var response = Get("?sessions");
            if (response.StatusCode != HttpStatusCode.OK)
                return null;
            dynamic converted = JsonConvert.DeserializeObject(response.Content);
            var sessionsJson = converted.Sessions.ToString();
            return JsonConvert.DeserializeObject<Session[]>(sessionsJson);
        }

        public Session GetSession(string sessionId)
        {
            var response = Get($"?session={sessionId}");
            if (response.StatusCode != HttpStatusCode.OK)
                return null;
            dynamic converted = JsonConvert.DeserializeObject(response.Content);
            var sessionJson = converted.Session.ToString();
            return JsonConvert.DeserializeObject<Session>(sessionJson);
        }

        private string GetResponseOrErrorMessage(string url)
        {
            var response = Get(url);
            if (response.StatusCode != HttpStatusCode.OK)
                return response.StatusDescription;
            return response.Content;
        }

        private IRestResponse Get(string url)
            => new RestClient(SessionBaseUrl).Get(new RestRequest(url));
    }
}
