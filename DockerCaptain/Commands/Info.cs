using System;
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

        infoDisplay.AppendLine(FiggleFonts.Slant.Render("DockerCaptain"));
        infoDisplay.AppendLine("");
        infoDisplay.AppendLine("");
        infoDisplay.AppendLine($"Version: {this.GetType().Assembly.GetName().Version!.ToString()}");
        infoDisplay.AppendLine($"Development by lk-code - https://www.github.com/lk-code");
        infoDisplay.AppendLine($"Application Directory: {Program.ApplicationFolderPath}");
        infoDisplay.AppendLine("");
        infoDisplay.AppendLine("");
        infoDisplay.AppendLine("Docker");
        infoDisplay.AppendLine($"Executable: {await this._platform.GetDockerExecutableAsync(CancellationToken.None)}");

        Console.WriteLine(infoDisplay.ToString());
    }
}