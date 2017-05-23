using Moq;
using NUnit.Framework;

namespace LAN.Core.Logging.Tests
{
	class LogFactoryTests
	{
		[Test]
		public void CreateLog_GivenValidLevel_CreatesLog()
		{
			var moqLogger = new Mock<ILogger>();

			var factory = new LogFactory(new[] { moqLogger.Object }, AsIsOrderer.Instance, LogEverythingFilter.Instance);

			var log = factory.CreateLog(LogLevels.Error);

			Assert.That(log, Is.Not.Null);
			Assert.That(log.LogLevel, Is.EqualTo(LogLevels.Error));
		}

		[TestCase("")]
		[TestCase(null)]
		[TestCase(" ")]
		public void CreateLog_GivenNullOrEmptyLogLevel_SetsLevelToTrace(string nullOrEmptyLogLevel)
		{
			var moqLogger = new Mock<ILogger>();

			var factory = new LogFactory(new[] { moqLogger.Object }, AsIsOrderer.Instance, LogEverythingFilter.Instance);

			var log = factory.CreateLog(nullOrEmptyLogLevel);

			Assert.That(log, Is.Not.Null);
			Assert.That(log.LogLevel, Is.EqualTo(LogLevels.Trace));
		}
	}
}
