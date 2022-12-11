using DockerCaptain.Data.Interfaces;
using DockerCaptain.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DockerCaptain.Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services,
            string localStoragePath,
            string databaseFileName)
    {
        string dbpath = Path.Combine(localStoragePath, databaseFileName);
        DataContext.DatabasePath = dbpath;
        Console.WriteLine($"set database path to {dbpath}");

        services.AddDbContext<DataContext>();

        services.AddSingleton<IImageRepository, ImageRepository>();
        services.AddSingleton<IContainerRepository, ContainerRepository>();

        return services;
    }
}