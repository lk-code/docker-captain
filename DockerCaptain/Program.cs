using System.Text.Json;
using Cocona;
using Cocona.Hosting;
using DockerCaptain.Commands;
using DockerCaptain.Core.Config;
using DockerCaptain.Data.Extensions;
using DockerCaptain.Docker.Extensions;
using DockerCaptain.Logging;
using DockerCaptain.PlatformCore.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using static System.Environment;

namespace DockerCaptain;

[HasSubCommands(typeof(Images), Description = "work with docker images")]
[HasSubCommands(typeof(Info), Description = "info about this app")]
public class Program : CoconaConsoleAppBase
{
    private const string DATABASE_FILE_NAME = "dockercaptain.db";
    private const string CONFIG_FILE_NAME = "config.json";
    private const string APP_FOLDER_NAME = "docker-captain";
    public static string OriginalApplicationFolderPath = "";
    public static string ApplicationFolderPath = "";
    public static UserConfiguration UserConfiguration = null!;

    static void Main(string[] args)
    {
        Console.WriteLine(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json")));

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
        Program.OriginalApplicationFolderPath = applicationFolderPath;
        Program.ApplicationFolderPath = applicationFolderPath;

        #endregion

        #region user config

        // create empty user config file
        string userConfigFile = Path.Combine(Program.ApplicationFolderPath, CONFIG_FILE_NAME);
        if (!File.Exists(userConfigFile))
        {
            File.AppendAllText(userConfigFile, "{}");
        }

        // load file
        string userConfigJson = File.ReadAllText(userConfigFile);
        Program.UserConfiguration = JsonSerializer.Deserialize<UserConfiguration>(userConfigJson)!;

        if (!string.IsNullOrEmpty(Program.UserConfiguration.AppDirectory))
        {
            string userApplicationFolderPath = Path.Combine(Program.UserConfiguration.AppDirectory, APP_FOLDER_NAME);

            // ensure application directory
            Directory.CreateDirectory(userApplicationFolderPath);
            Program.ApplicationFolderPath = userApplicationFolderPath;
        }

        #endregion

        builder.ConfigureLogging(logging =>
        {
            // disable ef core output
            logging.ClearProviders()
            //.AddConsoleLogger()
            .AddFileLogger(configuration =>
            {
                configuration.LogPath = Path.Combine(Program.ApplicationFolderPath, "logs");
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

            services.AddSingleton<UserConfiguration>(Program.UserConfiguration);

            try
            {
                // Platform Logic
                services.AddPlatform();

                if (!string.IsNullOrEmpty(Program.UserConfiguration.Docker))
                {

                }
            }
            catch (Exception err)
            {
                logger.LogError(LogEvents.ServicesAddPlatform, err, err.Message);
                return;
            }

            try
            {
                // Database Logic
                services.AddDatabase(Program.ApplicationFolderPath, DATABASE_FILE_NAME);
            }
            catch (Exception err)
            {
                logger.LogError(LogEvents.ServicesAddDatabase, err, err.Message);
                return;
            }

            try
            {
                // Docker Logic
                services.AddDocker();
            }
            catch (Exception err)
            {
                logger.LogError(LogEvents.ServicesAddDocker, err, err.Message);
                return;
            }
        });

        builder.Run<Program>(args);
    }
}
