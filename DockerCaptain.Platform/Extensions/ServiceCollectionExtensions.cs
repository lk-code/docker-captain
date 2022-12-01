using DockerCaptain.PlatformCore.Exceptions;
using DockerCaptain.PlatformCore.Helper;
using DockerCaptain.PlatformCore.Platforms;
using Microsoft.Extensions.DependencyInjection;

namespace DockerCaptain.PlatformCore.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPlatform(this IServiceCollection services)
    {
        Dictionary<string, Type> supportedPlatforms = new()
        {
            { "ubuntu", typeof(UbuntuPlatformService) },
            { "win10", typeof(WindowsPlatformService) },
            { "win11", typeof(WindowsPlatformService) }
        };

        // Debian:          debian.11-x64
        // Ubuntu:          ubuntu.22.04-x64
        // openSuse:        opensuse-leap.15.4-x64
        // Windows:         win10-x64
        // macOS:           osx.12-x64
        string runtimeIdentifier = System.Runtime.InteropServices.RuntimeInformation.RuntimeIdentifier;
        Platform platform = runtimeIdentifier.ToPlatform();

        // Debian:          Linux 5.15.74.2-microsoft-standard-WSL2 #1 SMP Wed Nov 2 19:50:29 UTC 2022
        // Ubuntu WSL:      Linux 5.15.74.2-microsoft-standard-WSL2 #1 SMP Wed Nov 2 19:50:29 UTC 2022
        // Ubuntu:          Linux 5.15.0-53-generic #59-Ubuntu SMP Mon Oct 17 18:53:30 UTC 2022
        // openSuse:        Linux 5.15.74.2-microsoft-standard-WSL2 #1 SMP Wed Nov 2 19:50:29 UTC 2022
        // Windows:         Microsoft Windows 10.0.22623
        // macOS: 
        string osType = System.Runtime.InteropServices.RuntimeInformation.OSDescription;
        platform.AddOSType(osType);

        if (!supportedPlatforms.ContainsKey(platform.OSName.ToLowerInvariant()))
        {
            throw new NotSupportedPlatformException($"platform '{platform.OSName}' is currently not supported");
        }

        var platformServiceType = supportedPlatforms[platform.OSName.ToLowerInvariant()];
        services.AddSingleton(typeof(IPlatform), platformServiceType);

        return services;
    }
}
