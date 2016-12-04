using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExceptionHandling.Task2.Extensions;

namespace ExceptionHandling.Task2.Core
{
    public static class Core
    {
        private const int IntMaxValueOrder = 10;

        public static int Parse(string inputString)
        {
            if (inputString == null)
            {
                throw new ArgumentNullException();
            }

            long returnValue = 0;

            string trimedString = inputString.TrimNonPrintableChars();

            if (trimedString == string.Empty)
            {
                throw new FormatException();
            }

            if (trimedString.Length > Core.IntMaxValueOrder)
            {
                throw new OverflowException();
            }

            int l = 0;

            for (int i = trimedString.Length - 1; i >= 0; i--)
            {
                int v = Map(trimedString[i]);

                returnValue += (long) (Math.Pow(10, l)*v);

                l++;
            }

            if (returnValue > int.MaxValue)
            {
                throw new OverflowException();
            }

            return (int)returnValue;
        }

        private static int Map(char c)
        {
            switch (c)
            {
                case '0':
                    return 0;
                case '1':
                    return 1;
                case '2':
                    return 2;
                case '3':
                    return 3;
                case '4':
                    return 4;
                case '5':
                    return 5;
                case '6':
                    return 6;
                case '7':
                    return 7;
                case '8':
                    return 8;
                case '9':
                    return 9;
                default:
                    throw new FormatException();
            }
        }
    }
}
