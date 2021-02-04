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

namespace Jellyfin.Plugin.FileHash.ScheduledTasks
{
    /// <summary>
    /// Task that will generate hashes for local files
    /// </summary>
    public class GenerateHashesTask : IScheduledTask
    {
        private readonly ILibraryManager _libraryManager;
        private readonly IMetadataSaver _metadataSaver;

        /// <summary>
        ///
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="libraryManager"></param>
        public GenerateHashesTask(
            ILoggerFactory loggerFactory,
            ILibraryManager libraryManager,
            IMetadataSaver metadataSaver)
        {
            _logger = loggerFactory.CreateLogger<GenerateHashesTask>();
            _libraryManager = libraryManager;
            _metadataSaver = metadataSaver;
        }

        public string Key => "GenerateHashesTask";

        public IEnumerable<TaskTriggerInfo> GetDefaultTriggers() => Enumerable.Empty<TaskTriggerInfo>();

        public string Name => "Generate hashes";

        public string Description => "Generate hashes for local files";

        public string Category => "File Hash";

        /// <summary>
        /// Gather users and call <see cref="SyncTraktDataForUser"/>
        /// </summary>
        public async Task Execute(CancellationToken cancellationToken, IProgress<double> progress)
        {
            List<BaseItem> mediaItems =
                _libraryManager.GetItemList(
                        new InternalItemsQuery(user)
                        {
                            IncludeItemTypes = new[] { typeof(Movie).Name, typeof(Episode).Name },
                            IsVirtualItem = false,
                            OrderBy = new[]
                            {
                                new ValueTuple<string, SortOrder>(ItemSortBy.SeriesSortName, SortOrder.Ascending),
                                new ValueTuple<string, SortOrder>(ItemSortBy.SortName, SortOrder.Ascending)
                            }
                        })
                    .ToList();

            double total = mediaItems.Count;

            foreach(BaseItem item in mediaItems) 
            {
                string filePath = item.Path;
                string checksumResult = String.Empty;
                int checksumBufferSize = 1200000;
                
                using (BufferedStream stream = new BufferedStream(File.OpenRead(filePath), checksumBufferSize))
                {
                    byte[] checksum = null;
                    System.Security.Cryptography.SHA1 sha = System.Security.Cryptography.SHA1.Create();
                    checksum = sha.ComputeHash(stream);
                    checksumResult = BitConverter.ToString(checksum).Replace("-", String.Empty);
                }



                // TODO: Update metadata





                // TODO: report on progress


            }


            // progress.Report(currentProgress);
        }
    }
}
