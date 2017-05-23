using System.IO;
using System.Linq;

namespace LAN.Core.Logging
{
	public class FileLogger : ILogger
	{
		private const string FileName = "captains.log";

		public void WriteLog(ILog log)
		{
			var fileLog = !File.Exists(FileName) ? new StreamWriter(FileName) : File.AppendText(FileName);

			fileLog.WriteLine("{");
			fileLog.WriteLine("Level: " + log.LogLevel + ",");
			fileLog.WriteLine("Utc: " + log.LogCreatedAtUtc + ",");

			var sections = log.GetSections().ToList();
			var indexOfLastSection = sections.Count() - 1;
			foreach (var section in sections)
			{
				var logLine = section.Key + ": " + section.Value;
				if (sections.IndexOf(section) != indexOfLastSection) logLine += ",";
				fileLog.WriteLine(logLine);
			}
			fileLog.WriteLine("}");

			fileLog.Close();
		}
	}
}
