using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bcl.Common
{
    public static class Extensions
    {
        public static IEnumerable<TOut> ForEach<TIn, TOut>(this IEnumerable<TIn> input, Func<TIn, TOut> func)
            where TOut: class 
            => input.Select(item => func?.Invoke(item));
    }
}
