using MediaBrowser.Controller.Entities.TV;
using MediaBrowser.Controller.Entities.Movies;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.Providers;

namespace Jellyfin.Plugin.FileHash.Providers.SHA1
{
    public class SHA1ExternalId : IExternalId
    {
        public bool Supports(IHasProviderIds item)
            => item is Series || item is Movie;

        public string ProviderName
            => "SHA1";

        public string Key
            => ProviderNames.SHA1;

        public ExternalIdMediaType? Type
            => null;

        public string UrlFormatString
            => "{0}";
    }
}
