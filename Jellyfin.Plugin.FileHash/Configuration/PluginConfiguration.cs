using MediaBrowser.Model.Plugins;

namespace Jellyfin.Plugin.FileHash.Configuration
{
    /// <summary>
    /// Class PluginConfiguration
    /// </summary>
    public class PluginConfiguration : BasePluginConfiguration
    {
        public int SHA1BufferSize { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginConfiguration" /> class.
        /// </summary>
        public PluginConfiguration()
        {
            SHA1BufferSize = 1200000;   
        }
    }
}
