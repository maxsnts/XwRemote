using System;
using Poderosa.Boot;

namespace Poderosa.TerminalControl
{
	/// <summary>
	/// Fake tracer class designed to avoid notices about DLLs unable to be loaded being shown to the user.
	/// </summary>
	public class EmptyTracer : ITracer
	{
		public EmptyTracer()
		{
			Document = new TraceDocument();
		}

		public void Trace(string string_id)
		{
		}

		public void Trace(string string_id, string param1)
		{
		}

		public void Trace(string string_id, string param1, string param2)
		{
		}

		public void Trace(Exception ex)
		{
		}

		public TraceDocument Document
		{
			get;
			private set;
		}
	}
}
