using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

// Reference link: https://docs.microsoft.com/en-us/dotnet/csharp/tutorials/console-webapiclient
namespace friendly_github_tools
{

    public class Repository
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    class Program
    {
        public static readonly HttpClient client = new HttpClient();

        private static async Task ProcessRepositories()
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

            //var stringTask = client.GetStringAsync("https://api.github.com/orgs/dotnet/repos");
            //var msg = await stringTask;
            //Console.Write(msg);
            
            var streamTask = client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
            var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(await streamTask);
            foreach (var repo in repositories)
                Console.WriteLine(repo.Name);

        }
        static async Task Main(string[] args)
        //static void Main(string[] args)
        {

            await ProcessRepositories();
            Console.WriteLine("Hello World!");
        }
    }
}
