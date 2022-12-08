using DockerCaptain.Data.Interfaces;
using DockerCaptain.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DockerCaptain.Data.Repositories;

public class ImageRepository : IImageRepository
{
    private readonly DataContext _dataContext;

    public ImageRepository(DataContext dataContext)
    {
        this._dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
    }

    /// <inheritdoc/>
    public async Task<Image?> GetImageByName(string name, CancellationToken cancellationToken)
    {
        Image? image = await this._dataContext
            .Images
            .SingleOrDefaultAsync(x => x.Name.ToLower() == name.ToLower(), cancellationToken);

        return image;
    }

    /// <inheritdoc/>
    public async Task<Image?> GetImageByDockerId(string dockerId, CancellationToken cancellationToken)
    {
        Image? image = await this._dataContext
            .Images
            .SingleOrDefaultAsync(x => x.DockerId.ToLower() == dockerId.ToLower(), cancellationToken);

        return image;
    }

    /// <inheritdoc/>
    public async Task<Image> CreateOrUpdateAsync(Image image, CancellationToken cancellationToken)
    {
        Image? existingImage = await this.GetImageByDockerId(image.DockerId, cancellationToken);

        Image imageEntity = null!;
        if (existingImage != null)
        {
            // update
            imageEntity = this._dataContext.Images.Update(image).Entity;
        }
        else
        {
            // create
            imageEntity = this._dataContext.Images.Add(image).Entity;
        }

        await this._dataContext.SaveChangesAsync(cancellationToken);

        return imageEntity;
    }
}
