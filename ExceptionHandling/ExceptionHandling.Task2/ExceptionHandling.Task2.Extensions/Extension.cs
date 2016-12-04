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

        public static bool IsConsistOfPrintableChars(this string s)
        {
            return s.All(c => c.IsPrintable());
        }

        public static string TrimNonPrintableChars(this string s)
        {
            StringBuilder sb = new StringBuilder(s.Length);

            sb.Append(s.Where(c => c.IsPrintable()).ToArray());

            return sb.ToString();
        }
    }
}
