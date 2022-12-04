using Cocona;
using DockerCaptain.Data.Interfaces;
using System;
using System.Diagnostics;

namespace DockerCaptain.Commands;

public class Images
{
    private readonly IImageRepository _imageRepository;

    public Images(IImageRepository imageRepository)
    {
        this._imageRepository = imageRepository ?? throw new ArgumentNullException(nameof(imageRepository));
    }

    [Command("register")]
    public async Task Register([Argument(Description = "name of the docker image")] string name)
    {
        var image = await this._imageRepository.GetImageByName(name, CancellationToken.None);

        if (image != null)
        {
            // image already registered

            Console.WriteLine($"image {name} already registered!");

            return;
        }

        var vars = Environment.GetEnvironmentVariables();

        // get docker information
        Process process = new Process();
        // Redirect the output stream of the child process.
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.FileName = "cmd.exe";
        process.StartInfo.Arguments = $"docker image inspect {name} & exit";

        process.Start();

        var output = new List<string>();

        while (process.StandardOutput.Peek() > -1)
        {
            string str = process.StandardOutput.ReadLine();

            Console.WriteLine(str);

            output.Add(str);
        }

        while (process.StandardError.Peek() > -1)
        {
            string str = process.StandardError.ReadLine();

            Console.WriteLine("ERROR: " + str);

            output.Add(str);
        }
        process.WaitForExit();

        // Do not wait for the child process to exit before
        // reading to the end of its redirected stream.
        // p.WaitForExit();
        // Read the output stream first and then wait.

        int i = 0;

        // save in db
    }
}
