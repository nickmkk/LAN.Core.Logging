using System.Collections.Generic;

namespace LAN.Core.Logging
{
	public class AsIsOrderer : ISectionOrderer
	{
		public IDictionary<string, string> Order(IDictionary<string, string> sections)
		{
			return sections;
		}

		public static AsIsOrderer Instance = new AsIsOrderer();
	}
}