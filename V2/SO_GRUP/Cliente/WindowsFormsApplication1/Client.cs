using System;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Client : Form
    {
        Socket server;
        Thread client;
        string USUARIO;
        string ServerState;

        public Client()
        {
            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;    //permet als threads acedir als diferents controls del forms
            //forcem a que s'hagi de tancar el programa amb el botó de desconnectar
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //this.Bounds = Screen.PrimaryScreen.WorkingArea;

            database.Enabled = false;
            clientList.Enabled = false;
            logout.Visible = false;
            welcomelbl.Visible = false;
            peticionesBox.Enabled = false;
            createGAME.Enabled = false;
            joinGame.Enabled = false;
            USUARIO = null;
        }

        private void Main()
        {
            while (true)
            {
                //Recibimos la respuesta del servidor
                byte[] msg_response = new byte[300];
                server.Receive(msg_response);

                //msg del tipus "2/Juan,"
                string[] parts = Encoding.ASCII.GetString(msg_response).Split('/');
                int code = Convert.ToInt32(parts[0]);
                string response = parts[1].Split(',')[0];

                switch (code)
                {
                    case 1:
                        {
                            if (response == "correcto")
                            {
                                string nombre = nameBox.Text;
                                USUARIO = nombre;
                                MessageBox.Show("Login Successful", "Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                database.Enabled = true;
                                clientList.Enabled = true;
                                logout.Visible = true;
                                peticionesBox.Enabled = true;
                                createGAME.Enabled = true;
                                joinGame.Enabled = true;
                                welcomelbl.Visible = true;
                                welcomelbl.Text = "WELCOME " + nombre;
                                nameBox.Text = "";
                                pswdBox.Text = "";
                                nameBox.Visible = false;
                                pswdBox.Visible = false;
                                login.Visible = false;
                                register.Visible = false;
                                namelbl.Visible = false;
                                pswdlbl.Visible = false;
                            }
                            else if (response == "incorrecto")
                            {
                                MessageBox.Show("Incorrect User or Password", "Client", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                nameBox.Text = "";
                                pswdBox.Text = "";
                            }
                            break;
                        }
                    case 2:  //REGISTER
                        {
                            if (response == "correcto")
                            {
                                string nombre = nameBox.Text;
                                USUARIO = nombre;
                                MessageBox.Show("User Added Correclty", "Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                database.Enabled = true;
                                clientList.Enabled = true;
                                logout.Visible = true;
                                peticionesBox.Enabled = true;
                                createGAME.Enabled = true;
                                joinGame.Enabled = true;
                                welcomelbl.Visible = true;
                                welcomelbl.Text = "WELCOME " + nombre;
                                nameBox.Text = "";
                                pswdBox.Text = "";
                                nameBox.Visible = false;
                                pswdBox.Visible = false;
                                login.Visible = false;
                                register.Visible = false;
                                namelbl.Visible = false;
                                pswdlbl.Visible = false;
                            }
                            else if (response == "incorrecto")
                            {
                                MessageBox.Show("User Already Existing", "Client", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                nameBox.Text = "";
                                pswdBox.Text = "";
                            }
                            break;
                        }
                    case 3:  //CONSULTA 1
                        {
                            MessageBox.Show(response, "Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            nombre.Text = "";
                            break;
                        }
                    case 4:  //CONSULTA 2
                        {
                            MessageBox.Show(response, "Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            fecha.Text = "";
                            break;
                        }
                    case 5:  //CONSULTA 3
                        {
                            MessageBox.Show(response, "Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            server_box.Text = "";
                            break;
                        }
                    case 99: //SHOW CONECTADOS
                        {
                            if (response != null)
                            {
                                MessageBox.Show("Connected Users: " + response, "Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("No Users Connected", "Client", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            break;
                        }
                    case 100:    //SHOW DATABASE
                        {
                            DataBase formDB = new DataBase();
                            formDB.SetBD(response);
                            formDB.ShowDialog();
                            break;
                        }
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            
            //Creamos el socket 
            /*
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
                //this.BackColor = Color.Green;
                MessageBox.Show("CONNECTED TO SERVER");
                ServerState = "UP";
                offlinelbl.Visible = false;
                conectserver.Visible = false;
            }
            catch (SocketException ex)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("SERVER NOT CONNECTED");
                ServerState = "DOWN";
                offlinelbl.Visible = true;
                conectserver.Visible = true;
                return;
            }
            */
        }

        private void Desconectar_Click(object sender, EventArgs e)
        {
            if(ServerState == "UP") //si el servidor encara esta RUNNING es tanca
            {
                //mensaje de desconexión del USUARIO en cuestion
                string mensaje = "0/" + USUARIO;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                // Se terminó el servicio. 
                // Nos desconectamos
                server.Shutdown(SocketShutdown.Both);
                server.Close();
                ServerState = "DOWN";
                client.Abort(); //tanquem el thread d'aquest client
            }
            this.Close();
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
            string name = nombre.Text;

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
            string date = fecha.Text;

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
            string servr = server_box.Text;

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

        private void clientList_Click(object sender, EventArgs e)
        {
            string mensaje_usuario = "99/";
            // Enviamos al servidor el nombre i contraseña introducidos
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje_usuario);
            server.Send(msg);
        }

        private void logout_Click(object sender, EventArgs e)
        {
            string mensaje = "0/" + USUARIO;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            // Se terminó el servicio. 
            // Nos desconectamos
            server.Shutdown(SocketShutdown.Both);
            server.Close();
            ServerState = "DOWN";

            database.Enabled = false;
            clientList.Enabled = false;
            logout.Visible = false;
            peticionesBox.Enabled = false;
            createGAME.Enabled = false;
            joinGame.Enabled = false;
            welcomelbl.Visible = false;
            welcomelbl.Text = "";
            nameBox.Visible = true;
            pswdBox.Visible = true;
            login.Visible = true;
            register.Visible = true;
            namelbl.Visible = true;
            pswdlbl.Visible = true;
        }

        private void conectserver_Click(object sender, EventArgs e)
        {
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, 9018);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
                //this.BackColor = Color.Green;
                MessageBox.Show("CONNECTION ESTABLISHED");
                ServerState = "UP";
                offlinelbl.Visible = false;
                conectserver.Visible = false;
            }
            catch (SocketException ex)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("SERVER NOT CONNECTED");
                ServerState = "DOWN";
                offlinelbl.Visible = true;
                conectserver.Visible = true;
                return;
            }
            //creem i inicialitzem el thread corresponent a aquest client
            ThreadStart thMain = delegate { Main(); };
            client = new Thread(thMain);
            client.Start();
        }

        private void createGAME_Click(object sender, EventArgs e)
        {
            GameWndw G = new GameWndw();
            G.SetGame(server, USUARIO);
            G.ShowDialog();
        }

        private void joinGame_Click(object sender, EventArgs e)
        {

        }
    }
}
