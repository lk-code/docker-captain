using DockerCaptain.Core.Models;

namespace DockerCaptain.Core.Interfaces;

public interface IImageService
{
    /// <summary>
    /// pulls a docker image and returns the result
    /// </summary>
    /// <param name="imageName">the name of the docker image</param>
    /// <returns></returns>
    Task<PullResult> PullAsync(string imageName);
}
