using RestSharp;

namespace Tester
{
    class Program
    {
        static void Main()
        {
            var client = new RestClient("http://osl-ejay:84/");

            const string baseUrl = "http://osl-ejay.co.int:85/ws.ashx";
            const string sessionGuid = "ebd2a1f4-d240-42ed-8d5e-74b0e40269cb";
            const string nick = "EJay";
            const string playerGuid = "E4B676A0-7875-4F56-8FBC-C49E2A4FE2E5";

            // Create
            client.Get(new RestRequest($"{baseUrl}?create={sessionGuid}"));

            // Join
            client.Get(new RestRequest($"{baseUrl}?join={sessionGuid}&playerid={playerGuid}&name={nick}"));

            // Start
            client.Get(new RestRequest($"{baseUrl}?start&session={sessionGuid}"));
        }
    }
}
