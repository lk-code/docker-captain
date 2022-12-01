using Cocona;
using Cocona.Builder;
using DockerCaptain.Commands;
using DockerCaptain.Core.Extensions;
using DockerCaptain.Data.Extensions;
using DockerCaptain.PlatformCore;
using DockerCaptain.PlatformCore.Exceptions;
using DockerCaptain.PlatformCore.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace DockerCaptain;

[HasSubCommands(typeof(Images), Description = "work with docker images")]
public class Program
{
    private const string DATABASE_FILE_NAME = "dockercaptain.db";
    private const string APP_FOLDER_NAME = "docker-captain";
    public static string ApplicationFolderPath = "";

    static void Main(string[] args)
    {
        CoconaAppBuilder? builder = CoconaApp.CreateBuilder();

        //builder.Services.TryAddSingleton<IHtmlRenderer, HtmlRenderer>();

        // Database
        //builder.Services.AddDatabase(ApplicationData.Current.LocalFolder.Path,
        //    DATABASE_FILE_NAME);

        var hostBuilder = CoconaApp.CreateHostBuilder();

        //hostBuilder.ConfigureLogging(logging =>
        //        {
        //            logging.AddDebug();
        //        });

        hostBuilder.ConfigureServices(services =>
        {
            try
            {
                // Platform
                services.AddPlatform();

                IPlatform platform = services.BuildServiceProvider().GetService<IPlatform>()!;

                // create application folder
                string applicationFolderPath = Path.Combine(platform.ApplicationDirectory, APP_FOLDER_NAME);
                Console.WriteLine($"set application directory to '{applicationFolderPath}'...");
                Directory.CreateDirectory(applicationFolderPath);
                Console.WriteLine("DONE");

                Program.ApplicationFolderPath = applicationFolderPath;
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

        hostBuilder.Run<Program>(args, options =>
        {
            options.EnableShellCompletionSupport = true;
        });
    }
}
