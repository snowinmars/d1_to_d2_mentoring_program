using Bcl.Interfaces;
using System.Text;
using Bcl.Enums;

namespace Bcl.Core
{
    public class WatcherRule : IWatcherRule
    {
        public WatcherRule(string regex, string destinationFolder)
        {
            this.Params = WatcherRuleParams.None;
            this.Regex = regex;
            this.DestinationFolder = destinationFolder;
        }

        /// <summary>
        /// This suppose to be full path to the directory
        /// </summary>
        public string DestinationFolder { get; set; }

        public WatcherRuleParams Params { get; set; }
        public string Regex { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"  {nameof(WatcherRule.Regex)}: {this.Regex},  ");
            sb.Append($"  {nameof(WatcherRule.DestinationFolder)}: {this.DestinationFolder},  ");
            sb.Append($"  {nameof(WatcherRule.Params)}: {this.Params}  ");

            return sb.ToString();
        }
    }
}