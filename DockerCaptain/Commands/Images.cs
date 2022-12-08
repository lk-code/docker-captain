using Cocona;
using DockerCaptain.Core.Exceptions;
using DockerCaptain.Core.Interfaces;
using DockerCaptain.Core.Models;
using DockerCaptain.Data.Interfaces;
using DockerCaptain.Data.Models;
using DockerCaptain.PlatformCore;
using System;
using System.Diagnostics;

namespace DockerCaptain.Commands;

public class Images
{
    private readonly IImageRepository _imageRepository;
    private readonly IImageService _imageService;
    private readonly IPlatform _platform;

    public Images(IImageRepository imageRepository,
        IImageService imageService,
        IPlatform platform)
    {
        this._imageRepository = imageRepository ?? throw new ArgumentNullException(nameof(imageRepository));
        this._imageService = imageService ?? throw new ArgumentNullException(nameof(imageService));
        this._platform = platform ?? throw new ArgumentNullException(nameof(platform));
    }

    [Command("register")]
    public async Task Register([Argument(Description = "name of the docker image")] string name)
    {
        Image? image = await this._imageRepository.GetImageByName(name, CancellationToken.None);
        if (image != null)
        {
            // image already registered

            Console.WriteLine($"image {name} already registered!");

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
            Console.WriteLine($"ERROR: {err.Message}");
        }
        if (hasError)
        {
            return;
        }


        // save in db
        try
        {
            image = await this._imageRepository.CreateOrUpdateAsync(new Image(pullResult.Id, pullResult.Name), CancellationToken.None);
        }
        catch (Exception err)
        {
            hasError = true;
            Console.WriteLine($"ERROR: {err.Message}");
        }
        if (hasError)
        {
            return;
        }

        Console.WriteLine($"Image {pullResult.Id} successfully registered and pulled to docker.");
    }
}
