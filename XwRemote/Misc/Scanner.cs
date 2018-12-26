using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using XwMaxLib.Extensions;

namespace XwRemote.Misc
{
    public partial class Scanner : Form
    {
        private ImageList imageList = new ImageList();
        bool cancel = false;
        bool running = false;
        
        //**********************************************************************************************
        public Scanner()
        {
            imageList.Images.Add(global::XwRemote.Properties.Resources.rdp);
            imageList.Images.Add(global::XwRemote.Properties.Resources.ssh);
            imageList.Images.Add(global::XwRemote.Properties.Resources.vnc);
            imageList.Images.Add(global::XwRemote.Properties.Resources.IE);
            imageList.Images.Add(global::XwRemote.Properties.Resources.ftp);
            InitializeComponent();
        }

        //**********************************************************************************************
        private void Scanner_Load(object sender, EventArgs e)
        {
            ipAddressControlFrom.Text = Main.config.GetValue("LASTSCANFROMADDRESS");
            ipAddressControlTo.Text = Main.config.GetValue("LASTSCANTOADDRESS");
            textTcpPorts.Text = Main.config.GetValue("LASTSCANPORTS", "80,443,21,22,3389");

            progressBar.Step = 1;
            progressBar.Minimum = 0;
            progressBar.Maximum = 100;
            progressBar.Value = 0;

            listViewHosts.SmallImageList = imageList;
            listViewHosts.FullRowSelect = true;
            listViewHosts.Columns.Add("IP");
            listViewHosts.Columns.Add("DNS");
            listViewHosts.Columns.Add("NetBios");
            listViewHosts.Columns.Add("Ping");
            listViewHosts.Columns.Add("Ports");
            listViewHosts.Columns.Add("MAC");
            listViewHosts.Columns.Add("Vendor");
            Scanner_Resize(sender, e);

            timerUI.Start();

            //get a list of vendors
            try
            {
                if (!File.Exists("oui.txt"))
                {
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(oui_DownloadStringCompleted);
                        client.DownloadStringAsync(new Uri("http://standards-oui.ieee.org/oui/oui.txt"));
                    }
                }
            }
            catch {/*not important, let the scan continue*/}
        }

