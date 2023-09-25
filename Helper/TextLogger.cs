namespace ForzaDynamicMapApi.Helper;

public class TextLogger : ILogger
{
    public IDisposable BeginScope<TState>(TState state)
    {
        throw new NotImplementedException();
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return (int)logLevel > (int)LogLevel.Information;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        Console.WriteLine($"{DateTime.Now.ToString("u")} : {logLevel} - {exception?.Message}");
    }
}
