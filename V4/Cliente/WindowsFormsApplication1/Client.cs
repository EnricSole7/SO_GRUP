﻿using System;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;

namespace WindowsFormsApplication1
{
    public partial class Client : Form
    {
        Socket server;
        Thread client;
        Thread game;
        string USER;
        string ServerState;
        string GAME;
        List<string> ListaConectados = new List<string>();
        List<string> Invitations = new List<string>();
        List<GameWndw> GameWndwForms = new List<GameWndw>();

        private static IPAddress direc = IPAddress.Parse("192.168.56.102");
        private static IPEndPoint ipep = new IPEndPoint(direc, 9049);

        public Client()
        {
            InitializeComponent();

            //CheckForIllegalCrossThreadCalls = false;    //permet als threads acedir als diferents controls del forms
            //forcem a que s'hagi de tancar el programa amb el botó de desconnectar
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //this.Bounds = Screen.PrimaryScreen.WorkingArea;

            database.Enabled = false;
            logout.Visible = false;
            welcomelbl.Visible = false;
            peticionesBox.Enabled = false;
            createGAME.Enabled = false;
            joinGame.Enabled = false;
            USER = null;
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
                                DelegateUSERdisCONNECTED del = new DelegateUSERdisCONNECTED(USERdisCONNECTED);
                                welcomelbl.Invoke(del);
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
                                else if (response == "incorrecto")
                                {
                                    MessageBox.Show("Incorrect User or Password", "Client", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    DelegateUSERnotCONNECTED del = new DelegateUSERnotCONNECTED(USERnotCONNECTED);
                                    welcomelbl.Invoke(del);
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
                                    MessageBox.Show("User Already Existing", "Client", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                        case 99: //NOTIFICACIO CONECTATS/DESCONNECTATS
                            {
                                string list = null;
                                string user_conn;
                                ListaConectados.Clear();    //reset de la llista de connectats (ara la plenarem)
                                int type_operation = Convert.ToInt32(parts[1].Split(',')[0]);

                                if (type_operation == 0)    //USER S'HA CONECTAT
                                {
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

                                    MessageBox.Show(user_conn + "\nUsers Online: " + conectados_en_string, "Client", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    
                                }
                                else if (type_operation == 1) //USER S'HA DESCONNECTAT
                                {
                                    response = parts[2].Split(',')[0];

                                    user_conn = response.Split('\n')[0];

                                    list = response.Split('\n')[1];     //conté la llista de connectats (|Name|Name|Name|...)

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

                                    MessageBox.Show(user_conn + "\nUsers Online: " + conectados_en_string, "Client", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

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
                                }

                                //Actualitzar llista connectats del joc

                                int i = 0;
                                while(i < GameWndwForms.Count)
                                {
                                    GameWndwForms[i].RefreshConnectedList(ListaConectados);
                                }


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
                        case 98:  //CREATE GAME
                            {
                                //int Nform = Convert.ToInt32(parts[1]);

                                response = parts[1];

                                if (response == "correcto")
                                {
                                    GAME = parts[2].Split(',')[0];
                                    //GameWndwForms[GameWndwForms.Count].SetGame(GAME);
                                }
                                break;
                            }
                        case 97:  //INVITATION RECEIVED
                            {
                                int type_operation = Convert.ToInt32(parts[1]);
                                int NForm;
                                
                                string invited;
                                string inviting;
                                int j = 0;

                                if (type_operation == 0) //rebem resposta a la nostra invitacio a un altre usuari
                                {
                                    //msg del tipus "97#0#%d#correcto#%s,"

                                    NForm = Convert.ToInt32(parts[1]);
                                    response = parts[3];

                                    if (response == "correcto")
                                    {
                                        invited = parts[4].Split(',')[0];
                                        GameWndwForms[NForm].InvitationSent(invited);
                                    }
                                    else
                                    {
                                        GameWndwForms[NForm].InvitationSent("ERROR");
                                    }
                                }
                                else if (type_operation == 1)   //rebem una invitacio d'un altre jugador
                                {
                                    //msg del tipus "97#1#%s,"
                                    inviting = parts[2].Split(',')[0];
                                    Invitations.Add(inviting);

                                    //HEM DE FER ARRIBAR LA INVITACIÓ AL CLIENT:
                                    //SI ESTA AL FORMS DEL CLIENT, -> MESSAGEBOX
                                    //SI ESTA AL FORMS DEL GAME, -> L'ENVIEM A TOTS ELS FORMS GAME QUE TINGUI OBERT VIA FUNCIÓ

                                    if(GameWndwForms.Count == 0)    //SI ESTA AL CLIENT:
                                    {
                                        MessageBox.Show("Invitaion from " + inviting + " received. Added to your invitation log in game", "Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else    //SI ESTA IN GAME, ENVIEM LA INVITACIO A TOTS ELS FORMS OBERTS
                                    {
                                        while (j < GameWndwForms.Count)
                                        {
                                            GameWndwForms[j].InvitationReceived(Invitations);
                                            j++;
                                        }
                                    }
                                }
                                break;
                            }
                    }
                }
            }
        }
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

        private void Desconectar_Click(object sender, EventArgs e)
        {
            if (ServerState == "UP") //si el servidor encara esta RUNNING es tanca
            {
                //mensaje de desconexión del USUARIO en cuestion
                string mensaje = "0/" + USER;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                // Se terminó el servicio. 
                // Nos desconectamos
                server.Shutdown(SocketShutdown.Both);
                //server.Close();
                ServerState = "DOWN";
                client.Abort(); //tanquem el thread d'aquest client
            }
            this.Close();
        }
        //BOTH SAME UTILITY
        private void Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ServerState == "UP") //si el servidor encara esta RUNNING es tanca
            {
                //mensaje de desconexión del USUARIO en cuestion
                string mensaje = "0/" + USER;
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

        private void logout_Click(object sender, EventArgs e)
        {
            string mensaje = "0/" + USER;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        private void login_Click(object sender, EventArgs e)
        {
            string nombre = nameBox.Text;
            string password = pswdBox.Text;

            if ((nombre != null) && (password != null))
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

            if ((nombre != null) && (password != null))
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

        private void database_Click(object sender, EventArgs e)
        {
            string mensaje_usuario = "100/";
            // Enviamos al servidor el nombre i contraseña introducidos
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje_usuario);
            server.Send(msg);
        }


        private void createGAME_Click(object sender, EventArgs e)
        {
            string mensaje = "98/" + USER;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //creem i inicialitzem el thread corresponent a aquest client per aquesta partida
            ThreadStart thGame = delegate { STARTGAME(); };
            game = new Thread(thGame);
            game.Start();
        }

        //THREAD DE createGAME
        private void STARTGAME()
        {
            int cont = GameWndwForms.Count;
            GameWndw G = new GameWndw();
            //PARAMETRES ESTETICS DEL LOBBY (HUD)
            G.SetLobby(cont, server, USER, Invitations);
            //PARAMETRES DE LA PARTIDA CREADA A MYSQL
            G.SetGame(GAME);
            //REFRESCAR LA LLISTA DE CONNECTATS PER PODER CONVIDAR
            G.RefreshConnectedList(ListaConectados);
            GameWndwForms.Add(G);
            G.ShowDialog();

            //quan acabi el joc:
            GameWndwForms.RemoveAt(cont);
        }
        private void joinGame_Click(object sender, EventArgs e)
        {

        }


        //DELEGATES

        delegate void DelegateUSERCONNECTED(string name);
        public void USERCONNECTED(string name)
        {
            welcomelbl.Text = "WELCOME " + name;
            welcomelbl.Visible = true;
            database.Enabled = true;
            logout.Visible = true;
            peticionesBox.Enabled = true;
            createGAME.Enabled = true;
            joinGame.Enabled = true;
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
            peticionesBox.Enabled = false;
            createGAME.Enabled = false;
            joinGame.Enabled = false;
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
            nameBox.Enabled = false;
            pswdBox.Enabled = false;
            login.Enabled = false;
            register.Enabled = false;
        }
    }
}
