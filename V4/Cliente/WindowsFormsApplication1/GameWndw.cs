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
using System.Runtime.CompilerServices;

namespace WindowsFormsApplication1
{
    public partial class GameWndw : Form
    {
        Socket SERVER;
        string USER;
        public string creator;
        string datos_partida;
        string server_info;
        int Nform;
        
        List<string> PLAYERS = new List<string>();
        List<string> ListaConnectados = new List<string>();

        string minigame = null;
        bool symbols_checked = false;
        bool maze_checked = false;
        bool tbd_checked = false;

        //SYMBOLS
        List<int> vectorimatges_host =  new List<int> { 0 };
        List<int> vectorimatges_jugador = new List<int> { 0 };

        //List<string> Invitations = new List<string>();
        public GameWndw()
        {
            InitializeComponent();

            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //this.Bounds = Screen.PrimaryScreen.WorkingArea;

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
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

                DelegateCLOSE del = new DelegateCLOSE(CLOSE);
                this.Invoke(del);
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

        private void checkSymbols_CheckedChanged(object sender, EventArgs e)
        {
            symbols_checked = true;
            maze_checked = false;
            tbd_checked = false;
            minigame = "SYMBOLS";
        }

        private void checkMaze_CheckedChanged(object sender, EventArgs e)
        {
            symbols_checked = false;
            maze_checked = true;
            tbd_checked = false;
            minigame = "MAZE";
        }

        private void checkTBD_CheckedChanged(object sender, EventArgs e)
        {
            symbols_checked = false;
            maze_checked = false;
            tbd_checked = true;
            minigame = "TBD";
        }

        private void startgamebtn_Click(object sender, EventArgs e)
        {
            if ((minigame != null) && (creator != null) && (minigame != "TBD") && (minigame != "MAZE")) 
            {
                string listplayers = null;
                int playerscount = PLAYERS.Count;

                foreach (string player in PLAYERS)  //guardem la llista de jugadors de la partida en un string parametre pel servidor (inclou host, altres i el user)
                {
                    listplayers += player + " ";
                }
                string mensaje = "94/" + Nform + "/" + USER + "/" + datos_partida + "/" + listplayers + "/" + playerscount + "/" + minigame;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                SERVER.Send(msg);
            }
            else
            {
                MessageBox.Show("Select a minigame before starting", "Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void StartGameSymbols(string vectorimatges, int destinat)
        {
            if (destinat == 0)  //destinat al host
            {
                vectorimatges = vectorimatges + " ";
                int j = 0;
                int separador;
                string intermid = null;

                while (j < vectorimatges.Length)
                {
                    separador = vectorimatges.IndexOf(" ", j);
                    while (j < separador)
                    {
                        intermid += vectorimatges[j];
                        j++;
                    }
                    if (intermid != null)
                    {
                        this.vectorimatges_host.Add(Convert.ToInt32(intermid));
                    }
                    intermid = null;
                    j++;
                }
            }
            else if (destinat == 1)
            {
                //de la llista de totes les imatges de la partida, assignem al jugador en concret només les que li pertoquen
                //per exemple, si son 4 players (1 host i 3 jugadors), el host tindra 3 imatges que hauran de buscar els jugaodors, mentre
                //que els jugadors tindran 5 * 3 = 15 imatges en total del vectorimatges (5 per cada un) -> els jugadors sempre tindran 5 imatges cadascu,
                //pero depenent del numero de jugadors que siguin, el host en tindrà equivalentment el mateix nombre (si son 3 jugadors, el host tindra 3 imatges)
                //per a desxifrar

                vectorimatges = vectorimatges + " ";
                int position_index = PLAYERS.IndexOf(USER); //o 1 o 2 o 3 o 4 (el 0 es el host)

                List<int> imatges_cpy = new List<int>();  //contindra totes les imatges que s'hauran de repartir entre jugadors

                int j = 0;
                int separador;
                string intermid = null;

                while (j < vectorimatges.Length)
                {
                    separador = vectorimatges.IndexOf(" ", j);
                    while (j < separador)
                    {
                        intermid += vectorimatges[j];
                        j++;
                    }
                    if (intermid != null)
                    {
                        imatges_cpy.Add(Convert.ToInt32(intermid));
                    }
                    intermid = null;
                    j++;
                }

                //un cop tenim tots els numeros d'imatge en el vector, busquem els que corresponen al jugador
                j = 5 * position_index - 1;
                while (j < j + 5)
                {
                    this.vectorimatges_jugador.Add(imatges_cpy[j]);
                    j++;
                }

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
            if (creator != null)    //es null quan no es el host qui modifica el seu valor
            {
                player1_lbl.Text = creator;
                this.PLAYERS.Add(creator);  //afegim el creador (host) a la seva propia llista de jugadors
            }
            else if (creator == null)
            {
                minigameBox.Enabled = false;    //NOMES EL HOST POT SELECCIONAR EL JOC
            }
        }

        delegate void DelegateCREATECONECTADOSGRID();
        public void CREATECONECTADOSGRID()
        {
            playersonlineGrid.ColumnCount = 1;
            playersonlineGrid.Rows.Clear();
            playersonlineGrid.RowCount = ListaConnectados.Count - 1;
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
            player1_lbl.Text = "<<<<>>>>";
            player2_lbl.Text = "<<<<>>>>";
            player3_lbl.Text = "<<<<>>>>";
            player4_lbl.Text = "<<<<>>>>";
            player5_lbl.Text = "<<<<>>>>";

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

        delegate void DelegateCLOSE();
        public void CLOSE()
        {
            this.Close();
        }
    }
}
