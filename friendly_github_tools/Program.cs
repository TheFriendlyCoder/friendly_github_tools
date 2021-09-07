using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using friendly_github_tools.GitHubAPI;

namespace friendly_github_tools
{
    
    class Program
    {
        static void show_rate_limit(string token)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", token);
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            // Show rate limit
            var streamTask2 = client.GetStringAsync("https://api.github.com/rate_limit");
            streamTask2.Wait();
            Console.WriteLine(streamTask2.Result);



        }
        static void sample(string token)
        {
            var client = new GitHub(token);
            foreach (var i in client.CurrentUser.Repositories)
            {
                Console.WriteLine(i.Name);
                if (i.Name == "friendly_jigger")
                {
                    foreach (var j in i.Releases)
                    {
                        if (j.IsDraft)
                        {
                            Console.WriteLine("\tRelease - {0} (Draft)", j.Name);
                        }

                        if (j.IsPreRelease)
                        {
                            Console.WriteLine("\tRelease - {0} (PreRelease)", j.Name);
                        }

                    }
                }
            }
        }

        static void Main(string[] args)
        {
            string token = args.Length > 0 ? args[0] : System.IO.File.ReadAllText(@"..\..\..\..\key.txt");
            // TODO: see if I can figure out how to read rate limits from octokit and if not don't use it
            // TODO: Add support for CI to build the package in 3 variations: windows, linux and mac
            // TODO: Add support for publishing a file to a release
            // TODO: Figure out how to pass release info to tool in a simple way for publishing files
            // TODO: Make use of the tool itself to publish it's own release to Github
            var client = new GitHub(token);
            var repo = client.CurrentUser.FindRepoByName("friendly_github_tools");
            if (repo is null)
            {
                Console.WriteLine("Not found");
            }
            else
            {
                Console.WriteLine(repo.Name);
            }
            

        }
    }
}
