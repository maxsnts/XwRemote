using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using XwMaxLib.Extensions;

namespace XwRemote.Misc
{
    public partial class Scanner : Form
    {
        enum State
        { 
            Stopped,
            Running,
            Canceling
        }

        private ImageList imageList = new ImageList();
        State state = State.Stopped;
        int maxRunningTasks = 10;
        int maxConnTimetout = 100;
        int curRunningTasks = 0;
        bool useARP = false;
        string localIps = "";

        //*************************************************************************************************************
        public Scanner()
        {
            imageList.Images.Add(global::XwRemote.Properties.Resources.play);   //0
            imageList.Images.Add(global::XwRemote.Properties.Resources.rdp);    //1
            imageList.Images.Add(global::XwRemote.Properties.Resources.ssh);    //2
            imageList.Images.Add(global::XwRemote.Properties.Resources.vnc);    //3
            imageList.Images.Add(global::XwRemote.Properties.Resources.IE);     //4
            imageList.Images.Add(global::XwRemote.Properties.Resources.ftp);    //5
            imageList.Images.Add(global::XwRemote.Properties.Resources.help);   //6
            imageList.Images.Add(global::XwRemote.Properties.Resources.error);  //7
            imageList.Images.Add(global::XwRemote.Properties.Resources.favs);   //8
            InitializeComponent();
        }

        //*************************************************************************************************************
        private void Scanner_Load(object sender, EventArgs e)
        {
            ipAddressControlFrom.Text = Main.config.GetValue("LASTSCANFROMADDRESS");
            ipAddressControlTo.Text = Main.config.GetValue("LASTSCANTOADDRESS");
            checkBoxHideDead.Checked = Main.config.GetValue("LASTSCANHIDEDEAT", "true").ToBoolOrDefault(true);
            checkTcpPorts.Checked = Main.config.GetValue("LASTSCANTESTPORTS", "true").ToBoolOrDefault(false);
            checkDNS.Checked = Main.config.GetValue("LASTSCANCHECKDNS", "true").ToBoolOrDefault(true);
            checkNetBios.Checked = Main.config.GetValue("LASTSCANCHECKNETBIOS", "true").ToBoolOrDefault(true);
            textTcpPorts.Text = Main.config.GetValue("LASTSCANPORTS", "");
            if (textTcpPorts.Text == "")
                textTcpPorts.Text = "80,443,21,22,3389,5900";
            numericMaxThreads.Value = Main.config.GetValue("LASTSCANMAXTRH", "20").ToIntOrDefault(20);
            numericTestTimeout.Value = Main.config.GetValue("LASTSCANCONNTIMEOUT", "4000").ToIntOrDefault(4000);

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
            localIps = GetAllLocalIPAddress();

            Pump.Start();
        }

        //*************************************************************************************************************
        private void oui_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            BeginInvoke((Action)(() =>
            {
                progressBar.Value = e.ProgressPercentage;
            }));
        }

