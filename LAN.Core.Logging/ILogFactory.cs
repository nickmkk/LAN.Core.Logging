using System.Diagnostics.Contracts;

namespace LAN.Core.Logging
{
	[ContractClass(typeof(LogFactoryContract))]
	public interface ILogFactory
	{
		ILog CreateLog(string logLevel);
		ILog Warn();
		ILog Error();
		ILog Trace();
	}

	[ContractClassFor(typeof(ILogFactory))]
	abstract class LogFactoryContract : ILogFactory
	{
		ILog ILogFactory.CreateLog(string logLevel)
		{
			Contract.Requires(logLevel != null);
			Contract.Ensures(Contract.Result<ILog>() != null);
			throw new System.NotImplementedException();
		}

		ILog ILogFactory.Warn()
		{
			Contract.Ensures(Contract.Result<ILog>() != null);
			throw new System.NotImplementedException();
		}

		ILog ILogFactory.Error()
		{
			Contract.Ensures(Contract.Result<ILog>() != null);
			throw new System.NotImplementedException();
		}

		ILog ILogFactory.Trace()
		{
			Contract.Ensures(Contract.Result<ILog>() != null);
			throw new System.NotImplementedException();
		}
	}
}