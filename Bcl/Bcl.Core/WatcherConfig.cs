using Bcl.Core;
using Bcl.Interfaces;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Bcl.Enums
{
    public class WatcherConfig : IWatcherConfig
    {
        public WatcherConfig(CultureInfo cultureInfo)
        {
            this.CultureInfo = cultureInfo;
            this.DirectoriesToListenFor = new List<string>();
            this.WatcherRules = new List<IWatcherRule>();
        }

        public bool IsVerbose { get; set; }

        public CultureInfo CultureInfo { get; set; }

        public string DefaultDestinationFolder { get; private set; }

        /// <summary>
        /// This suppose to be full pathes to the directories
        /// </summary>
        public IList<string> DirectoriesToListenFor { get; }

        public IList<IWatcherRule> WatcherRules { get; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine();
            sb.Append($"{nameof(WatcherConfig.IsVerbose)}: {this.IsVerbose}");
            sb.AppendLine();
            sb.Append($"{nameof(WatcherConfig.CultureInfo)}: {this.CultureInfo}");
            sb.AppendLine();

            sb.Append($"{nameof(WatcherConfig.DirectoriesToListenFor)}:");
            sb.AppendLine();
            foreach (var dir in this.DirectoriesToListenFor)
            {
                sb.Append($"    {dir}");
                sb.AppendLine();
            }

            sb.AppendLine();
            sb.Append($"{nameof(WatcherConfig.WatcherRules)}:");
            sb.AppendLine();
            foreach (var rule in this.WatcherRules)
            {
                sb.Append($"    {rule}");
                sb.AppendLine();
            }

            sb.AppendLine();

            return sb.ToString();
        }

        public static IWatcherConfig Load()
        {
            WatcherConfig watcherConfig = new WatcherConfig(CultureInfo.CurrentCulture);
            watcherConfig.DirectoriesToListenFor.Add(@"D:\");

            IWatcherRule rule = new WatcherRule(regex: @".*@.*\..*",
                                                destinationFolder: @"D:\tmp");
            watcherConfig.WatcherRules.Add(rule);

            watcherConfig.IsVerbose = true;

            watcherConfig.DefaultDestinationFolder = @"D:\def";

            return watcherConfig;
        }
    }
}