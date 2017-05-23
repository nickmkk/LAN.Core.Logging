using LAN.Core.Logging.NLog;
using NUnit.Framework;

namespace LAN.Core.Logging.Tests
{
	public class NLogLoggerTests
	{
		[Test]
		[Explicit]
		public void TestThreeSimpleLogs()
		{
			var logger = new NLogLogger();

			var errorLog = new Log(new[] { logger }, AsIsOrderer.Instance, LogEverythingFilter.Instance, LogLevels.Error);
			errorLog.AddLogSection("Error field", "EEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE");
			var warnLog = new Log(new[] { logger }, AsIsOrderer.Instance, LogEverythingFilter.Instance, LogLevels.Warn);
			warnLog.AddLogSection("Warn field", "WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW");
			var traceLog = new Log(new[] { logger }, AsIsOrderer.Instance, LogEverythingFilter.Instance, LogLevels.Trace);
			traceLog.AddLogSection("Trace field", "TTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT");

			errorLog.Write();
			warnLog.Write();
			traceLog.Write();
		}
	}
}
