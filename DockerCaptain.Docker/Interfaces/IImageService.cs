using DockerCaptain.Docker.Models;

namespace DockerCaptain.Docker.Interfaces;

public interface IImageService
{
    /// <summary>
    /// pulls a docker image and returns the result
    /// </summary>
    /// <param name="imageName">the name of the docker image</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PullResult> PullAsync(string imageName, CancellationToken cancellationToken);
}
