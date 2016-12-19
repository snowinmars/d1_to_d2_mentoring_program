using System;

namespace Bcl.Enums
{
    [Flags]
    public enum WatcherRuleParams
    {
        None = 0,
        AddNumber = 1,
        AddDate = 2,
    }
}