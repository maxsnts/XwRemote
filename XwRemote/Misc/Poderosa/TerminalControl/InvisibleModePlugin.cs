using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poderosa.Forms;
using Poderosa.Plugins;

[assembly: PluginDeclaration(typeof(Poderosa.TerminalControl.InvisibleModePlugin))]

namespace Poderosa.TerminalControl
{
	/// <summary>
	/// Poderosa plugin that will turn on invisible mode (main window is not shown) for the Poderosa application.
	/// </summary>
	[PluginInfo(ID = "org.poderosa.core.window.invisibleMode", Version = "1.0", Author = "Luke Stratman", Dependencies = "org.poderosa.core.window")]
	internal class InvisibleModePlugin : PluginBase
	{
		/// <summary>
		/// Called when the plugin is initialized, it gets <see cref="IWindowManager"/> and sets its <see cref="IWindowManager.InvisibleMode"/> property to 
		/// true and its <see cref="IWindowManager.StartMode"/> property to <see cref="StartMode.Slave"/>.
		/// </summary>
		/// <param name="poderosa">IPoderosaWorld interface for the application.</param>
		public override void InitializePlugin(IPoderosaWorld poderosa)
		{
			base.InitializePlugin(poderosa);

			IWindowManager windowManager = (IWindowManager) poderosa.PluginManager.FindPlugin("org.poderosa.core.window", typeof (IWindowManager));
			
			windowManager.InvisibleMode = true;
			windowManager.StartMode = StartMode.Slave;
		}
	}
}
