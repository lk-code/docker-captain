using System;
using static System.Environment;

namespace DockerCaptain.PlatformCore.Platforms;

public class OsxPlatformService : IPlatform
{

    private readonly string _applicationDirectory;
    public string ApplicationDirectory { get => _applicationDirectory; }

    public OsxPlatformService()
    {
        // Windows      C:\Users\larsk\AppData\Roaming\docker-captain
        // Ubuntu       /home/lkraemer/.config/docker-captain
        // Debian       /home/lkraemer/.config/docker-captain
        // openSuse     /home/lkraemer/.config/docker-captain
        // TODO: macOS        
        string appData = Path.Combine(Environment.GetFolderPath(SpecialFolder.ApplicationData, SpecialFolderOption.DoNotVerify), "docker-captain");

        _applicationDirectory = GetFolderPath(SpecialFolder.ApplicationData, SpecialFolderOption.DoNotVerify);
    }
}
