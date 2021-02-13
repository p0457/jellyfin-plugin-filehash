using MediaBrowser.Controller.Entities.TV;
using MediaBrowser.Controller.Entities.Movies;
using MediaBrowser.Controller.Entities.Audio;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.Providers;

namespace Jellyfin.Plugin.FileHash.Providers.ExternalId
{
    public static class FileHashSHA512ExternalIdStrings
    {
        public static string ProviderName = "SHA512";
        public static string Key = "sha512";
        public static ExternalIdMediaType? Type = null;
        public static string UrlFormatString = "{0}";
    }
    public class FileHashSHA512ExternalId : IExternalId
    {
        public bool Supports(IHasProviderIds item) => item is Series || item is Movie || item is Audio;

        public string ProviderName => FileHashSHA512ExternalIdStrings.ProviderName;

        public string Key => FileHashSHA512ExternalIdStrings.Key;

        public ExternalIdMediaType? Type => FileHashSHA512ExternalIdStrings.Type;

        public string UrlFormatString => FileHashSHA512ExternalIdStrings.UrlFormatString;
    }
}
