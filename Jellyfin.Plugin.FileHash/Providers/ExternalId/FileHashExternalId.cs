using MediaBrowser.Controller.Entities.TV;
using MediaBrowser.Controller.Entities.Movies;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.Providers;

namespace Jellyfin.Plugin.FileHash.Providers.ExternalId
{
    public class FileHashExternalId : IExternalId
    {
        public bool Supports(IHasProviderIds item) => item is Series || item is Movie;

        public string ProviderName => Plugin.ProviderName;

        public string Key => Plugin.ProviderId;

        public ExternalIdMediaType? Type => null;

        public string UrlFormatString => "{0}";
    }
}
