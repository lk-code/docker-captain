using Cocona;
using DockerCaptain.Core.Exceptions;
using DockerCaptain.Data.Interfaces;
using DockerCaptain.PlatformCore;
using System;
using System.Diagnostics;

namespace DockerCaptain.Commands;

public class Images
{
    private readonly IImageRepository _imageRepository;
    private readonly IPlatform _platform;

    public Images(IImageRepository imageRepository,
        IPlatform platform)
    {
        this._imageRepository = imageRepository ?? throw new ArgumentNullException(nameof(imageRepository));
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

        var vars = Environment.GetEnvironmentVariables();

        // get docker information
        try
        {
            string dockerExecutable = await this._platform.GetDockerExecutableAsync();

            string pullArguments = $"pull {name}";
            Console.WriteLine($"DOCKER: {pullArguments}");
            string pullOutput = await this._platform.ExecuteShellCommandAsync(dockerExecutable, pullArguments);

            string inspectArguments = $"image inspect {name}";
            Console.WriteLine($"DOCKER: {inspectArguments}");
            string inspectOutput = await this._platform.ExecuteShellCommandAsync(dockerExecutable, inspectArguments);
        }
        catch (Exception err)
        {
            Console.WriteLine($"ERROR: {err.Message}");
        }

        // save in db
    }
}
