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
                sb.Append(item.Name);
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }
    }
}