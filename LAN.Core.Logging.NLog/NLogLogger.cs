using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using NLog;

namespace LAN.Core.Logging.NLog
{
	public class NLogLogger : ILogger
	{
		private readonly List<Logger> _loggers = new List<Logger>();

		public NLogLogger()
		{
			if (!LogManager.Configuration.LoggingRules.Any())
			{
				throw new ConfigurationErrorsException("There are no NLog loggers configured, please configure at least one logger for logging.");
			}
			foreach (var loggingRule in LogManager.Configuration.LoggingRules)
			{
				var logger = LogManager.GetLogger(loggingRule.LoggerNamePattern);
				_loggers.Add(logger);
			}
		}

		public void WriteLog(ILog log)
		{
			var message = new StringBuilder();
			message.AppendLine("{");
			message.AppendLine("Level: " + log.LogLevel + ",");
			message.AppendLine("Utc: " + log.LogCreatedAtUtc + ",");

			var sections = log.GetSections().ToList();
			var indexOfLastSection = sections.Count() - 1;
			foreach (var section in sections)
			{
				var logLine = section.Key + ": " + section.Value;
				if (sections.IndexOf(section) != indexOfLastSection) logLine += ",";
				message.AppendLine(logLine);
			}
			message.AppendLine("}");

			switch (log.LogLevel)
			{
				case LogLevels.Trace:
					_loggers.ForEach(x => x.Trace(message.ToString()));
					break;
				case LogLevels.Warn:
					_loggers.ForEach(x => x.Warn(message.ToString()));
					break;
				case LogLevels.Error:
					_loggers.ForEach(x => x.Error(message.ToString()));
					break;
				default:
					_loggers.ForEach(x => x.Info(message.ToString()));
					break;
			}
		}
	}
}