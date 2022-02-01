using SharpRaven;
using SharpRaven.Data;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using XwMaxLib.Extentions;
using XwRemote.Properties;

namespace XwRemote.Misc
{
    public partial class Stuff : Form
    {
        private string NewVersion = "";

        //*************************************************************************************************************
        public Stuff()
        {
            InitializeComponent();
            version.Text = Main.CurrentVersion;
            faTabStrip1.SelectedItem = faTabAbout;
        }

        //*************************************************************************************************************
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel1.Text);
        }

        //*************************************************************************************************************
        private void Stuff_Load(object sender, EventArgs e)
        {
            buttonUpdate.Enabled = false;
            using (Stream oStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("XwRemote.Credits.rtf"))
            {
                richTextBox1.LoadFile(oStream, RichTextBoxStreamType.RichText);
            }
            
            scroller1.Interval = 40;
            scroller1.TextToScroll = Resources.whynot;
        }

        //*************************************************************************************************************
        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

        //*************************************************************************************************************
        private async void faTabStrip1_TabStripItemSelectionChanged(FarsiLibrary.Win.TabStripItemChangedEventArgs e)
        {
            scroller1.Stop();
            if (e.Item == faTabWhyNot)
                scroller1.Start();

            if (e.Item == faTabUpdates)
                await CheckUpdates();
        }

        //*************************************************************************************************************
        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (CommentBox.Text.Trim() == "")
            {
                CommentBox.ShowBalloon(ToolTipIcon.Warning, "", "Empty message?");
                return;
            }

            Cursor.Current = Cursors.WaitCursor;
            string message = "";
            message += "***************************** MESSAGE *****************************\n";
            message += CommentBox.Text + "\n";
            message += "****************************** NAME *******************************\n";
            message += NameBox.Text + "\n";
            message += "****************************** MAIL *******************************\n";
            message += MailBox.Text + "\n";
            message += "*******************************************************************\n";

            var ravenClient = new RavenClient("https://11dbb280832c4f52a000577bf8eee1f8@sentry.io/1210500");
            SentryMessage msg = new SentryMessage(message);
            SentryEvent ev = new SentryEvent(msg);
            ravenClient.Capture(ev);
            Close();
        }

        //*************************************************************************************************************
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel2.Text);
        }

        //*************************************************************************************************************
        private void linkLabel3_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.paypal.me/maxsnts");
        }

        //*************************************************************************************************************
        private async Task CheckUpdates()
        {
            using (WebClient client = new WebClient())
            {
                string URL = $"https://github.com/maxsnts/{Main.UpdateRepo}/releases/latest";

                try
                {
                    string content = await client.DownloadStringTaskAsync(URL);
                    Match m = Regex.Match(content, @"(?isx)/releases/tag/v(?<VERSION>\d+\.\d+\.\d+\.\d+)""");
                    string latestVersion = m.Result("${VERSION}");

                    if (latestVersion != Main.CurrentVersion)
                    {
                        labelVersion.Text = $"There is a new version available: {latestVersion}";
                        NewVersion = latestVersion;
                        buttonUpdate.Enabled = true;

                        try
                        {
                            //this is a bad way to do it, very bridle
                            m = Regex.Match(content, @"(?ixs)markdown-body.*?>(?<NOTES>.*?)</div>");
                            string notes = m.Result("${NOTES}").Trim();
                            notes = notes.Replace("<p>", "");
                            notes = notes.Replace("</p>", "\r\n");
                            notes = notes.Replace("<ul>", "");
                            notes = notes.Replace("</ul>", "");
                            notes = notes.Replace("<li>", "- ");
                            notes = notes.Replace("</li>", "\r\n\r\n");
                            notes = notes.Replace("<br>", "\r\n");
                            ReleaseNotes.Text = notes;
                        }
                        catch { /* dont care */}
                    }
                    else
                    {
                        labelVersion.Text = "There is no new version at this date";
                        buttonUpdate.Enabled = false;
                    }
                }
                catch
                {
                    labelVersion.Text = "Unable to check for updates, update manually";
                    linkLatest.Text = URL;
                    linkLatest.Visible = true;
                    buttonUpdate.Enabled = false;
                }
            }
        }

        //*************************************************************************************************************
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            buttonUpdate.Text = "Updating ...";
            buttonUpdate.Enabled = false;
            UpdateProgress.Minimum = 0;
            UpdateProgress.Maximum = 100;
            UpdateProgress.Step = 1;
            UpdateProgress.Visible = true;
            ReleaseNotes.Select(0, 0);
            
            string path = Environment.CurrentDirectory;
            using (WebClient client = new WebClient())
            {
                string URL = $"https://github.com/maxsnts/{Main.UpdateRepo}/releases/download/v{NewVersion}/{Main.UpdateRepo}.v{NewVersion}.zip";
                try
                {
                    client.DownloadFileCompleted += Client_DownloadFileCompleted;
                    client.DownloadProgressChanged += Client_DownloadProgressChanged;
                    client.DownloadFileAsync(new Uri(URL), Path.Combine(path, $"{Main.UpdateRepo}.zip"));
                }
                catch
                {
                    labelVersion.Text = "Unable to check for updates, update manually";
                    linkLatest.Text = URL;
                    linkLatest.Visible = true;
                    buttonUpdate.Enabled = false;
                }
            }
        }

        //*************************************************************************************************************
        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            BeginInvoke((Action)(() =>
            {
                UpdateProgress.Value = e.ProgressPercentage;
            }));
        }

        //*************************************************************************************************************
        private void Client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            try
            {
                string path = Environment.CurrentDirectory;

                //check if zip file exists
                string zipFile = Path.Combine(path, "XwRemote.zip");
                if (!File.Exists(zipFile))
                    throw new Exception("no file");

                //check if zip file is OK
                using (ZipArchive archive = ZipFile.Open(zipFile, ZipArchiveMode.Read)) { }

                //kill putty otherwise update may fail
                foreach (var process in Process.GetProcessesByName("putty"))
                {
                    if (process.MainModule.FileName.Contains(Environment.CurrentDirectory))
                        process.Kill();
                }

                //kill puttygen otherwise update may fail
                foreach (var process in Process.GetProcessesByName("puttygen"))
                {
                    if (process.MainModule.FileName.Contains(Environment.CurrentDirectory))
                        process.Kill();
                }

                //kill plink otherwise update may fail
                foreach (var process in Process.GetProcessesByName("plink"))
                {
                    if (process.MainModule.FileName.Contains(Environment.CurrentDirectory))
                        process.Kill();
                }

                File.WriteAllBytes(Path.Combine(path, "XwUpdater.exe"), Resources.XwUpdater);
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = Path.Combine(path, "XwUpdater.exe");
                    process.StartInfo.WorkingDirectory = path;
                    process.StartInfo.Arguments = $"\"{Main.UpdateRepo}.exe\" \"{Main.UpdateRepo}.zip\" \"{path}\"";
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    process.Start();
                    Environment.Exit(0);
                }
            }
            catch
            {
                labelVersion.Text = "Unable to check for updates, update manually";
                string URL = $"https://github.com/maxsnts/{Main.UpdateRepo}/releases/download/v{NewVersion}/{Main.UpdateRepo}.v{NewVersion}.zip";
                linkLatest.Text = URL;
                linkLatest.Visible = true;
                buttonUpdate.Enabled = false;
            }
        }

        //*************************************************************************************************************
        private void linkLatest_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLatest.Text);
        }

        //*************************************************************************************************************
        private void linkReleases_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkReleases.Text);
        }
    }
}