        //**********************************************************************************************
        void oui_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            string text = e.Result;
            File.WriteAllText("oui.txt", text);
        }

        //**********************************************************************************************
        private async void buttonStart_Click(object sender, EventArgs e)
        {
            if (running)
            {
                cancel = true;
                return;
            }

            listViewHosts.Items.Clear();
            progressBar.Value = 0;

            try
            {
                IPAddress fromIP = IPAddress.Parse(ipAddressControlFrom.Text);
                IPAddress toIP = IPAddress.Parse(ipAddressControlTo.Text);

                if (fromIP.Address > toIP.Address)
                {
                    MessageBox.Show("to Address must be higher than from Address");
                    return;
                }

                Main.config.SetValue("LASTSCANFROMADDRESS", ipAddressControlFrom.Text);
                Main.config.SetValue("LASTSCANTOADDRESS", ipAddressControlTo.Text);
                Main.config.GetValue("LASTSCANPORTS", textTcpPorts.Text);
                running = true;


                byte[] fromOctets = fromIP.GetAddressBytes();
                byte[] toOctets = toIP.GetAddressBytes();

                //So ugly...
                int numberOfHosts = 0;
                for (int O1 = fromOctets[0]; O1 <= toOctets[0]; O1++)
                    for (int O2 = fromOctets[1]; O2 <= toOctets[1]; O2++)
                        for (int O3 = fromOctets[2]; O3 <= toOctets[2]; O3++)
                            for (int O4 = fromOctets[3]; O4 <= toOctets[3]; O4++)
                                numberOfHosts++;

                progressBar.Maximum = numberOfHosts;

                for (int O1 = fromOctets[0]; O1 <= toOctets[0]; O1++)
                {
                    for (int O2 = fromOctets[1]; O2 <= toOctets[1]; O2++)
                    {
                        for (int O3 = fromOctets[2]; O3 <= toOctets[2]; O3++)
                        {
                            for (int O4 = fromOctets[3]; O4 <= toOctets[3]; O4++)
                            {
                                if (cancel)
                                {
                                    cancel = false;
                                    return;
                                }
                                
                                string ip = $"{O1}.{O2}.{O3}.{O4}";
                                string foundports = checkTcpPorts.Checked ? "" : "not checked";

                                if (checkTcpPorts.Checked)
                                {
                                    await Task.Run(() =>
                                    {
                                        string[] ports = textTcpPorts.Text.Split(",");
                                        foreach (string port in ports)
                                        {
                                            if (TestTcpPort(ip, port.ToIntOrDefault(0)))
                                                foundports += port + ",";
                                        }
                                    });
                                }

                                bool ping = await PingHost(ip);
                                if (ping || foundports.Length > 0)
                                {
                                    ListViewItem item = new ListViewItem();
                                    item.ImageIndex = GetIcon(foundports);
                                    item.Text = ip;
                                    item.Tag = string.Empty;
                                    item.SubItems.Add(GetReverseDNS(ip));
                                    item.SubItems.Add(GetNetbiosName(ip));
                                    item.SubItems.Add(ping ? "yes" : "no");
                                    item.SubItems.Add(foundports);
                                    string mac = GetMacAddress(ip, out string vendor);
                                    item.SubItems.Add(mac);
                                    item.SubItems.Add(GetMACVendor(vendor));

                                    if (cancel)
                                    {
                                        cancel = false;
                                        return;
                                    }

                                    Invoke((Action)(() =>
                                    {
                                        listViewHosts.Items.Add(item);
                                        item.EnsureVisible();
                                    }));
                                }

                                progressBar.Increment(1);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                running = false;
            }
        }

        //**********************************************************************************************
        private int GetIcon(string ports)
        {
            if (ports.Contains("3389"))
                return 0;
            if (ports.Contains("22"))
                return 1;
            //imageList.Images.Add(global::XwRemote.Properties.Resources.vnc);
            if (ports.Contains("80") || ports.Contains("443"))
                return 3;
            if (ports.Contains("21"))
                return 4;
            return -1;
        }

        //**********************************************************************************************
        private void Scanner_Resize(object sender, EventArgs e)
        {
            if (listViewHosts.Columns.Count == 0)
                return;

            int colW = (listViewHosts.Width - 20) / listViewHosts.Columns.Count;
            for (int i = 0; i < listViewHosts.Columns.Count; i++)
            {
                listViewHosts.Columns[i].Width = colW;
            }
        }

        //**********************************************************************************************
        public async Task<bool> PingHost(string nameOrAddress)
        {
            //Jeeasssusss... 
            //https://stackoverflow.com/questions/17756824/blue-screen-when-using-ping
#if DEBUG
            return false;
#endif

            try
            {
                using (Ping pinger = new Ping())
                {
                    PingReply reply = await pinger.SendPingAsync(nameOrAddress);
                    return (reply.Status == IPStatus.Success);
                }
            }
            catch { return false; }
       }

        //**********************************************************************************************
        public bool TestTcpPort(string host, int port)
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                try
                {
                    IAsyncResult result = socket.BeginConnect(host, port, null, null);
                    result.AsyncWaitHandle.WaitOne(100, true);
                    return socket.Connected;
                }
                catch (Exception)
                {
                    return false;
                }
                finally
                {
                    socket.Close();
                }
            }
        }

        //**********************************************************************************************
        private void ipAddressControlFrom_TextChanged(object sender, EventArgs e)
        {
            try
            {
                IPAddress.Parse(ipAddressControlFrom.Text);
                ipAddressControlFrom.BackColor = SystemColors.Window;
            }
            catch
            {
                ipAddressControlFrom.BackColor = Color.LightSalmon;
            }
        }

        //**********************************************************************************************
        private void ipAddressControlTo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                IPAddress.Parse(ipAddressControlTo.Text);
                ipAddressControlTo.BackColor = SystemColors.Window;
            }
            catch
            {
                ipAddressControlTo.BackColor = Color.LightSalmon;
            }
        }

        //**********************************************************************************************
        private void checkTcpPorts_CheckedChanged(object sender, EventArgs e)
        {
            textTcpPorts.Enabled = checkTcpPorts.Checked;
        }

        //**********************************************************************************************
        private void Scanner_FormClosing(object sender, FormClosingEventArgs e)
        {
            cancel = true;
        }

        //**********************************************************************************************
        private void timerUI_Tick(object sender, EventArgs e)
        {
            ipAddressControlFrom.Enabled = !running;
            ipAddressControlTo.Enabled = !running;
            checkTcpPorts.Enabled = !running;
            textTcpPorts.Enabled = checkTcpPorts.Checked && !running;
            buttonStart.Text = running ? "Cancel" : "Start"; 
        }

        //**********************************************************************************************
        private delegate IPHostEntry GetHostEntryHandler(string ip);
        public string GetReverseDNS(string ip, int timeout = 100)
        {
            try
            {
                GetHostEntryHandler callback = new GetHostEntryHandler(Dns.GetHostEntry);
                IAsyncResult result = callback.BeginInvoke(ip, null, null);
                if (result.AsyncWaitHandle.WaitOne(timeout, false))
                {
                    return callback.EndInvoke(result).HostName;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        //**********************************************************************************************
        // The following byte stream contains the necessary message
        // to request a NetBios name from a machine
        static byte[] NameRequest = new byte[]{
            0x80, 0x94, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x20, 0x43, 0x4b, 0x41,
            0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41,
            0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41,
            0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41,
            0x41, 0x41, 0x41, 0x41, 0x41, 0x00, 0x00, 0x21,
            0x00, 0x01 };

        public string GetNetbiosName(string ipAddress)
        {
            byte[] receiveBuffer = new byte[1024];
            Socket requestSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            requestSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 1000);
            IPAddress[] addressList = Dns.GetHostAddresses(ipAddress);
            if (addressList.Length == 0)
            {
                return "";
            }

            EndPoint remoteEndpoint = new IPEndPoint(addressList[0], 137);
            IPEndPoint originEndpoint = new IPEndPoint(IPAddress.Any, 0);
            requestSocket.Bind(originEndpoint);
            requestSocket.SendTo(NameRequest, remoteEndpoint);
            try
            {

                int receivedByteCount = requestSocket.ReceiveFrom(receiveBuffer, ref remoteEndpoint);
                if (receivedByteCount >= 90)
                {
                    Encoding enc = new ASCIIEncoding();
                    string deviceName = enc.GetString(receiveBuffer, 57, 16).Trim();
                    string networkName = enc.GetString(receiveBuffer, 75, 16).Trim();
                    return $"{deviceName}";
                }
            }
            catch (SocketException)
            {
                return "";
            }

            return "";
        }

        //**********************************************************************************************
        public string GetMacAddress(string ipAddress, out string vendor)
        {
            string macAddress = string.Empty;
            System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
            pProcess.StartInfo.FileName = "arp";
            pProcess.StartInfo.Arguments = "-a " + ipAddress;
            pProcess.StartInfo.UseShellExecute = false;
            pProcess.StartInfo.RedirectStandardOutput = true;
            pProcess.StartInfo.CreateNoWindow = true;
            pProcess.Start();
            string strOutput = pProcess.StandardOutput.ReadToEnd();
            Regex reg = new Regex(@"(?i)\b(?<one>[0-9A-F]{2})(?<delimiter>[-:]?)(?<two>[0-9A-F]{2})\k<delimiter>(?<three>[0-9A-F]{2})\k<delimiter>(?<four>[0-9A-F]{2})\k<delimiter>(?<five>[0-9A-F]{2})\k<delimiter>(?<six>[0-9A-F]{2})\b", RegexOptions.Compiled);
            Match m = reg.Match(strOutput);
            macAddress = $"{m.Groups["one"]}:{m.Groups["two"]}:{m.Groups["three"]}:{m.Groups["four"]}:{m.Groups["five"]}:{m.Groups["six"]}";
            vendor = $"{m.Groups["one"]}-{m.Groups["two"]}-{m.Groups["three"]}";
            return macAddress.ToUpper();
        }

        //**********************************************************************************************
        string temporatyListOfVendors = null;
        public string GetMACVendor(string macVendorBytes)
        {
            if (temporatyListOfVendors == null)
            {
                try
                {
                    if (File.Exists("oui.txt"))
                    {
                        temporatyListOfVendors = File.ReadAllText("oui.txt");
                    }
                }
                catch 
                {
                    //not important, let the scan continue
                    temporatyListOfVendors = "";
                }
            }

            if (temporatyListOfVendors != "")
            {
                Regex reg = new Regex($@"(?ixm){macVendorBytes}\s*\(hex\)\s*(?<Vendor>.*?)$", RegexOptions.Compiled);
                Match m = reg.Match(temporatyListOfVendors);
                return m.Groups["Vendor"].ToString();
            }

            return "";
        }
    }
}
