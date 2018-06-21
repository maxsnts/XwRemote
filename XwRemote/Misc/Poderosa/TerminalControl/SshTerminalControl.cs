using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Forms;
using Granados;
using Poderosa.Boot;
using Poderosa.Forms;
using Poderosa.Plugins;
using Poderosa.Protocols;
using Poderosa.Sessions;
using Poderosa.Terminal;

namespace Poderosa.TerminalControl
{
	/// <summary>
	/// Control class that contains the SSH terminal UI.  Creates a new <see cref="IPoderosaMainWindow"/> instance, kicks off the connection process, and then
	/// steals the <see cref="IContentReplaceableView"/> instance representing the actual terminal window and places it in this control.
	/// </summary>
	public partial class SshTerminalControl : UserControl, IInterruptableConnectorClient
	{
		/// <summary>
		/// Invisible Poderosa application window that we create to create connection windows and then steal them.
		/// </summary>
		protected static IPoderosaApplication _poderosaApplication;

		/// <summary>
		/// Interface to the various functionality in <see cref="_poderosaApplication"/>.
		/// </summary>
		protected static IPoderosaWorld _poderosaWorld;

		/// <summary>
		/// Settings to use when setting up the terminal.
		/// </summary>
		protected ITerminalSettings _settings;

		/// <summary>
		/// Event that is invoked when a connection is successfully established.
		/// </summary>
		public event EventHandler Connected;

		/// <summary>
		/// Event that is invoked when a previously established connection dies in an abnormal (i.e. user didn't log off) manner or when a connection attempt
		/// fails.
		/// </summary>
		public event ErrorEventHandler Disconnected;

		/// <summary>
		/// Event that is invoked when a previously established connection is terminated in a normal manner (i.e. when the user logs off).
		/// </summary>
		public event EventHandler LoggedOff;

		/// <summary>
		/// Default constructor, initializes the <see cref="Port"/>, <see cref="TerminalType"/>, and <see cref="SshProtocol"/> properties.
		/// </summary>
		public SshTerminalControl()
		{
			InitializeComponent();

			Port = 22;
			TerminalType = TerminalType.VT100;
			SshProtocol = SshProtocol.SSH2;
		}

		/// <summary>
		/// Static constructor, creates the invisible Poderosa application window once per execution.
		/// </summary>
		static SshTerminalControl()
		{
			_poderosaApplication = PoderosaStartup.CreatePoderosaApplication(new string[] { });
			_poderosaWorld = _poderosaApplication.Start(new EmptyTracer());
		}

		/// <summary>
		/// Username to use when establishing the SSH connection.
		/// </summary>
		public string Username
		{
			get;
			set;
		}

		/// <summary>
		/// Identity file to use when establishing the SSH connection.
		/// </summary>
		public string IdentityFile
		{
			get;
			set;
		}

		/// <summary>
		/// Password to use when establishing the SSH connection.
		/// </summary>
		public SecureString Password
		{
			get;
			set;
		}

		/// <summary>
		/// SSH host to connect to.
		/// </summary>
		public string HostName
		{
			get;
			set;
		}

		/// <summary>
		/// Port to use when establishing the SSH connection (defaults to 22).
		/// </summary>
		public int Port
		{
			get;
			set;
		}

		/// <summary>
		/// Terminal type to use once the SSH connection is established.
		/// </summary>
		public TerminalType TerminalType
		{
			get;
			set;
		}

		/// <summary>
		/// Protocol version to use when establishing the SSH connection.
		/// </summary>
		public SshProtocol SshProtocol
		{
			get;
			set;
		}

		/// <summary>
		/// Initiates the SSH connection process by getting the <see cref="IProtocolService"/> instance and calling 
		/// <see cref="IProtocolService.AsyncSSHConnect"/>.  This is an asynchronous process:  the <see cref="SuccessfullyExit"/> method is called when the
		/// connection is established successfully and <see cref="ConnectionFailed"/> method is called when we are unable to establish the connection.
		/// </summary>
		public void AsyncConnect()
		{
			ITerminalEmulatorService terminalEmulatorService =
				(ITerminalEmulatorService)_poderosaWorld.PluginManager.FindPlugin("org.poderosa.terminalemulator", typeof(ITerminalEmulatorService));
			IProtocolService protocolService = (IProtocolService)_poderosaWorld.PluginManager.FindPlugin("org.poderosa.protocols", typeof(IProtocolService));

			// Create and initialize the SSH login parameters
			ISSHLoginParameter sshLoginParameter = protocolService.CreateDefaultSSHParameter();

			sshLoginParameter.Account = Username;

			if (!String.IsNullOrEmpty(IdentityFile))
			{
				sshLoginParameter.AuthenticationType = AuthenticationType.PublicKey;
				sshLoginParameter.IdentityFileName = IdentityFile;
			}

			else
			{
				sshLoginParameter.AuthenticationType = AuthenticationType.Password;

				if (Password != null && Password.Length > 0)
				{
					IntPtr passwordBytes = Marshal.SecureStringToGlobalAllocAnsi(Password);
					sshLoginParameter.PasswordOrPassphrase = Marshal.PtrToStringAnsi(passwordBytes);
				}
			}

			sshLoginParameter.Method = (SSHProtocol)Enum.Parse(typeof(SSHProtocol), SshProtocol.ToString("G"));

			// Create and initialize the various socket connection properties
			ITCPParameter tcpParameter = (ITCPParameter)sshLoginParameter.GetAdapter(typeof(ITCPParameter));

			tcpParameter.Destination = HostName;
			tcpParameter.Port = Port;

			// Set the UI settings to use for the terminal itself
			terminalEmulatorService.TerminalEmulatorOptions.RightButtonAction = MouseButtonAction.Paste;
			_settings = terminalEmulatorService.CreateDefaultTerminalSettings(tcpParameter.Destination, null);

			_settings.BeginUpdate();
			_settings.TerminalType = (ConnectionParam.TerminalType)Enum.Parse(typeof(ConnectionParam.TerminalType), TerminalType.ToString("G"));
			_settings.RenderProfile = terminalEmulatorService.TerminalEmulatorOptions.CreateRenderProfile();
			_settings.RenderProfile.BackColor = BackColor;
			_settings.RenderProfile.ForeColor = ForeColor;
			_settings.RenderProfile.FontName = Font.Name;
			_settings.RenderProfile.FontSize = Font.Size;
			_settings.EndUpdate();

			ITerminalParameter param = (ITerminalParameter)tcpParameter.GetAdapter(typeof(ITerminalParameter));
			param.SetTerminalName(_settings.TerminalType.ToString("G").ToLower());

			// Initiate the connection process
			protocolService.AsyncSSHConnect(this, sshLoginParameter);
		}

