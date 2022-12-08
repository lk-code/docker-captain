using System.Diagnostics;
using System.Xml.Linq;
using DockerCaptain.Core.Exceptions;
using static System.Environment;

namespace DockerCaptain.PlatformCore.Platforms;

public class OsxPlatformService : IPlatform
{
    private readonly string _applicationDirectory;
    public string ApplicationDirectory { get => _applicationDirectory; }

    public OsxPlatformService()
    {
        _applicationDirectory = Environment.GetFolderPath(SpecialFolder.ApplicationData, SpecialFolderOption.DoNotVerify);
    }

    /// <inheritdoc/>
    public async Task<string> ExecuteShellCommandAsync(string executable, string arguments, CancellationToken cancellationToken)
    {
        Process process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = executable,
                Arguments = arguments,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };
        process.Start();

        string error = await process.StandardError.ReadToEndAsync();
        if (!string.IsNullOrEmpty(error.Trim()))
        {
            // ERROR
            string errMessage = error.Replace("Error:", "").Trim();
            throw new InvalidOperationException(errMessage);
        }

        // NO ERROR
        string output = await process.StandardOutput.ReadToEndAsync();
        return output;
    }

    /// <inheritdoc/>
    public async Task<string> GetDockerExecutableAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        string dockerExecutable = "/Applications/Docker.app/Contents/Resources/bin/docker";

        bool exists = File.Exists(@dockerExecutable);
        if (exists != true)
        {
            throw new InstallationNotFoundException($"docker installation not found at {dockerExecutable}");
        }

        return dockerExecutable;
    }
}
