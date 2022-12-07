using Cocona;
using DockerCaptain.Core.Exceptions;
using DockerCaptain.Core.Interfaces;
using DockerCaptain.Data.Interfaces;
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
        var image = await this._imageRepository.GetImageByName(name, CancellationToken.None);

        if (image != null)
        {
            // image already registered

            Console.WriteLine($"image {name} already registered!");

            return;
        }

        // get docker information
        try
        {
            await this._imageService.PullAsync(name);
        }
        catch (Exception err)
        {
            Console.WriteLine($"ERROR: {err.Message}");
        }

        try
        {
            string dockerExecutable = await this._platform.GetDockerExecutableAsync();

            string inspectArguments = $"image inspect {name}";
            Console.WriteLine($"DOCKER: {inspectArguments}");
            string inspectOutput = await this._platform.ExecuteShellCommandAsync(dockerExecutable, inspectArguments);

            // TODO: render INSPECT output
        }
        catch (Exception err)
        {
            Console.WriteLine($"ERROR: {err.Message}");
        }

        // save in db
    }
}
