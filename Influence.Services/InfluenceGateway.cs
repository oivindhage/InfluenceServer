using System;
using System.Net;
using Influence.Domain;
using Newtonsoft.Json;
using RestSharp;

namespace Influence.Services
{
    public class Gateway
    {
        public string SessionBaseUrl;
        public string EndReinforce(string sessionId, string playerId)
            => GetResponseOrErrorMessage($"?endreinforce&session={sessionId}&playerid={playerId}");

        public string EndAttack(string sessionId, string playerId)
           => GetResponseOrErrorMessage($"?endmove&session={sessionId}&playerid={playerId}");

        public string StartSession(string sessionId)
           => GetResponseOrErrorMessage($"?start&session={sessionId}");

        public string NewGameInSession(string sessionId)
            => GetResponseOrErrorMessage($"?newgame&session={sessionId}");

        public string Create(string guid = "")
           => GetResponseOrErrorMessage($"?create={guid ?? Guid.NewGuid().ToString()}");

        public string Move(string sessionId, string playerId, int attackFromTileId, int attackToTileId)
           => GetResponseOrErrorMessage($"?move&session={sessionId}&playerid={playerId}&from={attackFromTileId}&to={attackToTileId}");

        public string Reinforce(string sessionId, string playerId, int reinforceTileId)
           => GetResponseOrErrorMessage($"?reinforce&session={sessionId}&playerid={playerId}&tileid={reinforceTileId}");

        public string Join(string sessionId, string playerId, string name)
           => GetResponseOrErrorMessage($"?join&session={sessionId}&playerid={playerId}&name={name}");

        public Session[] GetSessions()
        {
            var response = Get("?sessions");
            if (response.StatusCode != HttpStatusCode.OK)
                return null;
            dynamic converted = JsonConvert.DeserializeObject(response.Content);
            var sessionsJson = converted.Sessions.ToString();

            // todo: see todo in GetSession(string)
            return JsonConvert.DeserializeObject<Session[]>(sessionsJson);
        }

        public Session GetSession(string sessionId)
        {
            var response = Get($"?session={sessionId}");
            if (response.StatusCode != HttpStatusCode.OK)
                return null;
            dynamic converted = JsonConvert.DeserializeObject(response.Content);
            var sessionJson = converted.Session.ToString();

            // todo: the session object isn't created correctly here
            // for example, instances of Tile get the wrong Id in the constructor due to the ctor param "numColumns" always having the value 0
            // also see todo note in GetSessions()
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
