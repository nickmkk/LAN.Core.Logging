using System.Diagnostics.Contracts;

namespace LAN.Core.Logging
{
	[ContractClass(typeof (LogFilterContract))]
	public interface ILogFilter
	{
		bool ShouldLog(ILog log);
	}

	[ContractClassFor(typeof (ILogFilter))]
	abstract class LogFilterContract : ILogFilter
	{
		bool ILogFilter.ShouldLog(ILog log)
		{
			Contract.Requires(log != null);
			throw new System.NotImplementedException();
		}
	}
}