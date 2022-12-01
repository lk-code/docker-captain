using DockerCaptain.Core.Interfaces;
using DockerCaptain.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DockerCaptain.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddSingleton<IImageService, ImageService>();

        return services;
    }
}
