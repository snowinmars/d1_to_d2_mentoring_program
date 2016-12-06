using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace ExceptionHandling.Task2.Extensions
{
    public static class Extension
    {
        public static bool IsConsistOfPrintableChars(this string s)
        {
            return s.All(c => c.IsPrintable());
        }

        public static bool IsPrintable(this char c)
        {
            return !char.IsControl(c) && !char.IsWhiteSpace(c);
        }

        public static string TrimNonPrintableChars(this string s)
        {
            StringBuilder sb = new StringBuilder(s.Length);

            int headPosition = Extension.GetFirstPrintableCharPosition(s);
            int tailPosition = Extension.GetLastPrintableCharPosition(s);

            for (int i = headPosition; i <= tailPosition; i++)
            {
                sb.Append(s[i]);
            }

            return sb.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int GetFirstPrintableCharPosition(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i].IsPrintable())
                {
                    return i;
                }
            }

            return 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int GetLastPrintableCharPosition(string s)
        {
            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (s[i].IsPrintable())
                {
                    return i;
                }
            }

            return 0;
        }
    }
}