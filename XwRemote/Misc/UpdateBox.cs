using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace XwRemote.Forms
{
    public partial class UpdateBox : Form
    {
        public string file = string.Empty;
        private string TMPFile = string.Empty;
        
        //********************************************************************************************
        public UpdateBox()
        {
            InitializeComponent();
        }

        //********************************************************************************************
        private void UpdateBox_Shown(object sender, EventArgs e)
        {
            changes.Text = "Listing...";
            Thread trd = new Thread(GetChangesList);
            trd.Start(this);
        }

        //********************************************************************************************
        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        //********************************************************************************************
        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show("Error downloading new update\r\n\r\nPlease try again later", 
                    "Update", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    Process process = new Process();
                    process.StartInfo.FileName = TMPFile;
                    process.StartInfo.UseShellExecute = true;
                    process.StartInfo.CreateNoWindow = true;
                    try //TODO: remove this once tested in the wild
                    {
                        process.StartInfo.Arguments = String.Format(@" /silent /dir=""{0}""", Path.GetDirectoryName(Application.ExecutablePath));
                    }
                    catch
                    {
                        process.StartInfo.Arguments = @" /silent";
                    }
                    process.Start();
                    Application.Exit();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //********************************************************************************************
        private void butCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        //********************************************************************************************
        private void butInstall_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            butInstall.Enabled = false;
            butCancel.Enabled = false;

            string URL = String.Format("http://xwega.com/tools/{0}", file);
            TMPFile = file;

            WebClient webClient = new WebClient();
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
            webClient.DownloadFileAsync(new Uri(URL), TMPFile);
        }

        //**********************************************************************************************
        static void GetChangesList(object state)
        {
            UpdateBox form = (UpdateBox)state;

            /*
            try
            {
                string URL = String.Format("http://xwega.com/sys/changes.aspx?PUID={0}",
                    Misc.GetAssemblyGuid(typeof(Main)));

                string resp = string.Empty;

                if (Net.MakeRequest(URL, string.Empty, string.Empty, out resp) == HttpStatusCode.Accepted)
                {
                    form.BeginInvoke((MethodInvoker)delegate 
                    {
                        form.changes.Text = resp;
                    });
                }
                else
                {
                    form.BeginInvoke((MethodInvoker)delegate 
                    {
                        form.changes.Text = "Error getting changes text";
                    });
                }
            }
            catch
            {
                form.BeginInvoke((MethodInvoker)delegate
                {
                    form.changes.Text = "Error getting changes text";
                });
            }
            */
        }
    }
}
