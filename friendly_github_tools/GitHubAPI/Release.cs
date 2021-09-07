using Octokit;

namespace friendly_github_tools.GitHubAPI
{
    /// <summary>
    /// Abstraction around a GitHub release
    /// </summary>
    class Release
    {
        /// <summary>
        /// Reference to underlying Octokit API used for interacting with the REST API
        /// </summary>
        private readonly GitHubClient _client;
        /// <summary>
        /// Reference to the Octokit object describing the release being managed
        /// </summary>
        private readonly Octokit.Release _release;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">Octokit object used for interacting with the GitHub REST API</param>
        /// <param name="repo">Reference to the Octokit object describing the GitHub release</param>
        public Release(GitHubClient client, Octokit.Release release)
        {
            _client = client;
            _release = release;
        }

        /// <summary>
        /// Descriptive name associated with the release
        /// </summary>
        public string Name => _release.Name;

        /// <summary>
        /// Indicates whether this release is yet to be promoted to a pre-release or full release status
        /// </summary>
        public bool IsDraft => _release.Draft;

        /// <summary>
        /// Indicates whether this release is being considered for full release or not
        /// </summary>
        public bool IsPreRelease => _release.Prerelease;
    }
}
