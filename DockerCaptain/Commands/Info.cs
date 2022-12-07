﻿using System;
using System.Text;
using DockerCaptain.Data.Interfaces;
using DockerCaptain.PlatformCore;
using Figgle;

namespace DockerCaptain.Commands;

public class Info
{
    private readonly IImageRepository _imageRepository;
    private readonly IPlatform _platform;

    public Info(IImageRepository imageRepository,
        IPlatform platform)
    {
        this._imageRepository = imageRepository ?? throw new ArgumentNullException(nameof(imageRepository));
        this._platform = platform ?? throw new ArgumentNullException(nameof(platform));
    }

    public async Task DisplayInfo()
    {
        StringBuilder infoDisplay = new StringBuilder();

        infoDisplay.AppendLine("");
        infoDisplay.AppendLine("");
        infoDisplay.AppendLine(FiggleFonts.Standard.Render("DockerCaptain"));
        infoDisplay.AppendLine("");
        infoDisplay.AppendLine("");
        infoDisplay.AppendLine($"Version: 1.0.0");
        infoDisplay.AppendLine($"Development by lk-code - https://www.github.com/lk-code");
        infoDisplay.AppendLine($"Application Directory: {Program.ApplicationFolderPath}");
        infoDisplay.AppendLine("");
        infoDisplay.AppendLine("");
        infoDisplay.AppendLine("Doker");
        infoDisplay.AppendLine($"Executable: {await this._platform.GetDockerExecutableAsync()}");

        Console.WriteLine(infoDisplay.ToString());
    }
}