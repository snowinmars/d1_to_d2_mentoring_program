using ExceptionHandling.Task2.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace ExceptionHandling.Task2.Core
{
    public static class Core
    {
        private const int IntMaxValueOrder = 10;
        private const string NumberRegexPattern = "^([+-]?)([0-9]*)$";

        public static int Parse(string inputString)
        {
            if (inputString == null)
            {
                throw new ArgumentNullException("Input string is null");
            }

            string trimedString = Core.Trim(inputString);

            // int cant contain 10^10. If it's more - anyway Overflow will be thrown
            //if (trimedString.Length > Core.IntMaxValueOrder)
            //{
            //    foreach (var sign in Digit.SignBinding.Keys.Where(s => s != string.Empty))
            //    {
            //        if (trimedString.StartsWith(sign, StringComparison.InvariantCultureIgnoreCase))
            //        {
            //            goto ok;
            //        }
            //    }

            //    throw new OverflowException("Input cant be fitted to Integer range");
            //}
            //ok:
            //;



            if (trimedString.Length > Core.IntMaxValueOrder)
            {
                if (!Digit.SignBinding.Keys
                        .Where(sign => sign != string.Empty)
                        .Any(sign => trimedString.StartsWith(sign, StringComparison.InvariantCultureIgnoreCase)))
                {
                    throw new OverflowException("Input cant be fitted to Integer range");
                }
            }



            //if (trimedString.Length > Core.IntMaxValueOrder)
            //{
            //    bool wasFound = false;

            //    foreach (var sign in Digit.SignBinding.Keys.Where(s => s != string.Empty))
            //    {
            //        if (trimedString.StartsWith(sign, StringComparison.InvariantCultureIgnoreCase))
            //        {
            //            wasFound = true;
            //        }

            //        if (wasFound)
            //        {
            //            break;
            //        }
            //    }

            //    if (!wasFound)
            //    {
            //        throw new OverflowException("Input cant be fitted to Integer range");
            //    }
            //}




            //if (trimedString.Length > Core.IntMaxValueOrder)
            //{
            //    bool wasFound = false;

            //    IEnumerator<string> enumerator = Digit.SignBinding.Keys.Where(s => s != string.Empty).GetEnumerator();

            //    if (enumerator.MoveNext())
            //    {
            //        do
            //        {
            //            string sign = enumerator.Current;

            //            if (trimedString.StartsWith(sign, StringComparison.InvariantCultureIgnoreCase))
            //            {
            //                wasFound = true;
            //            }
            //        } while (!wasFound && enumerator.MoveNext());
            //    }

            //    if (!wasFound)
            //    {
            //        throw new OverflowException("Input cant be fitted to Integer range");
            //    }
            //}

            Core.EnsureCorrectFormat(trimedString);
            return Core.ParseImplementation(trimedString);
        }

        private static void EnsureCorrectFormat(string trimedString)
        {
            bool isMatch = Regex.IsMatch(input: trimedString,
                            pattern: Core.NumberRegexPattern,
                            options: RegexOptions.CultureInvariant);

            if (!isMatch)
            {
                throw new FormatException($"Wrong input string format. Must be {NumberRegexPattern}");
            }
        }

        private static int ParseImplementation(string trimedString)
        {
            MatchCollection matches = Regex.Matches(input: trimedString,
                            pattern: Core.NumberRegexPattern,
                            options: RegexOptions.CultureInvariant);

            Match match = matches[0];

            Group signGroup = match.Groups[1];
            Group digitGroup = match.Groups[2];

            Digit digit = Digit.Parse(signGroup.Value, digitGroup.Value);

            return digit.Value;
        }

        private static string Trim(string inputString)
        {
            if (string.IsNullOrWhiteSpace(inputString))
            {
                throw new FormatException("Input string is empty");
            }

            string trimedString = inputString.TrimNonPrintableChars();

            if (trimedString == string.Empty)
            {
                throw new FormatException($"Wrong input string format. Must be {NumberRegexPattern}");
            }

            return trimedString;
        }
    }
}