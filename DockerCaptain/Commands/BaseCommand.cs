using System;
using Microsoft.Extensions.Logging;

namespace DockerCaptain.Commands;

public class BaseCommand
{
    protected readonly ILogger _logger;

    public BaseCommand(ILogger logger)
    {
        this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    protected void WriteError(string message)
    {
        ConsoleColor defaultColor = Console.ForegroundColor;

        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("ERROR: ");
        Console.ForegroundColor = defaultColor;

        Console.WriteLine(message);
    }

    protected void WriteWarning(string message)
    {
        ConsoleColor defaultColor = Console.ForegroundColor;

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("WARNING: ");
        Console.ForegroundColor = defaultColor;

        Console.WriteLine(message);
    }

    protected void WriteInformation(string message)
    {
        ConsoleColor defaultColor = Console.ForegroundColor;

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("INFO: ");
        Console.ForegroundColor = defaultColor;

        Console.WriteLine(message);
    }
}