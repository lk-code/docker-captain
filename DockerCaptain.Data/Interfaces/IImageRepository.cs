using DockerCaptain.Data.Models;

namespace DockerCaptain.Data.Interfaces;

public interface IImageRepository
{
    /// <summary>
    /// returns the image with the matching name
    /// </summary>
    /// <param name="name">the name of the docker image</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Image?> GetImageByName(string name, CancellationToken cancellationToken);
    /// <summary>
    /// returns the image with the matching docker-id
    /// </summary>
    /// <param name="dockerId">the docker-id of the docker image</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Image?> GetImageByDockerId(string dockerId, CancellationToken cancellationToken);
    /// <summary>
    /// creates or updates an image-entity
    /// </summary>
    /// <param name="image">the image entity to create/update</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Image> CreateOrUpdateAsync(Image image, CancellationToken cancellationToken);
}
