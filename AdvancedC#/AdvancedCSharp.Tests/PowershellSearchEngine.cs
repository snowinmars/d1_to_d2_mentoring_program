using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;

namespace AdvancedCSharp.Tests
{
    public class PowershellSearchEngine
    {
        public IEnumerable<string> Ls(string path, bool isRecursive = false, string filenameStartsWith = "")
        {
            var scriptPath = Path.Combine(TestsBase.RootPath, "DFSPSSearch.ps1");

            return PowershellSearchEngine.RunScript(scriptPath,
                 new[]
                 {
                    new CommandParameter(null, path),
                    new CommandParameter(null, filenameStartsWith),
                    new CommandParameter(null, $"${isRecursive}"),
                 });
        }

        /// <summary>
        /// Runs a Powershell script taking it's path and parameters.
        /// </summary>
        /// <param name="scriptFullPath">The full file path for the .ps1 file.</param>
        /// <param name="parameters">The parameters for the script, can be null.</param>
        /// <returns>The output from the Powershell execution.</returns>
        private static IEnumerable<string> RunScript(string scriptFullPath, ICollection<CommandParameter> parameters = null)
        {
            Runspace runspace = RunspaceFactory.CreateRunspace();

            runspace.Open();

            Pipeline pipeline = runspace.CreatePipeline();
            Command command = new Command(scriptFullPath);

            if (parameters != null)
            {
                foreach (var p in parameters)
                {
                    command.Parameters.Add(p);
                }
            }

            pipeline.Commands.Add(command);
            pipeline.Invoke();

            IEnumerable<object> enumerable = runspace.SessionStateProxy.GetVariable("global:results") as IEnumerable<object>;

            if (enumerable == null)
            {
                throw new InvalidOperationException();
            }

            IEnumerable<string> results = enumerable.Select(obj => obj as string);

            pipeline.Dispose();
            runspace.Dispose();
            return results;
        }
    }
}