using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class FormLogin : Form
    {
        Socket server;
        Form1 f1 = new Form1();
        public FormLogin()
        {
            InitializeComponent();
        }
        

        private void FormLogin_Load_1(object sender, EventArgs e)
        {
            
        }
        private void Login_Click(object sender, EventArgs e)
        {
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("192.168.1.43");
            IPEndPoint ipep = new IPEndPoint(direc, 9050);


            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
                this.BackColor = Color.Green;
                MessageBox.Show("Conectado");

            }
            catch (SocketException ex)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("No he podido conectar con el servidor");
                return;
            }
            string nombre = textBox2.Text;
            string password = textBox1.Text;

            if ((nombre != null) && (password != null))
            {
                string mensaje_sesion = "1/" + nombre +"/"+password+"/";
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje_sesion);
                server.Send(msg);
                
                

                //Recibimos la respuesta del servidor
                byte[] msg_response = new byte[80];
                server.Receive(msg_response);
                string response = Encoding.ASCII.GetString(msg_response).Split('\0')[0];
                //MessageBox.Show("La longitud de tu nombre es: " + mensaje);

                if (response == "correcto")
                {
                    MessageBox.Show("Inicio de sesión exitoso");
                   
                    f1.Show();
                    this.Close();
                }
                else if (response == "incorrecto")
                {
                    MessageBox.Show("Usuario o contraseña incorrecto");
                }
            }
            else
            {
                MessageBox.Show("Introduce valores validos");
            }
        }

        private void Register_Click(object sender, EventArgs e)
        {
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("192.168.1.43");
            IPEndPoint ipep = new IPEndPoint(direc, 9070);


            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
                this.BackColor = Color.Green;
                MessageBox.Show("Conectado");

            }
            catch (SocketException ex)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("No he podido conectar con el servidor");
                return;
            }
            string nombre = textBox2.Text;
            string password = textBox1.Text;

            if ((nombre != null) && (password != null))
            {
                string mensaje_registro = "2/" + nombre +"/"+ password +"/";
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje_registro);
                server.Send(msg);
               

                //Recibimos la respuesta del servidor
                byte[] msg_response = new byte[80];
                server.Receive(msg_response);
                string response = Encoding.ASCII.GetString(msg_response).Split('\0')[0];
                //MessageBox.Show("La longitud de tu nombre es: " + mensaje);

                if (response == "correcto")
                {
                    MessageBox.Show("Usuario registrado");
                    Form1 f1 = new Form1();
                    f1.Show();
                    this.Close();
                }
                else if (response == "incorrecto")
                {
                    MessageBox.Show("Usuario ya existente");
                }
            }
            else
            {
                MessageBox.Show("Introduce valores validos");
            }
        }
    }
}
