using System;
using System.Collections.Generic;
using System.Text;
using Octokit;

namespace friendly_github_tools.GitHubAPI
{
    class Asset
    {
        /// <summary>
        /// Reference to underlying Octokit API used for interacting with the REST API
        /// </summary>
        private readonly GitHubClient _client;

        /// <summary>
        /// Reference to the Octokit object describing the asset being managed
        /// </summary>
        private Octokit.ReleaseAsset _asset;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">Octokit object used for interacting with the GitHub REST API</param>
        /// <param name="repo">Reference to the Octokit object describing the GitHub asset</param>
        public Asset(GitHubClient client, Octokit.ReleaseAsset asset)
        {
            _client = client;
            _asset = asset;
        }

        /// <summary>
        /// the number of times this asset has been downloaded
        /// </summary>
        public int DownloadCount => _asset.DownloadCount;

        /// <summary>
        /// URL to the asset which can be used to download the file
        /// </summary>
        public string DownloadURL => _asset.BrowserDownloadUrl;
    }
}
