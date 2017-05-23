using System;
using System.Diagnostics.Contracts;

namespace LAN.Core.Logging
{
	[ContractClass(typeof (LoggerContract))]
	public interface ILogger
	{
		void WriteLog(ILog log);
	}

	[ContractClassFor(typeof (ILogger))]
	abstract class LoggerContract : ILogger
	{
		void ILogger.WriteLog(ILog log)
		{
			Contract.Requires(log != null);
			throw new NotImplementedException();
		}
	}
}