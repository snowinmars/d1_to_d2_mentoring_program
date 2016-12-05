using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ExceptionHandling.Task2.Extensions;

namespace ExceptionHandling.Task2.Core
{
    internal enum DigitSign
    {
        None = 0,
        Plus = 1,
        Minus = 2,
    }

    internal class Digit
    {
        private Digit(DigitSign sign, int value)
        {
            this.Sign = sign;
            this.Value = value;
        }

        internal DigitSign Sign { get; }

        internal int Value
        {
            get
            {
                switch (this.Sign)
                {
                    case DigitSign.None:
                    case DigitSign.Plus:
                        return this.value;
                    case DigitSign.Minus:
                        return -this.value;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            private set { this.value = value; }
        }

        private static readonly IDictionary<string, DigitSign> SignBinding = new Dictionary<string, DigitSign>
        {
            {string.Empty, DigitSign.None}, {"+", DigitSign.Plus}, {"-", DigitSign.Minus},
        };

        private static readonly IDictionary<char, int> DigitBinding = new Dictionary<char, int>
        {
            {'0', 0}, {'1', 1}, {'2', 2}, {'3', 3}, {'4', 4}, {'5', 5}, {'6', 6}, {'7', 7}, {'8', 8}, {'9', 9},
        };

        private int value;


        public static Digit Parse(string digit, string stringValue)
        {
            DigitSign sign = Digit.SignBinding[digit];

            long value = 0;
            int order = 0;
            string str = stringValue;

            for (int i = str.Length - 1; i >= 0; i--)
            {
                int v = Digit.DigitBinding[str[i]];

                value += (long) (Math.Pow(10, order)*v);

                order++;
            }

            if (value > int.MaxValue)
            {
                throw new OverflowException();
            }

            return new Digit(sign, (int) value);
        }
    }

    public static class Core
    {
        private const int IntMaxValueOrder = 10;
        private const string NumberRegexPattern = "^([+-]?)([0-9]*)$";

        public static int Parse(string inputString)
        {
            if (inputString == null)
            {
                throw new ArgumentNullException();
            }

            string trimedString = Core.Trim(inputString);

            bool isMatch = Regex.IsMatch(input: trimedString, pattern: Core.NumberRegexPattern, options: RegexOptions.CultureInvariant);

            if (!isMatch)
            {
                throw new FormatException();
            }

            MatchCollection matches = Regex.Matches(input: trimedString, pattern: Core.NumberRegexPattern, options: RegexOptions.CultureInvariant);

            Match match = matches[0];

            Group digitGroup = match.Groups[2];
            Group signGroup = match.Groups[1];

            Digit digit = Digit.Parse(signGroup.Value, digitGroup.Value);

            return digit.Value;
        }

        private static string Trim(string inputString)
        {
            string trimedString = inputString.TrimNonPrintableChars();

            if (trimedString == string.Empty)
            {
                throw new FormatException();
            }

            if (trimedString.Length > Core.IntMaxValueOrder)
            {
                throw new OverflowException();
            }

            return trimedString;
        }
    }
}
