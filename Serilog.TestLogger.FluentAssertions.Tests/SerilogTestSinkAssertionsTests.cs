using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Sdk;
using FluentAssertions;
using Serilog.Events;
using Serilog.Parsing;

namespace Serilog.TestLogger.FluentAssertions.Tests
{
    public class SerilogTestSinkAssertionsTests
    {
        private readonly SerilogTestSink sink = new SerilogTestSink();

        [Fact]
        public void HaveLoggedErrorContaining_LogEventContainsStringValue_MatchesByIntValue()
        {
            // Arrange
            var logEvent = new LogEvent(
                DateTimeOffset.UtcNow, 
                LogEventLevel.Error, 
                null, 
                new MessageTemplate("ABC",new List<MessageTemplateToken>()),
                new List<LogEventProperty>());

            // Act
            sink.Emit(logEvent);
            Action action = () =>
                sink.Should().HaveLoggedErrorContaining("ABC");

            // Assert
            action.Should().NotThrow();
        }
    }
}
