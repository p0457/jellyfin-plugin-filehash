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
using System.Threading.Tasks;

namespace Jellyfin.Plugin.FileHash
{
    public class Plugin : BasePlugin<PluginConfiguration>, IHasWebPages
    {
        /// <summary>
        /// Gets the provider name.
        /// </summary>
        public const string ProviderName = "File Hash";

        /// <summary>
        /// Gets the provider id.
        /// </summary>
        public const string ProviderId = "filehash";

        public Plugin(
            IApplicationPaths applicationPaths,
            IXmlSerializer xmlSerializer)
            : base(applicationPaths, xmlSerializer)
        {
            Instance = this;
            PollingTasks = new Dictionary<string, Task<bool>>();
        }

        public Dictionary<string, Task<bool>> PollingTasks { get; set; }

        public PluginConfiguration PluginConfiguration => Configuration;

        /// <inheritdoc />
        public override string Name => "File Hash";

        public override string Description => "Hash your files for identification";

        /// <inheritdoc />
        public override Guid Id => Guid.Parse("11ff9bad-19ef-4f91-98cf-7e3611fcd50e");

        /// <summary>
        /// Gets current plugin instance.
        /// </summary>

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
