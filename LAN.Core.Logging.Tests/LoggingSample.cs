using System;

namespace LAN.Core.Logging.Tests
{
	class LoggingSample
	{

		private readonly ILogFactory _logFactory;

		public LoggingSample(ILogFactory logFactory)
		{
			this._logFactory = logFactory;
		}

		public void DoExample(string sampleInput)
		{
			try
			{
				_logFactory.Trace().WithMessage("Customer ({0})", "AccountNumber");

				switch (sampleInput)
				{
					case "thing":
						break;
					default:
						_logFactory.Warn().WithMessage("Thing Comparor does not handler this case {0}", sampleInput);
						break;
				}
			}
			catch (Exception ex)
			{
				_logFactory.Error().WithException(ex);
				throw;
			}
		}
	}
}
