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
using System.Linq.Expressions;

namespace WindowsFormsApplication1
{
    public partial class GameWindow : Form
    {
        Socket SERVER;
        string USER;
        public string creator;
        string datos_partida;
        string server_info;
        int Nform;
        
        List<string> PLAYERS = new List<string>();
        List<string> ListaConnectados = new List<string>();
        
        const int chatcount = 18;
        List<string> Chat = new List<string>();
        bool resettext = true;

        string minigame = null;
        bool symbols_checked = false;
        bool maze_checked = false;
        bool tbd_checked = false;

        bool gamestarted = false;

        //SYMBOLS
        List<int> vectorimatges_host = new List<int>();
        List<int> vectorposicions = new List<int>();
        List<int> vectorimatges_jugador = new List<int>();
        List<int> posicions_picturebox = new List<int>();
        int contador_errors = 3;
        int contador_found = 0;
        int round = 0;

        //List<string> Invitations = new List<string>();
        public GameWindow()
        {
            InitializeComponent();

            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //this.Bounds = Screen.PrimaryScreen.WorkingArea;

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            
            for (int j = 0; j < chatcount; j++)
            {
                Chat.Add(null);
            }

            this.symbolsBox.Visible = false;
            this.endgamebtn.Visible = false;
            this.roundlbl.Visible = false;
        }

        //
        //
        ////////////////////////////////////////////////////    LOADERS     ///////////////////////////////////////////////////////////////////////////////////
        //
        //
        private void GameWndw_Load(object sender, EventArgs e)
        {

        }
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


