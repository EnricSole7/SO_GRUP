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
        public void SetOtherPlayersOnJoining(string otherplayers)   //funcio NOMES per al jugador que s'esta unint a la partida
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
            //afegim tots els altres usuaris (primer el host) i finalment l'usuari que s'esta unint a la seva llista
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

            DelegatePRINTSERVER del2 = new DelegatePRINTSERVER(PRINTSERVER);
            server_value.Invoke(del2, new object[] { this.server_info, this.Nform });

            DelegateINSERTPLAYERSINGAME del3 = new DelegateINSERTPLAYERSINGAME(INSERTPLAYERSINGAME);
            player1_lbl.Invoke(del3);

            DelegateCREATECONECTADOSGRID del4 = new DelegateCREATECONECTADOSGRID(CREATECONECTADOSGRID);
            playersonlineGrid.Invoke(del4);
        }
        //
        //
        //
        //
        //

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

        private void exitbtn_Click(object sender, EventArgs e)
        {
            string listplayers = null;
            int playerscount = PLAYERS.Count;

            foreach (string player in PLAYERS)  //guardem la llista de jugadors de la partida en un string parametre pel servidor (inclou host, altres i el user)
            {
                if (player != USER)
                    listplayers += player + " ";
            }

            if (creator != null)    //si tanca la partida el creador (host)
            {
                if (listplayers == null) //vol dir que nomes hi ha el host a la partida i que la tanca
                {
                    listplayers = "-";
                }
                string mensaje = "95/" + "1/" + Nform + "/" + USER + "/" + datos_partida + "/" + listplayers + "/" + playerscount;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                SERVER.Send(msg);
            }
            else    //si tanca la partida un altre jugador 
            {
                string mensaje = "95/" + "0/" + Nform + "/" + USER + "/" + datos_partida + "/" + listplayers + "/" + playerscount;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                SERVER.Send(msg);
            }

            PLAYERS.Clear();    //netegem la llista de jugadors del jugador que marxa
            this.Close();
        }

        public void PlayerLeft(int result, string disconnected)
        {
            if (result == 1)    //tanca partida el creador (informem als altres usuaris)
            {
                MessageBox.Show("Host " + disconnected + " has exited to lobby. Game closing", "Client", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            else if (result == 0)   //marxa de la partida un altre usuari (informem als altres usuaris i al host)
            {
                MessageBox.Show("Player " + disconnected + " disconnected. Waiting for additional player", "Client", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //la llista de jugadors ja s'ha actualitzat al crear el missatge al servidor, nomes queda actualitzar-la visualment

                this.PLAYERS.Remove(disconnected);  //treiem el jugador desconnectat de la llista
                DelegateINSERTPLAYERSINGAME del = new DelegateINSERTPLAYERSINGAME(INSERTPLAYERSINGAME);
                player1_lbl.Invoke(del);
            }
        }


        //DELEGATES

        delegate void DelegatePRINTSERVER(string server, int numform);
        public void PRINTSERVER(string server, int numform)
        {
            server_value.Text = server;
            form_value.Text = numform.ToString();
        }

        delegate void DelegateSETCREATOR(string creator);
        public void SETCREATOR(string creator)
        {
            player1_lbl.Text = creator;
            if (creator != null)    //es null quan no es el host qui modifica el seu valor
            {
                this.PLAYERS.Add(creator);  //afegim el creador (host) a la seva propia llista de jugadors
            }
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
            //primer resetegem els valors dels labels
            player1_lbl.Text = null;
            player2_lbl.Text = null;
            player3_lbl.Text = null;
            player4_lbl.Text = null;
            player5_lbl.Text = null;

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
            this.PLAYERS.Add(player);   //afegim el jugador que s'uneix a la llista dels jugadors que ja estan a la partida

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
