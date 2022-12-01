using Cocona;
using Cocona.Filters;
using DockerCaptain.Data.Interfaces;

namespace DockerCaptain.Commands;

public class Images
{
    private readonly IImageRepository _imageRepository;

    public Images(IImageRepository imageRepository)
    {
        this._imageRepository = imageRepository ?? throw new ArgumentNullException(nameof(imageRepository));
    }

    [Command("register")]
    public async Task Register(CoconaCommandExecutingContext ctx, [Argument(Description = "name of the docker image")] string name)
    {
        var image = await this._imageRepository.GetImageByName(name, CancellationToken.None);

        if (image != null)
        {
            // image already registered

            Console.WriteLine($"image {name} already registered!");

            return;
        }

        // get docker id

        // save in db
    }
}
