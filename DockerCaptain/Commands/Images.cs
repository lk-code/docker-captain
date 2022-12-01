using Cocona;
using DockerCaptain.Data;
using DockerCaptain.PlatformCore;

namespace DockerCaptain.Commands;

public class Images
{
    private readonly IPlatform _platform;
    private readonly DataContext dataContext;

    public Images(IPlatform platform,
        DataContext dataContext)
    {
        this._platform = platform ?? throw new ArgumentNullException(nameof(platform));
        this.dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
    }

    [Command("register")]
    public async Task Register([Argument(Description = "name of the docker image")] string dockerImage)
    {
        int i = 0;
        Console.WriteLine(this._platform.ToString());
    }
}
