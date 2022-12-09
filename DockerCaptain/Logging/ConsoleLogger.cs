using Microsoft.Extensions.Logging;

namespace DockerCaptain.Logging;

public sealed class ConsoleLogger : ILogger
{
    private readonly string _name;
    private readonly Func<ConsoleLoggerConfiguration> _getCurrentConfig;

    public ConsoleLogger(
        string name,
        Func<ConsoleLoggerConfiguration> getCurrentConfig)
    {
        _name = name;
        _getCurrentConfig = getCurrentConfig;
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

        ConsoleLoggerConfiguration config = _getCurrentConfig();
        if (config.EventId == 0 || config.EventId == eventId.Id)
        {
            string type = $"{logLevel,-12}".ToUpper();

            Console.WriteLine($"{DateTime.Now.ToLongTimeString()} - {type}: {formatter(state, exception)}");

            if (exception != null)
            {
                if (config.HideStackTrace != true)
                {
                    Console.WriteLine(exception.GetType());
                    Console.WriteLine(exception.StackTrace);
                }
            }
        }
    }
}