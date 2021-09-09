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
        /// <param name="productName">
        /// Identification string for the product that will be interacting with the GitHub API
        /// </param>
        public GitHub(string token, string productName)
        {
            var creds = new InMemoryCredentialStore(new Credentials(token));
            _client = new GitHubClient(new ProductHeaderValue(productName), creds);
            //var rl = new Octokit.RateLimit();
        }

        /// <summary>
        /// Reference to data associated with the currently authenticated user
        /// </summary>
        public User CurrentUser => new User(_client);

        /// <summary>
        /// Gets the number of HTTP requests that can be made before the GitHub API starts blocking
        /// you from making additional requests
        /// </summary>
        public int RemainingRateLimit => _client.Miscellaneous.GetRateLimits().Result.Rate.Remaining;
    }
}
