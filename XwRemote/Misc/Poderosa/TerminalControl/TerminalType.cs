namespace Poderosa.TerminalControl
{
	/// <summary>
	/// Terminal type to use when establishing an SSH connection.
	/// </summary>
	public enum TerminalType
	{
		/// <summary>
		/// Use the VT100 terminal type.
		/// </summary>
		VT100,

		/// <summary>
		/// Use the XTerm terminal type.
		/// </summary>
		XTerm,

		/// <summary>
		/// Use the KTerm terminal type.
		/// </summary>
		KTerm
	}
}
