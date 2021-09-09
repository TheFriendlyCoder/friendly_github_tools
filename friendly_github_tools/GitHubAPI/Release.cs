using System.IO;
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
        private Octokit.Release _release;
        /// <summary>
        /// Repository this release is associated with
        /// </summary>
        private readonly Octokit.Repository _parent;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">Octokit object used for interacting with the GitHub REST API</param>
        /// <param name="parentRepo">Reference to the repository this release is associated with</param>
        /// <param name="repo">Reference to the Octokit object describing the GitHub release</param>
        public Release(GitHubClient client, Octokit.Repository parentRepo, Octokit.Release release)
        {
            _client = client;
            _release = release;
            _parent = parentRepo;
        }

        /// <summary>
        /// Descriptive name associated with the release
        /// </summary>
        public string Name
        {
            get => _release.Name;
            set
            {
                var updateRelease = _release.ToUpdate();
                updateRelease.Name = value;
                _release = _client.Repository.Release.Edit(_parent.Id, _release.Id, updateRelease).Result;
            }
        }

        /// <summary>
        /// Indicates whether this release is yet to be promoted to a pre-release or full release status
        /// </summary>
        public bool IsDraft => _release.Draft;

        /// <summary>
        /// Indicates whether this release is being considered for full release or not
        /// </summary>
        public bool IsPreRelease => _release.Prerelease;

        public Asset AddAsset(string pathToFile)
        {
            using (var archiveData = File.OpenRead(pathToFile))
            {
                var assetUpload = new ReleaseAssetUpload()
                {
                    FileName = Path.GetFileName(pathToFile),
                    ContentType = "application/octet-stream",
                    RawData = archiveData
                };
                var asset = _client.Repository.Release.UploadAsset(_release, assetUpload).Result;
                return new Asset(_client, asset);
            }
        }

        /// <summary>
        /// Deletes this release from the repository. This operation is destructive and unrecoverable.
        /// </summary>
        public void Delete()
        {
            _client.Repository.Release.Delete(_parent.Id, _release.Id);
        }
    }
}
