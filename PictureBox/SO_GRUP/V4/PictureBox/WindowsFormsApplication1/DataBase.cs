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
    public partial class DataBase : Form
    {
        string BaseDades;
        public DataBase()
        {
            InitializeComponent();

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
        }

        private void FormLogin_Load_1(object sender, EventArgs e)
        {
            
        }


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
    }
}
