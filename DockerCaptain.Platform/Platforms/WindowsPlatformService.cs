using System.Diagnostics;
using System.Xml.Linq;
using static System.Environment;

namespace DockerCaptain.PlatformCore.Platforms;

public class WindowsPlatformService : IPlatform
{
    public WindowsPlatformService()
    {
    }

    /// <inheritdoc/>
    public async Task<string> ExecuteShellCommandAsync(string executable, string arguments, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        //Process process = new Process();
        //// Redirect the output stream of the child process.
        //process.StartInfo.UseShellExecute = false;
        //process.StartInfo.RedirectStandardOutput = true;
        //process.StartInfo.RedirectStandardError = true;
        //process.StartInfo.FileName = "cmd.exe";
        ////process.StartInfo.Arguments = $"docker image inspect {name} & exit";
        //process.StartInfo.Arguments = arguments;

        //process.Start();

        //var output = new List<string>();

        //while (process.StandardOutput.Peek() > -1)
        //{
        //    string str = await process.StandardOutput.ReadLineAsync()!;

        //    Console.WriteLine(str);

        //    output.Add(str);
        //}

        //while (process.StandardError.Peek() > -1)
        //{
        //    string str = await process.StandardError.ReadLineAsync()!;

        //    Console.WriteLine("ERROR: " + str);

        //    output.Add(str);
        //}
        //process.WaitForExitAsync(cancellationToken);

        // Do not wait for the child process to exit before
        // reading to the end of its redirected stream.
        // p.WaitForExit();
        // Read the output stream first and then wait.

        return "";
    }

    /// <inheritdoc/>
    public async Task<string> GetDockerExecutableAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        throw new NotImplementedException();
    }
}