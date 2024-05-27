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
    public partial class Stats : Form
    {
        string BaseDades;
        string USER;
        Socket SERVER;

        List<string> PlayerList = new List<string>();
        List<string> ResultsList = new List<string>();
        List<string> GamesList = new List<string>();
        public Stats()
        {
            InitializeComponent();
        }

        public void LoadStats(string usuari, Socket server)
        {
            this.USER = usuari;
            this.SERVER = server;
        }

        ////////////////////////////////////////////////////    PETICIONS STATS     //////////////////////////////////////////////////////////////////////////////

        private void check1_Click(object sender, EventArgs e)
        {
            string missatge = "3/" + USER;
            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(missatge);
            SERVER.Send(msg);
        }

        private void check2_Click(object sender, EventArgs e)
        {
            string players = playertxt.Text;

            if ((players != null) && (players != ""))
            {
                string missatge = "4/" + USER + "/" + players;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(missatge);
                SERVER.Send(msg);
            }
            else
            {
                MessageBox.Show("Introduce Valid Credentials", "Client", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void check3_Click(object sender, EventArgs e)
        {
            string d1 = date1.Text;
            string d2 = date2.Text;

            if ((d1 != null) && (d1 != "") && (d2 != null) && (d2 != ""))
            {
                string missatge = "5/" + d1 + "/" + d2;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(missatge);
                SERVER.Send(msg);
            }
            else
            {
                MessageBox.Show("Introduce Valid Credentials", "Client", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        ////////////////////////////////////////////////////    RESPOSTES SERVIDOR     //////////////////////////////////////////////////////////////////////////////


        public void SetResponse(string llista, int consulta)
        {
            this.PlayerList.Clear();
            this.ResultsList.Clear();
            this.GamesList.Clear();

            DelegateRESETGRID del = new DelegateRESETGRID(RESETGRID);
            grid.Invoke(del);

            string error = llista.Split('|')[0];

            if (error == "error")
            {
                if (consulta == 1)
                {
                    MessageBox.Show(llista.Split('|')[1], "Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (consulta == 2)
                {
                    MessageBox.Show(llista.Split('|')[1], "Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (consulta == 3)
                {
                    MessageBox.Show(llista.Split('|')[1], "Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                string spliting;

                if (consulta != 3)
                {
                    spliting = " ";
                    llista += spliting;
                }
                else
                {
                    spliting = "|";
                    llista += spliting;
                }

                int i = 0;
                int separador;
                string intermid = null;
                int row_counter = 0;

                while (i < llista.Length)
                {
                    separador = llista.IndexOf(spliting, i);

                    while (i < separador)
                    {
                        intermid += llista[i];
                        i++;
                    }
                    if (intermid != null)
                    {
                        if ((consulta == 1) && (!PlayerList.Contains(intermid)))
                        {
                            this.PlayerList.Add(intermid);
                            row_counter++;
                        }
                        else if ((consulta == 2) && (!ResultsList.Contains(intermid)))
                        {
                            this.ResultsList.Add(intermid);
                            row_counter++;
                        }
                        else if ((consulta == 3) && (!GamesList.Contains(intermid)))
                        {
                            this.GamesList.Add(intermid);
                            row_counter++;
                        }
                    }
                    intermid = null;
                    i++;
                }

                DelegateSETGRID del1 = new DelegateSETGRID(SETGRID);
                grid.Invoke(del1, new object[] { consulta, row_counter });
            }

        }

        private void infoPictureBox_cons2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("" +
            "                 Message Format:\n" +
            "   If multiple names, separate them by a blank space\n" +
            "              i.e. : Harry Paula Maia", "Format", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void infoPictureBox_cons3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("" +
            "                        Message Format:\n" +
            "  Display Day/Month/Year hour:minutes - Month with no 0 added\n" +
            "Please, make sure the second date is bigger than the first one\n" +
            "                     i.e. : 11-5-2021 17:14", "Format", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void returnbtn_Click(object sender, EventArgs e)
        {
            DelegateRESETTEXTS del = new DelegateRESETTEXTS(RESETTEXTS);
            playertxt.Invoke(del);
            this.PlayerList.Clear();
            this.ResultsList.Clear();
            this.GamesList.Clear();
            this.Close();
        }

        ////////////////////////////////////////////////////    DELEGATES     //////////////////////////////////////////////////////////////////////////////


        delegate void DelegateSETGRID(int consulta, int rows);
        public void SETGRID(int consulta, int rows)
        {
            grid.Rows.Clear();
            grid.ColumnCount = 1;
            grid.RowCount = rows;
            grid.RowHeadersVisible = false;
            grid.ColumnHeadersVisible = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            int j = 0;
            if (consulta == 1)
            {
                while (j < PlayerList.Count)
                {
                    grid.Rows[j].Cells[0].Value = PlayerList[j];
                    j++;
                }
            }
            else if (consulta == 2)
            {
                while (j < ResultsList.Count)
                {
                    grid.Rows[j].Cells[0].Value = ResultsList[j];
                    j++;
                }
            }
            else if (consulta == 3)
            {
                while (j < GamesList.Count)
                {
                    grid.Rows[j].Cells[0].Value = GamesList[j];
                    j++;
                }
            }
        }

        delegate void DelegateRESETTEXTS();

        public void RESETTEXTS()
        {
            this.playertxt.Text = null;
            this.date1.Text = null;
            this.date2.Text = null;
        }

        delegate void DelegateRESETGRID();

        public void RESETGRID()
        {
            this.grid.Rows.Clear();
        }







        /*
         * 
        //RowCount = rows;
        jugadoresGrid.ColumnCount = 3;
        jugadoresGrid.RowHeadersVisible = false;
        jugadoresGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
        jugadoresGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.DarkOrange;
        jugadoresGrid.Columns[0].HeaderText = "ID";
        jugadoresGrid.Columns[1].HeaderText = "NAME";
        jugadoresGrid.Columns[2].HeaderText = "PASSWORD";

        jugadoresGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        //RowCount = rows;
        serversGrid.ColumnCount = 2;
        serversGrid.RowHeadersVisible = false;
        serversGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
        serversGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.DarkOrange;
        serversGrid.Columns[0].HeaderText = "ID";
        serversGrid.Columns[1].HeaderText = "HOST";

        serversGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        //RowCount = rows;
        partidasGrid.ColumnCount = 8;
        partidasGrid.RowHeadersVisible = false;
        partidasGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
        partidasGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.DarkOrange;
        partidasGrid.Columns[0].HeaderText = "PLAYER 1";
        partidasGrid.Columns[1].HeaderText = "PLAYER 2";
        partidasGrid.Columns[2].HeaderText = "PLAYER 3";
        partidasGrid.Columns[3].HeaderText = "PLAYER 4";
        partidasGrid.Columns[4].HeaderText = "PLAYER 5";
        partidasGrid.Columns[5].HeaderText = "HOST";
        partidasGrid.Columns[6].HeaderText = "DATE";
        partidasGrid.Columns[7].HeaderText = "MINIGAME";

        partidasGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        public void SetBD(string BD)
        {
            this.BaseDades = BD;

            int i = 0;
            string jugadores = null;
            string servidores = null;
            string partidas = null;

            int separacion = BaseDades.IndexOf("|");   //posicion de separacion jug - servidor
            
            while (i< separacion)
            {
                jugadores += BaseDades[i];
                i++;
            }
            
            //como solo hay dos / (entre jugador - servidor y servidor - partida) usamos LastIndexOf
            separacion = BaseDades.LastIndexOf("|");    //posicion de separacion servidor - partida

            while (i < separacion)
            {
                servidores += BaseDades[i];
                i++;
            }

            while (i < BaseDades.Length)
            {
                partidas += BaseDades[i];
                i++;
            }

            InitializeJugadores(jugadores);
            InitializeServidores(servidores);
            InitializePartidas(partidas);
        }

        public void InitializeJugadores(string jugadores)
        {
            //exemple -> 234-Enric-enric03_235-Alan-alannn7_ ... | + servidors + | + partides + ,
            List<string> id_jug = new List<string>();
            List<string> jugador = new List<string>();
            List<string> contrasenya = new List<string>();

            int i = 0;  //index llistes 
            int k = 0;  //index string jugadores
            int separacion_atributos;
            string intermid = null;

            while (k < jugadores.Length)
            {
                //obtenim un jugador per complet a cada iteració

                separacion_atributos = jugadores.IndexOf("-", k);   //cada atribut esta separat per -
                while (k < separacion_atributos)
                {
                    intermid += jugadores[k];
                    k++;
                }
                id_jug.Add(intermid);
                intermid = null;
                k++;    //ens situem a l'index de després de - per no copiar-ho com a atribut
                separacion_atributos = jugadores.IndexOf("-", k);   //cada atribut esta separat per -
                while (k < separacion_atributos)
                {
                    intermid += jugadores[k];
                    k++;
                }
                jugador.Add(intermid);
                intermid = null;
                k++;
                separacion_atributos = jugadores.IndexOf("_", k);   //ultim atribut separat per _
                while (k < separacion_atributos)
                {
                    intermid += jugadores[k];
                    k++;
                }
                contrasenya.Add(intermid);
                intermid = null;
                k++;
                i++;
            }

            jugadoresGrid.RowCount = i; //i = numero de jugadors

            int j = 0;
            while (j < i)   //insertem a la taula jugadoresGrid els parametres de cada jugador
            {
                jugadoresGrid.Rows[j].Cells[0].Value = id_jug[j];
                jugadoresGrid.Rows[j].Cells[1].Value = jugador[j];
                jugadoresGrid.Rows[j].Cells[2].Value = contrasenya[j];
                j++;
            }
        }

        public void InitializeServidores(string servidores)
        {
            //exemple -> 2-Madrid_5-Barcelona ... |
            List<string> id_serv = new List<string>();  
            List<string> servidor = new List<string>();

            int i = 0;  //index llistes 
            int k = 1;  //index string servers (k = 0 es el separador | amb jugadors)
            int separacion_atributos;
            string intermid = null;

            while (k < servidores.Length) //un sol servidor
            {
                //obtenim un servidor per complet a cada iteració

                separacion_atributos = servidores.IndexOf("-", k);   //cada atribut esta separat per -
                while (k < separacion_atributos)
                {
                    intermid += servidores[k];
                    k++;
                }
                id_serv.Add(intermid);
                intermid = null;
                k++;    //ens situem a l'index de després de - per no copiar-ho com a atribut
                separacion_atributos = servidores.IndexOf("_", k);   //ultim atribut separat per _
                while (k < separacion_atributos)
                {
                    intermid += servidores[k];
                    k++;
                }
                servidor.Add(intermid);
                intermid = null;
                k++;
                i++;
            }

            serversGrid.RowCount = i; //i = numero de servidors

            int j = 0;
            while (j < i)   //insertem a la taula serversGrid els parametres de cada servidor
            {
                serversGrid.Rows[j].Cells[0].Value = id_serv[j];
                serversGrid.Rows[j].Cells[1].Value = servidor[j];
                j++;
            }
        }

        public void InitializePartidas(string partidas)
        {
            //exemple -> 234-235-2-12/04/2024-234_ ... |
            List<string> id_jug1 = new List<string>();
            List<string> id_jug2 = new List<string>();
            List<string> id_jug3 = new List<string>();
            List<string> id_jug4 = new List<string>();
            List<string> id_jug5 = new List<string>();
            List<string> id_serv = new List<string>();
            List<string> date = new List<string>();
            List<string> guanyador = new List<string>();

            int i = 0;  //index llistes 
            int k = 1;  //index string partidas (k = 0 es el separador | amb servers)
            int separacion_atributos;
            string intermid = null;

            while (k < partidas.Length)
            {
                //obtenim un jugador per complet a cada iteració

                separacion_atributos = partidas.IndexOf("-", k);   //cada atribut esta separat per -
                while (k < separacion_atributos)
                {
                    intermid += partidas[k];
                    k++;
                }
                id_jug1.Add(intermid);
                intermid = null;
                k++;    //ens situem a l'index de després de - per no copiar-ho com a atribut
                separacion_atributos = partidas.IndexOf("-", k);   //cada atribut esta separat per -
                while (k < separacion_atributos)
                {
                    intermid += partidas[k];
                    k++;
                }
                id_jug2.Add(intermid);
                intermid = null;
                k++;    //ens situem a l'index de després de - per no copiar-ho com a atribut
                separacion_atributos = partidas.IndexOf("-", k);   //cada atribut esta separat per -
                while (k < separacion_atributos)
                {
                    intermid += partidas[k];
                    k++;
                }
                id_jug3.Add(intermid);
                intermid = null;
                k++;    //ens situem a l'index de després de - per no copiar-ho com a atribut
                separacion_atributos = partidas.IndexOf("-", k);   //cada atribut esta separat per -
                while (k < separacion_atributos)
                {
                    intermid += partidas[k];
                    k++;
                }
                id_jug4.Add(intermid);
                intermid = null;
                k++;    //ens situem a l'index de després de - per no copiar-ho com a atribut
                separacion_atributos = partidas.IndexOf("-", k);   //cada atribut esta separat per -
                while (k < separacion_atributos)
                {
                    intermid += partidas[k];
                    k++;
                }
                id_jug5.Add(intermid);
                intermid = null;
                k++;    //ens situem a l'index de després de - per no copiar-ho com a atribut
                separacion_atributos = partidas.IndexOf("-", k);   //cada atribut esta separat per -
                while (k < separacion_atributos)
                {
                    intermid += partidas[k];
                    k++;
                }
                id_serv.Add(intermid);
                intermid = null;
                k++;    //ens situem a l'index de després de - per no copiar-ho com a atribut
                separacion_atributos = partidas.IndexOf("-", k);   //cada atribut esta separat per -
                while (k < separacion_atributos)
                {
                    intermid += partidas[k];
                    k++;
                }
                date.Add(intermid);
                intermid = null;
                k++;
                separacion_atributos = partidas.IndexOf("_", k);   //ultim atribut separat per _
                while (k < separacion_atributos)
                {
                    intermid += partidas[k];
                    k++;
                }
                guanyador.Add(intermid);
                intermid = null;
                k++;
                i++;
            }

            partidasGrid.RowCount = i; //i = numero de jugadors

            int j = 0;
            while (j < i)   //insertem a la taula partidasGrid els parametres de cada partida
            {
                partidasGrid.Rows[j].Cells[0].Value = id_jug1[j];
                partidasGrid.Rows[j].Cells[1].Value = id_jug2[j];
                partidasGrid.Rows[j].Cells[2].Value = id_jug3[j];
                partidasGrid.Rows[j].Cells[3].Value = id_jug4[j];
                partidasGrid.Rows[j].Cells[4].Value = id_jug5[j];
                partidasGrid.Rows[j].Cells[5].Value = id_serv[j];
                partidasGrid.Rows[j].Cells[6].Value = date[j];
                partidasGrid.Rows[j].Cells[7].Value = guanyador[j];
                j++;
            }
        }
        */
    }
}
