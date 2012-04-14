using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using SteamKit2;
using Vapor.State;

namespace Vapor
{
    partial class LoginDialog : Form
    {
        private bool useUdp;
        private Settings settings;

        public LoginDialog(Settings settings)
        {
            try
            {
                this.settings = settings;
                InitializeComponent();

                this.useUdp = settings.Main.steam3_useUdp;
                txtUser.Text = settings.Main.username;
                ActiveControl = txtUser;
            }
            catch (Exception ex)
            {
                new ErrorDialog(ex).ShowDialog();
            }
        }

        private void btnLogin_Click( object sender, EventArgs e )
        {
            this.Enabled = false;

            try
            {
                Steam3.UserName = txtUser.Text;
                Steam3.Password = txtPass.Text;

                Steam3.Initialize(settings);

                Steam3.Connect();
            }
            catch ( Steam3Exception ex )
            {
                Util.MsgBox( this, "Unable to login to Steam3: " + ex.Message );
                this.Enabled = true;
                return;
            }

            settings.Main.username = Steam3.UserName;
            var save = new SaveSettings();
            save.Save(settings);

            DialogResult = DialogResult.OK;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
