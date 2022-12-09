using Cocona;
using Cocona.Hosting;
using DockerCaptain.Commands;
using DockerCaptain.Core.Extensions;
using DockerCaptain.Data.Extensions;
using DockerCaptain.Logging;
using DockerCaptain.PlatformCore.Exceptions;
using DockerCaptain.PlatformCore.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using static System.Environment;

namespace DockerCaptain;

[HasSubCommands(typeof(Images), Description = "work with docker images")]
[HasSubCommands(typeof(Info), Description = "info about this app")]
public class Program : CoconaConsoleAppBase
{
    private const string DATABASE_FILE_NAME = "dockercaptain.db";
    private const string APP_FOLDER_NAME = "docker-captain";
    public static string ApplicationFolderPath = "";

    static void Main(string[] args)
    {
        CoconaAppHostBuilder? builder = CoconaApp.CreateHostBuilder();

        #region set application directory path

        // set application directory like: 
        // Windows      C:\Users\larsk\AppData\Roaming\docker-captain
        // Ubuntu       /home/lkraemer/.config/docker-captain
        // Debian       /home/lkraemer/.config/docker-captain
        // openSuse     /home/lkraemer/.config/docker-captain
        // osx          /Users/larskramer/.config/docker-captain
        string applicationFolderPath = Path.Combine(Environment.GetFolderPath(SpecialFolder.ApplicationData, SpecialFolderOption.DoNotVerify), APP_FOLDER_NAME);

        // ensure application directory
        Directory.CreateDirectory(applicationFolderPath);
        // set application directory
        Program.ApplicationFolderPath = applicationFolderPath;

        #endregion

        builder.ConfigureLogging(logging =>
        {
            // disable ef core output
            logging.ClearProviders()
            .AddConsoleLogger()
            .AddFileLogger(configuration =>
            {
                configuration.LogPath = Path.Combine(applicationFolderPath, "logs");
            });

#if DEBUG
            logging.AddDebug();
#endif
        });

        builder.ConfigureServices(services =>
        {

            ILogger logger = services.BuildServiceProvider().GetService<ILogger<Program>>()!;

            logger.LogTrace("----------");
            logger.LogTrace($"application-directory: {Program.ApplicationFolderPath}");

            try
            {
                // Platform
                services.AddPlatform();
            }
            catch (Exception err)
            {
                logger.LogError(LogEvents.ServicesAddPlatform, err, err.Message);
                return;
            }

            try
            {
                // Database
                services.AddDatabase(Program.ApplicationFolderPath, DATABASE_FILE_NAME);
            }
            catch (Exception err)
            {
                logger.LogError(LogEvents.ServicesAddDatabase, err, err.Message);
                return;
            }

            try
            {
                // Database
                services.AddCore();
            }
            catch (Exception err)
            {
                logger.LogError(LogEvents.ServicesAddCore, err, err.Message);
                return;
            }
        });

        builder.Run<Program>(args);
    }
}
