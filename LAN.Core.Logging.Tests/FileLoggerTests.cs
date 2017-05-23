using NUnit.Framework;

namespace LAN.Core.Logging.Tests
{
	public class FileLoggerTests
	{
		[Test]
		[Explicit]
		public void TestThreeSimpleLogs()
		{
			var fileLogger = new FileLogger("FileLoggerTest.log");
			var errorLog = new Log(new[] { fileLogger }, AsIsOrderer.Instance, LogEverythingFilter.Instance, LogLevels.Error);
			errorLog.AddLogSection("Error field", "EEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE");
			var warnLog = new Log(new[] { fileLogger }, AsIsOrderer.Instance, LogEverythingFilter.Instance, LogLevels.Warn);
			warnLog.AddLogSection("Warn field", "WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW");
			var traceLog = new Log(new[] { fileLogger }, AsIsOrderer.Instance, LogEverythingFilter.Instance, LogLevels.Trace);
			traceLog.AddLogSection("Trace field", "TTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT");

			errorLog.Write();
			warnLog.Write();
			traceLog.Write();
		}
	}
}
