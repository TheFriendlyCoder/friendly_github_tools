using System;
using friendly_github_tools.GitHubAPI;

namespace friendly_github_tools
{
    
    class Program
    {

        static void Main(string[] args)
        {
            string token = args.Length > 0 ? args[0] : System.IO.File.ReadAllText(@"..\..\..\..\key.txt");
            var client = new GitHub(token, "friendly_github_tools");
            var repo = client.CurrentUser.FindRepoByName("friendly_jigger");
            if (repo is null)
            {
                Console.WriteLine("Not found");
            }
            else
            {
                Console.WriteLine(repo.Name);
                foreach (var i in repo.Releases)
                {
                    Console.WriteLine(i.IsDraft);
                }
                
            }

            Console.WriteLine(client.RemainingRateLimit);
        }
    }
}
