using Bcl.Common;

namespace Bcl.Interfaces
{
    public interface IWatcherRule
    {
        string DestinationFolder { get; set; }
        WatcherRuleParams Params { get; set; }
        string Regex { get; set; }
    }
}