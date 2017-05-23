using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace LAN.Core.Logging
{
	[ContractClass(typeof(LogContract))]
	public interface ILog
	{
		IEnumerable<KeyValuePair<string, string>> GetSections();
		string LogLevel { get; }
		DateTime LogCreatedAtUtc { get; }
		ILog AddLogSection(string fieldName, string content);
		void Write();
	}

	[ContractClassFor(typeof(ILog))]
	abstract class LogContract : ILog
	{
		IEnumerable<KeyValuePair<string, string>> ILog.GetSections()
		{
			Contract.Ensures(Contract.Result<IEnumerable<KeyValuePair<string, string>>>() != null);
			throw new NotImplementedException();
		}

		public string LogLevel
		{
			get
			{
				Contract.Ensures(Contract.Result<string>() != null);
				throw new NotImplementedException();
			}
		}

		public DateTime LogCreatedAtUtc
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public ILog AddLogSection(string fieldName, string content)
		{
			Contract.Requires(fieldName != null);
			Contract.Requires(content != null);
			Contract.Ensures(Contract.Result<ILog>() != null);
			throw new NotImplementedException();
		}

		public void Write()
		{
			throw new NotImplementedException();
		}
	}
}