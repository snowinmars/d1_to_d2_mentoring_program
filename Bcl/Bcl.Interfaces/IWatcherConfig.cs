using System.Collections.Generic;
using System.Globalization;

namespace Bcl.Interfaces
{
    public interface IWatcherConfig
    {
        CultureInfo CultureInfo { get; set; }
        string DefaultDestinationFolder { get; }

        IList<string> SourceDirectories { get; }
        bool IsVerbose { get; set; }
        IList<IWatcherRule> WatcherRules { get; }
    }
}