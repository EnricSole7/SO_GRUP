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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApplication1
{
    public partial class Client : Form
    {
        Socket server;
        public Client()
        {
            InitializeComponent();

            database.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, 9080);


            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
                //this.BackColor = Color.Green;
                MessageBox.Show("CONNECTED TO SERVER");

            }
            catch (SocketException ex)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("SERVER NOT CONNECTED");
                return;
            }

        }
        
        private void Enviar_nombre_Click(object sender, EventArgs e)
        {
            string mensaje = "5/" + nombre.Text;
            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Recibimos la respuesta del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split (',')[0];
            MessageBox.Show("Las localizaciones de los servers son: " + mensaje);

        }

        private void Enviar_fecha_Click(object sender, EventArgs e)
        {
            string mensaje = "6/" + fecha.Text;
            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Recibimos la respuesta del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split(',')[0];
            MessageBox.Show("El ganador es: " + mensaje);
        }

        private void Enviar_server_Click(object sender, EventArgs e)
        {
            string mensaje = "7/" + server_box.Text;
            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Recibimos la respuesta del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split(',')[0];
            MessageBox.Show("Los jugadores son: " + mensaje);
        }

        private void Desconectar_Click(object sender, EventArgs e)
        {
            //mensaje de desconexión
            string mensaje = "0/";

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            // Se terminó el servicio. 
            // Nos desconectamos
            this.BackColor = Color.Gray;
            server.Shutdown(SocketShutdown.Both);
            server.Close();
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

                //Recibimos la respuesta del servidor
                byte[] msg_response = new byte[80];
                server.Receive(msg_response);
                string response = Encoding.ASCII.GetString(msg_response).Split(',')[0];

                if (response == "correcto")
                {
                    MessageBox.Show("Login Successful", "Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    database.Enabled = true;
                    nameBox.Text = "";
                    pswdBox.Text = "";
                }
                else if (response == "incorrecto")
                {
                    MessageBox.Show("Incorrect User or Password", "Client", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    nameBox.Text = "";
                    pswdBox.Text = "";
                }
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

                //Recibimos la respuesta del servidor
                byte[] msg_response = new byte[80];
                server.Receive(msg_response);
                string response = Encoding.ASCII.GetString(msg_response).Split(',')[0];
                //MessageBox.Show("La longitud de tu nombre es: " + mensaje);

                if (response == "correcto")
                {
                    MessageBox.Show("User Added Correclty", "Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    database.Enabled = true;
                    nameBox.Text = "";
                    pswdBox.Text = "";
                }
                else if (response == "incorrecto")
                {
                    MessageBox.Show("User Already Existing", "Client", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    nameBox.Text = "";
                    pswdBox.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Introduce valores validos");
            }
        }

        private void database_Click(object sender, EventArgs e)
        {
            string mensaje_usuario = "100/";
            // Enviamos al servidor el nombre i contraseña introducidos
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje_usuario);
            server.Send(msg);

            //Recibimos la respuesta del servidor
            byte[] msg_response = new byte[500];
            server.Receive(msg_response);
            string response = Encoding.ASCII.GetString(msg_response).Split(',')[0];

            DataBase formDB = new DataBase();
            formDB.SetBD(response);
            formDB.ShowDialog();
        }
    }
}
