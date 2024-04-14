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
            this.label2 = new System.Windows.Forms.Label();
            this.nombre = new System.Windows.Forms.TextBox();
            this.Enviar_nombre = new System.Windows.Forms.Button();
            this.peticionesBox = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Enviar_server = new System.Windows.Forms.Button();
            this.server_box = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Enviar_fecha = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.fecha = new System.Windows.Forms.TextBox();
            this.Desconectar = new System.Windows.Forms.Button();
            this.login = new System.Windows.Forms.Button();
            this.user_lbl = new System.Windows.Forms.Label();
            this.register = new System.Windows.Forms.Button();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.pswdBox = new System.Windows.Forms.TextBox();
            this.namelbl = new System.Windows.Forms.Label();
            this.pswdlbl = new System.Windows.Forms.Label();
            this.database = new System.Windows.Forms.Button();
            this.clientList = new System.Windows.Forms.Button();
            this.welcomelbl = new System.Windows.Forms.Label();
            this.logout = new System.Windows.Forms.Button();
            this.conectserver = new System.Windows.Forms.Button();
            this.offlinelbl = new System.Windows.Forms.Label();
            this.createGAME = new System.Windows.Forms.Button();
            this.joinGame = new System.Windows.Forms.Button();
            this.peticionesBox.SuspendLayout();
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
            // nombre
            // 
            this.nombre.Location = new System.Drawing.Point(100, 37);
            this.nombre.Margin = new System.Windows.Forms.Padding(4);
            this.nombre.Name = "nombre";
            this.nombre.Size = new System.Drawing.Size(219, 22);
            this.nombre.TabIndex = 3;
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
            // peticionesBox
            // 
            this.peticionesBox.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.peticionesBox.Controls.Add(this.label7);
            this.peticionesBox.Controls.Add(this.Enviar_server);
            this.peticionesBox.Controls.Add(this.server_box);
            this.peticionesBox.Controls.Add(this.label6);
            this.peticionesBox.Controls.Add(this.label5);
            this.peticionesBox.Controls.Add(this.Enviar_fecha);
            this.peticionesBox.Controls.Add(this.label4);
            this.peticionesBox.Controls.Add(this.label3);
            this.peticionesBox.Controls.Add(this.fecha);
            this.peticionesBox.Controls.Add(this.label2);
            this.peticionesBox.Controls.Add(this.Enviar_nombre);
            this.peticionesBox.Controls.Add(this.nombre);
            this.peticionesBox.Location = new System.Drawing.Point(340, 75);
            this.peticionesBox.Margin = new System.Windows.Forms.Padding(4);
            this.peticionesBox.Name = "peticionesBox";
            this.peticionesBox.Padding = new System.Windows.Forms.Padding(4);
            this.peticionesBox.Size = new System.Drawing.Size(459, 268);
            this.peticionesBox.TabIndex = 6;
            this.peticionesBox.TabStop = false;
            this.peticionesBox.Text = "REQUESTS";
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
            // server_box
            // 
            this.server_box.Location = new System.Drawing.Point(122, 189);
            this.server_box.Margin = new System.Windows.Forms.Padding(4);
            this.server_box.Name = "server_box";
            this.server_box.Size = new System.Drawing.Size(197, 22);
            this.server_box.TabIndex = 15;
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
            // fecha
            // 
            this.fecha.Location = new System.Drawing.Point(100, 114);
            this.fecha.Margin = new System.Windows.Forms.Padding(4);
            this.fecha.Name = "fecha";
            this.fecha.Size = new System.Drawing.Size(219, 22);
            this.fecha.TabIndex = 10;
            // 
            // Desconectar
            // 
            this.Desconectar.Location = new System.Drawing.Point(687, 13);
            this.Desconectar.Margin = new System.Windows.Forms.Padding(4);
            this.Desconectar.Name = "Desconectar";
            this.Desconectar.Size = new System.Drawing.Size(112, 38);
            this.Desconectar.TabIndex = 7;
            this.Desconectar.Text = "CLOSE";
            this.Desconectar.UseVisualStyleBackColor = true;
            this.Desconectar.Click += new System.EventHandler(this.Desconectar_Click);
            // 
            // login
            // 
            this.login.Location = new System.Drawing.Point(29, 153);
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
            this.user_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.user_lbl.Location = new System.Drawing.Point(24, 23);
            this.user_lbl.Name = "user_lbl";
            this.user_lbl.Size = new System.Drawing.Size(66, 25);
            this.user_lbl.TabIndex = 9;
            this.user_lbl.Text = "USER";
            // 
            // register
            // 
            this.register.Location = new System.Drawing.Point(149, 153);
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
            this.nameBox.Location = new System.Drawing.Point(129, 52);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(137, 22);
            this.nameBox.TabIndex = 11;
            // 
            // pswdBox
            // 
            this.pswdBox.Location = new System.Drawing.Point(129, 108);
            this.pswdBox.Name = "pswdBox";
            this.pswdBox.Size = new System.Drawing.Size(137, 22);
            this.pswdBox.TabIndex = 12;
            // 
            // namelbl
            // 
            this.namelbl.AutoSize = true;
            this.namelbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.namelbl.Location = new System.Drawing.Point(25, 54);
            this.namelbl.Name = "namelbl";
            this.namelbl.Size = new System.Drawing.Size(58, 20);
            this.namelbl.TabIndex = 13;
            this.namelbl.Text = "Name:";
            // 
            // pswdlbl
            // 
            this.pswdlbl.AutoSize = true;
            this.pswdlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pswdlbl.Location = new System.Drawing.Point(25, 110);
            this.pswdlbl.Name = "pswdlbl";
            this.pswdlbl.Size = new System.Drawing.Size(88, 20);
            this.pswdlbl.TabIndex = 14;
            this.pswdlbl.Text = "Password:";
            // 
            // database
            // 
            this.database.Location = new System.Drawing.Point(11, 215);
            this.database.Margin = new System.Windows.Forms.Padding(4);
            this.database.Name = "database";
            this.database.Size = new System.Drawing.Size(130, 52);
            this.database.TabIndex = 15;
            this.database.Text = "SHOW DATABASE";
            this.database.UseVisualStyleBackColor = true;
            this.database.Click += new System.EventHandler(this.database_Click);
            // 
            // clientList
            // 
            this.clientList.Location = new System.Drawing.Point(149, 215);
            this.clientList.Margin = new System.Windows.Forms.Padding(4);
            this.clientList.Name = "clientList";
            this.clientList.Size = new System.Drawing.Size(130, 52);
            this.clientList.TabIndex = 16;
            this.clientList.Text = "SHOW USERS ONLINE";
            this.clientList.UseVisualStyleBackColor = true;
            this.clientList.Click += new System.EventHandler(this.clientList_Click);
            // 
            // welcomelbl
            // 
            this.welcomelbl.AutoSize = true;
            this.welcomelbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.welcomelbl.Location = new System.Drawing.Point(38, 161);
            this.welcomelbl.Name = "welcomelbl";
            this.welcomelbl.Size = new System.Drawing.Size(0, 29);
            this.welcomelbl.TabIndex = 17;
            // 
            // logout
            // 
            this.logout.Location = new System.Drawing.Point(81, 81);
            this.logout.Margin = new System.Windows.Forms.Padding(4);
            this.logout.Name = "logout";
            this.logout.Size = new System.Drawing.Size(130, 38);
            this.logout.TabIndex = 18;
            this.logout.Text = "LOGOUT";
            this.logout.UseVisualStyleBackColor = true;
            this.logout.Click += new System.EventHandler(this.logout_Click);
            // 
            // conectserver
            // 
            this.conectserver.Location = new System.Drawing.Point(480, 13);
            this.conectserver.Margin = new System.Windows.Forms.Padding(4);
            this.conectserver.Name = "conectserver";
            this.conectserver.Size = new System.Drawing.Size(199, 38);
            this.conectserver.TabIndex = 19;
            this.conectserver.Text = "CONNECT TO SERVER";
            this.conectserver.UseVisualStyleBackColor = true;
            this.conectserver.Click += new System.EventHandler(this.conectserver_Click);
            // 
            // offlinelbl
            // 
            this.offlinelbl.AutoSize = true;
            this.offlinelbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.offlinelbl.Location = new System.Drawing.Point(343, 17);
            this.offlinelbl.Name = "offlinelbl";
            this.offlinelbl.Size = new System.Drawing.Size(130, 31);
            this.offlinelbl.TabIndex = 20;
            this.offlinelbl.Text = "OFFLINE";
            // 
            // createGAME
            // 
            this.createGAME.Location = new System.Drawing.Point(11, 291);
            this.createGAME.Margin = new System.Windows.Forms.Padding(4);
            this.createGAME.Name = "createGAME";
            this.createGAME.Size = new System.Drawing.Size(130, 52);
            this.createGAME.TabIndex = 21;
            this.createGAME.Text = "CREATE GAME";
            this.createGAME.UseVisualStyleBackColor = true;
            this.createGAME.Click += new System.EventHandler(this.createGAME_Click);
            // 
            // joinGame
            // 
            this.joinGame.Location = new System.Drawing.Point(149, 291);
            this.joinGame.Margin = new System.Windows.Forms.Padding(4);
            this.joinGame.Name = "joinGame";
            this.joinGame.Size = new System.Drawing.Size(130, 52);
            this.joinGame.TabIndex = 22;
            this.joinGame.Text = "JOIN GAME";
            this.joinGame.UseVisualStyleBackColor = true;
            this.joinGame.Click += new System.EventHandler(this.joinGame_Click);
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 374);
            this.Controls.Add(this.joinGame);
            this.Controls.Add(this.createGAME);
            this.Controls.Add(this.offlinelbl);
            this.Controls.Add(this.conectserver);
            this.Controls.Add(this.logout);
            this.Controls.Add(this.welcomelbl);
            this.Controls.Add(this.clientList);
            this.Controls.Add(this.database);
            this.Controls.Add(this.pswdlbl);
            this.Controls.Add(this.namelbl);
            this.Controls.Add(this.pswdBox);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.register);
            this.Controls.Add(this.user_lbl);
            this.Controls.Add(this.login);
            this.Controls.Add(this.Desconectar);
            this.Controls.Add(this.peticionesBox);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Client";
            this.Text = "Client";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.peticionesBox.ResumeLayout(false);
            this.peticionesBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox nombre;
        private System.Windows.Forms.Button Enviar_nombre;
        private System.Windows.Forms.GroupBox peticionesBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox fecha;
        private System.Windows.Forms.Button Desconectar;
        private System.Windows.Forms.Button Enviar_fecha;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Enviar_server;
        private System.Windows.Forms.TextBox server_box;
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
        private System.Windows.Forms.Button clientList;
        private System.Windows.Forms.Label welcomelbl;
        private System.Windows.Forms.Button logout;
        private System.Windows.Forms.Button conectserver;
        private System.Windows.Forms.Label offlinelbl;
        private System.Windows.Forms.Button createGAME;
        private System.Windows.Forms.Button joinGame;
    }
}

