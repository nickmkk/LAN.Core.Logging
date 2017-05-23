using System.Collections.Generic;
using Moq;
using NUnit.Framework;

namespace LAN.Core.Logging.Tests
{
	public class LogTests
	{
		[Test]
		public void Write_GivenAValidLogSection_WillPassTheSectionToEachLogger()
		{
			var moqLogger1 = new Mock<ILogger>();
			var moqLogger2 = new Mock<ILogger>();

			var log = new Log(new[] { moqLogger1.Object, moqLogger2.Object }, AsIsOrderer.Instance, LogEverythingFilter.Instance, "test");

			log.AddLogSection("someField", "someValue");

			log.Write();

			moqLogger1.Verify(x => x.WriteLog(It.IsAny<Log>()));
			moqLogger2.Verify(x => x.WriteLog(It.IsAny<Log>()));
		}

		[Test]
		public void Write_GivenAValidLogSection_WillOrderOnce()
		{
			var moqLogger1 = new Mock<ILogger>();
			var moqLogger2 = new Mock<ILogger>();
			var mockOrderer = new Mock<ISectionOrderer>();

			var log = new Log(new[] { moqLogger1.Object, moqLogger2.Object }, mockOrderer.Object, LogEverythingFilter.Instance, "test");

			log.AddLogSection("someField", "someValue");

			log.Write();

			mockOrderer.Verify(x => x.Order(It.IsAny<IDictionary<string, string>>()), Times.Once);
		}

		[Test]
		public void Write_WhenFilterReturnsTrue_WillLog()
		{
			var moqLogger = new Mock<ILogger>();
			var moqFilter = new Mock<ILogFilter>();
			moqFilter.Setup(x => x.ShouldLog(It.IsAny<Log>())).Returns(true);

			var log = new Log(new[] { moqLogger.Object }, AsIsOrderer.Instance, moqFilter.Object, "test");

			log.AddLogSection("someField", "someValue");
			log.Write();

			moqLogger.Verify(x => x.WriteLog(It.IsAny<Log>()));
		}

		[Test]
		public void Write_WhenFilterReturnsFalse_WillSkipAnyProcessing()
		{
			var moqLogger = new Mock<ILogger>();
			var moqFilter = new Mock<ILogFilter>();
			var mockOrderer = new Mock<ISectionOrderer>();
			moqFilter.Setup(x => x.ShouldLog(It.IsAny<Log>())).Returns(false);

			var log = new Log(new[] { moqLogger.Object }, mockOrderer.Object, moqFilter.Object, "test");

			log.AddLogSection("someField", "someValue");
			log.Write();

			moqLogger.Verify(x => x.WriteLog(It.IsAny<Log>()), Times.Never);
			mockOrderer.Verify(x => x.Order(It.IsAny<IDictionary<string, string>>()), Times.Never);
		}
	}
}
