namespace LAN.Core.Logging
{
	public class LogEverythingFilter : ILogFilter
	{
		public static LogEverythingFilter Instance = new LogEverythingFilter();

		public bool ShouldLog(ILog log)
		{
			return true;
		}
	}
}