using System;
using friendly_github_tools.GitHubAPI;

namespace friendly_github_tools
{
    
    class Program
    {

        static void Main(string[] args)
        {
            // TODO: Add support for CI to build the package in 3 variations: windows, linux and mac
            // TODO: Add support for publishing a file to a release
            // TODO: Add command line parameter for API token
            // TODO: Figure out how to pass release info to tool in a simple way for publishing files
            // TODO: Make use of the tool itself to publish it's own release to Github
            string token = System.IO.File.ReadAllText(@"..\..\..\..\key.txt");

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
    }
}
