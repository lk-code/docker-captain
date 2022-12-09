using DockerCaptain.Docker.Exceptions;
using DockerCaptain.Docker.Interfaces;
using DockerCaptain.Docker.Models;
using DockerCaptain.Docker.Services;
using DockerCaptain.PlatformCore;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace DockerCaptain.Test.Core.Services;

[TestClass]
public class ImageServiceTests
{
    private Mock<IPlatform> _platform = null!;

    private IImageService _instance = null!;

    [TestInitialize]
    public void SetUp()
    {
        ServiceProvider serviceProvider = new ServiceCollection()
            .AddLogging(logging => logging.AddDebug())
            .BuildServiceProvider();

        this._platform = new Mock<IPlatform>();

        this._instance = new ImageService(serviceProvider.GetService<ILogger<ImageService>>()!, this._platform.Object);
    }

    [TestMethod]
    public async Task PullAsync_WithExistingPullResult_ReturnsCorrectData()
    {
        string pullOutput = "Using default tag: latest" + Environment.NewLine +
            "latest: Pulling from lkcode/test" + Environment.NewLine +
            "Digest: sha256:954b6e902037133938fd1884633d55cda9891061e6d5eca84a07b230e610e013" + Environment.NewLine +
            "Status: Image is up to date for lkcode/test:latest" + Environment.NewLine +
            "docker.io/lkcode/test:latest";

        this._platform
            .Setup(x => x.GetDockerExecutableAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync("/docker");

        this._platform
            .Setup(x => x.ExecuteShellCommandAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(pullOutput);

        PullResult result = await this._instance.PullAsync("lkcode/test", CancellationToken.None);

        result.Should().NotBeNull();
        result.Id.Should().Be("docker.io/lkcode/test:latest");
        result.Name.Should().Be("lkcode/test");
        result.Tag.Should().Be("latest");
        result.Digest.Should().Be("sha256:954b6e902037133938fd1884633d55cda9891061e6d5eca84a07b230e610e013");
        result.Status.Should().NotBeNull();
        result.Status.Should().Be("Image is up to date for lkcode/test:latest");
    }

    [TestMethod]
    public async Task PullAsync_WithNewPullResult_ReturnsCorrectData()
    {
        string pullOutput = "Using default tag: latest" + Environment.NewLine +
            "latest: Pulling from lkcode/test" + Environment.NewLine +
            "a603fa5e3b41: Pulling fs layer" + Environment.NewLine +
            "478909de3ddd: Pulling fs layer" + Environment.NewLine +
            "c6ca03fe2040: Pulling fs layer" + Environment.NewLine +
            "8fa56940bfcc: Pulling fs layer" + Environment.NewLine +
            "d954040416f1: Waiting" + Environment.NewLine +
            "8fa56940bfcc: Waiting" + Environment.NewLine +
            "478909de3ddd: Download complete" + Environment.NewLine +
            "d954040416f1: Verifying Checksum" + Environment.NewLine +
            "c6ca03fe2040: Verifying Checksum" + Environment.NewLine +
            "c6ca03fe2040: Download complete" + Environment.NewLine +
            "a603fa5e3b41: Pull complete" + Environment.NewLine +
            "6469da64d204: Verifying Checksum" + Environment.NewLine +
            "6469da64d204: Download complete" + Environment.NewLine +
            "478909de3ddd: Pull complete" + Environment.NewLine +
            "4f4fb700ef54: Verifying Checksum" + Environment.NewLine +
            "4f4fb700ef54: Download complete" + Environment.NewLine +
            "27c35e965fb9: Verifying Checksum" + Environment.NewLine +
            "27c35e965fb9: Download complete" + Environment.NewLine +
            "c6ca03fe2040: Pull complete" + Environment.NewLine +
            "d954040416f1: Pull complete" + Environment.NewLine +
            "4f4fb700ef54: Pull complete" + Environment.NewLine +
            "8fa56940bfcc: Download complete" + Environment.NewLine +
            "8fa56940bfcc: Pull complete" + Environment.NewLine +
            "Digest: sha256:954b6e902037133938fd1884633d55cda9891061e6d5eca84a07b230e610e013" + Environment.NewLine +
            "Status: Downloaded newer image for lkcode/test:latest" + Environment.NewLine +
            "docker.io/lkcode/test:latest";

        this._platform
            .Setup(x => x.GetDockerExecutableAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync("/docker");

        this._platform
            .Setup(x => x.ExecuteShellCommandAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(pullOutput);

        PullResult result = await this._instance.PullAsync("lkcode/test", CancellationToken.None);

        result.Should().NotBeNull();
        result.Id.Should().Be("docker.io/lkcode/test:latest");
        result.Name.Should().Be("lkcode/test");
        result.Tag.Should().Be("latest");
        result.Digest.Should().Be("sha256:954b6e902037133938fd1884633d55cda9891061e6d5eca84a07b230e610e013");
        result.Status.Should().NotBeNull();
        result.Status.Should().Be("Downloaded newer image for lkcode/test:latest");
    }

    [TestMethod]
    public async Task PullAsync_WithNonexistingPullResult_ReturnsErrorData()
    {
        string pullOutput = "Using default tag: latest" + Environment.NewLine +
            "Error response from daemon: pull access denied for lkcode/test, repository does not exist or may require 'docker login': denied: requested access to the resource is denied";

        this._platform
        .Setup(x => x.GetDockerExecutableAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync("/docker");

        this._platform
            .Setup(x => x.ExecuteShellCommandAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(pullOutput);

        bool exceptionThrown = false;
        try
        {
            await this._instance.PullAsync("lkcode/test", CancellationToken.None);
        }
        catch (DockerOperationException)
        {
            exceptionThrown = true;
        }
        exceptionThrown.Should().BeTrue();
    }
}