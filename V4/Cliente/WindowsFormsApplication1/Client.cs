using System;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using System.Drawing;

namespace WindowsFormsApplication1
{
    public partial class Client : Form
    {
        Socket server;
        Thread client;
        Thread game;
        string USER;
        string ServerState;
        bool JoinClick = false;
        bool Joining = false;
        string invitation;
        bool alreadyclosing = false;
        string GAME;
        string OTHERPLAYERS;
        int NForm;
        List<string> ListaConectados = new List<string>();
        List<string> Invitations = new List<string>();
        Dictionary<string, string> GameInvitations = new Dictionary<string, string>();  //utilitzat per unir-se a partides
        Dictionary<string, int> FormInvitations = new Dictionary<string, int>();  //utilitzat per unir-se a partides
        List<GameWindow> GameWndwForms = new List<GameWindow>();

        //private static IPAddress direc = IPAddress.Parse("10.4.119.5");
        //private static IPEndPoint ipep = new IPEndPoint(direc, 50080);
        private static IPAddress direc = IPAddress.Parse("192.168.56.102");
        private static IPEndPoint ipep = new IPEndPoint(direc, 9075);

        public Client()
        {
            InitializeComponent();

            //CheckForIllegalCrossThreadCalls = false;    //permet als threads acedir als diferents controls del forms
            //forcem a que s'hagi de tancar el programa amb el botó de desconnectar
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //this.Bounds = Screen.PrimaryScreen.WorkingArea;

            database.Enabled = false;
            createGAME.Enabled = false;
            joinGame.Enabled = false;
            statsbtn.Enabled = false;
            DEV_closebtn.Visible = false;
            logout.Visible = false;
            welcomelbl.Visible = false;
            returnbtn.Visible = false;
            statsBox.Visible = false;
            USER = null;

            playersonlineGrid.ColumnCount = 1;
            playersonlineGrid.RowHeadersVisible = false;
            playersonlineGrid.ColumnHeadersVisible = false;
            playersonlineGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            playersonlineGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            invitationsGrid.ColumnCount = 1;
            invitationsGrid.RowHeadersVisible = false;
            invitationsGrid.ColumnHeadersVisible = false;
            invitationsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            invitationsGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void Main()
        {
            while (true)
            {
                //Recibimos la respuesta del servidor
                byte[] msg_response = new byte[400];
                server.Receive(msg_response);

                //msg del tipus "2#Juan,"

                //NOTA: TOTS ELS MSG DEL SERVER ACABEN AMB COMA (,) I ES DISTINGEIX EL CODI AMB HASHTAG (#)
                string[] parts = Encoding.ASCII.GetString(msg_response).Split('#');

                string comprovacio = Encoding.ASCII.GetString(msg_response).Split('\0')[0];
                if (comprovacio != "")
                {
                    int code = Convert.ToInt32(parts[0]);
                    string response;

                    switch (code)
                    {
                        case 0:
                            {
                                int operation = Convert.ToInt32(parts[1].Split(',')[0]);

                                ServerState = "DOWN";



                                if (operation == 0)  //CLOSE GAME
                                {
                                    server.Shutdown(SocketShutdown.Both);
                                    //server.Close();
                                }
                                else                //LOGOUT
                                {
                                    DelegateUSERdisCONNECTED del = new DelegateUSERdisCONNECTED(USERdisCONNECTED);
                                    welcomelbl.Invoke(del);
                                    DelegateOFFLINE del_ = new DelegateOFFLINE(OFFLINE);
                                    offlinelbl.Invoke(del_);
                                    server.Shutdown(SocketShutdown.Both);
                                    server.Close();
                                }

                                client.Abort(); //tanquem el thread d'aquest client
                                break;
                            }
                        case 1:     //LOGIN
                            {
                                response = parts[1].Split(',')[0];
                                if (response == "correcto")
                                {
                                    string nombre = nameBox.Text;
                                    USER = nombre;
                                    MessageBox.Show("Login Successful", "Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    DelegateUSERCONNECTED del = new DelegateUSERCONNECTED(USERCONNECTED);
                                    welcomelbl.Invoke(del, new object[] { nombre });
                                }
                                else    //error
                                {
                                    MessageBox.Show(response, "Client", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    DelegateUSERnotCONNECTED del = new DelegateUSERnotCONNECTED(USERnotCONNECTED);
                                    welcomelbl.Invoke(del);
                                }

                                if (USER == "HOST")
                                {
                                    DelegateDEVbtn del = new DelegateDEVbtn(USERnotDEVbtn);
                                    DEV_closebtn.Invoke(del);
                                }

                                break;
                            }
                        case 2:  //REGISTER
                            {
                                response = parts[1].Split(',')[0];
                                if (response == "correcto")
                                {
                                    string nombre = nameBox.Text;
                                    USER = nombre;
                                    MessageBox.Show("User Added Correclty", "Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    DelegateUSERCONNECTED del = new DelegateUSERCONNECTED(USERCONNECTED);
                                    welcomelbl.Invoke(del, new object[] { nombre });
                                }
                                else if (response == "incorrecto")
                                {
                                    MessageBox.Show("User Already Exists", "Client", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    DelegateUSERnotCONNECTED del = new DelegateUSERnotCONNECTED(USERnotCONNECTED);
                                    welcomelbl.Invoke(del);
                                }

                                break;
                            }
                        case 3:  //CONSULTA 1
                            {
                                response = parts[1].Split(',')[0];
                                MessageBox.Show(response, "Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                DelegateREFRESHCONSULTAS del = new DelegateREFRESHCONSULTAS(REFRESHCONSULTAS);
                                name_txt.Invoke(del);
                                break;
                            }
                        case 4:  //CONSULTA 2
                            {
                                response = parts[1].Split(',')[0];
                                MessageBox.Show(response, "Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                DelegateREFRESHCONSULTAS del = new DelegateREFRESHCONSULTAS(REFRESHCONSULTAS);
                                date_txt.Invoke(del);
                                break;
                            }
                        case 5:  //CONSULTA 3
                            {
                                response = parts[1].Split(',')[0];
                                MessageBox.Show(response, "Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                DelegateREFRESHCONSULTAS del = new DelegateREFRESHCONSULTAS(REFRESHCONSULTAS);
                                server_txt.Invoke(del);
                                break;
                            }
                        case 100:    //SHOW DATABASE
                            {
                                response = parts[1].Split(',')[0];
                                DataBase formDB = new DataBase();
                                formDB.SetBD(response);
                                formDB.ShowDialog();
                                break;
                            }
                        case 99: //NOTIFICACIO CONECTATS/DESCONNECTATS
                            {
                                string list = parts[1].Split('\0')[0] ;
                                
                                ListaConectados.Clear();    //reset de la llista de connectats (ara la plenarem)

                                int j = 0;
                                int separador_noms = 0;
                                string intermid = null;
                                while (j < list.Length)
                                {
                                    separador_noms = list.IndexOf(" ", j);

                                    if (separador_noms == -1)   //l'ultim de list no te espai
                                    {
                                        separador_noms = list.IndexOf(",", j);
                                    }

                                    while (j < separador_noms)  //guardem nom per nom a la llista de connectats
                                    {
                                        intermid += list[j];
                                        j++;
                                    }
                                    if (intermid != null)
                                    {
                                        ListaConectados.Add(intermid);
                                    }

                                    intermid = null;
                                    j++;
                                }

                                //Actualitzar llista connectats del JOC i al MENU

                                int i = 0;
                                while(i < GameWndwForms.Count)
                                {
                                    GameWndwForms[i].RefreshConnectedList(ListaConectados);
                                    i++;
                                }

                                DelegateREFRESHnumCONNECTED del = new DelegateREFRESHnumCONNECTED(REFRESHnumCONNECTED);
                                numplayerslbl.Invoke(del);

                                break;
                            }
                        case 98:  //CREATE GAME
                            {
                                //int Nform = Convert.ToInt32(parts[1]);

                                response = parts[1];

                                if (response == "correcto")
                                {
                                    GAME = parts[2].Split(',')[0];
                                    OTHERPLAYERS = null;
                                    game.Start();
                                    //GameWndwForms[GameWndwForms.Count].SetGame(GAME);
                                }
                                break;
                            }
                        case 97:  //INVITATION RECEIVED
                            {
                                int type_operation = Convert.ToInt32(parts[1]);
                                int NumForm;
                                
                                string invited;
                                string inviting;
                                int j = 0;

                                if (type_operation == 0) //rebem resposta a la nostra invitacio a un altre usuari
                                {
                                    //msg del tipus "97#0#%d#correcto#%s,"

                                    NumForm = Convert.ToInt32(parts[2]);
                                    response = parts[3];

                                    if (response == "correcto")
                                    {
                                        invited = parts[4].Split(',')[0];
                                        GameWndwForms[NumForm].InvitationSent(invited);
                                    }
                                    else
                                    {
                                        GameWndwForms[NumForm].InvitationSent("ERROR");
                                    }
                                }
                                else if (type_operation == 1)   //rebem una invitacio d'un altre jugador
                                {
                                    //msg del tipus "97#1#%s#%s#d,", INVITING, game_info, NForm
                                    inviting = parts[2];
                                    string datos_partida = parts[3].Split(',')[0];
                                    NumForm = Convert.ToInt32(parts[4].Split(',')[0]);
                                    Invitations.Add(inviting);
                                    GameInvitations.Add(inviting, datos_partida);
                                    FormInvitations.Add(inviting, NumForm);
                                    
                                    DelegateREFRESHINVITATIONS del = new DelegateREFRESHINVITATIONS(REFRESHINVITATIONS);
                                    invitationsGrid.Invoke(del);
                                    //MessageBox.Show("Invitaion from " + inviting + " received. Added to your invitation log in game", "Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    
                                    if (GameWndwForms.Count != 0)    //SI ESTA IN GAME, ENVIEM LA INVITACIO A TOTS ELS FORMS OBERTS
                                    {
                                        while (j < GameWndwForms.Count)
                                        {
                                            GameWndwForms[j].InvitationReceived(inviting);
                                            j++;
                                        }
                                    }
                                }
                                break;
                            }
                        case 96:    //JOIN GAME
                            {

                                int result = Convert.ToInt32(parts[1]);


                                if (result == 0)    // result = 0 per al jugador que s'esta unint
                                {
                                    // "96#0#%s#%s#d,", game_info, otherplayers, NForm)
                                    string game_info = parts[2];
                                    string otherplayers = parts[3];
                                    NForm = Convert.ToInt32(parts[4].Split(',')[0]);

                                    GAME = game_info;
                                    OTHERPLAYERS = otherplayers;

                                    //MessageBox.Show("OK", "Client", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                                    Joining = true;

                                    //creem i inicialitzem el thread corresponent a aquest client per la partida a la que s'uneix (segons el numero de form del que convida)
                                    /*
                                    if (GameWndwForms[NForm] != null)
                                    {
                                        MessageBox.Show("A new game has started in one of your current lobbys", "Client", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                                    }
                                    */
                                    ThreadStart thGame = delegate { STARTGAME(NForm); };
                                    game = new Thread(thGame);
                                    game.Start();
                                }
                                else if (result == 1)    // result = 1 per actualitzar als jugadors que ja estan a la partida
                                {
                                    //"96#1#%s#d#d,", invited , resp, NForm)
                                    //resp indica el numero de sockets que s'han hagut d'informar (equivalent al numero de jugadors que es trobaven a la partida)
                                    //la posicio del nou jugador sera resp + 1
                                    string joiningplayer = parts[2];
                                    int positionplayer = Convert.ToInt32(parts[3]) + 1;
                                    NForm = Convert.ToInt32(parts[4].Split(',')[0]);

                                    GameWndwForms[NForm].PlayerJoined(joiningplayer, positionplayer);
                                }
                                else if (result == -1)  //partida plena
                                {
                                    MessageBox.Show("The game you are trying to join is already full", "Client", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                                else if (result == -2)  //no existeix la partida ja
                                {
                                    MessageBox.Show("The game you are trying to join has already ended or doesn't exist", "Client", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                                else if (result == -3)  //la partida ja ha començat
                                {
                                    MessageBox.Show("The game you are trying to join has already begun. Wait for another invitation", "Client", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }

                                break;
                            }
                        case 95:    //LEAVE GAME
                            {
                                // "95#0#%s#%d,", disconnecting, NForm);

                                int result = Convert.ToInt32(parts[1]);
                                string disconnecting = parts[2];
                                NForm = Convert.ToInt32(parts[3].Split(',')[0]);

                                if (result == -1)    // tanca partida el host
                                {
                                    MessageBox.Show("Error player leaving game", "Client", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                                }
                                if (result == 2)    // missatge nomes pel host quan tanca la partida i pel jugador quan tanca partida
                                {
                                    MessageBox.Show("Game closed", "Client", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                                else
                                {
                                    GameWndwForms[NForm].PlayerLeft(result, disconnecting);
                                }
                                break;
                            }
                        case 94:    //START GAME
                            {
                                int game = Convert.ToInt32(parts[1]);

                                if(game == 0)   //SYMBOLS
                                {
                                    int destination = Convert.ToInt32(parts[2]);
                                    if (destination == 0)   //destinat al host
                                    {
                                        // "94#0#0#%s#%d,", vectorimagenes_host, NForm);

                                        string vectorimatges_host = parts[3];
                                        NForm = Convert.ToInt32(parts[4].Split(',')[0]);

                                        GameWndwForms[NForm].StartGameSymbols(vectorimatges_host, null, destination);
                                    }
                                    else if (destination == 1)  //destinat als altres jugadors
                                    {
                                        //   "94#0#1#%s#%s#%d,", vectorimagenes_others, vectorposiciones, NForm);

                                        string vectorimatges_jugadors = parts[3];
                                        string vector_posicions = parts[4];
                                        NForm = Convert.ToInt32(parts[5].Split(',')[0]);

                                        GameWndwForms[NForm].StartGameSymbols(vectorimatges_jugadors, vector_posicions, destination);
                                    }
                                }
                                else if (game == 1) //MAZE
                                {

                                }
                                else if(game == 2)  //TBD
                                {

                                }


                                break;
                            }

                        case 50:    //SEND MESSAGE IN GAME
                            {
                                //  "50#%s: %s#%d,", sender, message, NForm
                                string message = parts[1];
                                NForm = Convert.ToInt32(parts[2].Split(',')[0]);

                                GameWndwForms[NForm].ReceiveMessage(message);
                                break;
                            }
                        case 51:
                            {
                                // "51#%s#%s#%d,", expresion, sender, NForm

                                string expresion = parts[1];
                                string sender = parts[2];
                                NForm = Convert.ToInt32(parts[3].Split(',')[0]);

                                GameWndwForms[NForm].UpdateClickExpression(expresion, sender);

                                break;
                            }
                    }
                }
            }
        }

        ////////////////////////////////////////////////////    LOAD / CLOSE SERVER     /////////////////////////////////////////////////////////////////////////
        private void Form1_Load(object sender, EventArgs e)
        {
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos

            //Creamos el socket 

            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
                //this.BackColor = Color.Green;
                MessageBox.Show("CONNECTED TO SERVER");
                ServerState = "UP";
                DelegateOFFLINEnot del = new DelegateOFFLINEnot(OFFLINEnot);
                offlinelbl.Invoke(del);
            }
            catch (SocketException ex)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("SERVER NOT CONNECTED");
                ServerState = "DOWN";
                DelegateOFFLINE del = new DelegateOFFLINE(OFFLINE);
                offlinelbl.Invoke(del);
                return;
            }
            //creem i inicialitzem el thread corresponent a aquest client
            ThreadStart thMain = delegate { Main(); };
            client = new Thread(thMain);
            client.Start();
        }

        private void conectserver_Click(object sender, EventArgs e)
        {
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
                //this.BackColor = Color.Green;
                MessageBox.Show("CONNECTION ESTABLISHED");
                ServerState = "UP";
                DelegateOFFLINEnot del = new DelegateOFFLINEnot(OFFLINEnot);
                offlinelbl.Invoke(del);
            }
            catch (SocketException ex)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("SERVER NOT CONNECTED");
                ServerState = "DOWN";
                DelegateOFFLINE del = new DelegateOFFLINE(OFFLINE);
                offlinelbl.Invoke(del);
                return;
            }
            //creem i inicialitzem el thread corresponent a aquest client
            ThreadStart thMain = delegate { Main(); };
            client = new Thread(thMain);
            client.Start();
        }

        private void Desconectar_Click(object sender, EventArgs e)
        {
            alreadyclosing = true;
            if (ServerState == "UP") //si el servidor encara esta RUNNING es tanca
            {
                //mensaje de desconexión del USUARIO en cuestion
                string mensaje = "0/0/" + USER;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            this.Close();
        }
        //BOTH SAME UTILITY
        private void Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((ServerState == "UP") && (!alreadyclosing))//si el servidor encara esta RUNNING es tanca
            {
                //mensaje de desconexión del USUARIO en cuestion
                string mensaje = "0/0/" + USER;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                // Se terminó el servicio. 
                // Nos desconectamos
                server.Shutdown(SocketShutdown.Both);
                //server.Close();
                ServerState = "DOWN";
                client.Abort(); //tanquem el thread d'aquest client
            }
            //this.Close();
        }

        ////////////////////////////////////////////////////    LOGIN, LOGOUT, REGISTER     ////////////////////////////////////////////////////////////////////////
        private void logout_Click(object sender, EventArgs e)
        {
            string mensaje = "0/1/" + USER;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        private void login_Click(object sender, EventArgs e)
        {
            string nombre = nameBox.Text;
            string password = pswdBox.Text;

            if (((nombre != null) && (nombre != "")) && ((password != null) && (password != "")))
            {
                string mensaje_usuario = "1/" + nombre + "/" + password;
                // Enviamos al servidor el nombre i contraseña introducidos
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje_usuario);
                server.Send(msg);
            }
            else
            {
                MessageBox.Show("Introduce Valid Values", "Client", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void register_Click(object sender, EventArgs e)
        {
            string nombre = nameBox.Text;
            string password = pswdBox.Text;

            if (((nombre != null) && (nombre != "")) && ((password != null) && (password != "")))
            {
                string mensaje_usuario = "2/" + nombre + "/" + password;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje_usuario);
                server.Send(msg);
            }
            else
            {
                MessageBox.Show("Introduce Valid Values", "Client", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        ////////////////////////////////////////////////////    PETICIONS STATS     //////////////////////////////////////////////////////////////////////////////
        private void Enviar_nombre_Click(object sender, EventArgs e)
        {
            string name = name_txt.Text;

            if (name != null)
            {
                string mensaje_usuario = "3/" + name;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje_usuario);
                server.Send(msg);
            }
            else
            {
                MessageBox.Show("Introduce Valid Values", "Client", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Enviar_fecha_Click(object sender, EventArgs e)
        {
            string date = date_txt.Text;

            if (date != null)
            {
                string mensaje_usuario = "4/" + date;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje_usuario);
                server.Send(msg);
            }
            else
            {
                MessageBox.Show("Introduce Valid Values", "Client", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Enviar_server_Click(object sender, EventArgs e)
        {
            string servr = server_txt.Text;

            if (servr != null)
            {
                string mensaje_usuario = "5/" + servr;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje_usuario);
                server.Send(msg);
            }
            else
            {
                MessageBox.Show("Introduce Valid Values", "Client", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        ////////////////////////////////////////////////////    DATABASE     //////////////////////////////////////////////////////////////////////////////////
        private void database_Click(object sender, EventArgs e)
        {
            string mensaje_usuario = "100/";
            // Enviamos al servidor el nombre i contraseña introducidos
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje_usuario);
            server.Send(msg);
        }


        ////////////////////////////////////////////////////    CREATE GAME & THREAD     ///////////////////////////////////////////////////////////////////////
        private void createGAME_Click(object sender, EventArgs e)
        {
            string mensaje = "98/" + USER;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            NForm = GameWndwForms.Count;

            //creem i inicialitzem el thread corresponent a aquest client per aquesta partida
            ThreadStart thGame = delegate { STARTGAME(NForm); };
            game = new Thread(thGame);
        }

        //THREAD DE createGAME
        private void STARTGAME(int numform)
        {
            GameWindow G = new GameWindow();
            GameWndwForms.Add(G);
            //REFRESCAR LA LLISTA DE CONNECTATS PER PODER CONVIDAR
            //G.RefreshConnectedList(ListaConectados);
            //PARAMETRES ESTETICS DEL LOBBY (HUD)
            G.SetLobby(numform, server, USER, ListaConectados);
            //PARAMETRES DE LA PARTIDA CREADA A MYSQL
            G.SetGame(GAME);

            if (!Joining)   //no s'uneix ningu (la funcio es crida pel creador de la partida per primer cop)
            {
                G.creator = USER;
            }
            if (Joining)    //s'uneix algu i per tant s'ha d'actualitzar
            {
                G.creator = null;
                G.SetOtherPlayersOnJoining(OTHERPLAYERS);
            }
            Joining = false;

            G.ShowDialog();

            //quan acabi el joc:
            GameWndwForms.RemoveAt(numform);
        }

        ////////////////////////////////////////////////////    JOIN GAME     ////////////////////////////////////////////////////////////////////////////////
        private void joinGame_Click(object sender, EventArgs e)
        {
            string game = GameInvitations[invitation];
            int form = FormInvitations[invitation];

            //el valor de invitation ve de la funcio de invitationsGrid_CellContentClick
            string mensaje_usuario = "96/" + USER + "/" + invitation + "/" + game + "/" + form;
            // Enviamos al servidor el nombre i contraseña introducidos
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje_usuario);
            server.Send(msg);

            Invitations.Remove(invitation);
            GameInvitations.Remove(invitation);
            FormInvitations.Remove(invitation);

            invitation = null;

            DelegateREFRESHINVITATIONS del = new DelegateREFRESHINVITATIONS(REFRESHINVITATIONS);
            invitationsGrid.Invoke(del);
        }

        private void invitationsGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //si cliquem damunt de la invitacio ens podrem unir. Si tornem a clicar es cancela unir-se
            if (!JoinClick)
            {
                JoinClick = true;
                joinGame.Enabled = true;

                if (invitationsGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    invitation = invitationsGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                }
            }
            else if (JoinClick)
            {
                JoinClick = false;
                joinGame.Enabled = false;
            }
        }

        ////////////////////////////////////////////////////    VARIED BUTTONS     /////////////////////////////////////////////////////////////////////////////////

        private void DEV_closebtn_Click(object sender, EventArgs e)
        {
            string mensaje = "-1/" + USER;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            server.Shutdown(SocketShutdown.Both);
            server.Close();
            ServerState = "DOWN";
            client.Abort(); //tanquem el thread d'aquest client

            this.Close();
        }

        private void statsbtn_Click(object sender, EventArgs e)
        {
            DelegateSHOWSTATS del = new DelegateSHOWSTATS(SHOWSTATS);
            statsBox.Invoke(del);
        }

        private void returnbtn_Click(object sender, EventArgs e)
        {
            DelegateQUITSTATS del = new DelegateQUITSTATS(QUITSTATS);
            statsBox.Invoke(del);
        }



        ////////////////////////////////////////////////////    DELEGATES     //////////////////////////////////////////////////


        delegate void DelegateUSERCONNECTED(string name);
        public void USERCONNECTED(string name)
        {
            welcomelbl.Text = "WELCOME " + name;
            welcomelbl.Visible = true;
            database.Enabled = true;
            logout.Visible = true;
            statsbtn.Enabled = true;
            createGAME.Enabled = true;
            playersonlineGrid.Enabled = true;
            invitationsGrid.Enabled = true;
            nameBox.Text = null;
            pswdBox.Text = null;
            nameBox.Visible = false;
            pswdBox.Visible = false;
            login.Visible = false;
            register.Visible = false;
            namelbl.Visible = false;
            pswdlbl.Visible = false;
        }

        delegate void DelegateUSERnotCONNECTED();
        public void USERnotCONNECTED()
        {
            nameBox.Text = null;
            pswdBox.Text = null;
        }

        delegate void DelegateUSERdisCONNECTED();
        public void USERdisCONNECTED()
        {
            welcomelbl.Visible = false;
            welcomelbl.Text = null;
            database.Enabled = false;
            logout.Visible = false;
            statsbtn.Enabled = false;
            createGAME.Enabled = false;
            joinGame.Enabled = false;
            playersonlineGrid.Enabled = false;
            invitationsGrid.Enabled = false;
            nameBox.Visible = true;
            pswdBox.Visible = true;
            login.Visible = true;
            register.Visible = true;
            namelbl.Visible = true;
            pswdlbl.Visible = true;
        }

        delegate void DelegateREFRESHCONSULTAS();
        public void REFRESHCONSULTAS()
        {
            name_txt.Text = null;
            date_txt.Text = null;
            server_txt.Text = null;
        }

        delegate void DelegateOFFLINEnot();
        public void OFFLINEnot()
        {
            offlinelbl.Visible = false;
            conectserver.Visible = false;
            playersonlineGrid.Visible = true;
            playersonlinelbl.Visible = true;
            invitationsGrid.Visible = true;
            invitationslbl.Visible = true;
            nameBox.Enabled = true;
            pswdBox.Enabled = true;
            login.Enabled = true;
            register.Enabled = true;
        }

        delegate void DelegateOFFLINE();
        public void OFFLINE()
        {
            offlinelbl.Visible = true;
            conectserver.Visible = true;
            playersonlineGrid.Visible = false;
            playersonlinelbl.Visible = false;
            invitationsGrid.Visible = false;
            invitationslbl.Visible = false;
            nameBox.Enabled = false;
            pswdBox.Enabled = false;
            login.Enabled = false;
            register.Enabled = false;
        }

        delegate void DelegateDEVbtn();
        public void USERnotDEVbtn()
        {
            DEV_closebtn.Visible = true;
        }

        delegate void DelegateREFRESHnumCONNECTED();
        public void REFRESHnumCONNECTED()
        {
            numplayerslbl.Text = "Number of players Online: " + ListaConectados.Count;

            playersonlineGrid.Rows.Clear();
            playersonlineGrid.RowCount = ListaConectados.Count; //menys l'usuari mateix que no conta

            int j = 0;
            int k = 0;
            while(j < ListaConectados.Count)
            {
                if (ListaConectados[j] != USER)
                {
                    playersonlineGrid.Rows[k].Cells[0].Value = ListaConectados[j];
                    k++;
                }
                j++; 
            }
        }

        delegate void DelegateREFRESHINVITATIONS();
        public void REFRESHINVITATIONS()
        {
            //numplayerslbl.Text = "Number of players Online: " + ListaConectados.Count;
            invitationsGrid.Rows.Clear();
            invitationsGrid.RowCount = Invitations.Count;

            int j = 0;
            while (j < Invitations.Count)
            {
                invitationsGrid.Rows[j].Cells[0].Value = Invitations[j];
                j++;
            }
        }

        delegate void DelegateSHOWSTATS();
        public void SHOWSTATS()
        {
            createGAME.Visible = false;
            joinGame.Visible = false;
            database.Visible = false;
            statsbtn.Visible = false;
            Desconectar.Visible = false;
            playersonlineGrid.Visible = false;
            playersonlinelbl.Visible = false;
            invitationsGrid.Visible = false;
            invitationslbl.Visible = false;
            returnbtn.Visible = true;
            statsBox.Visible = true;
        }

        delegate void DelegateQUITSTATS();
        public void QUITSTATS()
        {
            createGAME.Visible = true;
            joinGame.Visible = true;
            database.Visible = true;
            statsbtn.Visible = true;
            Desconectar.Visible = true;
            playersonlineGrid.Visible = true;
            playersonlinelbl.Visible = true;
            invitationsGrid.Visible = true;
            invitationslbl.Visible = true;
            returnbtn.Visible = false;
            statsBox.Visible = false;
        }

























        //CODI PER SI DE CAS

        //  -- notificacions

        /*
        response = parts[2].Split(',')[0];

        user_conn = response.Split('\n')[0];
        list = response.Split('\n')[1];     //conté la llista de connectats (|Name|Name|Name|...)

        int j = 0;
        int separador_noms = 0;
        string intermid = null;
        string conectados_en_string = null;
        while(j < list.Length)
        {
            separador_noms = list.IndexOf("|", j);
            while (j < separador_noms)  //guardem nom per nom a la llista de connectats
            {
                intermid += list[j];
                j++;
            }
            if(intermid != null)
            {
                ListaConectados.Add(intermid);
                conectados_en_string += intermid + " ";
            }

            intermid = null;
            j++;
        }
        */

        /*
        int j = 0;
        int separador_noms = 0;
        string intermid = null;
        string conectados_en_string = null;
        while (j < list.Length)
        {
            if (list[list.Length - 1] == '|')
            {
                separador_noms = list.IndexOf("|", j);
            }
            else
            {
                separador_noms = list.IndexOf("\0", j);
            }

            while (j < separador_noms)  //guardem nom per nom a la llista de connectats
            {
                intermid += list[j];
                j++;
            }
            if (intermid != null)
            {
                ListaConectados.Add(intermid);
            }
            conectados_en_string += intermid + " ";
            intermid = null;
            j++;
        }
        */


        /*
        //COMPROVEM QUI S'ESTÀ DESCONNECTANT (SI JO O UN ALTRE USUARI)
        bool isitmedisconnecting = false;
        string whoisdisconnecting = response.Split('\n')[0];

        if (whoisdisconnecting == "User Disconnected: " + USER)
        {
            isitmedisconnecting = true;
        }

        if (isitmedisconnecting == true)
        {
            // Se terminó el servicio. 
            // Nos desconectamos
            server.Shutdown(SocketShutdown.Both);
            server.Close();
            ServerState = "DOWN";
            DelegateOFFLINE del = new DelegateOFFLINE(OFFLINE);
            offlinelbl.Invoke(del);
            client.Abort(); //tanquem el thread d'aquest client
        }
        */

        //NOTIS

        /*
        int type_operation = Convert.ToInt32(parts[1].Split(',')[0]);
                                
        if (type_operation == 0)    //USER S'HA CONECTAT
        {

            response = parts[2].Split(',')[0];

            user_conn = response.Split('\n')[0];
            list = response.Split('\n')[1];     //conté la llista de connectats (Name Name Name...)

            MessageBox.Show(user_conn + "\nUsers Online: " + list, "Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else if (type_operation == 1) //USER S'HA DESCONNECTAT
        {
            response = parts[2].Split(',')[0];

            user_conn = response.Split('\n')[0];

            list = response.Split('\n')[1];     //conté la llista de connectats (|Name|Name|Name|...)

            MessageBox.Show(user_conn + "\nUsers Online: " + list, "Client", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        */
        
    }
}
