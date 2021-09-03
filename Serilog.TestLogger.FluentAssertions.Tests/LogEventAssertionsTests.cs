using System;
using Xunit;
using Xunit.Sdk;
using FluentAssertions;
using Serilog.Events;

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
        public void HaveLoggedErrorContaining_TwoLogEventsWithSameTemplate_MatchesByTemplateAndProperty()
        {
            // Arrange
            logger.Error("MSG {ObjectId}", 123);
            logger.Error("MSG {ObjectId}", 124);

            // Act

            Action action = () =>
                logger.Should().HaveLoggedEventOnLevelWithMatchingTemplateAndPropertyValue(
                        LogEventLevel.Error, "MSG", "ObjectId", "124");

            // Assert
            action.Should().NotThrow();
        }

        [Fact]
        public void HaveLoggedInformationContaining_TwoLogEventsWithSameTemplate_MatchesByTemplateAndProperty()
        {
            // Arrange
            logger.Information("MSG {ObjectId}", 123);
            logger.Information("MSG {ObjectId}", 124);

            // Act

            Action action = () =>
                logger.Should().HaveLoggedInformationWithMatchingTemplateAndPropertyValue(
                    "MSG", "ObjectId", "124");

            // Assert
            action.Should().NotThrow();
        }

        [Fact]
        public void HaveLoggedErrorContaining_TwoLogEventsWithSameTemplate_DoesNotMatchByTemplateAndProperty()
        {
            // Arrange
            logger.Error("MSG {ObjectId}", 123);
            logger.Error("MSG {ObjectId}", 124);

            // Act

            Action action = () =>
                logger.Should().HaveLoggedEventOnLevelWithMatchingTemplateAndPropertyValue(
                    LogEventLevel.Error, "MSG", "ObjectId", "125");

            // Assert
            action.Should().Throw<XunitException>()
                .WithMessage("Expected logEvents to contain 1 message on level \"Error\" with message containing \"MSG\" and property \"ObjectId\"=\"125\", but it contained 0");
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
                        propertyValue);

            // Assert
            action.Should().NotThrow();
        }

        [Fact]
        public void HaveLoggedErrorContaining_LogEventContainsDateTime_MatchesByDateTime()
        {
            // Arrange
            var propertyValue = DateTime.Now;
            logger.Error("MSG {SomeDate}", propertyValue);

            // Act
            Action action = () =>
                logger.Should().HaveLoggedErrorContaining("MSG")
                    .Which.Should().ContainPropertyWithValue(
                        "SomeDate",
                        propertyValue);

            // Assert
            action.Should().NotThrow();
        }

        [Fact]
        public void HaveLoggedErrorContaining_LogEventContainsDateTime_NotMatchesByDateTime()
        {
            // Arrange
            var propertyValue = new DateTime(2020, 01, 02, 10 ,9 ,8, 7);
            logger.Error("MSG {SomeDate}", propertyValue);

            // Act
            Action action = () =>
                logger.Should().HaveLoggedErrorContaining("MSG")
                    .Which.Should().ContainPropertyWithValue(
                        "SomeDate",
                        propertyValue.AddMilliseconds(1));

            // Assert
            action.Should().Throw<XunitException>()
                .WithMessage("*to contain datetime property \"SomeDate\" with value <2020-01-02 10:09:08.008> but instead <2020-01-02 10:09:08.007> was received*");
        }

        [Fact]
        public void HaveLoggedErrorContaining_LogEventContainsDateTime_PropertyIsNotADateTime()
        {
            // Arrange
            var propertyValue = new DateTime(2020, 01, 02, 10, 9, 8, 7);
            logger.Error("MSG {SomeDate}", "SOME STRING");

            // Act
            Action action = () =>
                logger.Should().HaveLoggedErrorContaining("MSG")
                    .Which.Should().ContainPropertyWithValue(
                        "SomeDate",
                        propertyValue);

            // Assert
            action.Should().Throw<XunitException>()
                .WithMessage("*to contain datetime property \"SomeDate\" but it is not a datetime*");
        }

        [Fact]
        public void HaveLoggedErrorContaining_LogEventContainsDateTime_PropertyIsMissing()
        {
            // Arrange
            var propertyValue = new DateTime(2020, 01, 02, 10, 9, 8, 7);
            logger.Error("MSG {SomeDate2}", "SOME STRING");

            // Act
            Action action = () =>
                logger.Should().HaveLoggedErrorContaining("MSG")
                    .Which.Should().ContainPropertyWithValue(
                        "SomeDate",
                        propertyValue);

            // Assert
            action.Should().Throw<XunitException>()
                .WithMessage("*to contain datetime property \"SomeDate\" but it is missing*");
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
