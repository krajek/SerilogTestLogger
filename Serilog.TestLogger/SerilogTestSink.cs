﻿using System.Collections.Generic;
using Serilog.Core;
using Serilog.Events;

// ReSharper disable once CheckNamespace
namespace Serilog.TestLogger
{
    public class SerilogTestSink : ILogEventSink, ILogEventsContainer
    {
        public IList<LogEvent> Events { get; } = new List<LogEvent>();

        public void Emit(LogEvent logEvent)
        {
            Events.Add(logEvent);
        }
    }
}