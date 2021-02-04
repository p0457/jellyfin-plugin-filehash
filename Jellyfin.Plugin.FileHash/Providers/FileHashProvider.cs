using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MediaBrowser.Common.Json;
using MediaBrowser.Common.Net;
using MediaBrowser.Controller.Configuration;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Entities.Audio;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Model.Dto;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.Providers;
using MediaBrowser.Controller.Entities.TV;
using MediaBrowser.Controller.Entities.Movies;

namespace Jellyfin.Plugin.FileHash.Providers
{
    public class FileHashProvider : ILocalMetadataProvider<Movie>
    {
        private readonly IServerConfigurationManager _config;

        public FileHashProvider(IServerConfigurationManager config)
        {
            _config = config;
        }

        /// <inheritdoc />
        public string Name => Plugin.ProviderName;

        /// <inheritdoc />
        public bool Supports(BaseItem item) => item is Series || item is Movie;

        public Task<MetadataResult<Movie>> GetMetadata(ItemInfo info, IDirectoryService directoryService, CancellationToken cancellationToken)
        {
            string filePath = info.Path;
            string checksumResult = String.Empty;
            int checksumBufferSize = 1200000;

            using (BufferedStream stream = new BufferedStream(File.OpenRead(filePath), checksumBufferSize))
            {
                byte[] checksum = null;
                System.Security.Cryptography.SHA1 sha = System.Security.Cryptography.SHA1.Create();
                checksum = sha.ComputeHash(stream);
                checksumResult = BitConverter.ToString(checksum).Replace("-", String.Empty);
            }

            MetadataResult<Movie> result = new MetadataResult<Movie>() 
            {
                HasMetadata = true,
                Provider = Plugin.ProviderId,
                Item = new Movie()
            };
            result.Item.SetProviderId(Plugin.ProviderId, "TEST");

            return Task.FromResult(result);
        }
    }
}
