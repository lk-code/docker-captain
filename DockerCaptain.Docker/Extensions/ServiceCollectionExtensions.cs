using DockerCaptain.Docker.Interfaces;
using DockerCaptain.Docker.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DockerCaptain.Docker.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDocker(this IServiceCollection services)
    {
        services.AddSingleton<IImageService, ImageService>();

        return services;
    }
}
