using Bcl.Interfaces;
using System.Text;

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

        public string Regex { get; set; }

        /// <summary>
        /// This suppose to be full path to the directory
        /// </summary>
        public string DestinationFolder { get; set; }

        public WatcherRuleParams Params { get; set; }

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