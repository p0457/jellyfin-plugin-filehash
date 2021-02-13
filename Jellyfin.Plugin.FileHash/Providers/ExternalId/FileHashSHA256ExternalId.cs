using MediaBrowser.Controller.Entities.TV;
using MediaBrowser.Controller.Entities.Movies;
using MediaBrowser.Controller.Entities.Audio;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.Providers;

namespace Jellyfin.Plugin.FileHash.Providers.ExternalId
{
    public static class FileHashSHA256ExternalIdStrings
    {
        public static string ProviderName = "SHA256";
        public static string Key = "sha256";
        public static ExternalIdMediaType? Type = null;
        public static string UrlFormatString = "{0}";
    }
    public class FileHashSHA256ExternalId : IExternalId
    {
        public bool Supports(IHasProviderIds item) => item is Series || item is Movie || item is Audio;

        public string ProviderName => FileHashSHA256ExternalIdStrings.ProviderName;

        public string Key => FileHashSHA256ExternalIdStrings.Key;

        public ExternalIdMediaType? Type => FileHashSHA256ExternalIdStrings.Type;

        public string UrlFormatString => FileHashSHA256ExternalIdStrings.UrlFormatString;
    }
}
