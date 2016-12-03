using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExceptionHandling.Task1.Extensions;

namespace ExceptionHandling.Task1.Core
{
    // ReSharper disable once InconsistentNaming
    public static class IEnumerableExtension
    {
        public static IEnumerable<char> Go(this IEnumerable<string> input)
        {
            if (input == null)
            {
                throw new ArgumentNullException();
            }

            if (!input.Any())
            {
                throw new IndexOutOfRangeException();
            }

            foreach (var str in input)
            {
                StringBuilder sb = new StringBuilder(str.Length);
                sb.Append(str.Where(c => c.IsPrintable()).ToArray());

                string trimmedString = sb.ToString();

                if (trimmedString == string.Empty)
                {
                    throw new IndexOutOfRangeException();
                }

                yield return trimmedString[0];
            }
        }
    }
}
