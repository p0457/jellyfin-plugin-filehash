using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MediaBrowser.Common.Net;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Library;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.Providers;
using MediaBrowser.Controller.Configuration;
using MediaBrowser.Controller.Entities.TV;
using MediaBrowser.Controller.Entities.Movies;
using Microsoft.Extensions.Logging;

namespace Jellyfin.Plugin.FileHash.Providers
{
    public class FileHashProvider : IRemoteMetadataProvider<Movie, MovieInfo>
    {
        private readonly IServerConfigurationManager _config;
        private readonly ILogger<FileHashProvider> _log;
        private readonly IProviderManager _providerManager;

        public FileHashProvider(IServerConfigurationManager config, ILogger<FileHashProvider> logger, IProviderManager providerManager)
        {
            _config = config;
            _log = logger;
            _providerManager = providerManager;
            _log.LogInformation("TEST FILE HASH CONSTRUCTOR");
        }

        /// <inheritdoc />
        public string Name => Plugin.ProviderName;

        /// <inheritdoc />
        public bool Supports(BaseItem item) => item is Series || item is Movie;

        public Task<MetadataResult<Movie>> GetMetadata(MovieInfo info, CancellationToken cancellationToken)
        {
            _log.LogInformation("TEST FILE HASH GETMETADATA START");

            string filePath = info.Path;
            string checksumResult = String.Empty;
            int checksumBufferSize = 1200000;

            /*using (BufferedStream stream = new BufferedStream(File.OpenRead(filePath), checksumBufferSize))
            {
                byte[] checksum = null;
                System.Security.Cryptography.SHA1 sha = System.Security.Cryptography.SHA1.Create();
                checksum = sha.ComputeHash(stream);
                checksumResult = BitConverter.ToString(checksum).Replace("-", String.Empty);
            }*/

            MetadataResult<Movie> result = new MetadataResult<Movie>();
            result.HasMetadata = true;
            result.Item = new Movie();
            result.Item.ProviderIds.Add(Plugin.ProviderName, "TEST1");
            //_providerManager.SaveMetadata(result.Item, MediaBrowser.Controller.Library.ItemUpdateType.MetadataImport);

            //result.Item.SetProviderId(Plugin.ProviderName, "TEST");

            _log.LogInformation("TEST FILE HASH GETMETADATA END");

            return Task.FromResult(result);
        }

        public Task<IEnumerable<RemoteSearchResult>> GetSearchResults(MovieInfo searchInfo, CancellationToken cancellationToken)
        {
            _log.LogInformation("TEST FILE HASH GetSearchResults END");
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> GetImageResponse(string url, CancellationToken cancellationToken)
        {
            _log.LogInformation("TEST FILE HASH GetImageResponse END");
            throw new NotImplementedException();
        }
    }
}
