using DockerCaptain.Data.Models;

namespace DockerCaptain.Data.Interfaces;

public interface IImageRepository
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Image?> GetImageByName(string name, CancellationToken cancellationToken);
}
