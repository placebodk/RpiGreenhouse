using Microsoft.Extensions.Logging;

namespace RpiGreenhouse.Elverstien;

class LoggerFactory : ILoggerFactory
{
    /// <inheritdoc />
    public void Dispose()
    {
    }

    /// <inheritdoc />
    public ILogger CreateLogger(string categoryName)
    {
        return new ConsoleLogger(categoryName);
    }

    /// <inheritdoc />
    public void AddProvider(ILoggerProvider provider)
    {
        throw new NotImplementedException();
    }

    class ConsoleLogger : ILogger
    {
        private readonly string _name;

        public ConsoleLogger(string name)
        {
            _name = name;
        }

        /// <inheritdoc />
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {

            Console.Write($"{DateTime.Now:HH:mm:ss.fff}");
            Console.Write($"[{LogLevelToString(logLevel)}]");
            Console.WriteLine(formatter.Invoke(state, exception));
        }

        /// <inheritdoc />
        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        /// <inheritdoc />
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            throw new NotImplementedException();
        }

        private string LogLevelToString(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Critical:
                    return "Critical";
                case LogLevel.Error:
                    return "Error";
                case LogLevel.Warning:
                    return "Warning";
                case LogLevel.Information:
                    return "Info";
                case LogLevel.Debug:
                    return "Debug";
                case LogLevel.Trace:
                    return "Trace";
                default:
                    return "None";
            }
        }
    }
}