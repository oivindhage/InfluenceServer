using System;
using RestSharp;

namespace Tester
{
    class Program
    {
        static void Main()
        {
            var client = new RestClient("http://osl-ejay:82/");

            while (true)
            {
                const string baseUrl = "ws.ashx";
                string sessionGuid = Guid.NewGuid().ToString();

                // Create
                var response = client.Get(new RestRequest($"{baseUrl}?create={sessionGuid}"));
                var content = response.Content;

                // Join 3 players
                response = client.Get(new RestRequest($"{baseUrl}?join&session={sessionGuid}&playerid={Guid.NewGuid().ToString()}&name=Dummy1"));
                content = response.Content;

                response = client.Get(new RestRequest($"{baseUrl}?join&session={sessionGuid}&playerid={Guid.NewGuid().ToString()}&name=Dummy2"));
                content = response.Content;

                response = client.Get(new RestRequest($"{baseUrl}?join&session={sessionGuid}&playerid={Guid.NewGuid().ToString()}&name=Dummy3"));
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
