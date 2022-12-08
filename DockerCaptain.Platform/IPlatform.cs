namespace DockerCaptain.PlatformCore;

public interface IPlatform
{
    /// <summary>
    /// executes a shell command and returns the output as string
    /// </summary>
    /// <param name="executable">the executed file (cmd.exe, etc.)</param>
    /// <param name="arguments">the arguments for the execution</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    Task<string> ExecuteShellCommandAsync(string executable, string arguments, CancellationToken cancellationToken);

    /// <summary>
    /// returns the path to the docker installation or throws an exception if the docker executable was not found
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="InstallationNotFoundException"></exception>
    Task<string> GetDockerExecutableAsync(CancellationToken cancellationToken);
}
