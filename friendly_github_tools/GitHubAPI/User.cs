using System.Collections.Generic;
using Octokit;

namespace friendly_github_tools.GitHubAPI
{
    /// <summary>
    /// Abstraction around a GitHub user
    /// </summary>
    class User
    {
        /// <summary>
        /// Reference to underlying Octokit API used for interacting with the REST API
        /// </summary>
        private readonly GitHubClient _client;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">Octokit object used to interact with the GitHub REST API</param>
        public User(GitHubClient client)
        {
            _client = client;
        }

        /// <summary>
        /// List of 0 or more repositories owned by this user
        /// </summary>
        public List<Repository> Repositories
        {
            get
            {
                var repositories = _client.Repository.GetAllForCurrent().Result;
                var retval = new List<Repository>();
                foreach (var i in repositories)
                {
                    retval.Add(new Repository(_client, i));
                }

                return retval;
            }
        }
    }
}
