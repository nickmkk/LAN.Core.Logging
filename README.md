# LAN.Core.Logging
Extendable Logging Abstraction

* Logging Abstraction - [Nuget](https://www.nuget.org/packages/LAN.Core.Logging/)
* NLog Logger - [Nuget](https://www.nuget.org/packages/LAN.Core.Logging.NLog/)

Creating The LogFactory
---
Construct a log factory with your loggers, a section orderer, and a log filter.  Then, register the factory with your container or hang onto it for later logging.
```c#
  var loggers = new ILogger[] { new FileLogger("application.log"), new NLogLogger() }
  var logSectionOrderer = new AsIsOrderer()
  var logFilter = new LogEverythingFilter();
  var logFactory = new LogFactory(loggers, logSectionOrderer, logFilter);
  
  container.RegisterSingleton<ILogFactory>(logFactory);
```

Performing Logging
---
You can use the provided WithMessage and WithException extensions.
```c#
    logFactory.Error()
      .WithMessage("An error occurred")
      .withException(ex)
      .Write();
```
Or you can create your own logging sections if the provided extensions don't meet your needs.
```c#
  _logFactory.Error()
    .AddLogSection(MyLogSectionNames.Message, "An error occurred")
    .AddLogSection(MyLogSectionNames.Exception, ex.ToString())
    .Write();
```
Here is an example of a custom log extension that you might add
```c#
  public static class LogExtensions
  {  
    public static ILog WithCustomer(this ILog log, string customerName, Exception ex)
    {
      log.AddLogSection(MyLogSectionNames.Customer, customerName);
      return log;
    }
  }
```
Which would be used like this
```c#
  _logFactory.Error()
    .WithCustomer(customer.name)
    .Write();
```

Creating Custom Loggers
---
You can create your own custom loggers by implementing the ILogger interface.
```c#
  public class ConsoleLogger : ILogger
  {
    public void WriteLog(ILog log)
    {
      var logMessage = new StringBuilder();
      logMessage.AppendLine("{");
      logMessage.AppendLine("Level: " + log.LogLevel + ",");
      logMessage.AppendLine("Utc: " + log.LogCreatedAtUtc + ",");
      var sectionLogs = log.GetSections().Select(section => section.Key + ": " + section.Value);
      foreach (var sectionLog in sectionLogs)
      {
        logMessage.AppendLine(sectionLog);
      }
      logMessage.AppendLine("}");

      Console.WriteLine(logMessage.ToString());
    }
  }
```

Creating Custom Section Orderers
---
Create a custom section orderer if you need to ensure that logging sections are displayed in a consistent order.
```c#
  public class ExceptionLastOrderer : ISectionOrderer
  {
    public IDictionary<string, string> Order(IDictionary<string, string> sections)
    {
      return sections
        .OrderBy(x => x.Key == WellKnownFieldNames.Exception)
        .ToDictionary(pair => pair.Key, pair => pair.Value);
    }
  }
```

Creating Custom Log Filters
---
Create a custom log filter if you need to filter out some logs, for example you could skip trace logging.
```c#
  public class SkipTraceLogsFilter : ILogFilter
  {
    public bool ShouldLog(ILog log)
    {
      return log.LogLevel != LogLevels.Trace;
    }
  }
```
