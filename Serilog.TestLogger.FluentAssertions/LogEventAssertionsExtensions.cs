using Serilog.Events;

// ReSharper disable once CheckNamespace
namespace FluentAssertions
{
    public static class LogEventAssertionsExtensions
    {
        public static LogEventAssertions Should(this LogEvent instance)
        {
            return new LogEventAssertions(instance);
        }
    }
}
