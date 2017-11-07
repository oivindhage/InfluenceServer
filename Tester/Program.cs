using System;
using RestSharp;

namespace Tester
{
    class Program
    {
        static void Main()
        {
            var client = new RestClient("http://osl-ejay:84/");

            while (true)
            {
                const string baseUrl = "ws.ashx";
                const string nick = "EJay";
                string sessionGuid = Guid.NewGuid().ToString();
                string playerGuid = Guid.NewGuid().ToString();

                // Create
                var response = client.Get(new RestRequest($"{baseUrl}?create={sessionGuid}"));
                var content = response.Content;

                // Join
                response = client.Get(new RestRequest($"{baseUrl}?join&session={sessionGuid}&playerid={playerGuid}&name={nick}"));
                content = response.Content;

                // Start
                response = client.Get(new RestRequest($"{baseUrl}?start&session={sessionGuid}"));
                content = response.Content;

                Console.WriteLine(content);
                Console.ReadLine();
            }
        }
    }
}
