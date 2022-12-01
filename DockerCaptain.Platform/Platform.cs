namespace DockerCaptain.PlatformCore;

public class Platform
{
    private string _osIdentifier = null!;
    /// <summary>
    /// 
    /// </summary>
    public string OSIdentifier
    {
        get { return _osIdentifier; }
        private set
        {
            _osIdentifier = value;

            OSName = GetOsName(_osIdentifier);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public readonly string OSArchitecture;
    /// <summary>
    /// 
    /// </summary>
    public string? OSType { get; internal set; } = null;
    /// <summary>
    /// 
    /// </summary>
    public string OSName { get; internal set; } = null!;

    public Platform(string osIdentifier,
        string osArchitecture)
    {
        OSIdentifier = osIdentifier;
        OSArchitecture = osArchitecture;
    }

    /// <summary>
    /// returns the os-name from the os-identifier
    /// </summary>
    /// <param name="osIdentifier">the os-identifier like: debian.11, ubuntu.22.04, opensuse-leap.15.4, win10, osx.12</param>
    private string GetOsName(string osIdentifier)
    {
        return osIdentifier.Split('.')[0];
    }
}
