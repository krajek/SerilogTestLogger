using System;
using System.Linq;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using Serilog.Events;
using Serilog.TestLogger;

// ReSharper disable once CheckNamespace
namespace FluentAssertions
{
    public class TestLoggerAssertions : ReferenceTypeAssertions<ILogEventsContainer, TestLoggerAssertions>
    {
        public TestLoggerAssertions(ILogEventsContainer logEvents):base(logEvents)
        {
        }

        protected override string Identifier => "logEvents";

        public AndWhichConstraint<TestLoggerAssertions, LogEvent> HaveLoggedInformationContaining(
            string partOfMessage,
            string because = "",
            params object[] becauseArgs)
        {
            return HaveLoggedEventOnLevelContaining(LogEventLevel.Information, partOfMessage, because, becauseArgs);
        }

        public AndWhichConstraint<TestLoggerAssertions, LogEvent> HaveLoggedWarningContaining(
            string partOfMessage,
            string because = "",
            params object[] becauseArgs)
        {
            return HaveLoggedEventOnLevelContaining(LogEventLevel.Warning, partOfMessage, because, becauseArgs);
        }

        public AndWhichConstraint<TestLoggerAssertions, LogEvent> HaveLoggedErrorContaining(
            string partOfMessage,
            string because = "",
            params object[] becauseArgs)
        {
            return HaveLoggedEventOnLevelContaining(LogEventLevel.Error, partOfMessage, because, becauseArgs);
        }

        public AndWhichConstraint<TestLoggerAssertions, LogEvent> HaveLoggedEventOnLevelContaining(
            LogEventLevel logEventLevel,
            string partOfMessage,
            string because = "",
            params object[] becauseArgs)
        {
            return HaveLoggedEventsOnLevelContaining(logEventLevel, partOfMessage, 1, because, becauseArgs);
        }

        public AndWhichConstraint<TestLoggerAssertions, LogEvent>
            HaveLoggedInformationWithMatchingTemplateAndPropertyValue(
                string partOfMessage,
                string propertyKey,
                string propertyValue,
                string because = "",
                params object[] becauseArgs)
        {
            return HaveLoggedEventOnLevelWithMatchingTemplateAndPropertyValue(
                LogEventLevel.Information,
                partOfMessage,
                propertyKey,
                propertyValue,
                because,
                becauseArgs);
        }

        public AndWhichConstraint<TestLoggerAssertions, LogEvent>
            HaveLoggedWarningWithMatchingTemplateAndPropertyValue(
                string partOfMessage,
                string propertyKey,
                string propertyValue,
                string because = "",
                params object[] becauseArgs)
        {
            return HaveLoggedEventOnLevelWithMatchingTemplateAndPropertyValue(
                LogEventLevel.Warning,
                partOfMessage,
                propertyKey,
                propertyValue,
                because,
                becauseArgs);
        }

        public AndWhichConstraint<TestLoggerAssertions, LogEvent>
            HaveLoggedErrorWithMatchingTemplateAndPropertyValue(
                string partOfMessage,
                string propertyKey,
                string propertyValue,
                string because = "",
                params object[] becauseArgs)
        {
            return HaveLoggedEventOnLevelWithMatchingTemplateAndPropertyValue(
                LogEventLevel.Error,
                partOfMessage,
                propertyKey,
                propertyValue,
                because,
                becauseArgs);
        }

        public AndWhichConstraint<TestLoggerAssertions, LogEvent> HaveLoggedEventOnLevelWithMatchingTemplateAndPropertyValue(
            LogEventLevel logEventLevel,
            string partOfMessage,
            string propertyKey,
            string propertyValue,
            string because = "",
            params object[] becauseArgs)
        {
            var matchingLogEvents = Subject.Events
                .Where(x => 
                    x.MessageTemplate.Text.IndexOf(partOfMessage, StringComparison.InvariantCultureIgnoreCase) >= 0 && 
                    x.Level == logEventLevel && 
                    x.Properties.ContainsKey(propertyKey) &&
                    propertyValue.Equals(
                        LogEventAssertions.ExtractStringRepresentationOfLogEventPropertyValue(x.Properties[propertyKey]),
                        StringComparison.InvariantCultureIgnoreCase)
                    ).ToList();

            const int count = 1;
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(matchingLogEvents.Count == count)
                .FailWith("Expected {context:logEvents} to contain {0} message on level {1} with message containing {2}{reason} and property {3}={4}, but it contained {5}",
                    count, 
                    Enum.GetName(typeof(LogEventLevel), logEventLevel), 
                    partOfMessage, 
                    propertyKey, 
                    propertyValue, 
                    matchingLogEvents.Count);

            var matchingEvent = matchingLogEvents[0];

            return new AndWhichConstraint<TestLoggerAssertions, LogEvent>(this, matchingEvent);
        }

        public AndWhichConstraint<TestLoggerAssertions, LogEvent> HaveLoggedEventsOnLevelContaining(
            LogEventLevel logEventLevel,
            string partOfMessage,
            int count,
            string because = "",
            params object[] becauseArgs)
        {
            var matchingLogEvents = Subject.Events.Where(
                x => x.MessageTemplate.Text.IndexOf(partOfMessage, StringComparison.InvariantCultureIgnoreCase) >= 0
                     && x.Level == logEventLevel).ToList();

            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(matchingLogEvents.Count == count)
                .FailWith("Expected {context:logEvents} to contain {0} message on level {1} with message containing {2}{reason}, but it contained {3}",
                    count, logEventLevel, partOfMessage, matchingLogEvents.Count);

            var matchingEvent = matchingLogEvents[0];

            return new AndWhichConstraint<TestLoggerAssertions, LogEvent>(this, matchingEvent);
        }

        public AndConstraint<TestLoggerAssertions> HaveNotLoggedEventsOnLevel(
            LogEventLevel logEventLevel,
            string because = "",
            params object[] becauseArgs)
        {
            var matchingLogEvents = Subject.Events
                .Where(x => x.Level == logEventLevel).ToList();

            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(matchingLogEvents.Count == 0)
                .FailWith(
                    () => new FailReason(
                        "Expected {context:logEvents} to not contain any events on level {0}{reason}," +
                        " but it contained {1}. First one: \"{2}\"",
                        logEventLevel,
                        matchingLogEvents.Count,
                        matchingLogEvents[0].RenderMessage()));

            return new AndConstraint<TestLoggerAssertions>(this);
        }

        public AndConstraint<TestLoggerAssertions> HaveNotLoggedAnyWarning(
            string because = "",
            params object[] becauseArgs)
        {
            return HaveNotLoggedEventsOnLevel(LogEventLevel.Warning, because, becauseArgs);
        }

        public AndConstraint<TestLoggerAssertions> HaveNotLoggedAnyError(
            string because = "",
            params object[] becauseArgs)
        {
            return HaveNotLoggedEventsOnLevel(LogEventLevel.Error, because, becauseArgs);
        }


    }
}
