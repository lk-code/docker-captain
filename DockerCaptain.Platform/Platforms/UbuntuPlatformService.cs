using static System.Environment;

namespace DockerCaptain.PlatformCore.Platforms;

public class UbuntuPlatformService : IPlatform
{

    private readonly string _applicationDirectory;
    public string ApplicationDirectory { get => _applicationDirectory; }

    public UbuntuPlatformService()
    {
        _applicationDirectory = GetFolderPath(SpecialFolder.ApplicationData, SpecialFolderOption.DoNotVerify);
    }
}
