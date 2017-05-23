using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace LAN.Core.Logging
{
	public class ListOfSupportedLevelFilter : ILogFilter
	{
		private readonly string[] _supportedLevels;

		public ListOfSupportedLevelFilter(IEnumerable<string> supportedLevels)
		{
			Contract.Requires(supportedLevels != null);
			this._supportedLevels = supportedLevels.ToArray();
		}

		public bool ShouldLog(ILog log)
		{
			return _supportedLevels.Contains(log.LogLevel);
		}
	}
}