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
        List<string> ListaConnectados = new List<string>();
        public GameWndw()
        {
            InitializeComponent();

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.WorkingArea;

            int i = 0;
            while(i < ListaConnectados.Count)
            {
                InvitePlayersBOX.Items.Insert(i, ListaConnectados[i]);
                i++;
            }
        }

        private void GameWndw_Load(object sender, EventArgs e)
        {

        }

        public void SetGame(Socket serv, string user, List<string> listconn)
        {
            SERVER = serv;
            USER = user;
            ListaConnectados = listconn;
        }
    }
}
