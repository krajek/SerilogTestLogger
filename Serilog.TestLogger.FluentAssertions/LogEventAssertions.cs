using System;
using System.Diagnostics;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using Serilog.Events;

// ReSharper disable once CheckNamespace
namespace FluentAssertions
{
    public class LogEventAssertions : ReferenceTypeAssertions<LogEvent, LogEventAssertions>
    {
        public LogEventAssertions(LogEvent logEvent):base(logEvent)
        {
        }

        protected override string Identifier => "logEvent";

        public AndWhichConstraint<LogEventAssertions, LogEventPropertyValue> ContainPropertyWithValue(
            string propertyName,
            DateTime propertyValue,
            string because = "",
            params object[] becauseArgs)
        {
            var propertyExists = Subject.Properties.ContainsKey(propertyName);

            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(propertyExists)
                .FailWith("Expected {context:logEvent} to contain datetime property {0}{reason} but it is missing",
                    propertyName);

            var actualProperty = Subject.Properties[propertyName];
            var actualValue = ExtractDateTimeRepresentationOfLogEventPropertyValue(actualProperty);
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(actualValue.HasValue)
                .FailWith("Expected {context:logEvent} to contain datetime property {0}{reason} but it is not a datetime",
                    propertyName);

            // ReSharper disable once PossibleInvalidOperationException
            var valueMatches = actualValue.Value.Equals(propertyValue);

            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(valueMatches)
                .FailWith("Expected {context:logEvent} to contain datetime property {0} with value {1}{reason} but instead {2} was received",
                    propertyName, propertyValue, actualValue);

            return new AndWhichConstraint<LogEventAssertions, LogEventPropertyValue>(this, actualProperty);
        }

        public AndWhichConstraint<LogEventAssertions, LogEventPropertyValue> ContainPropertyWithValue(
            string propertyName,
            Guid propertyValue,
            string because = "",
            params object[] becauseArgs)
        {
            return ContainPropertyWithValue(propertyName, propertyValue.ToString(), because, becauseArgs);
        }

        public AndWhichConstraint<LogEventAssertions, LogEventPropertyValue> ContainPropertyWithValue(
            string propertyName,
            string propertyValue,
            string because = "",
            params object[] becauseArgs)
        {
            var propertyExists = Subject.Properties.ContainsKey(propertyName);

            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(propertyExists)
                .FailWith("Expected {context:logEvent} to contain property {0}{reason} but it is missing",
                    propertyName);

            var actualProperty = Subject.Properties[propertyName];
            var actualValue = ExtractStringRepresentationOfLogEventPropertyValue(actualProperty);
            var valueMatches = actualValue.Equals(propertyValue, StringComparison.InvariantCultureIgnoreCase);

            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(valueMatches)
                .FailWith("Expected {context:logEvent} to contain property {0} with value {1}{reason} but instead {2} was received",
                    propertyName, propertyValue, actualValue);

            return new AndWhichConstraint<LogEventAssertions, LogEventPropertyValue>(this, actualProperty);
        }

        public static bool MatchesStringRepresentationOfLogEventPropertyValue(
            string expectedValue,
            LogEventPropertyValue property)
        {
            var actualValue = ExtractStringRepresentationOfLogEventPropertyValue(property);
            var valueMatches = actualValue.Equals(expectedValue, StringComparison.InvariantCultureIgnoreCase);
            return valueMatches;
        }

        public static string ExtractStringRepresentationOfLogEventPropertyValue(
            LogEventPropertyValue property)
        {
            // We don't want to decorate strings we additional
            // unnecessary quotes, so we need to extract its value directly
            if (property is ScalarValue sv)
            {
                if (sv.Value is string s)
                {
                    return s;
                }
            }
            
            var actualValue = property.ToString();
            return actualValue;
        }

        public static DateTime? ExtractDateTimeRepresentationOfLogEventPropertyValue(
            LogEventPropertyValue property)
        {
            // We don't want to decorate strings we additional
            // unnecessary quotes, so we need to extract its value directly
            if (property is ScalarValue sv)
            {
                if (sv.Value is DateTime dt)
                {
                    return dt;
                }
            }

            return null;
        }

        public AndWhichConstraint<LogEventAssertions, LogEventPropertyValue> ContainProperty(
            string propertyName,
            string because = "",
            params object[] becauseArgs)
        {
            var propertyExists = Subject.Properties.ContainsKey(propertyName);

            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(propertyExists)
                .FailWith("Expected {context:logEvent} to contain property {0}{reason} but it is missing",
                    propertyName);

            var property = Subject.Properties[propertyName];

            return new AndWhichConstraint<LogEventAssertions, LogEventPropertyValue>(this, property);
        }

    }
}
