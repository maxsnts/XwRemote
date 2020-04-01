using SharpRaven;
using SharpRaven.Data;
using System;
using System.Windows.Forms;
using XwMaxLib.Data;

namespace XwRemote.Misc
{
    public partial class SendError : Form
    {
        private Exception Ex = null;
        private string Report = string.Empty;
        private string Comment = string.Empty;
        private bool Shutdown = true;

        //*************************************************************************************************************
        public SendError(Exception exception, bool shutdown)
        {
            InitializeComponent();
            Ex = exception;
            Shutdown = shutdown;
        }

        //*************************************************************************************************************
        private void SendError_Load(object sender, EventArgs e)
        {
            BuildReport();
            ReportBox.Text = Report;
            ReportBox.Select(0, 0);
        }

        //*************************************************************************************************************
        private void BuildReport()
        {
            Report += AddSeparator("Exception", '=', false, true);
            if (Ex == null)
                Report += "Exception is NULL!";
            else
                Report += GetExceptionData(Ex);
        }

        //*************************************************************************************************************
        private string AddSeparator(string title, char c, bool startWithLineBreak, bool endWithLineBreak)
        {
            int total = 80;
            string s = string.Empty;

            if (startWithLineBreak)
                s += "\r\n";

            s += string.Format("{0} {1} {0}", 
                (c.ToString()).PadLeft(((total - title.Length) / 2) - 1, c), title).PadRight(total, c);

            if (endWithLineBreak)
                s += "\r\n";

            return s;
        }

        //*************************************************************************************************************
        private string GetExceptionData(Exception ex)
        {
            string tmp = string.Empty;

            tmp += ex.Message;

            if (ex is XwDbException)
            {
                tmp += AddSeparator("Command", '-', true, true);
                tmp += ((XwDbException)ex).Command;
            }

            tmp += AddSeparator("CallStack", '-', true, true);
            tmp += ex.StackTrace.ToString().Replace(" in ", "\r\n\tin ");

            if (ex.InnerException != null)
            {
                tmp += AddSeparator("InnerException", '-', true, true);
                tmp += GetExceptionData(ex.InnerException);
            }

            return tmp;
        }

        //*************************************************************************************************************
        private void SendError_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;

            if (Shutdown)
                Environment.Exit(1);
            else
                Close();
        }

        //*************************************************************************************************************
        private void buttonOK_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            var ravenClient = new RavenClient("https://11dbb280832c4f52a000577bf8eee1f8@sentry.io/1210500");
            SentryEvent ev = new SentryEvent(Ex);
            ev.Extra = ReportBox.Text;
            ev.Extra += AddSeparator("Comment", '*', true, true);
            ev.Extra += CommentBox.Text;
            ravenClient.Capture(ev);
            Close();
        }
    }
}
