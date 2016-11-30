using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdvancedCSharp.Extensions
{
    public static class Extensions
    {
        public static string RepresentAsString(this IEnumerable<FileSystemInfo> collection)
        {
            StringBuilder sb = new StringBuilder(128);

            foreach (var item in collection)
            {
                sb.Append(item.FullName);
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }

        public static string RepresentAsString(this IEnumerable<string> collection)
        {
            StringBuilder sb = new StringBuilder(128);

            foreach (var item in collection)
            {
                sb.Append($"{item}");
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }
    }
}