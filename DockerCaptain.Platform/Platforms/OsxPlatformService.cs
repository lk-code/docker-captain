﻿using System.Diagnostics;
using System.Xml.Linq;
using static System.Environment;

namespace DockerCaptain.PlatformCore.Platforms;

public class OsxPlatformService : IPlatform
{
    private readonly string _applicationDirectory;
    public string ApplicationDirectory { get => _applicationDirectory; }

    public OsxPlatformService()
    {
        _applicationDirectory = Environment.GetFolderPath(SpecialFolder.ApplicationData, SpecialFolderOption.DoNotVerify);
    }

    public async Task<string> ExecuteShellCommandAsync(string arguments)
    {
        await Task.CompletedTask;

        Process process = new Process();
        // Redirect the output stream of the child process.
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.FileName = @"/System/Applications/Utilities/Terminal.app/Contents/MacOS/Terminal";
        //process.StartInfo.Arguments = "-c \"docker image inspect " + name + "\"";
        process.StartInfo.Arguments = "-c \"" + arguments + "\"";

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

        return "";
    }
}
