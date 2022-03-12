using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Windows.Forms;
using XwMaxLib.Extensions;
using XwRemote.Settings;

namespace XwRemote.Misc
{
    public partial class InOut : Form
    {
        private bool import = false;
        private Server server = null;

        //*************************************************************************************************************
        public InOut(bool import, Server server)
        {
            InitializeComponent();
            this.import = import;
            this.server = server;
        }

        //*************************************************************************************************************
        private void InOut_Load(object sender, EventArgs e)
        {
            if (import)
            {
                Text = "Import server";
            }
            else
            {
                Text = "Export server";
                buttonOK.Visible = false;
                buttonCancel.Text = "Close";

                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;
                JsonBox.Text = JsonConvert.SerializeObject(server, Formatting.Indented, settings);
            }
        }

        //*************************************************************************************************************
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        //*************************************************************************************************************
        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                JObject parsed = JObject.Parse(JsonBox.Text);
                if (parsed == null)
                {
                    MessageBox.Show("Unable to read server, some error in the json", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                server = Server.GetServerInstance((ServerType)parsed["Type"].ToIntOrDefault(0));
                JsonConvert.PopulateObject(JsonBox.Text, server);
                server.ID = 0;
                Main.config.SaveServer(server);
            }
            catch
            {
                MessageBox.Show("Unable to read server, some error in the json", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Close();
        }
    }
}
