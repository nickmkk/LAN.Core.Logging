using System;
using System.IO;
using System.Linq;
using System.Security;

namespace LAN.Core.Logging
{
    public class FileLogger : ILogger
    {
        private readonly string _logPath;

        /// <exception cref="ArgumentNullException">fileName is null.</exception>
        /// <exception cref="SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="ArgumentException">The file name is empty, contains only white spaces, or contains invalid characters.</exception>
        /// <exception cref="UnauthorizedAccessException">Access to fileName is denied.</exception>
        /// <exception cref="PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        /// <exception cref="NotSupportedException">fileName contains a colon (:) in the middle of the string.</exception>
        public FileLogger(string logPath)
        {
            //validate the log path using FileInfo constructor
            _logPath = new FileInfo(logPath).FullName;
        }

        public void WriteLog(ILog log)
        {
            var logFile = new FileInfo(_logPath);
            using (StreamWriter fileWriter = logFile.Exists ? logFile.AppendText() : logFile.CreateText())
            {
                fileWriter.WriteLine("{");
                fileWriter.WriteLine("Level: " + log.LogLevel + ",");
                fileWriter.WriteLine("Utc: " + log.LogCreatedAtUtc + ",");

                var sections = log.GetSections().ToList();
                var indexOfLastSection = sections.Count() - 1;
                foreach (var section in sections)
                {
                    var logLine = section.Key + ": " + section.Value;
                    if (sections.IndexOf(section) != indexOfLastSection) logLine += ",";
                    fileWriter.WriteLine(logLine);
                }
                fileWriter.WriteLine("}");
            }
        }
    }
}
