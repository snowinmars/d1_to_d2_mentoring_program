using System.Collections.Generic;
using System.Globalization;
using System.Resources;

namespace Bcl.Interfaces
{
    public interface IWatcherConfig
    {
        ResourceManager ResourceManager { get;  }
        CultureInfo CultureInfo { get; set; }
        string DefaultDestinationFolder { get; }

        IList<string> SourceDirectories { get; }
        bool IsVerbose { get; set; }
        IList<IWatcherRule> WatcherRules { get; }
    }
}