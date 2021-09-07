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

        public string Name => _obj.Name;
    }
}
