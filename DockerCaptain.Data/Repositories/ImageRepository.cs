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
            .SingleOrDefaultAsync(x => x.Name.ToLowerInvariant() == name.ToLowerInvariant(), cancellationToken);

        return image;
    }
}
