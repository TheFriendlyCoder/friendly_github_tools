using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using friendly_github_tools.GitHubAPI;
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
            // Reference: https://octokitnet.readthedocs.io/en/latest/
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

        static void ThirdPass(string token)
        {
            var client = new GitHub(token);
            foreach (var i in client.CurrentUser.Repositories)
            {
                Console.WriteLine(i.Name);
            }
        }
        static void Main(string[] args)
        {
            // TODO: Add support for CI to build the package in 3 variations: windows, linux and mac
            // TODO: Add support for reading releases for a project
            // TODO: Add support for publishing a file to a release
            // TODO: Add command line parameter for API token
            // TODO: Figure out how to pass release info to tool in a simple way for publishing files
            // TODO: Make use of the tool itself to publish it's own release to Github
            string token = System.IO.File.ReadAllText(@"..\..\..\..\key.txt");
            //FirstPass();
            //SecondPass();
            ThirdPass(token);
            Console.WriteLine("Hello World!");
        }
    }
}
