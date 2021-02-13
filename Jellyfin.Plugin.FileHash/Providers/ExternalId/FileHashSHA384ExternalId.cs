using MediaBrowser.Controller.Entities.TV;
using MediaBrowser.Controller.Entities.Movies;
using MediaBrowser.Controller.Entities.Audio;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.Providers;

namespace Jellyfin.Plugin.FileHash.Providers.ExternalId
{
    public static class FileHashSHA384ExternalIdStrings
    {
        public static string ProviderName = "SHA384";
        public static string Key = "sha384";
        public static ExternalIdMediaType? Type = null;
        public static string UrlFormatString = "{0}";
    }
    public class FileHashSHA384ExternalId : IExternalId
    {
        public bool Supports(IHasProviderIds item) => item is Series || item is Movie || item is Audio;

        public string ProviderName => FileHashSHA384ExternalIdStrings.ProviderName;

        public string Key => FileHashSHA384ExternalIdStrings.Key;

        public ExternalIdMediaType? Type => FileHashSHA384ExternalIdStrings.Type;

        public string UrlFormatString => FileHashSHA384ExternalIdStrings.UrlFormatString;
    }
}
