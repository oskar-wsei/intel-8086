using System.Diagnostics;

namespace Assembler.Core.Processes;

public class ProcessUtils
{
    public static ProcessResult Run(string command, string arguments)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = command,
            Arguments = arguments,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        };

        var process = new Process
        {
            StartInfo = startInfo,
            EnableRaisingEvents = true,
        };

        process.Start();
        process.WaitForExit();

        return new ProcessResult
        {
            Output = process.StandardOutput.ReadToEnd(),
            Errors = process.StandardError.ReadToEnd(),
        };
    }
}
