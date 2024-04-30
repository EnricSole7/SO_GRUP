namespace WindowsFormsApplication1
{
    partial class Client
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label2 = new System.Windows.Forms.Label();
            this.name_txt = new System.Windows.Forms.TextBox();
            this.Enviar_nombre = new System.Windows.Forms.Button();
            this.statsBox = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Enviar_server = new System.Windows.Forms.Button();
            this.server_txt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Enviar_fecha = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.date_txt = new System.Windows.Forms.TextBox();
            this.Desconectar = new System.Windows.Forms.Button();
            this.login = new System.Windows.Forms.Button();
            this.user_lbl = new System.Windows.Forms.Label();
            this.register = new System.Windows.Forms.Button();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.pswdBox = new System.Windows.Forms.TextBox();
            this.namelbl = new System.Windows.Forms.Label();
            this.pswdlbl = new System.Windows.Forms.Label();
            this.database = new System.Windows.Forms.Button();
            this.welcomelbl = new System.Windows.Forms.Label();
            this.logout = new System.Windows.Forms.Button();
            this.conectserver = new System.Windows.Forms.Button();
            this.offlinelbl = new System.Windows.Forms.Label();
            this.createGAME = new System.Windows.Forms.Button();
            this.joinGame = new System.Windows.Forms.Button();
            this.DEV_closebtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numgameslbl = new System.Windows.Forms.Label();
            this.labelTitulo = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.statsbtn = new System.Windows.Forms.Button();
            this.returnbtn = new System.Windows.Forms.Button();
            this.numplayerslbl = new System.Windows.Forms.Label();
            this.playersonlineGrid = new System.Windows.Forms.DataGridView();
            this.statsBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.playersonlineGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(22, 37);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "NAME";
            // 
            // name_txt
            // 
            this.name_txt.Location = new System.Drawing.Point(100, 37);
            this.name_txt.Margin = new System.Windows.Forms.Padding(4);
            this.name_txt.Name = "name_txt";
            this.name_txt.Size = new System.Drawing.Size(219, 22);
            this.name_txt.TabIndex = 3;
            // 
            // Enviar_nombre
            // 
            this.Enviar_nombre.Location = new System.Drawing.Point(327, 37);
            this.Enviar_nombre.Margin = new System.Windows.Forms.Padding(4);
            this.Enviar_nombre.Name = "Enviar_nombre";
            this.Enviar_nombre.Size = new System.Drawing.Size(100, 24);
            this.Enviar_nombre.TabIndex = 5;
            this.Enviar_nombre.Text = "SEND";
            this.Enviar_nombre.UseVisualStyleBackColor = true;
            this.Enviar_nombre.Click += new System.EventHandler(this.Enviar_nombre_Click);
            // 
            // statsBox
            // 
            this.statsBox.BackColor = System.Drawing.SystemColors.Control;
            this.statsBox.Controls.Add(this.label7);
            this.statsBox.Controls.Add(this.Enviar_server);
            this.statsBox.Controls.Add(this.server_txt);
            this.statsBox.Controls.Add(this.label6);
            this.statsBox.Controls.Add(this.label5);
            this.statsBox.Controls.Add(this.Enviar_fecha);
            this.statsBox.Controls.Add(this.label4);
            this.statsBox.Controls.Add(this.label3);
            this.statsBox.Controls.Add(this.date_txt);
            this.statsBox.Controls.Add(this.label2);
            this.statsBox.Controls.Add(this.Enviar_nombre);
            this.statsBox.Controls.Add(this.name_txt);
            this.statsBox.Location = new System.Drawing.Point(340, 116);
            this.statsBox.Margin = new System.Windows.Forms.Padding(4);
            this.statsBox.Name = "statsBox";
            this.statsBox.Padding = new System.Windows.Forms.Padding(4);
            this.statsBox.Size = new System.Drawing.Size(459, 251);
            this.statsBox.TabIndex = 6;
            this.statsBox.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(29, 215);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(310, 16);
            this.label7.TabIndex = 17;
            this.label7.Text = "Returns the player/s that have played on this server";
            // 
            // Enviar_server
            // 
            this.Enviar_server.Location = new System.Drawing.Point(327, 188);
            this.Enviar_server.Margin = new System.Windows.Forms.Padding(4);
            this.Enviar_server.Name = "Enviar_server";
            this.Enviar_server.Size = new System.Drawing.Size(100, 24);
            this.Enviar_server.TabIndex = 16;
            this.Enviar_server.Text = "SEND";
            this.Enviar_server.UseVisualStyleBackColor = true;
            this.Enviar_server.Click += new System.EventHandler(this.Enviar_server_Click);
            // 
            // server_txt
            // 
            this.server_txt.Location = new System.Drawing.Point(122, 189);
            this.server_txt.Margin = new System.Windows.Forms.Padding(4);
            this.server_txt.Name = "server_txt";
            this.server_txt.Size = new System.Drawing.Size(197, 22);
            this.server_txt.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 140);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(303, 16);
            this.label6.TabIndex = 14;
            this.label6.Text = "Returns the minigame that was played on this date";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(22, 189);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 25);
            this.label5.TabIndex = 13;
            this.label5.Text = "SERVER";
            // 
            // Enviar_fecha
            // 
            this.Enviar_fecha.Location = new System.Drawing.Point(327, 111);
            this.Enviar_fecha.Margin = new System.Windows.Forms.Padding(4);
            this.Enviar_fecha.Name = "Enviar_fecha";
            this.Enviar_fecha.Size = new System.Drawing.Size(100, 24);
            this.Enviar_fecha.TabIndex = 8;
            this.Enviar_fecha.Text = "SEND";
            this.Enviar_fecha.UseVisualStyleBackColor = true;
            this.Enviar_fecha.Click += new System.EventHandler(this.Enviar_fecha_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(308, 16);
            this.label4.TabIndex = 12;
            this.label4.Text = "Returns the location/s where this player has played\r\n";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(22, 111);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 25);
            this.label3.TabIndex = 11;
            this.label3.Text = "DATE";
            // 
            // date_txt
            // 
            this.date_txt.Location = new System.Drawing.Point(100, 114);
            this.date_txt.Margin = new System.Windows.Forms.Padding(4);
            this.date_txt.Name = "date_txt";
            this.date_txt.Size = new System.Drawing.Size(219, 22);
            this.date_txt.TabIndex = 10;
            // 
            // Desconectar
            // 
            this.Desconectar.Location = new System.Drawing.Point(340, 328);
            this.Desconectar.Margin = new System.Windows.Forms.Padding(4);
            this.Desconectar.Name = "Desconectar";
            this.Desconectar.Size = new System.Drawing.Size(130, 39);
            this.Desconectar.TabIndex = 7;
            this.Desconectar.Text = "CLOSE";
            this.Desconectar.UseVisualStyleBackColor = true;
            this.Desconectar.Click += new System.EventHandler(this.Desconectar_Click);
            // 
            // login
            // 
            this.login.Location = new System.Drawing.Point(20, 204);
            this.login.Margin = new System.Windows.Forms.Padding(4);
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(112, 38);
            this.login.TabIndex = 8;
            this.login.Text = "LOGIN";
            this.login.UseVisualStyleBackColor = true;
            this.login.Click += new System.EventHandler(this.login_Click);
            // 
            // user_lbl
            // 
            this.user_lbl.AutoSize = true;
            this.user_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.user_lbl.Location = new System.Drawing.Point(6, 23);
            this.user_lbl.Name = "user_lbl";
            this.user_lbl.Size = new System.Drawing.Size(79, 29);
            this.user_lbl.TabIndex = 9;
            this.user_lbl.Text = "USER";
            // 
            // register
            // 
            this.register.Location = new System.Drawing.Point(150, 204);
            this.register.Margin = new System.Windows.Forms.Padding(4);
            this.register.Name = "register";
            this.register.Size = new System.Drawing.Size(112, 38);
            this.register.TabIndex = 10;
            this.register.Text = "REGISTER";
            this.register.UseVisualStyleBackColor = true;
            this.register.Click += new System.EventHandler(this.register_Click);
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(111, 73);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(137, 22);
            this.nameBox.TabIndex = 11;
            // 
            // pswdBox
            // 
            this.pswdBox.Location = new System.Drawing.Point(111, 126);
            this.pswdBox.Name = "pswdBox";
            this.pswdBox.Size = new System.Drawing.Size(137, 22);
            this.pswdBox.TabIndex = 12;
            // 
            // namelbl
            // 
            this.namelbl.AutoSize = true;
            this.namelbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.namelbl.Location = new System.Drawing.Point(7, 75);
            this.namelbl.Name = "namelbl";
            this.namelbl.Size = new System.Drawing.Size(58, 20);
            this.namelbl.TabIndex = 13;
            this.namelbl.Text = "Name:";
            // 
            // pswdlbl
            // 
            this.pswdlbl.AutoSize = true;
            this.pswdlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pswdlbl.Location = new System.Drawing.Point(7, 128);
            this.pswdlbl.Name = "pswdlbl";
            this.pswdlbl.Size = new System.Drawing.Size(88, 20);
            this.pswdlbl.TabIndex = 14;
            this.pswdlbl.Text = "Password:";
            // 
            // database
            // 
            this.database.Location = new System.Drawing.Point(340, 217);
            this.database.Margin = new System.Windows.Forms.Padding(4);
            this.database.Name = "database";
            this.database.Size = new System.Drawing.Size(130, 50);
            this.database.TabIndex = 15;
            this.database.Text = "SHOW DATABASE";
            this.database.UseVisualStyleBackColor = true;
            this.database.Click += new System.EventHandler(this.database_Click);
            // 
            // welcomelbl
            // 
            this.welcomelbl.AutoSize = true;
            this.welcomelbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.welcomelbl.Location = new System.Drawing.Point(15, 148);
            this.welcomelbl.Name = "welcomelbl";
            this.welcomelbl.Size = new System.Drawing.Size(0, 29);
            this.welcomelbl.TabIndex = 17;
            // 
            // logout
            // 
            this.logout.Location = new System.Drawing.Point(74, 204);
            this.logout.Margin = new System.Windows.Forms.Padding(4);
            this.logout.Name = "logout";
            this.logout.Size = new System.Drawing.Size(130, 37);
            this.logout.TabIndex = 18;
            this.logout.Text = "LOGOUT";
            this.logout.UseVisualStyleBackColor = true;
            this.logout.Click += new System.EventHandler(this.logout_Click);
            // 
            // conectserver
            // 
            this.conectserver.Location = new System.Drawing.Point(736, 13);
            this.conectserver.Margin = new System.Windows.Forms.Padding(4);
            this.conectserver.Name = "conectserver";
            this.conectserver.Size = new System.Drawing.Size(201, 38);
            this.conectserver.TabIndex = 19;
            this.conectserver.Text = "CONNECT TO SERVER";
            this.conectserver.UseVisualStyleBackColor = true;
            this.conectserver.Click += new System.EventHandler(this.conectserver_Click);
            // 
            // offlinelbl
            // 
            this.offlinelbl.AutoSize = true;
            this.offlinelbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.offlinelbl.ForeColor = System.Drawing.Color.Red;
            this.offlinelbl.Location = new System.Drawing.Point(771, 55);
            this.offlinelbl.Name = "offlinelbl";
            this.offlinelbl.Size = new System.Drawing.Size(130, 31);
            this.offlinelbl.TabIndex = 20;
            this.offlinelbl.Text = "OFFLINE";
            // 
            // createGAME
            // 
            this.createGAME.Location = new System.Drawing.Point(340, 119);
            this.createGAME.Margin = new System.Windows.Forms.Padding(4);
            this.createGAME.Name = "createGAME";
            this.createGAME.Size = new System.Drawing.Size(130, 39);
            this.createGAME.TabIndex = 21;
            this.createGAME.Text = "NEW GAME";
            this.createGAME.UseVisualStyleBackColor = true;
            this.createGAME.Click += new System.EventHandler(this.createGAME_Click);
            // 
            // joinGame
            // 
            this.joinGame.Location = new System.Drawing.Point(340, 170);
            this.joinGame.Margin = new System.Windows.Forms.Padding(4);
            this.joinGame.Name = "joinGame";
            this.joinGame.Size = new System.Drawing.Size(130, 39);
            this.joinGame.TabIndex = 22;
            this.joinGame.Text = "JOIN GAME";
            this.joinGame.UseVisualStyleBackColor = true;
            this.joinGame.Click += new System.EventHandler(this.joinGame_Click);
            // 
            // DEV_closebtn
            // 
            this.DEV_closebtn.Location = new System.Drawing.Point(710, 346);
            this.DEV_closebtn.Margin = new System.Windows.Forms.Padding(4);
            this.DEV_closebtn.Name = "DEV_closebtn";
            this.DEV_closebtn.Size = new System.Drawing.Size(157, 38);
            this.DEV_closebtn.TabIndex = 23;
            this.DEV_closebtn.Text = "CLOSE SERVER";
            this.DEV_closebtn.UseVisualStyleBackColor = true;
            this.DEV_closebtn.Click += new System.EventHandler(this.DEV_closebtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numplayerslbl);
            this.groupBox1.Controls.Add(this.numgameslbl);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.user_lbl);
            this.groupBox1.Controls.Add(this.login);
            this.groupBox1.Controls.Add(this.register);
            this.groupBox1.Controls.Add(this.nameBox);
            this.groupBox1.Controls.Add(this.pswdBox);
            this.groupBox1.Controls.Add(this.namelbl);
            this.groupBox1.Controls.Add(this.logout);
            this.groupBox1.Controls.Add(this.pswdlbl);
            this.groupBox1.Controls.Add(this.welcomelbl);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(284, 355);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(86, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 16);
            this.label1.TabIndex = 26;
            this.label1.Text = "___________________";
            // 
            // numgameslbl
            // 
            this.numgameslbl.AutoSize = true;
            this.numgameslbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numgameslbl.Location = new System.Drawing.Point(16, 271);
            this.numgameslbl.Name = "numgameslbl";
            this.numgameslbl.Size = new System.Drawing.Size(0, 20);
            this.numgameslbl.TabIndex = 27;
            // 
            // labelTitulo
            // 
            this.labelTitulo.AutoSize = true;
            this.labelTitulo.BackColor = System.Drawing.Color.Transparent;
            this.labelTitulo.Font = new System.Drawing.Font("Brandish", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitulo.Location = new System.Drawing.Point(326, 12);
            this.labelTitulo.Name = "labelTitulo";
            this.labelTitulo.Size = new System.Drawing.Size(386, 81);
            this.labelTitulo.TabIndex = 25;
            this.labelTitulo.Tag = "04";
            this.labelTitulo.Text = "PLAY COOP";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(337, 91);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(327, 16);
            this.label11.TabIndex = 30;
            this.label11.Text = "________________________________________";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(337, 81);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(327, 16);
            this.label8.TabIndex = 31;
            this.label8.Text = "________________________________________";
            // 
            // statsbtn
            // 
            this.statsbtn.Location = new System.Drawing.Point(340, 277);
            this.statsbtn.Margin = new System.Windows.Forms.Padding(4);
            this.statsbtn.Name = "statsbtn";
            this.statsbtn.Size = new System.Drawing.Size(130, 39);
            this.statsbtn.TabIndex = 32;
            this.statsbtn.Text = "STATS";
            this.statsbtn.UseVisualStyleBackColor = true;
            this.statsbtn.Click += new System.EventHandler(this.statsbtn_Click);
            // 
            // returnbtn
            // 
            this.returnbtn.Location = new System.Drawing.Point(817, 320);
            this.returnbtn.Margin = new System.Windows.Forms.Padding(4);
            this.returnbtn.Name = "returnbtn";
            this.returnbtn.Size = new System.Drawing.Size(130, 39);
            this.returnbtn.TabIndex = 33;
            this.returnbtn.Text = "RETURN";
            this.returnbtn.UseVisualStyleBackColor = true;
            this.returnbtn.Click += new System.EventHandler(this.returnbtn_Click);
            // 
            // numplayerslbl
            // 
            this.numplayerslbl.AutoSize = true;
            this.numplayerslbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numplayerslbl.Location = new System.Drawing.Point(16, 315);
            this.numplayerslbl.Name = "numplayerslbl";
            this.numplayerslbl.Size = new System.Drawing.Size(0, 20);
            this.numplayerslbl.TabIndex = 28;
            // 
            // playersonlineGrid
            // 
            this.playersonlineGrid.AllowUserToAddRows = false;
            this.playersonlineGrid.AllowUserToDeleteRows = false;
            this.playersonlineGrid.AllowUserToResizeColumns = false;
            this.playersonlineGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.playersonlineGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.playersonlineGrid.BackgroundColor = System.Drawing.SystemColors.MenuText;
            this.playersonlineGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.playersonlineGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.playersonlineGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.playersonlineGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.playersonlineGrid.DefaultCellStyle = dataGridViewCellStyle3;
            this.playersonlineGrid.GridColor = System.Drawing.SystemColors.MenuText;
            this.playersonlineGrid.Location = new System.Drawing.Point(761, 116);
            this.playersonlineGrid.Name = "playersonlineGrid";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.playersonlineGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.playersonlineGrid.RowHeadersVisible = false;
            this.playersonlineGrid.RowHeadersWidth = 51;
            this.playersonlineGrid.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.playersonlineGrid.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.MenuText;
            this.playersonlineGrid.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.playersonlineGrid.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.MenuText;
            this.playersonlineGrid.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.playersonlineGrid.RowTemplate.Height = 24;
            this.playersonlineGrid.Size = new System.Drawing.Size(160, 165);
            this.playersonlineGrid.TabIndex = 34;
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 397);
            this.Controls.Add(this.playersonlineGrid);
            this.Controls.Add(this.returnbtn);
            this.Controls.Add(this.statsbtn);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.labelTitulo);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.DEV_closebtn);
            this.Controls.Add(this.joinGame);
            this.Controls.Add(this.createGAME);
            this.Controls.Add(this.offlinelbl);
            this.Controls.Add(this.conectserver);
            this.Controls.Add(this.database);
            this.Controls.Add(this.Desconectar);
            this.Controls.Add(this.statsBox);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Client";
            this.Text = "Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Client_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statsBox.ResumeLayout(false);
            this.statsBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.playersonlineGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox name_txt;
        private System.Windows.Forms.Button Enviar_nombre;
        private System.Windows.Forms.GroupBox statsBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox date_txt;
        private System.Windows.Forms.Button Desconectar;
        private System.Windows.Forms.Button Enviar_fecha;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Enviar_server;
        private System.Windows.Forms.TextBox server_txt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button login;
        private System.Windows.Forms.Label user_lbl;
        private System.Windows.Forms.Button register;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.TextBox pswdBox;
        private System.Windows.Forms.Label namelbl;
        private System.Windows.Forms.Label pswdlbl;
        private System.Windows.Forms.Button database;
        private System.Windows.Forms.Label welcomelbl;
        private System.Windows.Forms.Button logout;
        private System.Windows.Forms.Button conectserver;
        private System.Windows.Forms.Label offlinelbl;
        private System.Windows.Forms.Button createGAME;
        private System.Windows.Forms.Button joinGame;
        private System.Windows.Forms.Button DEV_closebtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label numgameslbl;
        private System.Windows.Forms.Label labelTitulo;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button statsbtn;
        private System.Windows.Forms.Button returnbtn;
        private System.Windows.Forms.Label numplayerslbl;
        private System.Windows.Forms.DataGridView playersonlineGrid;
    }
}

