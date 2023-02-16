using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace XwRemote.Servers
{
    class PuttyAppPanel : System.Windows.Forms.Panel
    {
        //****************************************************************************************************
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("User32")]
        private static extern int SetForegroundWindow(IntPtr hwnd);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        //****************************************************************************************************
        public delegate void PuttyAppStartedCallback();
        public delegate void PuttyAppClosedCallback(bool error);
        internal PuttyAppStartedCallback StartedCallback = null;
        internal PuttyAppClosedCallback ClosedCallback = null;

        private bool TryCorrectFocus = true;
        private const int GWL_STYLE = (-16);
        private const int WM_CLOSE = 0x10;
        private const int WS_BORDER = 0x00800000;
        private const int WS_THICKFRAME = 0x00040000;
        private TransparentPanel overlay = null;
        private Process AppProcess;
        private IntPtr AppWindow = IntPtr.Zero;
        private int AppMargin = 3;
        public string ApplicationCommand = "";
        public string ApplicationParameters = "";
        private bool callCloseEvent = true;

        //****************************************************************************************************
        public PuttyAppPanel(bool tryCorrectFocus)
        {
            TryCorrectFocus = tryCorrectFocus;
        }

        //****************************************************************************************************
        public void Open()
        {
            AppProcess = new Process();
            AppProcess.EnableRaisingEvents = true;
            AppProcess.StartInfo.FileName = ApplicationCommand;
            AppProcess.StartInfo.Arguments = ApplicationParameters;
            AppProcess.Exited += delegate (object sender, EventArgs ev)
            {
                if (callCloseEvent)
                    ClosedCallback?.Invoke(((Process)sender).ExitCode == 0 ? false : true );
            };
            AppProcess.Start();
            AppProcess.WaitForInputIdle();
            AppWindow = AppProcess.MainWindowHandle;
            SetParent(AppWindow, Handle);
            //SetForegroundWindow(this.Handle);

            ShowWindow(AppWindow, 3); //SW_SHOWMAXIMIZED
            int lStyle = GetWindowLong(AppWindow, GWL_STYLE);
            lStyle &= ~(WS_BORDER | WS_THICKFRAME);
            SetWindowLong(AppWindow, GWL_STYLE, lStyle);
            
            if (TryCorrectFocus)
            {
                if (overlay == null)
                overlay = new TransparentPanel();
                overlay.BackColor = Color.Transparent;
                overlay.Dock = System.Windows.Forms.DockStyle.Fill;
                overlay.Parent = this;
                overlay.AppWindow = AppWindow;
            }

            OnResize(null);
            Show();
            StartedCallback?.Invoke();
        }

        //****************************************************************************************************
        public void Close()
        {
            callCloseEvent = false;
            try
            {
                AppProcess?.Kill();
            }
            catch
            { }
        }

        //****************************************************************************************************
        protected override void OnResize(EventArgs e)
        {
            if (AppWindow != IntPtr.Zero)
            {
                MoveWindow(AppWindow, -AppMargin, -AppMargin, this.Width + AppMargin, this.Height + AppMargin, true);
                if (TryCorrectFocus)
                {
                    overlay.Width = Width;
                    overlay.Height = Height;
                    overlay.BringToFront();
                    overlay.Focus();
                }
            }
        }

        //****************************************************************************************************
        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (AppWindow != IntPtr.Zero)
            {
                PostMessage(AppWindow, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            }
            base.OnHandleDestroyed(e);
        }

        //****************************************************************************************************
        public new void Focus()
        {
            overlay?.Focus();
        }

        //****************************************************************************************************
        //****************************************************************************************************
        //****************************************************************************************************
        //****************************************************************************************************
        //****************************************************************************************************
        public class TransparentPanel : Panel
        {
            public IntPtr AppWindow = IntPtr.Zero;
            public TransparentPanel()
            {
                SetStyle(ControlStyles.Selectable, true);
                TabStop = true;
            }
            protected override CreateParams CreateParams
            {
                get
                {
                    CreateParams cp = base.CreateParams;
                    cp.ExStyle |= 0x00000020; // WS_EX_TRANSPARENT
                    return cp;
                }
            }

            protected override void OnPaintBackground(PaintEventArgs e)
            {
                // Do not paint background.
            }

            protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
            {
                if (AppWindow != IntPtr.Zero)
                    PostMessage(AppWindow, (uint)msg.Msg, msg.WParam, msg.LParam);
                return true;
            }
            
            [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
            protected override void WndProc(ref Message m)
            {
                // Ignore Ctrl+RightClick to prevent Putty context menu
                if (m.Msg == 516 && ((int)m.WParam & 0x0008) == 0x0008)
                    return;

                switch (m.Msg)
                {
                    case 33: //WM_MOUSEACTIVATE
                    case 160: //WM_NCMOUSEMOVE
                    case 512: //WM_MOUSEMOVE
                    case 513: //WM_LBUTTONDOWN
                    case 514: //WM_LBUTTONUP
                    case 515: //WM_LBUTTONDBLCLK
                    case 516: //WM_RBUTTONDOWN
                    case 517: //WM_RBUTTONUP
                    case 518: //WM_RBUTTONDBLCLK
                    case 519: //WM_MBUTTONDOWN
                    case 520: //WM_MBUTTONUP
                    case 521: //WM_MBUTTONDBLCLK
                    case 522: //WM_MOUSEWHEEL
                    case 526: //WM_MOUSEHWHEEL
                    case 672: //WM_NCMOUSEHOVER
                    case 673: //WM_MOUSEHOVER
                    case 674: //WM_NCMOUSELEAVE
                    case 675: //WM_MOUSELEAVE
                    {
                        if (AppWindow != IntPtr.Zero)
                        {
                            PostMessage(AppWindow, (uint)m.Msg, m.WParam, m.LParam);
                        }
                    }
                    break;
                    default:
                        base.WndProc(ref m);
                        break;
                }
            }
        }
    }
}
