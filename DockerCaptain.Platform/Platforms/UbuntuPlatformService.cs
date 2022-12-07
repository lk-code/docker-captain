using System.Diagnostics;
using DockerCaptain.Core.Exceptions;
using static System.Environment;

namespace DockerCaptain.PlatformCore.Platforms;

public class UbuntuPlatformService : IPlatform
{
    private readonly string _applicationDirectory;
    public string ApplicationDirectory { get => _applicationDirectory; }

    public UbuntuPlatformService()
    {
        _applicationDirectory = Environment.GetFolderPath(SpecialFolder.ApplicationData, SpecialFolderOption.DoNotVerify);
    }

    /// <inheritdoc/>
    public async Task<string> ExecuteShellCommandAsync(string executable, string arguments)
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
    public async Task<string> GetDockerExecutableAsync()
    {
        await Task.CompletedTask;

        List<string> dockerInstallLocations = new List<string>
        {
            "/bin/docker",
            "/var/lib/docker"
        };

        foreach (string dockerInstallLocation in dockerInstallLocations)
        {
            bool exists = File.Exists(@dockerInstallLocation);
            if (exists == true)
            {
                return dockerInstallLocation;
            }
        }

        throw new InstallationNotFoundException($"no potential docker installation found");
    }
}
