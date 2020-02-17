using System;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using Serilog.Events;

// ReSharper disable once CheckNamespace
namespace FluentAssertions
{
    public class LogEventAssertions : ReferenceTypeAssertions<LogEvent, LogEventAssertions>
    {
        public LogEventAssertions(LogEvent logEvent)
        {
            Subject = logEvent;
        }

        protected override string Identifier => "logEvent";

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

            var property = Subject.Properties[propertyName];
            var actualValue = ExtractStringRepresentationOfLogEventPropertyValue(property);
            var valueMatches = actualValue.Equals(propertyValue, StringComparison.InvariantCultureIgnoreCase);

            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(valueMatches)
                .FailWith("Expected {context:logEvent} to contain property {0} with value {1}{reason} but instead {2} was received",
                    propertyName, propertyValue, actualValue);

            return new AndWhichConstraint<LogEventAssertions, LogEventPropertyValue>(this, property);
        }

        private static string ExtractStringRepresentationOfLogEventPropertyValue(
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
