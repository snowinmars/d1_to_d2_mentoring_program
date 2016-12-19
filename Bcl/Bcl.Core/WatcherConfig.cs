﻿using Bcl.Interfaces;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Bcl.Core
{
    public class WatcherConfig : IWatcherConfig
    {
        private WatcherConfig(CultureInfo cultureInfo)
        {
            this.CultureInfo = cultureInfo;
            this.SourceDirectories = new List<string>();
            this.WatcherRules = new List<IWatcherRule>();
        }

        public CultureInfo CultureInfo { get; set; }
        public string DefaultDestinationFolder { get; private set; }

        /// <summary>
        /// This suppose to be full pathes to the directories
        /// </summary>
        public IList<string> SourceDirectories { get; }

        public bool IsVerbose { get; set; }
        public IList<IWatcherRule> WatcherRules { get; }

        public static IWatcherConfig Load()
        {
            WatcherConfig watcherConfig = new WatcherConfig(CultureInfo.CurrentCulture);
            watcherConfig.SourceDirectories.Add(@"D:\");

            IWatcherRule rule = new WatcherRule(regex: @".*@.*\..*",
                                                destinationFolder: @"D:\tmp");
            watcherConfig.WatcherRules.Add(rule);

            watcherConfig.IsVerbose = true;

            watcherConfig.DefaultDestinationFolder = @"D:\def";

            return watcherConfig;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine();
            sb.Append($"{nameof(WatcherConfig.IsVerbose)}: {this.IsVerbose}");
            sb.AppendLine();
            sb.Append($"{nameof(WatcherConfig.CultureInfo)}: {this.CultureInfo}");
            sb.AppendLine();

            sb.Append($"{nameof(WatcherConfig.SourceDirectories)}:");
            sb.AppendLine();
            foreach (var dir in this.SourceDirectories)
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
    }
}