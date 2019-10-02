using Serilog.TestLogger;

// ReSharper disable once CheckNamespace
namespace FluentAssertions
{
    public static class TestLoggerAssertionsExtensions
    {
        public static TestLoggerAssertions Should(this SerilogTestLogger instance)
        {
            return new TestLoggerAssertions(instance);
        }
    }
}
