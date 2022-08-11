using System;
using System.Collections.Generic;
using Serilog.Core;
using Serilog.Events;

#pragma warning disable Serilog004 // Constant MessageTemplate verifier
// ReSharper disable once CheckNamespace
namespace Serilog.TestLogger
{
    public class SerilogTestLogger : ILogger, ILogEventsContainer, IDisposable
    {
        private readonly SerilogTestSink testSink = new SerilogTestSink();
        private readonly Logger innerLogger;

        public SerilogTestLogger(Func<LoggerConfiguration,LoggerConfiguration> loggerConfiguration = default)
        {
            innerLogger = (loggerConfiguration?.Invoke(new LoggerConfiguration()) ?? new LoggerConfiguration())
                .Enrich.FromLogContext()
                .WriteTo.Sink(testSink).CreateLogger();
        }

        public IList<LogEvent> Events => testSink.Events;

        public ILogger ForContext(ILogEventEnricher enricher)
        {
            return innerLogger.ForContext(enricher);
        }

        public ILogger ForContext(IEnumerable<ILogEventEnricher> enrichers)
        {
            return innerLogger.ForContext(enrichers);
        }

        public ILogger ForContext(string propertyName, object value, bool destructureObjects = false)
        {
            return innerLogger.ForContext(propertyName, value, destructureObjects);
        }

        public ILogger ForContext<TSource>()
        {
            return innerLogger.ForContext<TSource>();
        }

        public ILogger ForContext(Type source)
        {
            return innerLogger.ForContext(source);
        }

        public void Write(LogEvent logEvent)
        {
            innerLogger.Write(logEvent);
        }

        public void Write(LogEventLevel level, string messageTemplate)
        {
            innerLogger.Write(level, messageTemplate);
        }

        public void Write<T>(LogEventLevel level, string messageTemplate, T propertyValue)
        {
            innerLogger.Write(level, messageTemplate, propertyValue);
        }

        public void Write<T0, T1>(LogEventLevel level, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            innerLogger.Write(level, messageTemplate, propertyValue0, propertyValue1);
        }

        public void Write<T0, T1, T2>(LogEventLevel level, string messageTemplate, T0 propertyValue0, T1 propertyValue1,
            T2 propertyValue2)
        {
            innerLogger.Write(level, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Write(LogEventLevel level, string messageTemplate, params object[] propertyValues)
        {
            innerLogger.Write(level, messageTemplate, propertyValues);
        }

        public void Write(LogEventLevel level, Exception exception, string messageTemplate)
        {
            innerLogger.Write(level, exception, messageTemplate);
        }

        public void Write<T>(LogEventLevel level, Exception exception, string messageTemplate, T propertyValue)
        {
            innerLogger.Write(level, exception, messageTemplate, propertyValue);
        }

        public void Write<T0, T1>(LogEventLevel level, Exception exception, string messageTemplate, T0 propertyValue0,
            T1 propertyValue1)
        {
            innerLogger.Write(level, exception, messageTemplate, propertyValue0, propertyValue1);
        }

        public void Write<T0, T1, T2>(LogEventLevel level, Exception exception, string messageTemplate, T0 propertyValue0,
            T1 propertyValue1, T2 propertyValue2)
        {
            innerLogger.Write(level, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Write(LogEventLevel level, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            innerLogger.Write(level, exception, messageTemplate, propertyValues);
        }

        public bool IsEnabled(LogEventLevel level)
        {
            return innerLogger.IsEnabled(level);
        }

        public void Verbose(string messageTemplate)
        {
            innerLogger.Verbose(messageTemplate);
        }

        public void Verbose<T>(string messageTemplate, T propertyValue)
        {
            innerLogger.Verbose(messageTemplate, propertyValue);
        }

        public void Verbose<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            innerLogger.Verbose(messageTemplate, propertyValue0, propertyValue1);
        }

        public void Verbose<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            innerLogger.Verbose(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Verbose(string messageTemplate, params object[] propertyValues)
        {
            innerLogger.Verbose(messageTemplate, propertyValues);
        }

        public void Verbose(Exception exception, string messageTemplate)
        {
            innerLogger.Verbose(exception, messageTemplate);
        }

        public void Verbose<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            innerLogger.Verbose(exception, messageTemplate, propertyValue);
        }

        public void Verbose<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            innerLogger.Verbose(exception, messageTemplate, propertyValue0, propertyValue1);
        }

        public void Verbose<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1,
            T2 propertyValue2)
        {
            innerLogger.Verbose(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Verbose(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            innerLogger.Verbose(exception, messageTemplate, propertyValues);
        }

        // ---------------------------------------

        public void Debug(string messageTemplate)
        {
            innerLogger.Debug(messageTemplate);
        }

        public void Debug<T>(string messageTemplate, T propertyValue)
        {
            innerLogger.Debug(messageTemplate, propertyValue);
        }

        public void Debug<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            innerLogger.Debug(messageTemplate, propertyValue0, propertyValue1);
        }

        public void Debug<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            innerLogger.Debug(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Debug(string messageTemplate, params object[] propertyValues)
        {
            innerLogger.Debug(messageTemplate, propertyValues);
        }

        public void Debug(Exception exception, string messageTemplate)
        {
            innerLogger.Debug(exception, messageTemplate);
        }

        public void Debug<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            innerLogger.Debug(exception, messageTemplate, propertyValue);
        }

        public void Debug<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            innerLogger.Debug(exception, messageTemplate, propertyValue0, propertyValue1);
        }

        public void Debug<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1,
            T2 propertyValue2)
        {
            innerLogger.Debug(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Debug(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            innerLogger.Debug(exception, messageTemplate, propertyValues);
        }

        // -----

        public void Information(string messageTemplate)
        {
            innerLogger.Information(messageTemplate);
        }

        public void Information<T>(string messageTemplate, T propertyValue)
        {
            innerLogger.Information(messageTemplate, propertyValue);
        }

        public void Information<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            innerLogger.Information(messageTemplate, propertyValue0, propertyValue1);
        }

        public void Information<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            innerLogger.Information(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Information(string messageTemplate, params object[] propertyValues)
        {
            innerLogger.Information(messageTemplate, propertyValues);
        }

        public void Information(Exception exception, string messageTemplate)
        {
            innerLogger.Information(exception, messageTemplate);
        }

        public void Information<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            innerLogger.Information(exception, messageTemplate, propertyValue);
        }

        public void Information<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            innerLogger.Information(exception, messageTemplate, propertyValue0, propertyValue1);
        }

