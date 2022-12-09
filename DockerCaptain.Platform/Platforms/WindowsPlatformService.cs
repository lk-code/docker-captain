using System.Diagnostics;
using DockerCaptain.Core.Config;

namespace DockerCaptain.PlatformCore.Platforms;

public class WindowsPlatformService : IPlatform
{
    private readonly UserConfiguration _userConfiguration;

    public WindowsPlatformService(UserConfiguration userConfiguration)
    {
        this._userConfiguration = userConfiguration ?? throw new ArgumentNullException(nameof(userConfiguration));
    }

    /// <inheritdoc/>
    public async Task<string> ExecuteShellCommandAsync(string executable,
        string arguments,
        CancellationToken cancellationToken)
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

        if (!string.IsNullOrEmpty(this._userConfiguration.Docker))
        {
            return this._userConfiguration.Docker;
        }

        throw new NotImplementedException();
    }
}