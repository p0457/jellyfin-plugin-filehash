using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Jellyfin.Plugin.FileHash.Configuration;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Common.Net;
using MediaBrowser.Model.Plugins;
using MediaBrowser.Model.Serialization;
using Microsoft.Extensions.Logging;

namespace Jellyfin.Plugin.FileHash
{
    public class Plugin : BasePlugin<PluginConfiguration>, IHasWebPages
    {
        IHttpClientFactory _httpClientFactory;
        public Plugin(
            IApplicationPaths applicationPaths,
            IXmlSerializer xmlSerializer,
            IHttpClientFactory httpClientFactory)
            : base(applicationPaths, xmlSerializer)
        {
            Instance = this;
            _httpClientFactory = httpClientFactory;
        }

        public HttpClient GetHttpClient() {
            var httpClient = _httpClientFactory.CreateClient(NamedClient.Default);
            httpClient.DefaultRequestHeaders.UserAgent.Add(
                new ProductInfoHeaderValue(Name, Version.ToString()));

            return httpClient;
        }

        /// <inheritdoc />
        public override string Name => "File Hash";

        /// <inheritdoc />
        public override Guid Id => Guid.Parse("11ff9bad-19ef-4f91-98cf-7e3611fcd50e");

        public static Plugin Instance { get; private set; }

        /// <inheritdoc />
        public IEnumerable<PluginPageInfo> GetPages()
        {
            return new[]
            {
                new PluginPageInfo
                {
                    Name = Name,
                    EmbeddedResourcePath = string.Format("{0}.Configuration.configPage.html", GetType().Namespace)
                }
            };
        }
    }
}
