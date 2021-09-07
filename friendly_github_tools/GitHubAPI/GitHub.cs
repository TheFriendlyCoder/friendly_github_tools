using Octokit;
using Octokit.Internal;

namespace friendly_github_tools.GitHubAPI
{
    /// <summary>
    /// Main entry point for all GitHub API interactions. From this class the caller
    /// can interact with the various GitHub primitives using user friendly abstractions
    /// </summary>
    class GitHub
    {
        /// <summary>
        /// Reference to underlying Octokit API used for interacting with the REST API
        /// </summary>
        private readonly GitHubClient _client;

        /// <summary>
        /// Constructor for teh class
        /// </summary>
        /// <param name="token">Authentication token used for non-anonymous access to the API</param>
        public GitHub(string token)
        {
            var creds = new InMemoryCredentialStore(new Credentials(token));
            _client = new GitHubClient(new ProductHeaderValue("friendly_github_tools"), creds);
        }

        /// <summary>
        /// Reference to data associated with the currently authenticated user
        /// </summary>
        public User CurrentUser => new User(_client);
    }
}