		/// <summary>
		/// Called when <see cref="IProtocolService.AsyncSSHConnect"/> is able to successfully establish the SSH connection.  Creates a new 
		/// <see cref="IPoderosaMainWindow"/> instance and points the newly created connection to a new document instance within that window.  We then steal
		/// the <see cref="IContentReplaceableView"/> terminal window from Poderosa and set its parent to ourself, thus displaying the terminal within this
		/// control.
		/// </summary>
		/// <param name="result">Newly-created SSH connection.</param>
		public void SuccessfullyExit(ITerminalConnection result)
		{
			ICoreServices coreServices = (ICoreServices)_poderosaWorld.GetAdapter(typeof(ICoreServices));
			IWindowManager windowManager = coreServices.WindowManager;

			// Wire up the event handlers on the newly-created connection.
			(result as ICloseableTerminalConnection).ConnectionClosed += TerminalControl_ConnectionClosed;
			(result as ICloseableTerminalConnection).ConnectionLost += TerminalControl_ConnectionLost;

			// We run all of this logic within an Invoke() to avoid trying to do this on the wrong GUI thread
			windowManager.MainWindows.First().AsForm().Invoke(
				new Action(
					() =>
						{
							// Create a new Poderosa window to contain the connection terminal
							IPoderosaMainWindow window = windowManager.CreateNewWindow(new MainWindowArgument(ClientRectangle, FormWindowState.Normal, "", "", 1));
							IViewManager viewManager = window.ViewManager;
							Sessions.TerminalSession session = new Sessions.TerminalSession(result, _settings);

							// Create a new connection window within the Poderosa window, which we will later steal
							IContentReplaceableView view = (IContentReplaceableView)viewManager.GetCandidateViewForNewDocument().GetAdapter(typeof(IContentReplaceableView));
							coreServices.SessionManager.StartNewSession(session, view);

							session.TerminalControl.HideSizeTip = true;

							Form containerForm = view.ParentForm.AsForm();

							// Hide all of the toolbar and statusbar chrome in the connection window and display only the terminal itself (it's a little 
							// clumsy, I know)
							foreach (Control control in containerForm.Controls)
							{
								if (control is MenuStrip || control.GetType().Name == "PoderosaStatusBar")
									control.Visible = false;

								else if (control.GetType().Name == "PoderosaToolStripContainer")
								{
									foreach (ToolStripPanel child in control.Controls.OfType<ToolStripPanel>())
										child.Visible = false;

									foreach (ToolStripContentPanel child in control.Controls.OfType<ToolStripContentPanel>())
									{
										foreach (Control grandChild in child.Controls)
										{
											if (grandChild.GetType().Name != "TerminalControl")
												grandChild.Visible = false;
										}
									}
								}
							}

							// Steal the terminal view and display it within this control
							containerForm.TopLevel = false;
							containerForm.FormBorderStyle = FormBorderStyle.None;
							containerForm.Width = Width;
							containerForm.Height = Height;
							containerForm.Dock = DockStyle.Fill;
							containerForm.Parent = this;

							view.AsControl().Focus();
						}));

			// Invoke the Connected event
			if (Connected != null)
				Connected(this, new EventArgs());
		}

		/// <summary>
		/// Event handler that's called when a previously established connection is terminated in an abnormal (i.e. the user didn't log off) manner.
		/// </summary>
		/// <param name="sender">Object from which this event originated.</param>
		/// <param name="e">Arguments associated with this event, in this case the reason the connection was lost.</param>
		void TerminalControl_ConnectionLost(object sender, ErrorEventArgs e)
		{
			ConnectionFailed(e.GetException().Message);
		}

		/// <summary>
		/// Event handler that's called when a previously established connection is terminated normally (i.e. the user logged off).
		/// </summary>
		/// <param name="sender">Object from which this event originated.</param>
		/// <param name="e">Arguments associated with this event.</param>
		void TerminalControl_ConnectionClosed(object sender, EventArgs e)
		{
			if (LoggedOff != null)
				LoggedOff(this, new EventArgs());
		}

		/// <summary>
		/// Event handler that's called when <see cref="IProtocolService.AsyncSSHConnect"/> is unable to establish a connection to an SSH host.
		/// </summary>
		/// <param name="message">Message associated with the connection failure.</param>
		public void ConnectionFailed(string message)
		{
			if (Disconnected != null)
				Disconnected(this, new ErrorEventArgs(new Exception(message)));
		}
	}
}
