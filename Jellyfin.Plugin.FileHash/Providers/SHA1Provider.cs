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
    public class SHA1Provider : ILocalMetadataProvider
    {
        private readonly IServerConfigurationManager _config;
        private readonly IHttpClientFactory _httpClientFactory;

        public SHA1Provider(IServerConfigurationManager config, IHttpClientFactory httpClientFactory)
        {
            _config = config;
            _httpClientFactory = httpClientFactory;
        }

        /// <inheritdoc />
        public string Name => "SHA1";

        /// <inheritdoc />
        public bool Supports(BaseItem item)
            => item is Series || item is Movie;

        public Task<MetadataResult<string>> GetMetadata(ItemInfo info, IDirectoryService directoryService, CancellationToken cancellationToken)
        {
            string filePath = info.Path;
            string checksumResult = String.Empty;
            int checksumBufferSize = 1200000;

            /*using (BufferedStream stream = new BufferedStream(File.OpenRead(filePath), checksumBufferSize))
            {
                byte[] checksum = null;
                System.Security.Cryptography.SHA1 sha = System.Security.Cryptography.SHA1.Create();
                checksum = sha.ComputeHash(stream);
                checksumResult = BitConverter.ToString(checksum).Replace("-", String.Empty);

                MetadataResult<string> result = new MetadataResult<string>() 
                {
                    HasMetadata = true,
                    Provider = "SHA1",
                    Item = checksumResult
                };

                return Task.FromResult(result);
            }*/



            // For some reason this isn't working? Perhaps the hook is not being called. Try generating a file to see.
            // Otherwise, maybe it's not passing the proper model back
            MetadataResult<string> result = new MetadataResult<string>() 
            {
                HasMetadata = true,
                Provider = ProviderNames.SHA1,
                Item = "TEST"
            };

            return Task.FromResult(result);
        }
    }
}
