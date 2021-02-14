using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using MediaBrowser.Model.Tasks;
using MediaBrowser.Controller.Entities;
using System.IO;
using MediaBrowser.Controller.Library;
using MediaBrowser.Model.Entities;
using Microsoft.Extensions.Logging;
using MediaBrowser.Controller.Entities.Movies;
using MediaBrowser.Controller.Entities.TV;
using Jellyfin.Data.Enums;
using MediaBrowser.Model.Querying;
using System.Diagnostics;
using Jellyfin.Plugin.FileHash.Providers.ExternalId;
using System.Security.Cryptography;

namespace Jellyfin.Plugin.FileHash
{
    /// <summary>
    /// Task that will generate hashes for local files
    /// </summary>
    public class GenerateHashesTask : IScheduledTask
    {
        private ILibraryManager _libraryManager;
        private readonly ILogger<GenerateHashesTask> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="libraryManager"></param>
        public GenerateHashesTask(ILibraryManager libraryManager, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<GenerateHashesTask>();
            _libraryManager = libraryManager;
        }

        public string Key => "GenerateHashesTask";

        public IEnumerable<TaskTriggerInfo> GetDefaultTriggers() => Enumerable.Empty<TaskTriggerInfo>();

        public string Name => "Generate Hashes";

        public string Description => "Generate hashes for local files";

        public string Category => "File Hash";

        public bool IsHidden => false;

        public bool IsEnabled => true;

        public bool IsLogged => true;


        /// <summary>
        /// 
        /// </summary>
        public async Task Execute(CancellationToken cancellationToken, IProgress<double> progress)
        {
            progress.Report(1);

            List<BaseItem> mediaItems =
                _libraryManager.GetItemList(
                        new InternalItemsQuery()
                        {
                            IncludeItemTypes = new[] { typeof(Movie).Name, typeof(Episode).Name },
                            IsVirtualItem = false,
                            OrderBy = new[]
                            {
                                new ValueTuple<string, SortOrder>(ItemSortBy.SeriesSortName, SortOrder.Ascending),
                                new ValueTuple<string, SortOrder>(ItemSortBy.SortName, SortOrder.Ascending)
                            }
                            // Not sure how to filter for provider id to make this easier
                        })
                    .ToList();

            double totalItemCount = mediaItems.Count;

            _logger.LogInformation($"Found {totalItemCount} items to process");

            int maxItemsToProcessPerRun = Plugin.Instance.PluginConfiguration.MaxItemsToProcessPerRun;
            if (maxItemsToProcessPerRun > 0 && maxItemsToProcessPerRun < totalItemCount) {
                mediaItems = mediaItems.Take(maxItemsToProcessPerRun).ToList();
                totalItemCount = mediaItems.Count;
                _logger.LogInformation($"Items to process exceeds configured max items to process, clamping to configured value of {maxItemsToProcessPerRun}");
            }

            double progressCurrent = 5;

            progress.Report(progressCurrent);

            List<HashAlgorithm> algorithms = HashAlgorithmsDictionary.GetConfiguredAlgorithms();

            double endBuffer = 5;
            
            double totalAlgCount = algorithms.Count;
            double eachAlgInterval = (100 - progressCurrent - endBuffer)/(totalItemCount * totalAlgCount);

            foreach(BaseItem item in mediaItems) 
            {
                var test1 = item.GetProviderId("sha1"); // Empty on 1st, 2nd run
                _logger.LogInformation($"____TESTING1 for sha1.. {test1}");
                _logger.LogInformation($"____TESTING1 ITEM PROVIDER ID KEYS = {String.Join(",", item.ProviderIds.Keys)}");
                _logger.LogInformation($"____TESTING1 ITEM PROVIDER ID VALUES = {String.Join(",", item.ProviderIds.Values)}");




                Stopwatch sw = new Stopwatch();

                string filePath = item.Path;

                _logger.LogTrace($"Processing {filePath}...");
                
                algorithms.ForEach(async (HashAlgorithm alg) => 
                {
                    sw.Start();
                    string hashResult = String.Empty;

                    _logger.LogTrace($"Processing {filePath} using configured buffer size {alg.BufferSize} for algorithm {alg.Enum.ToString()}...");

                    using (BufferedStream stream = new BufferedStream(File.OpenRead(filePath), alg.BufferSize))
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        hashResult = alg.Hash(stream);
                        item.SetProviderId(alg.ExternalId, hashResult);
                    }
                    sw.Stop();
                    string elapsed = $"{sw.Elapsed.TotalSeconds.ToString()} seconds";

                    _logger.LogInformation($"Generated hash in {elapsed} for '{filePath}' using configured buffer size {alg.BufferSize} for algorithm {alg.Enum.ToString()}: {hashResult}");

                    await _libraryManager.RunMetadataSavers(item, ItemUpdateType.MetadataEdit);

                    var test1 = item.GetProviderId("sha1"); // Populated on 1st, 2nd run
                    _logger.LogInformation($"____TESTING2 for sha1.. {test1}");
                    _logger.LogInformation($"____TESTING2 ITEM PROVIDER ID KEYS = {String.Join(",", item.ProviderIds.Keys)}");
                    _logger.LogInformation($"____TESTING2 ITEM PROVIDER ID VALUES = {String.Join(",", item.ProviderIds.Values)}");
                    
                    progressCurrent += eachAlgInterval;
                    
                    progress.Report(progressCurrent);
                });

                progress.Report(progressCurrent);
            }

            progressCurrent += endBuffer;

            progress.Report(progressCurrent);
        }
    }
}
