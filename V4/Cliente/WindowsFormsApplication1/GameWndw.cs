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
using System.ComponentModel.Design.Serialization;

namespace WindowsFormsApplication1
{
    public partial class GameWndw : Form
    {
        Socket SERVER;
        string USER;
        string datos_partida;
        int Nform;
        List<string> ListaConnectados = new List<string>();

        List<string> Invitations = new List<string>();
        public GameWndw()
        {
            InitializeComponent();

            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //this.Bounds = Screen.PrimaryScreen.WorkingArea;

            invitationsgrid.ColumnCount = 1;
            invitationsgrid.RowHeadersVisible = false;
            invitationsgrid.ColumnHeadersVisible = false;
            invitationsgrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            invitationsgrid.ForeColor = Color.White;
        }

        private void GameWndw_Load(object sender, EventArgs e)
        {

        }

        //
        //
        //  LOADERS DEL FROM
        //
        //
        public void SetLobby(int numForm, Socket serv, string user, List<string> invitations)
        {
            this.Nform = numForm;
            this.SERVER = serv;
            this.USER = user;
            this.Invitations = invitations;

            InvitationReceived(Invitations);
        }

        public void SetGame(string partida)
        {
            this.datos_partida = partida;

            int i = 0;
            int separador = partida.IndexOf("|", i);
            string server = null;

            while (i < separador)
            {
                server += partida[i];
                i++;
            }

            //DelegatePRINTSERVER del = new DelegatePRINTSERVER(PRINTSERVER);
            //server_value.Invoke(del, new object[] { server });
            server_value.Text = server;
        }
        public void RefreshConnectedList(List<string> listconn)
        {
            this.ListaConnectados = listconn;

            playersonlineGrid.RowCount = ListaConnectados.Count;
            playersonlineGrid.RowHeadersVisible = false;
            playersonlineGrid.Columns[0].HeaderText = "PLAYERS ONLINE";
            playersonlineGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            int i = 0;
            while (i < ListaConnectados.Count)
            {
                if (ListaConnectados[i] != USER) //nomes mostrem els que no son l'usuari en questio
                {
                    playersonlineGrid.Rows[i].Cells[0].Value = ListaConnectados[i];
                }
                i++;
            }
        }
        //
        //
        //
        //
        //


        private void exitbtn_Click(object sender, EventArgs e)
        {

            this.Close();

        }

        //INVITE PLAYER
        private void playersonlineGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string invited;

            if (playersonlineGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                invited = playersonlineGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                string mensaje = "97/" + Nform + "/" + USER + "/" + invited;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                SERVER.Send(msg);
            }
        }

        //POTS TENIR UNA LLISTA D'INVITACIONS REBUDES (List<string>) PERO NOMES POTS FER UNA INVITACIO A LA VEGADA (string)

        //REBEM SI LA NOSTRA INVITACIÓ HA ANAT BÉ O NO
        public void InvitationSent(string invited)
        {
            if(invited != "ERROR")
            {
                MessageBox.Show("Invitaion to " + invited + " received correctly", "Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Invitaion to " + invited + " not received", "Client", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        //REBEM UNA INVITACIO 
        public void InvitationReceived(List<string> invitations)
        {
            this.Invitations = invitations;

            int i = 0;

            while(i < Invitations.Count)
            {
                invitationsgrid.Rows[i].Cells[0].Value = Invitations[i];
                i++;
            }

            //FALTA FER UN REMOVE.AT DE LA LLISTA D'INVITATIONS QUAN ACCEPTES UNA INVITACIO (EN UNA ALTRA FUNCIO)
        }

        //DELEGATES

        delegate void DelegatePRINTSERVER(string server);
        public void PRINTSERVER(string server)
        {
            server_value.Text = server;
        }
    }
}
