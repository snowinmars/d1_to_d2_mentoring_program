using Bcl.Core.ConfigEntities;
using Bcl.Interfaces;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
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

        /// <summary>
        /// This suppose to be full path to the directory
        /// </summary>
        public string DefaultDestinationFolder { get; private set; }

        public bool IsVerbose { get; set; }
        public bool MustAddDate { get; private set; }
        public bool MustAddNumber { get; private set; }
        public ResourceManager ResourceManager { get; private set; }

        /// <summary>
        /// This suppose to be full pathes to the directories
        /// </summary>
        public IList<string> SourceDirectories { get; }

        public IList<IWatcherRule> WatcherRules { get; }

        public static IWatcherConfig Load()
        {
            BclConfigSection config = BclConfigSection.GetConfig();

            CultureInfo culture = new CultureInfo(config.CultureInfo.Value);

            WatcherConfig watcherConfig = new WatcherConfig(culture);

            WatcherConfig.EnsureDirectoryExist(config.DefaultDestinationFolder.Value);
            watcherConfig.DefaultDestinationFolder = config.DefaultDestinationFolder.Value;

            foreach (FolderElement sourceFolder in config.SourceFoldersCollection)
            {
                WatcherConfig.EnsureDirectoryExist(sourceFolder.Path);
                watcherConfig.SourceDirectories.Add(sourceFolder.Path);
            }

            foreach (RuleElement ruleElement in config.Rules)
            {
                WatcherRule rule = new WatcherRule(ruleElement.Regex, ruleElement.MoveTo);

                WatcherConfig.EnsureDirectoryExist(rule.DestinationFolder);
                watcherConfig.WatcherRules.Add(rule);
            }

            watcherConfig.IsVerbose = config.IsVerbose.Value;
            watcherConfig.MustAddDate = config.MustAddDate.Value;
            watcherConfig.MustAddNumber = config.MustAddNumber.Value;

            Assembly assembly = typeof(Common.WatcherRuleParams).Assembly; // shit
            watcherConfig.ResourceManager = new ResourceManager("Bcl.Common.Resources.BclResource", assembly);

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

        private static void EnsureDirectoryExist(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}