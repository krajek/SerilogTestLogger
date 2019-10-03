## Serilog.TestLogger

In-memory Serilog logger for usage in automatic tests.

![Azure DevOps builds](https://img.shields.io/azure-devops/build/arturkrajewski/Serilog.TestLogger/2)

| Serilog.TestLogger | Serilog.TestLogger.FluentAssertions |
|:--------------------:|:-------------------------------------:|
|    [![Serilog.TestLogger nuget package](https://img.shields.io/nuget/v/Serilog.TestLogger.svg)](https://www.nuget.org/packages/Serilog.TestLogger/)                | [![Serilog.TestLogger.FluentAssertions nuget package](https://img.shields.io/nuget/v/Serilog.TestLogger.FluentAssertions.svg)](https://www.nuget.org/packages/Serilog.TestLogger.FluentAssertions/)    

## What Does It Do?

Provides convenient way to create an instance of Serilog's `ILogger` that collects all the events and let's you examine them afterwards.
It is designed so that you can test your logging as easy as possible.

```
using Serilog.TestLogger;

// Setup tests logger
var testLogger = new SerilogTestLogger();

// Spit out some log events (or not)
logger.Error("Object added {ObjectId}", 123);

// Verify logging behaviour
Assert.AreEqual(testLogger.Events.Count, 1);
```

## Integration with `FluentAssertions`

Logger's `Events` property exposes a `IList<LogEvents>` so obviously having full access to the events allows you to use any kind of assertions you like. 

However, you may find built-in integration with `FluentAssertions` very useful, especially if you already use that package for your assertions.

```
logger.Should().HaveNotLoggedAnyError();

logger.Should().HaveLoggedErrorContaining("MSG")
  .Which.Should().ContainPropertyWithValue("ObjectId", "123");
```


