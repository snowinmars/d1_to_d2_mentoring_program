using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;

namespace AdvancedCSharp.Tests
{
    public static class Extensions
    {
        public static string RepresentAsString(this IEnumerable<PSObject> collection)
        {
            StringBuilder sb = new StringBuilder(128);

            foreach (var item in collection)
            {
                sb.Append(item);
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }
    }

    public class PowershellSearchEngine
    {
        public IEnumerable<PSObject> Ls(string path, bool isRecursive = false, string filenameStartsWith = "")
        {
            StringBuilder command = new StringBuilder(128);

            command.Append($" ls -path '{path}'");

            if (isRecursive)
            {
                command.Append(" -R ");
            }

            command.Append($" -filter '{filenameStartsWith}*.*' ");

            using (RunspaceInvoke invoke = new RunspaceInvoke())
            {
                return invoke.Invoke(command.ToString());
            }
        }
    }
}