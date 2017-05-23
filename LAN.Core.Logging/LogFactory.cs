using System.Collections.Generic;

namespace LAN.Core.Logging
{
	public class LogFactory : ILogFactory
	{
		private readonly IEnumerable<ILogger> _loggers;
		private readonly ISectionOrderer _orderer;
		private readonly ILogFilter _filter;

		public LogFactory(IEnumerable<ILogger> loggers, ISectionOrderer orderer, ILogFilter filter)
		{
			this._loggers = loggers;
			this._orderer = orderer;
			this._filter = filter;
		}

		public ILog CreateLog(string logLevel)
		{
			if (string.IsNullOrWhiteSpace(logLevel)) logLevel = LogLevels.Trace;
			var newLog =  new Log(this._loggers, this._orderer, this._filter, logLevel);
			return newLog;
		}

		public ILog Warn()
		{
			return CreateLog(LogLevels.Warn);
		}

		public ILog Error()
		{
			return CreateLog(LogLevels.Error);
		}

		public ILog Trace()
		{
			return CreateLog(LogLevels.Trace);
		}
	}
}