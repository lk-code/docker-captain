using System.Text.RegularExpressions;

namespace DockerCaptain.PlatformCore.Helper;

public static class PlatformHelperTests
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="runtimeIdentifier">like win10-x64, ubuntu.22.04-x64 or osx.12-x64</param>
    /// <returns></returns>
    public static Platform ToPlatform(this string runtimeIdentifier)
    {
        Regex regex = new Regex("-{1}(x86|x64|arm64|arm)");         // Split on hyphens.
        string[] substrings = regex.Split(runtimeIdentifier);

        return new Platform(substrings[0], substrings[1]);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="platform"></param>
    /// <param name="osType">like "Linux 5.15.0-53-generic #59-Ubuntu SMP Mon Oct 17 18:53:30 UTC 2022"</param>
    /// <returns></returns>
    public static Platform AddOSType(this Platform platform, string osType)
    {
        // TODO: check for macOS

        if (osType.ToLowerInvariant().Contains("linux"))
        {
            // check for linux
            platform.OSType = "linux";
        }
        else if (osType.ToLowerInvariant().Contains("windows"))
        {
            // check for windows
            platform.OSType = "windows";
        }
        else if (osType.ToLowerInvariant().Contains("macos"))
        {
            // check for macOS
            platform.OSType = "macos";
        }
        else
        {
            platform.OSType = "Unknown";
        }

        return platform;
    }
}
