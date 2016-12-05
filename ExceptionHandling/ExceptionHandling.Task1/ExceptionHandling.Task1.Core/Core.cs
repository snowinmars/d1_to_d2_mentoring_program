using ExceptionHandling.Task1.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExceptionHandling.Task1.Core
{
    // ReSharper disable once InconsistentNaming
    public static class Core
    {
        public static char CutOff(this string input)
            => new[] { input }.CutOff().First();

        public static IEnumerable<char> CutOff(this IEnumerable<string> input)
        {
            if (input == null)
            {
                throw new ArgumentNullException();
            }

            if (!input.Any())
            {
                throw new IndexOutOfRangeException();
            }

            return input.Select(Core.HandleEntry).Select(trimmedString => trimmedString[0]);
        }

        private static string HandleEntry(string entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException();
            }

            if (entry == string.Empty)
            {
                throw new IndexOutOfRangeException();
            }

            string trimmedString = Core.Trim(entry);

            if (trimmedString == string.Empty)
            {
                throw new IndexOutOfRangeException();
            }

            return trimmedString;
        }

        private static string Trim(string entry)
        {
            StringBuilder sb = new StringBuilder(entry.Length);

            sb.Append(entry.Where(c => c.IsPrintable()).ToArray());

            return sb.ToString();
        }
    }
}