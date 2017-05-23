using System;
using System.Diagnostics.Contracts;

namespace LAN.Core.Logging
{
	public static class LogExtensions
	{
		public static ILog WithException(this ILog log, Exception ex)
		{
			Contract.Requires(log != null);
			Contract.Requires(ex != null);
			log.AddLogSection(WellKnownFieldNames.Exception, ex.ToString());
			return log;
		}

		public static ILog WithMessage(this ILog log, string message)
		{
			Contract.Requires(log != null);
			Contract.Requires(message != null);
			log.AddLogSection(WellKnownFieldNames.Message, message);
			return log;
		}

		public static ILog WithMessage(this ILog log, string message, params object[] replacementValues)
		{
			Contract.Requires(log != null);
			Contract.Requires(message != null);
			Contract.Requires(replacementValues != null);
			log.AddLogSection(WellKnownFieldNames.Message, string.Format(message, replacementValues));
			return log;
		}
	}
}