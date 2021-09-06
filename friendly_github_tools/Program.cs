using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using Octokit;
using Octokit.Internal;

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
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", token);
            }


            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(
            //    new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var streamTask = client.GetStreamAsync("https://api.github.com/user/repos");
            streamTask.Wait();
            var json_task = JsonSerializer.DeserializeAsync<List<Repository>>(streamTask.Result);
            return json_task.GetAwaiter().GetResult();
        }

    }
    class Program
    {

        static void FirstPass()
        {
            var gh = new Github();
            var repositories = gh.GetRepos();
            foreach (var i in repositories)
            {
                Console.WriteLine(i.Name);
            }
        }

        static void SecondPass()
        {
            var token = "";
            var project_name = "friendly_github_tools";

            var creds = new InMemoryCredentialStore(new Credentials(token));
            var client = new GitHubClient(new Octokit.ProductHeaderValue(project_name), creds);
            var repositories = client.Repository.GetAllForCurrent().Result;
            foreach (var i in repositories)
            {
                Console.WriteLine(i.Name);
            }
        }
        static void Main(string[] args)
        {
            FirstPass();
            //SecondPass();
            Console.WriteLine("Hello World!");
        }
    }
}
