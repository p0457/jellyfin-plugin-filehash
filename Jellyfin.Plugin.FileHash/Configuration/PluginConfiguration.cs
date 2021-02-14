using MediaBrowser.Model.Plugins;

namespace Jellyfin.Plugin.FileHash.Configuration
{
    /// <summary>
    /// Class PluginConfiguration
    /// </summary>
    public class PluginConfiguration : BasePluginConfiguration
    {
        public int MaxItemsToProcessPerRun { get; set; }
        public bool MD5Enabled { get; set; }
        public int MD5BufferSize { get; set; }
        public bool SHA1Enabled { get; set; }
        public int SHA1BufferSize { get; set; }
        public bool SHA256Enabled { get; set; }
        public int SHA256BufferSize { get; set; }
        public bool SHA384Enabled { get; set; }
        public int SHA384BufferSize { get; set; }
        public bool SHA512Enabled { get; set; }
        public int SHA512BufferSize { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginConfiguration" /> class.
        /// </summary>
        public PluginConfiguration()
        {
            MaxItemsToProcessPerRun = 3;

            MD5Enabled = true;
            MD5BufferSize = 1200000;

            SHA1Enabled = true;
            SHA1BufferSize = 1200000;

            SHA256Enabled = true;
            SHA256BufferSize = 1200000;

            SHA384Enabled = true;
            SHA384BufferSize = 1200000;

            SHA512Enabled = true;
            SHA512BufferSize = 1200000;
        }
    }
}
