using System;
using System.Collections.Generic;

namespace ExceptionHandling.Task2.Core
{
    internal class Digit
    {
        private static readonly IDictionary<char, int> DigitBinding = new Dictionary<char, int>
        {
            {'0', 0}, {'1', 1}, {'2', 2}, {'3', 3}, {'4', 4}, {'5', 5}, {'6', 6}, {'7', 7}, {'8', 8}, {'9', 9},
        };

        private static readonly IDictionary<string, DigitSign> SignBinding = new Dictionary<string, DigitSign>
        {
            {string.Empty, DigitSign.None}, {"+", DigitSign.Plus}, {"-", DigitSign.Minus},
        };

        private int value;

        private Digit(DigitSign sign, int value)
        {
            this.Sign = sign;
            this.Value = value;
        }

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

        private DigitSign Sign { get; }

        public static Digit Parse(string digit, string str)
        {
            long value = 0;
            int order = 0;

            for (int i = str.Length - 1; i >= 0; i--)
            {
                int v = Digit.DigitBinding[str[i]];

                value += (long)(Math.Pow(10, order) * v);

                order++;
            }

            if (value > int.MaxValue) // TODO check all boundaries conditions here. Now it's a bit buggy
            {
                throw new OverflowException();
            }

            DigitSign sign = Digit.SignBinding[digit];
            return new Digit(sign, (int)value);
        }
    }
}