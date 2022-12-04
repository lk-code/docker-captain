namespace DockerCaptain.PlatformCore;

public interface IPlatform
{
    // Windows      C:\Users\larsk\AppData\Roaming\docker-captain
    // Ubuntu       /home/lkraemer/.config/docker-captain
    // Debian       /home/lkraemer/.config/docker-captain
    // openSuse     /home/lkraemer/.config/docker-captain
    // osx          /Users/larskramer/.config/docker-captain
    string ApplicationDirectory { get; }

    /// <summary>
    /// executes a shell command and returns the output as string
    /// </summary>
    /// <param name="exe">the executed file (cmd.exe, etc.)</param>
    /// <param name="arguments">the arguments for the execution</param>
    /// <returns></returns>
    Task<string> ExecuteShellCommandAsync(string arguments);
}