        public void Information<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1,
            T2 propertyValue2)
        {
            innerLogger.Information(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Information(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            innerLogger.Information(exception, messageTemplate, propertyValues);
        }

        // ----

        public void Warning(string messageTemplate)
        {
            innerLogger.Warning(messageTemplate);
        }

        public void Warning<T>(string messageTemplate, T propertyValue)
        {
            innerLogger.Warning(messageTemplate, propertyValue);
        }

        public void Warning<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            innerLogger.Warning(messageTemplate, propertyValue0, propertyValue1);
        }

        public void Warning<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            innerLogger.Warning(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Warning(string messageTemplate, params object[] propertyValues)
        {
            innerLogger.Warning(messageTemplate, propertyValues);
        }

        public void Warning(Exception exception, string messageTemplate)
        {
            innerLogger.Warning(exception, messageTemplate);
        }

        public void Warning<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            innerLogger.Warning(exception, messageTemplate, propertyValue);
        }

        public void Warning<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            innerLogger.Warning(exception, messageTemplate, propertyValue0, propertyValue1);
        }

        public void Warning<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1,
            T2 propertyValue2)
        {
            innerLogger.Warning(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Warning(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            innerLogger.Warning(exception, messageTemplate, propertyValues);
        }

        // -----

        public void Error(string messageTemplate)
        {
            innerLogger.Error(messageTemplate);
        }

        public void Error<T>(string messageTemplate, T propertyValue)
        {
            innerLogger.Error(messageTemplate, propertyValue);
        }

        public void Error<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            innerLogger.Error(messageTemplate, propertyValue0, propertyValue1);
        }

        public void Error<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            innerLogger.Error(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Error(string messageTemplate, params object[] propertyValues)
        {
            innerLogger.Error(messageTemplate, propertyValues);
        }

        public void Error(Exception exception, string messageTemplate)
        {
            innerLogger.Error(exception, messageTemplate);
        }

        public void Error<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            innerLogger.Error(exception, messageTemplate, propertyValue);
        }

        public void Error<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            innerLogger.Error(exception, messageTemplate, propertyValue0, propertyValue1);
        }

        public void Error<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1,
            T2 propertyValue2)
        {
            innerLogger.Error(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Error(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            innerLogger.Error(exception, messageTemplate, propertyValues);
        }

        // ----

        public void Fatal(string messageTemplate)
        {
            innerLogger.Fatal(messageTemplate);
        }

        public void Fatal<T>(string messageTemplate, T propertyValue)
        {
            innerLogger.Fatal(messageTemplate, propertyValue);
        }

        public void Fatal<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            innerLogger.Fatal(messageTemplate, propertyValue0, propertyValue1);
        }

        public void Fatal<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            innerLogger.Fatal(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Fatal(string messageTemplate, params object[] propertyValues)
        {
            innerLogger.Fatal(messageTemplate, propertyValues);
        }

        public void Fatal(Exception exception, string messageTemplate)
        {
            innerLogger.Fatal(exception, messageTemplate);
        }

        public void Fatal<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            innerLogger.Fatal(exception, messageTemplate, propertyValue);
        }

        public void Fatal<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            innerLogger.Fatal(exception, messageTemplate, propertyValue0, propertyValue1);
        }

        public void Fatal<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1,
            T2 propertyValue2)
        {
            innerLogger.Fatal(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
        {

            innerLogger.Fatal(exception, messageTemplate, propertyValues);
        }

        public bool BindMessageTemplate(string messageTemplate, object[] propertyValues, out MessageTemplate parsedTemplate,
            out IEnumerable<LogEventProperty> boundProperties)
        {
            return innerLogger.BindMessageTemplate(messageTemplate, propertyValues, out parsedTemplate, out boundProperties);
        }

        public bool BindProperty(string propertyName, object value, bool destructureObjects, out LogEventProperty property)
        {
            return innerLogger.BindProperty(propertyName, value, destructureObjects, out property);
        }

        public void Dispose()
        {
            innerLogger?.Dispose();
        }
    }
}
#pragma warning restore Serilog004 // Constant MessageTemplate verifier