        ////////////////////////////////////////////////////    INVITATIONS     ///////////////////////////////////////////////////////////////////////////////////
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
        //REBEM SI LA NOSTRA INVITACIÓ HA ANAT BÉ O NO
        public void InvitationSent(string invited)
        {
            if(invited != "ERROR")
            {
                MessageBox.Show("Invitaion to " + invited + " received correctly", "Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Invitaion to " + invited + " not received", "Game", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        //REBEM UNA INVITACIO 
        public void InvitationReceived(string inviting)
        {

            MessageBox.Show("Invitaion from " + inviting + " added to your client hub", "Game", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        ////////////////////////////////////////////////////    JOIN / LEAVE GAME     /////////////////////////////////////////////////////////////////////////////
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

            DelegateCLOSE del = new DelegateCLOSE(CLOSE);
            this.Invoke(del);
        }

        public void PlayerLeft(int result, string disconnected)
        {
            if (result == 1)    //tanca partida el creador (informem als altres usuaris)
            {
                MessageBox.Show("Host " + disconnected + " has exited to lobby. Game closing", "Game", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DelegateCLOSE del = new DelegateCLOSE(CLOSE);
                this.Invoke(del);
            }
            else if (result == 0)   //marxa de la partida un altre usuari (informem als altres usuaris i al host)
            {
                //la llista de jugadors ja s'ha actualitzat al crear el missatge al servidor, nomes queda actualitzar-la visualment

                this.PLAYERS.Remove(disconnected);  //treiem el jugador desconnectat de la llista
                DelegateINSERTPLAYERSINGAME del = new DelegateINSERTPLAYERSINGAME(INSERTPLAYERSINGAME);
                player1_lbl.Invoke(del);

                if (gamestarted == true)    //si la partida ha començat, fem shuffle de la ronda actual
                {
                    RoundManagement(disconnected, 1);  //actualitzacio de canvi de ronda quan algu marxa
                }
                else if (gamestarted == false)  //sino, nomes informem que ha amrxat un jugador
                {
                    MessageBox.Show("Player " + disconnected + " disconnected", "Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        ////////////////////////////////////////////////////    CHAT MSG     ////////////////////////////////////////////////////////////////////////////////////////
        private void sendmsgbtn_Click(object sender, EventArgs e)
        {
            if ((messageTxt.Text != "Message:") && (messageTxt.Text != null) && (messageTxt.Text != ""))
            {
                string listplayers = null;
                int playerscount = PLAYERS.Count;

                foreach (string player in PLAYERS)  //guardem la llista de jugadors de la partida en un string parametre pel servidor (inclou host, altres i el user)
                {
                    if (player != USER)
                        listplayers += player + " ";
                }

                string mensaje = "50/" + messageTxt.Text +"/" + Nform + "/" + USER + "/" + listplayers + "/" + playerscount;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                SERVER.Send(msg);

                DelegateSETTEXT del = new DelegateSETTEXT(SETTEXT);
                messageTxt.Invoke(del);
            }

        }
        private void messageTxt_Click(object sender, EventArgs e)
        {
            DelegateRESETTEXT del = new DelegateRESETTEXT(RESETTEXT);
            messageTxt.Invoke(del);
        }
        public void ReceiveMessage(string message)
        {
            DelegateSETCHAT del = new DelegateSETCHAT(SETCHAT);
            messageTxt.Invoke(del, new object[] { message });
        }

        ////////////////////////////////////////////////////    START GAME     /////////////////////////////////////////////////////////////////////////////////////
        private void checkSymbols_CheckedChanged(object sender, EventArgs e)
        {
            symbols_checked = true;
            minigame = "SYMBOLS";
        }

        private void startgamebtn_Click(object sender, EventArgs e)
        {
            if ((minigame != null) && (creator != null))
            {
                string listplayers = null;
                int playerscount = PLAYERS.Count;

                foreach (string player in PLAYERS)  //guardem la llista de jugadors de la partida en un string parametre pel servidor (inclou host, altres i el user)
                {
                    if (player != USER)
                        listplayers += player + " ";
                }
                string mensaje = "94/" + Nform + "/" + USER + "/" + datos_partida + "/" + listplayers + "/" + playerscount;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                SERVER.Send(msg);
                gamestarted = true;
            }
            else
            {
                MessageBox.Show("Select the minigame before starting", "Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        ////////////////////////////////////////////////////    GAME: SYMBOLS    /////////////////////////////////////////////////////////////////////////////////////

        public void SetSymbols(string vectorimatges, string vectorposicions, int destinat)
        {
            //vectorimatges varia segons si ets el host o un guest

            this.vectorimatges_host.Clear();
            this.vectorimatges_jugador.Clear();
            this.vectorposicions.Clear();

            picture1.Show();
            picture2.Show();
            picture3.Show();
            picture4.Show();
            picture5.Show();

            int k = 0;
            List<Image> vecimag = new List<Image>();
            for (k = 0; k < 40; k++)
            {
                vecimag.Add(Image.FromFile(@"imagenes/" + (k + 1) + ".png"));
            }

            if (destinat == 0)  //destinat al host
            {
                vectorimatges = vectorimatges + " ";
                int j = 0;
                int separador;
                string intermid = null;
                int numpics = PLAYERS.Count - 1;

                while (j < vectorimatges.Length)
                {
                    separador = vectorimatges.IndexOf(" ", j);
                    while (j < separador)
                    {
                        intermid += vectorimatges[j];
                        j++;
                    }
                    if (intermid != "0".ToString())
                    {
                        this.vectorimatges_host.Add(Convert.ToInt32(intermid));
                    }
                    intermid = null;
                    j++;
                }

                DelegateSYMBOLS_PICTUREBOX_HOST del = new DelegateSYMBOLS_PICTUREBOX_HOST(SYMBOLS_PICTUREBOX_HOST);
                picture1.Invoke(del, new object[] { vecimag });
            }
            else if (destinat == 1)
            {
                //de la llista de totes les imatges de la partida, assignem al jugador en concret només les que li pertoquen
                //per exemple, si son 4 players (1 host i 3 jugadors), el host tindra 3 imatges que hauran de buscar els jugaodors, mentre
                //que els jugadors tindran 5 * 3 = 15 imatges en total del vectorimatges (5 per cada un) -> els jugadors sempre tindran 5 imatges cadascu,
                //pero depenent del numero de jugadors que siguin, el host en tindrà equivalentment el mateix nombre (si son 3 jugadors, el host tindra 3 imatges)
                //per a desxifrar

                vectorimatges = vectorimatges + " ";
                vectorposicions = vectorposicions + " ";
                int position_index = PLAYERS.IndexOf(USER); //o 1 o 2 o 3 o 4 (el 0 es el host)

                List<int> imatges_cpy = new List<int>();  //contindra totes les imatges que s'hauran de repartir entre jugadors

                int j = 0;
                int separador;
                string intermid = null;

                //primer guardem el vector de les posicions bones de les imatges
                while (j < vectorposicions.Length)
                {
                    separador = vectorposicions.IndexOf(" ", j);
                    while (j < separador)
                    {
                        intermid += vectorposicions[j];
                        j++;
                    }
                    if ((intermid != null) && (Convert.ToInt32(intermid) != 0))
                    {
                        this.vectorposicions.Add(Convert.ToInt32(intermid));
                    }
                    intermid = null;
                    j++;
                }

                j = 0;
                intermid = null;

                //busquem les imatges destinades a aquest jugador
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
                j = 5 * position_index;
                int num = j;
                k = 0;
                while (j < num + 5)
                {
                    this.vectorimatges_jugador.Add(imatges_cpy[j]);
                    this.posicions_picturebox.Add(j);
                    j++;
                    k++;
                }

                DelegateSYMBOLS_PICTUREBOX_GUEST del = new DelegateSYMBOLS_PICTUREBOX_GUEST(SYMBOLS_PICTUREBOX_GUEST);
                picture1.Invoke(del, new object[] { vecimag });
            }

            MessageBox.Show("The game has started!\nWatch out, you have " + contador_errors + " lives.", "Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DelegateSYMBOLSBOX del1 = new DelegateSYMBOLSBOX(SYMBOLSBOX);
            symbolsBox.Invoke(del1);
        }

        private void picture1_Click(object sender, EventArgs e)
        {
            string expresion;
            int j = 1;
            bool found = false;
            while (j < PLAYERS.Count)
            {
                if (PLAYERS[j] == this.USER)
                {
                    for (int k = 0; k < PLAYERS.Count; k++)
                    {
                        if (vectorimatges_jugador[5 * (j - 1)] == vectorimatges_host[k])
                        {
                            found = true;
                        }
                    }
                }
            }
            if (found)
            {
                contador_found++;
                expresion = "Nice!";
                MessageBox.Show(expresion + "\nYou have found " + contador_found + " out of " + PLAYERS.Count, "Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                contador_errors++;
                expresion = "Oh!";
                MessageBox.Show(expresion + " You missed it\nYou have " + contador_errors + " remaining lives", "Game", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            string listplayers = null;
            int playerscount = PLAYERS.Count;

            foreach (string player in PLAYERS)  //guardem la llista de jugadors de la partida en un string parametre pel servidor (altres i el user)
            {
                if (player != USER)
                    listplayers += player + " ";
            }

            string mensaje = "51/" + expresion + "/" + Nform + "/" + USER + "/" + listplayers + "/" + playerscount;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            SERVER.Send(msg);

            RoundManagement(USER, 0); //es compara aqui si s ha perdut s ha passat de ronda
        }

        private void picture2_Click(object sender, EventArgs e)
        {
            string expresion;
            int j = 1;
            bool found = false;
            while (j < PLAYERS.Count)
            {
                if (PLAYERS[j] == this.USER)
                {
                    for (int k = 0; k < PLAYERS.Count; k++)
                    {
                        if (vectorimatges_jugador[(5 * (j - 1)) + 1] == vectorimatges_host[k])
                        {
                            found = true;
                        }
                    }
                }
            }
            if (found)
            {
                contador_found++;
                expresion = "Nice!";
                MessageBox.Show(expresion + "\nYou have found " + contador_found + " out of " + PLAYERS.Count, "Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                contador_errors++;
                expresion = "Oh!";
                MessageBox.Show(expresion + " You missed it\nYou have " + contador_errors + " remaining lives", "Game", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            string listplayers = null;
            int playerscount = PLAYERS.Count;

            foreach (string player in PLAYERS)  //guardem la llista de jugadors de la partida en un string parametre pel servidor (altres i el user)
            {
                if (player != USER)
                    listplayers += player + " ";
            }

            string mensaje = "51/" + expresion + "/" + Nform + "/" + USER + "/" + listplayers + "/" + playerscount;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            SERVER.Send(msg);

            RoundManagement(USER, 0); //es compara aqui si s ha perdut s ha passat de ronda
        }

        private void picture3_Click(object sender, EventArgs e)
        {
            string expresion;
            int j = 1;
            bool found = false;
            while (j < PLAYERS.Count)
            {
                if (PLAYERS[j] == this.USER)
                {
                    for (int k = 0; k < PLAYERS.Count; k++)
                    {
                        if (vectorimatges_jugador[(5 * (j - 1)) + 2] == vectorimatges_host[k])
                        {
                            found = true;
                        }
                    }
                }
            }
            if (found)
            {
                contador_found++;
                expresion = "Nice!";
                MessageBox.Show(expresion + "\nYou have found " + contador_found + " out of " + PLAYERS.Count, "Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                contador_errors++;
                expresion = "Oh!";
                MessageBox.Show(expresion + " You missed it\nYou have " + contador_errors + " remaining lives", "Game", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            string listplayers = null;
            int playerscount = PLAYERS.Count;

            foreach (string player in PLAYERS)  //guardem la llista de jugadors de la partida en un string parametre pel servidor (altres i el user)
            {
                if (player != USER)
                    listplayers += player + " ";
            }

            string mensaje = "51/" + expresion + "/" + Nform + "/" + USER + "/" + listplayers + "/" + playerscount;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            SERVER.Send(msg);

            RoundManagement(USER, 0); //es compara aqui si s ha perdut s ha passat de ronda
        }

        private void picture4_Click(object sender, EventArgs e)
        {
            string expresion;
            int j = 1;
            bool found = false;
            while (j < PLAYERS.Count)
            {
                if (PLAYERS[j] == this.USER)
                {
                    for (int k = 0; k < PLAYERS.Count; k++)
                    {
                        if (vectorimatges_jugador[(5 * (j - 1)) + 3] == vectorimatges_host[k])
                        {
                            found = true;
                        }
                    }
                }
            }
            if (found)
            {
                contador_found++;
                expresion = "Nice!";
                MessageBox.Show(expresion + "\nYou have found " + contador_found + " out of " + PLAYERS.Count, "Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                contador_errors++;
                expresion = "Oh!";
                MessageBox.Show(expresion + " You missed it\nYou have " + contador_errors + " remaining lives", "Game", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            string listplayers = null;
            int playerscount = PLAYERS.Count;

            foreach (string player in PLAYERS)  //guardem la llista de jugadors de la partida en un string parametre pel servidor (altres i el user)
            {
                if (player != USER)
                    listplayers += player + " ";
            }

            string mensaje = "51/" + expresion + "/" + Nform + "/" + USER + "/" + listplayers + "/" + playerscount;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            SERVER.Send(msg);

            RoundManagement(USER, 0); //es compara aqui si s ha perdut s ha passat de ronda
        }

        private void picture5_Click(object sender, EventArgs e)
        {
            string expresion;
            int j = 1;
            bool found = false;
            while (j < PLAYERS.Count)
            {
                if (PLAYERS[j] == this.USER)
                {
                    for (int k = 0; k < PLAYERS.Count; k++)
                    {
                        if (vectorimatges_jugador[(5 * (j - 1)) + 4] == vectorimatges_host[k])
                        {
                            found = true;
                        }
                    }
                }
            }
            if (found)
            {
                contador_found++;
                expresion = "Nice!";
                MessageBox.Show(expresion + "\nYou have found " + contador_found + " out of " + PLAYERS.Count, "Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                contador_errors++;
                expresion = "Oh!";
                MessageBox.Show(expresion + " You missed it\nYou have " + contador_errors + " remaining lives", "Game", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            string listplayers = null;
            int playerscount = PLAYERS.Count;

            foreach (string player in PLAYERS)  //guardem la llista de jugadors de la partida en un string parametre pel servidor (altres i el user)
            {
                if (player != USER)
                    listplayers += player + " ";
            }

            string mensaje = "51/" + expresion + "/" + Nform + "/" + USER + "/" + listplayers + "/" + playerscount;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            SERVER.Send(msg);

            RoundManagement(USER, 0); //es compara aqui si s ha perdut s ha passat de ronda
        }

        public void UpdateClickExpression(string expresion, string sender)
        {
            if (expresion == "Nice!")
            {
                if (contador_found != PLAYERS.Count)    //quan siguin iguals, enviarem una alta notificacio de canvi de ronda
                    MessageBox.Show(sender + " has found a Symbol!\n" + contador_found + " out of " + PLAYERS.Count, "Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (expresion == "Oh!")
            {
                if (contador_errors != 0)   //quan sigui 0, enviarem una altra notificacio que s ha acabat la partida
                    MessageBox.Show(sender + " has missed...\nYou have " + contador_errors + " remaining lives", "Game", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        ////////////////////////////////////////////////////    GAME MANAGEMENT    /////////////////////////////////////////////////////////////////////////////////

        private void RoundManagement(string guest, int op)
        {
            // what = 1 vol dir que ha marxat algu de la partida (shuffle), si what = 0 no res
            if (contador_found == PLAYERS.Count)    //op = 0
            {
                string listplayers = null;
                int playerscount = PLAYERS.Count;

                foreach (string player in PLAYERS)  //guardem la llista de jugadors de la partida en un string parametre pel servidor (altres i el user)
                {
                    if (player != USER)
                        listplayers += player + " ";
                }
                string mensaje = "49/" + "0/" + Nform + "/" + guest + "/" + datos_partida + "/" + listplayers + "/" + playerscount + "/" + round;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                SERVER.Send(msg);

                //this.round++;
            }
            else if (contador_errors == 0)  //op = 0
            {
                EndGame(2, guest, this.round);
            }
            else if (op == 1)  //ha marxat algu i s han d actualitzar les rondes
            {
                string listplayers = null;
                int playerscount = PLAYERS.Count;

                foreach (string player in PLAYERS)  //guardem la llista de jugadors de la partida en un string parametre pel servidor (altres i el user)
                {
                    if (player != USER)
                        listplayers += player + " ";
                }
                if (playerscount != 1)  //mirem si es queda el host sol o no un cop ja ha començat. Si hi ha mes de 1 persona a la partida, es fa shuffle
                {
                    string mensaje = "49/" + "2/" + Nform + "/" + guest + "/" + datos_partida + "/" + listplayers + "/" + playerscount + "/" + round;
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    SERVER.Send(msg);
                }
                else    //si esta el host sol, parem la partida
                {
                    EndGame(1, guest, this.round);
                }
                
            }
        }

        public void Round(int operation, string player, int ronda)  //rebem resposta del server de la funcio RoundManagement
        {
            if (operation == 0) //next round
            {
                this.round = ronda; //pugem de ronda (al server ja s'ha sumat el valor)
                MessageBox.Show(player + " has found the last Symbol!\nRound " + ronda + " starting!", "Game", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //actualitzem el contador de rondes
                DelegateROUNDSLBL del = new DelegateROUNDSLBL(ROUNDSLBL);
                roundlbl.Invoke(del);
            }
            if (operation == 2) //shuffle jugador ha abandonat
            {
                this.round = ronda; //mateix valor de ronda que el que hi havia
                MessageBox.Show(player + " disconnected\nRestarting round " + ronda , "Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        ////////////////////////////////////////////////////    END GAME     /////////////////////////////////////////////////////////////////////////////////

        private void endgamebtn_Click(object sender, EventArgs e)
        {
            EndGame(0, this.USER, this.round);
        }

        private void EndGame(int op, string guest, int ronda)
        {
            string listplayers = null;
            int playerscount = PLAYERS.Count;

            foreach (string player in PLAYERS)  //guardem la llista de jugadors de la partida en un string parametre pel servidor (altres i el user)
            {
                if (player != USER)
                    listplayers += player + " ";
            }

            if (op == 0)    //clica el boto el USER del form i informa a la resta que l'ha apretat i que ha acabat la partida
            {
                string mensaje = "48/" + "0/" + Nform + "/" + guest + "/" + datos_partida + "/" + listplayers + "/" + playerscount + "/" + ronda;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                SERVER.Send(msg);
            }
            else if (op == 1)   //un jugador ha abandonat la partida i nomes queda el host (per tant no es pot seguir jugant)
            {
                string mensaje = "48/" + "1/" + Nform + "/" + guest + "/" + datos_partida + "/" + listplayers + "/" + playerscount + "/" + ronda;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                SERVER.Send(msg);
            }
            else if (op == 2)   //es perd la partida per haver arribat al maxim numero d errors
            {
                string mensaje = "48/" + "2/" + Nform + "/" + guest + "/" + datos_partida + "/" + listplayers + "/" + playerscount + "/" + ronda;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                SERVER.Send(msg);
            }

            //ara toca reiniciar la interficie i els parametres

            gamestarted = false;
            DelegateSYMBOLSBOX del = new DelegateSYMBOLSBOX(SYMBOLSBOX);
            symbolsBox.Invoke(del);

            this.contador_errors = 3;
            this.contador_found = 0;
            this.round = 0;
            this.vectorimatges_host.Clear();
            this.vectorimatges_jugador.Clear();
            this.vectorposicions.Clear();
        }

        public void GameLost(int operation, string player, int ronda)        //resposta del servidor a la funcio EndGame
        {
            if (operation == 0) //clica el boto el USER del form i informa a la resta que l'ha apretat i que ha acabat la partida
            {
                //player conte el nom del host
                if (USER != player) //missatge per als que no son host, ja que al clicar el boto no cal que rebi el missatge (el que si que se li reiniciara la partida)
                { 
                    MessageBox.Show(player + " host has ended the game on round " + ronda, "Game", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            if (operation == 1) //un jugador ha abandonat la partida i nomes queda el host (per tant no es pot seguir jugant)
            {
                //el missatge nomes arribara al host (player es el nom del jugador desconnectat)
                MessageBox.Show(player + " has disconnected. Ending game at round " + ronda + "\nYou need more players to play the game!", "Game", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            if (operation == 2) //es perd la partida per haver arribat al maxim numero d errors
            {
                //el missatge arribara a tots els usuaris per igual
                //player conte el nom del jugador que ha comes l error
                MessageBox.Show("Game lost. No more lifes remaining!\n" + player + " has commited the last error. Ending game at round " + ronda, "Game", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        ////////////////////////////////////////////////////    DELEGATES     /////////////////////////////////////////////////////////////////////////////////

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

        delegate void DelegateSETCHAT(string message);
        public void SETCHAT(string message)
        {
            messageTxt.Text = "Message:";

            chatGrid.ColumnCount = 1;
            chatGrid.Rows.Clear();
            chatGrid.RowCount = chatcount; //la llista de missatges del chat tambe depen de chatcount (variable global constant)
            chatGrid.RowHeadersVisible = false;
            chatGrid.ColumnHeadersVisible = false;
            chatGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            List<string> intermidiate = new List<string>();

            int j = 0; //penultima posicio
            while (j < chatcount - 1)  //posem el nou missatge al final de la llista i borrem el primer
            {
                intermidiate.Add(Chat[j + 1]);
                j++;
            }
            intermidiate.Add(message);
            Chat = intermidiate;
            j = 0;
            while (j < chatcount)
            {
                chatGrid.Rows[j].Cells[0].Value = Chat[j];
                j++;
            }
        }
        delegate void DelegateRESETTEXT();
        public void RESETTEXT()
        {
            if (resettext == true)
            {
                resettext = false;
                messageTxt.Text = null;
            }
        }
        delegate void DelegateSETTEXT();
        public void SETTEXT()
        {
            if (resettext == false)
            {
                resettext = true;
                messageTxt.Text = "MESSAGE:";
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

        delegate void DelegateSYMBOLS_PICTUREBOX_HOST(List<Image> vecimag);
        public void SYMBOLS_PICTUREBOX_HOST(List<Image> vecimag)
        {
            int j = 0;
            //primer resetegem els valors dels labels
            this.picture1.Enabled = false;
            this.picture2.Enabled = false;
            this.picture3.Enabled = false;
            this.picture4.Enabled = false;
            this.picture5.Enabled = false;

            int numpicturebox = PLAYERS.Count - 1;

            if (j < numpicturebox)
            {
                picture1.Image = vecimag[vectorimatges_host[j]];
                j++;
                if (j < numpicturebox)
                {
                    picture2.Image = vecimag[vectorimatges_host[j]];
                    j++;
                    if (j < numpicturebox)
                    {
                        picture3.Image = vecimag[vectorimatges_host[j]];
                        j++;
                        if (j < numpicturebox)
                        {
                            picture4.Image = vecimag[vectorimatges_host[j]];
                            j++;
                            if (j < numpicturebox)
                            {
                                picture5.Image = vecimag[vectorimatges_host[j]];
                            }
                        }
                    }
                }
            }
        }

        delegate void DelegateSYMBOLS_PICTUREBOX_GUEST(List<Image> vecimag);
        public void SYMBOLS_PICTUREBOX_GUEST(List<Image> vecimag)
        {
            //primer resetegem els valors dels labels
            this.picture1.Enabled = true;
            this.picture2.Enabled = true;
            this.picture3.Enabled = true;
            this.picture4.Enabled = true;
            this.picture5.Enabled = true;

            picture1.Image = vecimag[vectorimatges_jugador[0]];
            picture2.Image = vecimag[vectorimatges_jugador[1]];
            picture3.Image = vecimag[vectorimatges_jugador[2]];
            picture4.Image = vecimag[vectorimatges_jugador[3]];
            picture5.Image = vecimag[vectorimatges_jugador[4]];
        }

        delegate void DelegateSYMBOLSBOX();
        public void SYMBOLSBOX()
        {
            if (gamestarted)
            {
                this.symbolsBox.Visible = true;
                this.roundlbl.Visible = true;
                this.roundlbl.Text = "Round 1";
                if (creator != null)
                {
                    endgamebtn.Visible = true;
                    endgamebtn.Enabled = true;
                }
            }
            else if (!gamestarted)
            {
                this.symbolsBox.Visible = false;
                if (creator != null)
                {
                    endgamebtn.Visible = false;
                    endgamebtn.Enabled = false;
                }
            }
        }

        delegate void DelegateROUNDSLBL(int round);
        public void ROUNDSLBL(int round)
        {
            roundlbl.Text = "Round " + round.ToString();
        }


        delegate void DelegateRESET();
        public void RESET()
        {

        }

        delegate void DelegateCLOSE();
        public void CLOSE()
        {
            PLAYERS.Clear();    //netegem la llista de jugadors del jugador que marxa
            Chat.Clear();
            this.Close();
        }
    }
}
