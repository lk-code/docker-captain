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

        services.AddDbContext<DataContext>();

        return services;
    }
}