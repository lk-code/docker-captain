using System.Xml.Linq;
using DockerCaptain.Core.Exceptions;
using DockerCaptain.Core.Interfaces;
using DockerCaptain.Core.Models;
using DockerCaptain.PlatformCore;
using Microsoft.Extensions.Logging;

namespace DockerCaptain.Core.Services;

public class ImageService : IImageService
{
    private readonly ILogger<ImageService> _logger;
    private readonly IPlatform _platform;

    public ImageService(ILogger<ImageService> logger,
        IPlatform platform)
    {
        this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this._platform = platform ?? throw new ArgumentNullException(nameof(platform));
    }

    /// <inheritdoc/>
    public async Task<PullResult> PullAsync(string imageName,
        CancellationToken cancellationToken)
    {
        string dockerExecutable = await this._platform.GetDockerExecutableAsync(cancellationToken);

        string pullArguments = $"pull {imageName}";
        this._logger.LogInformation($"DOCKER: {pullArguments}");

        string pullOutput = await this._platform.ExecuteShellCommandAsync(dockerExecutable, pullArguments, cancellationToken);

        string[] lines = pullOutput
            .Split(Environment.NewLine)
            .Where(x => !string.IsNullOrEmpty(x.Trim()))
            .ToArray();

        if (lines[1].ToLowerInvariant().StartsWith("error"))
        {
            // the output is an error message!

            throw new DockerOperationException(lines[1]);
        }

        // docker id => the last line
        string dockerId = lines[lines.Count() - 1];

        // docker name => the second line
        string dockerNameLine = lines[1];
        string dockerName = dockerNameLine
            .Substring(dockerNameLine.IndexOf(':') + 1)
            .Replace("Pulling from", string.Empty)
            .Trim();

        // docker tag => the first line
        string dockerTagLine = lines[0];
        string dockerTag = dockerTagLine
            .Substring(dockerTagLine.IndexOf(':') + 1)
            .Trim();

        // docker digest => the third last line
        string dockerDigestLine = lines[lines.Count() - 3];
        string dockerDigest = dockerDigestLine
            .Substring(dockerDigestLine.IndexOf(':') + 1)
            .Trim();

        // docker pull status => the second last line
        string dockerPullStatusLine = lines[lines.Count() - 2];
        string dockerPullStatus = dockerPullStatusLine
            .Substring(dockerPullStatusLine.IndexOf(':') + 1)
            .Trim();

        PullResult pullResult = new PullResult(dockerId,
            dockerName,
            dockerTag,
            dockerDigest,
            ((!string.IsNullOrEmpty(dockerPullStatus)) ? dockerPullStatus : null));

        return pullResult;
    }
}
