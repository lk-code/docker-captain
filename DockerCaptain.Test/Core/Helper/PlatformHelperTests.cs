using DockerCaptain.PlatformCore;
using DockerCaptain.PlatformCore.Helper;
using FluentAssertions;

namespace DockerCaptain.Test.Core.Helper;

[TestClass]
public class PlatformHelperTests
{
    public PlatformHelperTests()
    {

    }

    [TestMethod]
    public void ToPlatform_WithDifferentIdentifiers_ReturnsCorrectData()
    {
        Dictionary<string, Platform> testData = new()
        {
            { "win11-x64", new ("win11", "x64") },
            { "win11-arm64", new ("win11", "arm64") },

            { "win-10-x64", new ("win-10", "x64") },
            { "win-10-x86", new ("win-10", "x86") },
            { "win-10-arm", new ("win-10", "arm") },
            { "win-10-arm64", new ("win-10", "arm64") },

            { "osx.12-x64", new ("osx.12", "x64") },
            { "osx.12-x86", new ("osx.12", "x86") },
            { "osx.12-arm64", new ("osx.12", "arm64") },

            { "opensuse-leap.15.4-x64", new ("opensuse-leap.15.4", "x64") },
            { "ubuntu.22.04-x64", new ("ubuntu.22.04", "x64") },
            { "debian.11-x64", new ("debian.11", "x64") }
        };

        foreach (var testDataEntry in testData)
        {
            Platform platformResult = testDataEntry.Key.ToPlatform();

            platformResult.Should().NotBeNull();
            platformResult.OSIdentifier.Should().BeEquivalentTo(testDataEntry.Value.OSIdentifier);
            platformResult.OSArchitecture.Should().BeEquivalentTo(testDataEntry.Value.OSArchitecture);
            platformResult.OSName.Should().BeEquivalentTo(testDataEntry.Value.OSName);
        }
    }

    [TestMethod]
    public void AddOSType_WithDefaultOs_StoredCorrectOs()
    {
        Platform testPlatform = new("test", "x64");
        Dictionary<string, string> testData = new()
        {
            { "Linux 5.15.74.2-microsoft-standard-WSL2 #1 SMP Wed Nov 2 19:50:29 UTC 2022", "linux" },
            { "Linux 5.15.0-53-generic #59-Ubuntu SMP Mon Oct 17 18:53:30 UTC 2022", "linux" },
            { "Microsoft Windows 10.0.22623", "windows" }
        };

        foreach (var testDataEntry in testData)
        {
            testPlatform.AddOSType(testDataEntry.Key);

            testPlatform.Should().NotBeNull();
            testPlatform.OSType.Should().NotBeNull();
            testPlatform.OSType.Should().BeEquivalentTo(testDataEntry.Value);
        }
    }
}