using MediaBrowser.Controller.Entities.TV;
using MediaBrowser.Controller.Entities.Movies;
using MediaBrowser.Controller.Entities.Audio;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.Providers;

namespace Jellyfin.Plugin.FileHash.Providers.ExternalId
{
    public static class FileHashMD5ExternalIdStrings
    {
        public static string ProviderName = "MD5";
        public static string Key = "md5";
        public static ExternalIdMediaType? Type = null;
        public static string UrlFormatString = "{0}";
    }
    public class FileHashMD5ExternalId : IExternalId
    {
        public bool Supports(IHasProviderIds item) => item is Series || item is Movie || item is Audio;

        public string ProviderName => FileHashMD5ExternalIdStrings.ProviderName;

        public string Key => FileHashMD5ExternalIdStrings.Key;

        public ExternalIdMediaType? Type => FileHashMD5ExternalIdStrings.Type;

        public string UrlFormatString => FileHashMD5ExternalIdStrings.UrlFormatString;
    }
}
