using System;
using Xunit;
using Xunit.Sdk;
using FluentAssertions;

namespace Serilog.TestLogger.FluentAssertions.Tests
{
    public class LogEventAssertionsTests
    {
        private readonly SerilogTestLogger logger = new SerilogTestLogger();

        [Fact]
        public void HaveLoggedErrorContaining_LogEventContainsStringValue_MatchesByIntValue()
        {
            // Arrange
            logger.Error("MSG {ObjectId}", 123);

            // Act

            Action action = () =>
                logger.Should().HaveLoggedErrorContaining("MSG")
                    .Which.Should().ContainPropertyWithValue("ObjectId", "123");

            // Assert
            action.Should().NotThrow();
        }

        [Fact]
        public void HaveLoggedErrorContaining_LogEventContainsStringValue_MatchesByName()
        {
            // Arrange
            logger.Error("MSG {ObjectId}", "123");

            // Act
            Action action = () => 
                logger.Should().HaveLoggedErrorContaining("MSG")
                .Which.Should().ContainPropertyWithValue("ObjectId", "123");
            
            // Assert
            action.Should().NotThrow();
        }

        [Fact]
        public void HaveLoggedErrorContaining_LogEventContainsGuid_MatchesBySerializedGuid()
        {
            // Arrange
            var propertyValue = Guid.Parse("8e1e1358-5095-4730-8e34-bb98913c13c2");
            logger.Error("MSG {ObjectId}", propertyValue);

            // Act
            Action action = () =>
                logger.Should().HaveLoggedErrorContaining("MSG")
                    .Which.Should().ContainPropertyWithValue(
                        "ObjectId", 
                        "8e1e1358-5095-4730-8e34-bb98913c13c2");

            // Assert
            action.Should().NotThrow();
        }

        [Fact]
        public void HaveNotLoggedAnyError_WhenErrorIsLogged_ThrowsAndProvidesErrorMessage()
        {
            // Arrange
            logger.Error("MSG {ObjectId}", 12);

            // Act
            Action action = () => logger.Should().HaveNotLoggedAnyError();

            // Assert
            action.Should().Throw<XunitException>()
                .WithMessage("*contained 1*MSG 12*");
        }

        [Fact]
        public void HaveNotLoggedAnyError_WhenInfoIsLogged_DoesNotThrow()
        {
            // Arrange
            logger.Information("MSG");

            // Act
            Action action = () => logger.Should().HaveNotLoggedAnyError();

            // Assert
            action.Should().NotThrow();
        }
    }
}
