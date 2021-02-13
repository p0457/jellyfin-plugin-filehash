using MediaBrowser.Controller.Entities.TV;
using MediaBrowser.Controller.Entities.Movies;
using MediaBrowser.Controller.Entities.Audio;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.Providers;

namespace Jellyfin.Plugin.FileHash.Providers.ExternalId
{
    public static class FileHashSHA1ExternalIdStrings
    {
        public static string ProviderName = "SHA1";
        public static string Key = "sha1";
        public static ExternalIdMediaType? Type = null;
        public static string UrlFormatString = "{0}";
    }
    public class FileHashSHA1ExternalId : IExternalId
    {
        public bool Supports(IHasProviderIds item) => item is Series || item is Movie || item is Audio;

        public string ProviderName => FileHashSHA1ExternalIdStrings.ProviderName;

        public string Key => FileHashSHA1ExternalIdStrings.Key;

        public ExternalIdMediaType? Type => FileHashSHA1ExternalIdStrings.Type;

        public string UrlFormatString => FileHashSHA1ExternalIdStrings.UrlFormatString;
    }
}
