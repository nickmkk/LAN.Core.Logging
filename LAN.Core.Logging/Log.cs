using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LAN.Core.Logging
{
	public class Log : ILog
	{
		private readonly IEnumerable<ILogger> _loggers;
		private readonly ISectionOrderer _sectionOrderer;
		private readonly ILogFilter _filter;
		private IDictionary<string, string> _sections;

		public IEnumerable<KeyValuePair<string, string>> GetSections()
		{
			return _sections.ToDictionary(x => x.Key, x => x.Value);
		}

		public string LogLevel { get; private set; }
		public DateTime LogCreatedAtUtc { get; private set; }

		public Log(IEnumerable<ILogger> loggers, ISectionOrderer sectionOrderer, ILogFilter filter, string logLevel)
		{
			this._loggers = loggers;
			this._sectionOrderer = sectionOrderer;
			this._filter = filter;
			this._sections = new Dictionary<string, string>();

			this.LogCreatedAtUtc = DateTime.UtcNow;
			this.LogLevel = logLevel;
		}

		public ILog AddLogSection(string fieldName, string content)
		{
			this._sections.Add(fieldName, content);
			return this;
		}

		public void Write()
		{
			if (!this._filter.ShouldLog(this)) return;

			var orderedSections = this._sectionOrderer.Order(this._sections);
			this._sections = orderedSections;
			foreach (var logger in this._loggers)
			{
				try
				{
					logger.WriteLog(this);
				}
				catch (Exception ex)
				{
					Debug.Write("Logger threw this exception " + ex.Message);
				}
			}
		}
	}
}