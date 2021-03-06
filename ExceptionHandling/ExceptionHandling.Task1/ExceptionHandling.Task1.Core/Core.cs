﻿using ExceptionHandling.Task1.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExceptionHandling.Task1.Core
{
    // ReSharper disable once InconsistentNaming
    public static class Core
    {
        public static char CutOff(this string entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException("Entry is null");
            }

            if (entry == string.Empty)
            {
                throw new IndexOutOfRangeException("Entry is empty");
            }

            string trimmedString = Core.Trim(entry);

            if (trimmedString == string.Empty)
            {
                throw new IndexOutOfRangeException("Entry is empty");
            }

            return trimmedString[0];
        }

        public static IEnumerable<char> CutOff(this IEnumerable<string> input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("Input is null");
            }

            if (!input.Any())
            {
                return new char[0];
            }

            return input.Select(s => s.CutOff());
        }

        private static string Trim(string entry)
        {
            StringBuilder sb = new StringBuilder(entry.Length);

            sb.Append(entry.Where(c => c.IsPrintable()).ToArray());

            return sb.ToString();
        }
    }
}