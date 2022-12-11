using System;
using Microsoft.Extensions.Logging;

namespace DockerCaptain.Logging;

public sealed class FileLogger : ILogger
{
    private readonly string _name;
    private readonly Func<FileLoggerConfiguration> _getCurrentConfig;

    public FileLogger(
        string name,
        Func<FileLoggerConfiguration> getCurrentConfig)
    {
        _name = name;
        _getCurrentConfig = getCurrentConfig;

        // ensure log-directory
        FileLoggerConfiguration config = _getCurrentConfig();
        if (!string.IsNullOrEmpty(config.LogPath))
        {
            Directory.CreateDirectory(config.LogPath);
        }
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => default!;

    public bool IsEnabled(LogLevel logLevel) => true;

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        FileLoggerConfiguration config = _getCurrentConfig();
        if (config.EventId == 0 || config.EventId == eventId.Id)
        {
            string logPath = "";
            if (!string.IsNullOrEmpty(config.LogPath))
            {
                logPath = config.LogPath;
            }

            string logFile = Path.Combine(logPath, $"log_{DateTime.Now.ToString("HH-mm-ss")}.txt");

            string type = $"{logLevel,-12}".ToUpper();

            File.AppendAllText(logFile, $"{DateTime.Now.ToLongTimeString()} - {type}: {formatter(state, exception)}{Environment.NewLine}");

            if (exception != null)
            {
                File.AppendAllText(logFile, $"{exception.GetType()}{Environment.NewLine}");
                File.AppendAllText(logFile, $"{exception.Message}{Environment.NewLine}");
                File.AppendAllText(logFile, $"{exception.StackTrace}{Environment.NewLine}");
            }
        }
    }
}