using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace WindowsFormsApplication1
{
    public partial class GameWndw : Form
    {
        Socket SERVER;
        string USER;
        public GameWndw()
        {
            InitializeComponent();

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.WorkingArea;
        }

        private void GameWndw_Load(object sender, EventArgs e)
        {

        }

        public void SetGame(Socket serv, string user)
        {
            SERVER = serv;
            USER = user;
        }
    }
}
