using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace LAN.Core.Logging
{
	[ContractClass(typeof(SectionOrdererContract))]
	public interface ISectionOrderer
	{
		IDictionary<string, string> Order(IDictionary<string, string> sections);
	}

	[ContractClassFor(typeof(ISectionOrderer))]
	abstract class SectionOrdererContract : ISectionOrderer
	{
		IDictionary<string, string> ISectionOrderer.Order(IDictionary<string, string> sections)
		{
			Contract.Requires(sections != null);
			Contract.Ensures(Contract.Result<IDictionary<string, string>>() != null);
			throw new System.NotImplementedException();
		}
	}
}