        //*************************************************************************************************************
        private void Client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            File.WriteAllText("oui.txt", e.Result);
            Start();
        }

        //*************************************************************************************************************
        private void Pump_Tick(object sender, EventArgs e)
        {
            if (state == State.Canceling && curRunningTasks == 0)
            {
                state = State.Stopped;
            }

            if (state == State.Running)
                Scan();

            ipAddressControlFrom.Enabled = (state == State.Stopped);
            ipAddressControlTo.Enabled = (state == State.Stopped);
            checkTcpPorts.Enabled = (state == State.Stopped);
            checkBoxHideDead.Enabled = (state == State.Stopped);
            checkDNS.Enabled = (state == State.Stopped);
            checkNetBios.Enabled = (state == State.Stopped);
            numericMaxThreads.Enabled = (state == State.Stopped);
            textTcpPorts.Enabled = checkTcpPorts.Checked && (state == State.Stopped);
            numericTestTimeout.Enabled = checkTcpPorts.Checked && (state == State.Stopped);
            buttonStartARP.Enabled = (state == State.Running || state == State.Stopped);
            buttonStartNoARP.Enabled = (state == State.Running || state == State.Stopped);

#if DEBUG
            buttonStartARP.Text = $"LAN -> {state.ToString()} ({curRunningTasks})";
            buttonStartNoARP.Text = $"WAN -> {state.ToString()} ({curRunningTasks})";
#else
            buttonStartARP.Text = (state == State.Stopped) ? "Start for LAN (use ARP, faster)" : $"Cancel ({curRunningTasks})";
            buttonStartNoARP.Text = (state == State.Stopped) ? "Start for WAN (no ARP, slower)" : $"Cancel ({curRunningTasks})";
#endif
        }

        //*************************************************************************************************************
        private void buttonStart_ARP_Click(object sender, EventArgs e)
        {
            useARP = true;
            Init();
        }

        //*************************************************************************************************************
        private void buttonStart_NoARP_Click(object sender, EventArgs e)
        {
            useARP = false;
            Init();
        }

        //*************************************************************************************************************
        private void Init()
        {
            if (state == State.Canceling)
                return;

            if (state == State.Running)
            {
                state = State.Canceling;
                progressBar.Value = 0;
                return;
            }

            try
            {
                if (!File.Exists("oui.txt"))
                {
                    MessageBox.Show("We need to download the MAC address info (one time)");

                    using (WebClient client = new WebClient())
                    {
                        client.DownloadStringCompleted += Client_DownloadStringCompleted;
                        client.DownloadProgressChanged += oui_DownloadProgressChanged;
                        client.DownloadStringAsync(new Uri("http://standards-oui.ieee.org/oui/oui.txt"));
                    }
                }
                else
                {
                    Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to download networkcard information.\n\n{ex.Message}", "NIC info");
            }
        }

        //*************************************************************************************************************
        private void Start()
        {
            listViewHosts.Items.Clear();
            progressBar.Value = 0;
            maxRunningTasks = numericMaxThreads.Value.ToIntOrDefault(10);
            maxConnTimetout = numericTestTimeout.Value.ToIntOrDefault(100);

            try
            {
                IPAddress fromIP = IPAddress.Parse(ipAddressControlFrom.Text);
                IPAddress toIP = IPAddress.Parse(ipAddressControlTo.Text);

                if (fromIP.Address > toIP.Address)
                {
                    MessageBox.Show("to Address must be higher than from Address");
                    return;
                }

                state = State.Running;

                Main.config.SetValue("LASTSCANFROMADDRESS", ipAddressControlFrom.Text);
                Main.config.SetValue("LASTSCANTOADDRESS", ipAddressControlTo.Text);
                Main.config.SetValue("LASTSCANPORTS", textTcpPorts.Text);
                Main.config.SetValue("LASTSCANHIDEDEAT", checkBoxHideDead.Enabled.ToString());
                Main.config.SetValue("LASTSCANTESTPORTS", checkTcpPorts.Enabled.ToString());
                Main.config.SetValue("LASTSCANMAXTRH", numericMaxThreads.Value.ToString());
                Main.config.SetValue("LASTSCANCONNTIMEOUT", numericTestTimeout.Value.ToString());
                Main.config.SetValue("LASTSCANCHECKDNS", checkDNS.Checked.ToString());
                Main.config.SetValue("LASTSCANCHECKNETBIOS", checkNetBios.Checked.ToString());


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

                SuspendLayout();
                for (int O1 = fromOctets[0]; O1 <= toOctets[0]; O1++)
                {
                    for (int O2 = fromOctets[1]; O2 <= toOctets[1]; O2++)
                    {
                        for (int O3 = fromOctets[2]; O3 <= toOctets[2]; O3++)
                        {
                            for (int O4 = fromOctets[3]; O4 <= toOctets[3]; O4++)
                            {
                                if (state == State.Canceling)
                                    return;
                                
                                string ip = $"{O1}.{O2}.{O3}.{O4}";

                                ListViewItem item = new ListViewItem();
                                item.Text = ip;
                                item.Tag = false;
                                item.SubItems.Add("");
                                item.SubItems.Add("");
                                item.SubItems.Add("");
                                item.SubItems.Add("");
                                item.SubItems.Add("");
                                item.SubItems.Add("");
                                listViewHosts.Items.Add(item);
                            }
                        }
                    }
                }
                ResumeLayout();
                Scan();
            }
            catch (Exception ex)
            {
                state = State.Canceling;
                MessageBox.Show(ex.Message);
            }
        }

        //*************************************************************************************************************
        private void Scan()
        {
            ThreadPool.SetMinThreads(maxRunningTasks, maxRunningTasks);
            
            bool alldone = true;
            foreach (ListViewItem item in listViewHosts.Items)
            {
                if (state == State.Canceling)
                    return;

                if (curRunningTasks >= maxRunningTasks)
                    return;

                if (item.Tag.ToBoolOrDefault(false) == true)
                    continue;

                item.Tag = true;
                curRunningTasks++;
                Task.Run(() => ScanIp(item));
                alldone = false;
            }

            if (alldone)
                state = State.Stopped;
        }

        //*************************************************************************************************************
        private async void ScanIp(ListViewItem item)
        {
            try
            {
                Invoke((Action)(() =>
                {
                    item.ImageIndex = (state == State.Canceling) ? -1: 0;
                }));

                if (state == State.Canceling)
                    return;

                string ip = item.Text;

                //local IP?
                if (localIps.Contains($" {ip} "))
                {
                    Invoke((Action)(() =>
                    {
                        item.ImageIndex = 8;
                        item.SubItems[1].Text = GetReverseDNS(ip);
                        item.SubItems[2].Text = GetNetbiosName(ip);
                        item.SubItems[3].Text = "This Machine";
                        item.SubItems[4].Text = "Not Tested";
                        item.SubItems[5].Text = "";
                        item.SubItems[6].Text = "";
                        item.EnsureVisible();
                        Update();
                    }));
                    return;
                }

                if (state == State.Canceling)
                    return;

                string foundports = " ";
                string mac = "";
                bool ping = false;
                string dns = "";
                string netbios = "";
                string vendor = "";

                if (useARP)
                {
                    //this is just to force a apr cache refresh
                    //its way faster than using ping
                    TestTcpPort(ip, 1);
                }

                if (state == State.Canceling)
                    return;

                mac = GetMacAddress(ip);
                if (mac != "" || !useARP)
                {
                    if (state == State.Canceling)
                        return;

                    ping = PingHost(ip);

                    if (state == State.Canceling)
                        return;

                    if (checkTcpPorts.Checked)
                    {
                        await Task.Run(() =>
                        {
                            string[] ports = textTcpPorts.Text.Split(",");
                            foreach (string port in ports)
                            {
                                if (TestTcpPort(ip, port.ToIntOrDefault(0)))
                                    foundports += port + " ";
                            }
                        });
                    }

                    if (state == State.Canceling)
                        return;

                    if (checkDNS.Checked)
                        dns = GetReverseDNS(ip, maxConnTimetout);

                    if (state == State.Canceling)
                        return;

                    if (checkNetBios.Checked)
                        netbios = GetNetbiosName(ip, maxConnTimetout);

                    if (state == State.Canceling)
                        return;

                    vendor = GetMACVendor(mac);
                }

                if (state == State.Canceling)
                    return;

                bool dead = ping == false && foundports.Trim() == "" && mac == "" && (netbios == "" || netbios == "- - -");
                int image = GetIcon(foundports);

                if (state == State.Canceling)
                    return;

                Invoke((Action)(() =>
                {
                    if (dead)
                    {
                        if (checkBoxHideDead.Checked)
                            item.Remove();
                        else
                            item.ImageIndex = 7;
                    }
                    else
                    {
                        item.ImageIndex = image;
                        item.SubItems[1].Text = dns;
                        item.SubItems[2].Text = netbios;
                        item.SubItems[3].Text = ping ? "YES" : "no";
                        item.SubItems[4].Text = foundports;
                        item.SubItems[5].Text = mac;
                        item.SubItems[6].Text = vendor;
                        item.EnsureVisible();
                        Update();
                    }
                    progressBar.Increment(1);
                    }));
            }
            catch (Exception ex)
            {
                //Windows was closed with running scan.
                if (ex.Message.Contains("BeginInvoke"))
                    return;
            }
            finally
            {
                curRunningTasks--;
            }
        }

        //*************************************************************************************************************
        private int GetIcon(string ports)
        {
            if (ports.Contains(" 3389 "))
                return 1;
            if (ports.Contains(" 5900 "))
                return 3;
            if (ports.Contains(" 22 "))
                return 2;
            if (ports.Contains(" 80 ") || ports.Contains(" 443 "))
                return 4;
            if (ports.Contains(" 21 "))
                return 5;
            return 6;
        }

        //*************************************************************************************************************
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

        //*************************************************************************************************************
        public bool PingHost(string nameOrAddress)
        {
            //Jeeasssusss... 
            //https://stackoverflow.com/questions/17756824/blue-screen-when-using-ping
//#if DEBUG
//            return false;
//#endif
            try
            {
                using (Ping pinger = new Ping())
                {
                    PingReply reply = pinger.Send(nameOrAddress);
                    return (reply.Status == IPStatus.Success);
                }
            }
            catch { return false; }
        }

        //*************************************************************************************************************
        public bool TestTcpPort(string host, int port)
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.NoDelay, 1);

                try
                {
                    IAsyncResult result = socket.BeginConnect(host, port, null, null);
                    result.AsyncWaitHandle.WaitOne(maxConnTimetout, true);
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

        //*************************************************************************************************************
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

        //*************************************************************************************************************
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

        //*************************************************************************************************************
        private void checkTcpPorts_CheckedChanged(object sender, EventArgs e)
        {
            textTcpPorts.Enabled = checkTcpPorts.Checked;
        }

        //*************************************************************************************************************
        private void Scanner_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (state == State.Running)
            {
                state = State.Canceling;
            }
        }

        //*************************************************************************************************************
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

        //*************************************************************************************************************
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

        public string GetNetbiosName(string ipAddress, int timeout = 100)
        {
            byte[] receiveBuffer = new byte[1024];
            Socket requestSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            requestSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, timeout);
            requestSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, timeout);
            requestSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.NoDelay, 1);
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
            catch (SocketException ex)
            {
                return "- - -";
            }
            return "";
        }

        //*************************************************************************************************************
        public string GetMacAddress(string ipAddress)
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
            return macAddress == ":::::" ? "" : macAddress.ToUpper();
        }

        //*************************************************************************************************************
        string temporatyListOfVendors = null;
        public string GetMACVendor(string mac)
        {
            string[] b = mac.Split(":");
            if (b.Length != 6)
                return "";

            string vendor = $"{b[0]}-{b[1]}-{b[2]}";

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
                Regex reg = new Regex($@"(?ixm){vendor}\s*\(hex\)\s*(?<Vendor>.*?)$", RegexOptions.Compiled);
                Match m = reg.Match(temporatyListOfVendors);
                return m.Groups["Vendor"].ToString();
            }

            return "";
        }

        //*************************************************************************************************************
        public string GetAllLocalIPAddress()
        {
            string ips = " ";
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ips += ip.ToString() + " ";
                }
            }
            return ips;
        }
    }
}
