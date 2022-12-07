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
    public Task<string> ExecuteShellCommandAsync(string executable, string arguments)
    {
        throw new NotImplementedException();
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
