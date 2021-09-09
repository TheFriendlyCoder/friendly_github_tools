using System;
using System.IO;
using System.Runtime.InteropServices;
using friendly_github_tools.GitHubAPI;

namespace friendly_github_tools
{
    
    class Program
    {

        static void Main(string[] args)
        {
            string projectFolder = Path.GetFullPath(@"..\..\..\..\");
            string fileToUpload =
                Path.Combine(projectFolder, @"friendly_github_tools\bin\Debug\netcoreapp3.1\friendly_github_tools.exe");
            string token = args.Length > 0 ? args[0] : System.IO.File.ReadAllText(Path.Combine(projectFolder, @"key.txt"));
            var client = new GitHub(token, "friendly_github_tools");
            var repo = client.CurrentUser.FindRepoByName("friendly_jigger");
            if (repo is null)
            {
                Console.WriteLine("Not found");
                return;
            }
            
            // Purge all previous draft releases
            foreach (var curRelease in repo.Releases)
            {
                if (!curRelease.IsDraft) continue;
                if (curRelease.Name != "MyCoolName") continue;
                curRelease.Delete();
            }

            var newRelease = repo.CreateRelease();
            newRelease.Name = "MyCoolName";
            var asset = newRelease.AddAsset(fileToUpload);
            Console.WriteLine(string.Format("File uploaded: {0}", asset.DownloadURL));
            
            // TODO: make this property writeable
            // TODO: make sure this operation creates a tag
            //newRelease.IsPreRelease = true;

            Console.WriteLine(string.Format("Operation complete ({0})", client.RemainingRateLimit));
        }
    }
}
