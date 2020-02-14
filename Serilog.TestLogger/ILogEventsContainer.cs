using System.Collections.Generic;
using Serilog.Events;

namespace Serilog.TestLogger
{
    public interface ILogEventsContainer
    {
        IList<LogEvent> Events { get; }
    }
}