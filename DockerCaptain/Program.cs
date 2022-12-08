using Cocona;
using Cocona.Hosting;
using DockerCaptain.Commands;
using DockerCaptain.Core.Extensions;
using DockerCaptain.Data.Extensions;
using DockerCaptain.Logging;
using DockerCaptain.PlatformCore.Exceptions;
using DockerCaptain.PlatformCore.Extensions;
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
            logging.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.Warning);

            logging.ClearProviders().AddFileLogger(configuration =>
            {
                configuration.LogPath = Path.Combine(applicationFolderPath, "logs");
            });

#if DEBUG
            logging.AddDebug();
#endif
        });

        builder.ConfigureServices(services =>
        {
            try
            {
                // Platform
                services.AddPlatform();
            }
            catch (InvalidOperationException err)
            {
                Console.WriteLine($"ERROR: {err.Message}");
                return;
            }
            catch (NotSupportedPlatformException err)
            {
                // current platform isn't supported
                Console.WriteLine($"ERROR: {err.Message}");
                return;
            }
            catch (Exception err)
            {
                Console.WriteLine($"ERROR: {err.Message}");
                return;
            }

            try
            {
                // Database
                services.AddDatabase(Program.ApplicationFolderPath, DATABASE_FILE_NAME);
            }
            catch (Exception err)
            {
                Console.WriteLine($"ERROR: {err.Message}");
                return;
            }

            try
            {
                // Database
                services.AddCore();
            }
            catch (Exception err)
            {
                Console.WriteLine($"ERROR: {err.Message}");
                return;
            }
        });

        builder.Run<Program>(args);
    }
}
