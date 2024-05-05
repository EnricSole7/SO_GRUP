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
using Microsoft.Win32;

namespace WindowsFormsApplication1
{
    public partial class GameWndw : Form
    {
        Socket SERVER;
        string USER;
        public string creator;
        string datos_partida;
        string server_info;
        List<string> PLAYERS = new List<string>();
        int Nform;
        List<string> ListaConnectados = new List<string>();

        //List<string> Invitations = new List<string>();
        public GameWndw()
        {
            InitializeComponent();

            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //this.Bounds = Screen.PrimaryScreen.WorkingArea;
        }

        private void GameWndw_Load(object sender, EventArgs e)
        {

        }

        //
        //
        //  LOADERS DEL FORM
        //
        //
        public void SetLobby(int numForm, Socket serv, string user, List<string> listaconn)
        {
            this.Nform = numForm;
            this.SERVER = serv;
            this.USER = user;
            this.ListaConnectados = listaconn;
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
            this.server_info = server;
        }
        public void SetOtherPlayersOnJoining(string otherplayers)
        {

            otherplayers = otherplayers + " ";
            int i = 0;
            int separador;
            string intermid = null;

            while (i < otherplayers.Length)
            {
                separador = otherplayers.IndexOf(" ", i);
                while (i < separador)
                {
                    intermid += otherplayers[i];
                    i++;
                }
                if (intermid != null)
                {
                    this.PLAYERS.Add(intermid);
                }
                intermid = null;
                i++;
            }
            //afegim tots els altres usuaris (primer el host) i finalment l'usuari que s'esta unint
            this.PLAYERS.Add(this.USER);
        }

        public void RefreshConnectedList(List<string> listconn)
        {
            this.ListaConnectados = listconn;

            DelegateCREATECONECTADOSGRID del = new DelegateCREATECONECTADOSGRID(CREATECONECTADOSGRID);
            playersonlineGrid.Invoke(del);
        }

        private void GameWndw_Shown(object sender, EventArgs e)
        {
            DelegateSETCREATOR del1 = new DelegateSETCREATOR(SETCREATOR);
            player2_lbl.Invoke(del1, new object[] { this.creator });

            DelegateCREATEINVITATIONSGRID del2 = new DelegateCREATEINVITATIONSGRID(CREATEINVITATIONSGRID);
            invitationsgrid.Invoke(del2);

            DelegatePRINTSERVER del3 = new DelegatePRINTSERVER(PRINTSERVER);
            server_value.Invoke(del3, new object[] { this.server_info });

            DelegateINSERTPLAYERSINGAME del4 = new DelegateINSERTPLAYERSINGAME(INSERTPLAYERSINGAME);
            player1_lbl.Invoke(del4);

            DelegateCREATECONECTADOSGRID del5 = new DelegateCREATECONECTADOSGRID(CREATECONECTADOSGRID);
            playersonlineGrid.Invoke(del5);
        }

        //
        //
        //
        //
        //


        private void exitbtn_Click(object sender, EventArgs e)
        {
            if (creator != null)    //si tanca la partida el creador
            {
                string mensaje = "95/" + "1" + Nform + "/" + USER + "/" + datos_partida;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                SERVER.Send(msg);
            }
            else    //si tanca la partida un altre jugador 
            {
                string mensaje = "95/" + "0" + Nform + "/" + USER + "/" + datos_partida;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                SERVER.Send(msg);
            }


            this.Close();
        }

        //INVITE PLAYER
        private void playersonlineGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string invited;
            if (creator != null)    //NOMES POT CONVIDAR EL HOST DE LA PARTIDA
            {
                if (playersonlineGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    invited = playersonlineGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                    string mensaje = "97/" + Nform + "/" + USER + "/" + invited + "/" + datos_partida;
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    SERVER.Send(msg);
                }
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
        public void InvitationReceived(string inviting)
        {

            MessageBox.Show("Invitaion from " + inviting + " added to your client hub", "Client", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        public void PlayerJoined(string playerjoined, int position)
        {
            DelegatePLAYERJOINED del = new DelegatePLAYERJOINED(PLAYERJOINED);
            player2_lbl.Invoke(del, new object[] { playerjoined , position });
        }





        //DELEGATES

        delegate void DelegatePRINTSERVER(string server);
        public void PRINTSERVER(string server)
        {
            server_value.Text = server;
        }

        delegate void DelegateSETCREATOR(string creator);
        public void SETCREATOR(string creator)
        {
            player1_lbl.Text = creator;
        }

        delegate void DelegateCREATEINVITATIONSGRID();
        public void CREATEINVITATIONSGRID()
        {
            invitationsgrid.ColumnCount = 1;
            invitationsgrid.RowHeadersVisible = false;
            invitationsgrid.ColumnHeadersVisible = false;
            invitationsgrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            invitationsgrid.ForeColor = Color.White;
        }

        delegate void DelegateCREATECONECTADOSGRID();
        public void CREATECONECTADOSGRID()
        {
            playersonlineGrid.ColumnCount = 1;
            playersonlineGrid.Rows.Clear();
            playersonlineGrid.RowCount = ListaConnectados.Count /*- 1*/;
            playersonlineGrid.RowHeadersVisible = false;
            playersonlineGrid.ColumnHeadersVisible = false;
            playersonlineGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            int j = 0;
            int k = 0;
            while (j < ListaConnectados.Count)
            {
                if (ListaConnectados[j] != USER) //nomes mostrem els que no son l'usuari en questio
                {
                    playersonlineGrid.Rows[k].Cells[0].Value = ListaConnectados[j];
                    k++;
                }
                j++;
            }
        }

        delegate void DelegateINSERTPLAYERSINGAME();
        public void INSERTPLAYERSINGAME()
        {
            int i = 0;

            if (i < this.PLAYERS.Count)
            {
                player1_lbl.Text = this.PLAYERS[i];
                i++;
                if (i < this.PLAYERS.Count)
                {
                    player2_lbl.Text = this.PLAYERS[i];
                    i++;
                    if (i < this.PLAYERS.Count)
                    {
                        player3_lbl.Text = this.PLAYERS[i];
                        i++;
                        if (i < this.PLAYERS.Count)
                        {
                            player4_lbl.Text = this.PLAYERS[i];
                            i++;
                            if (i < this.PLAYERS.Count)
                            {
                                player5_lbl.Text = this.PLAYERS[i];
                            }
                        }
                    }
                }
            }
        }

        delegate void DelegatePLAYERJOINED(string player, int position);
        public void PLAYERJOINED(string player, int position)
        {
            if(position == 2)
            {
                player2_lbl.Text = player;
            }
            if (position == 3)
            {
                player3_lbl.Text = player;
            }
            if (position == 4)
            {
                player4_lbl.Text = player;
            }
            if (position == 5)
            {
                player5_lbl.Text = player;
            }
        }
    }
}
