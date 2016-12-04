using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandling.Task2.Extensions
{
    public static class Extension
    {
        public static bool IsPrintable(this char c)
        {
            return !char.IsControl(c) && !char.IsWhiteSpace(c);
        }
    }
}
