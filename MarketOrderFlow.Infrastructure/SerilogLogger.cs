using MarketOrderFlow.Domain.Concracts;
using Serilog;
using ILogger = Serilog.ILogger;

namespace MarketOrderFlow.Infrastructure;

public class SerilogLogger<T> : IAppLogger<T>
{
    private readonly ILogger _logger;

    public SerilogLogger()
    {
        _logger = Log.ForContext<T>();
    }

    public void LogInformation(string message, params object[] args)
    {
        _logger.Information(message, args);
    }

    public void LogWarning(string message, params object[] args)
    {
        _logger.Warning(message, args);
    }

    public void LogError(string message, params object[] args)
    {
        _logger.Error(message, args);
    }
}
