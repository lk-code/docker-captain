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

        throw new NotImplementedException();
    }
}
