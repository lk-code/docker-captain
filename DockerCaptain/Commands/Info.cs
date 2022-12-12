﻿using System;
using System.Text;
using Cocona;
using DockerCaptain.Data.Interfaces;
using DockerCaptain.Logging;
using DockerCaptain.PlatformCore;
using Figgle;
using Microsoft.Extensions.Logging;

namespace DockerCaptain.Commands;

public class Info : BaseCommand
{
    private readonly IImageRepository _imageRepository;
    private readonly IPlatform _platform;

    public Info(ILogger<Info> logger,
        IImageRepository imageRepository,
        IPlatform platform) : base(logger)
    {
        this._imageRepository = imageRepository ?? throw new ArgumentNullException(nameof(imageRepository));
        this._platform = platform ?? throw new ArgumentNullException(nameof(platform));
    }

    public async Task InfoCommand()
    {
        this._logger.LogTrace("Info->DisplayInfo");

        try
        {
            StringBuilder infoDisplay = new StringBuilder();

            this._logger.LogTrace("load DockerCaptain version");
            string version = this.GetType().Assembly.GetName().Version!.ToString();
            this._logger.LogTrace(version);

            this._logger.LogTrace("load ApplicationFolderPath");
            string appDirectoryPath = Program.OriginalApplicationFolderPath;
            this._logger.LogTrace(appDirectoryPath);

            this._logger.LogTrace("load UserConfig ApplicationFolderPath");
            string userConfigAppDirectoryPath = Program.ApplicationFolderPath;
            this._logger.LogTrace(userConfigAppDirectoryPath);

            this._logger.LogTrace("load Docker executable path");
            string dockerExecutablePath = await this._platform.GetDockerExecutableAsync(CancellationToken.None);
            this._logger.LogTrace(dockerExecutablePath);

            infoDisplay.AppendLine(FiggleFonts.Slant.Render("DockerCaptain"));
            infoDisplay.AppendLine("");
            infoDisplay.AppendLine("");
            infoDisplay.AppendLine($"Version: {version}");
            infoDisplay.AppendLine($"DockerCaptain Project - https://www.github.com/lk-code/docker-captain");
            infoDisplay.AppendLine($"Development by lk-code - https://www.github.com/lk-code");
            infoDisplay.AppendLine($"Application Directory: {appDirectoryPath}");
            infoDisplay.AppendLine($"User Configured Application Directory: {((appDirectoryPath.ToLowerInvariant() != userConfigAppDirectoryPath.ToLower()) ? userConfigAppDirectoryPath : "None specified")}");
            infoDisplay.AppendLine("");
            infoDisplay.AppendLine("");
            infoDisplay.AppendLine("Docker");
            infoDisplay.AppendLine($"Executable: {dockerExecutablePath}");

            Console.WriteLine(infoDisplay.ToString());
        }
        catch (Exception err)
        {
            this.WriteError(err.Message);

            this._logger.LogError(LogEvents.CommandInfoDisplayInfo, err, err.Message);
        }
    }
}