using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using XwRemote.Misc;

namespace XwRemote
{
    static class Program
    {
        //[DllImport("user32.dll")]
        //public static extern bool SetForegroundWindow(IntPtr hWnd);

        //[DllImport("USER32.DLL")]
        //public static extern IntPtr SetFocus(IntPtr hWnd);

            /*
        [DllImport("shcore.dll")]
        static extern int SetProcessDpiAwareness(_Process_DPI_Awareness value);
        enum _Process_DPI_Awareness
        {
            Process_DPI_Unaware = 0,
            Process_System_DPI_Aware = 1,
            Process_Per_Monitor_DPI_Aware = 2
        }
        */

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //SetProcessDpiAwareness(_Process_DPI_Awareness.Process_Per_Monitor_DPI_Aware);

//#if !DEBUG
            // Add the event handler for handling UI thread exceptions to the event.
            Application.ThreadException += new ThreadExceptionEventHandler(UIThreadException);
            // Set the unhandled exception mode to force all Windows Forms errors to go through our handler.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            // Add the event handler for handling non-UI thread exceptions to the event. 
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
            //#endif

            //Reset working folder to the exe folder
            var process = Process.GetCurrentProcess();
            string fullPath = process.MainModule.FileName;
            Directory.SetCurrentDirectory(Directory.GetParent(fullPath).FullName);


            if (File.Exists("XwUpdater.exe"))
            {
                try
                {
                    Thread.Sleep(1000);
                    File.Delete("XwUpdater.exe");
                }
                catch { /* dont care */ }
            }

            var dir = new DirectoryInfo(Environment.CurrentDirectory);
            foreach (var file in dir.EnumerateFiles("*.tmp"))
                file.Delete();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }

        private static void UIThreadException(object sender, ThreadExceptionEventArgs t)
        {
            SendError error = new SendError(t.Exception, true);
            error.ShowDialog();
        }

        private static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            SendError error = new SendError((Exception)e.ExceptionObject, true);
            error.ShowDialog();
        }
    }
}
