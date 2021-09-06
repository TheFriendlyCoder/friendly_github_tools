using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

// Reference link: https://docs.microsoft.com/en-us/dotnet/csharp/tutorials/console-webapiclient
namespace friendly_github_tools
{

    public class Repository
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    class Github
    {
        public static readonly HttpClient client = new HttpClient();

        public List<Repository> GetRepos()
        {
            var token = "";
            if (token != "")
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
            }


            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(
            //    new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var streamTask = client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
            
            return JsonSerializer.DeserializeAsync<List<Repository>>(streamTask.Result).Result;
        }

    }
    class Program
    {
        

        static void Main(string[] args)
        {
            var gh = new Github();
            var repositories = gh.GetRepos();
            foreach (var i in repositories)
            {
                Console.WriteLine(i.Name);
            }

            Console.WriteLine("Hello World!");
        }
    }
}
