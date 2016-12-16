using System;

namespace Bcl.Core
{
    [Flags]
    public enum WatcherRuleParams
    {
        None = 0,
        AddNumber = 1,
        AddDate = 2,
    }
}