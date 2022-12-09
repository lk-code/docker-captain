using Cocona;
using DockerCaptain.Data.Interfaces;
using DockerCaptain.Data.Models;
using DockerCaptain.Docker.Interfaces;
using DockerCaptain.Docker.Models;
using DockerCaptain.Logging;
using DockerCaptain.PlatformCore;
using Microsoft.Extensions.Logging;

namespace DockerCaptain.Commands;

public class Images
{
    private readonly ILogger<Images> _logger;
    private readonly IImageRepository _imageRepository;
    private readonly IImageService _imageService;
    private readonly IPlatform _platform;

    public Images(ILogger<Images> logger,
        IImageRepository imageRepository,
        IImageService imageService,
        IPlatform platform)
    {
        this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this._imageRepository = imageRepository ?? throw new ArgumentNullException(nameof(imageRepository));
        this._imageService = imageService ?? throw new ArgumentNullException(nameof(imageService));
        this._platform = platform ?? throw new ArgumentNullException(nameof(platform));
    }

    [Command("register")]
    public async Task Register([Option('f')] bool force,
        [Argument(Description = "name of the docker image")] string name)
    {
        this._logger.LogTrace("Images->Register");

        Image? image = await this._imageRepository.GetImageByName(name, CancellationToken.None);
        if (image != null
            && force != true)
        {
            // image already registered

            this._logger.LogInformation($"image {name} already registered!");

            return;
        }

        bool hasError = false;

        // get docker information
        PullResult pullResult = null!;
        try
        {
            pullResult = await this._imageService.PullAsync(name, CancellationToken.None);
        }
        catch (Exception err)
        {
            hasError = true;
            this._logger.LogError(LogEvents.CommandImagesRegister, err, err.Message);
        }
        if (hasError)
        {
            return;
        }


        // save in db
        try
        {
            this._logger.LogTrace("try to create or update the docker image registry");
            image = await this._imageRepository.CreateOrUpdateAsync(new Image(pullResult.Id, pullResult.Name), CancellationToken.None);
        }
        catch (Exception err)
        {
            hasError = true;
            this._logger.LogError(LogEvents.CommandImagesRegister, err, err.Message);
        }
        if (hasError)
        {
            return;
        }

        this._logger.LogInformation($"Image {pullResult.Id} successfully registered and pulled to docker.");
    }
}
