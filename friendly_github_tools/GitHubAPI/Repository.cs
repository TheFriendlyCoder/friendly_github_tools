using System.Collections.Generic;
using Octokit;

namespace friendly_github_tools.GitHubAPI
{
    /// <summary>
    /// Abstraction around a GitHub code repository
    /// </summary>
    class Repository
    {
        /// <summary>
        /// Reference to underlying Octokit API used for interacting with the REST API
        /// </summary>
        private readonly GitHubClient _client;
        /// <summary>
        /// Reference to the underlying Octokit object describing the GitHub repo
        /// </summary>
        private readonly Octokit.Repository _obj;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">Octokit object used for interacting with the GitHub REST API</param>
        /// <param name="repo">Reference to the Octokit object describing the GitHub repo</param>
        public Repository(GitHubClient client, Octokit.Repository repo)
        {
            _client = client;
            _obj = repo;
        }

        /// <summary>
        /// Name of the repository
        /// </summary>
        public string Name => _obj.Name;

        /// <summary>
        /// List of 0 or more releases associated with this repository
        /// </summary>
        public List<Release> Releases
        {
            get
            {
                
                List<Release> retval = new List<Release>();
                foreach (var i in _client.Repository.Release.GetAll(_obj.Owner.Login, Name).Result)
                {
                    retval.Add(new Release(_client, i));
                }
                return retval;
            }
        }
    }
}
