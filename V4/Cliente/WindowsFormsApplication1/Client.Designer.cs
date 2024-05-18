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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Desconectar = new System.Windows.Forms.Button();
            this.login = new System.Windows.Forms.Button();
            this.user_lbl = new System.Windows.Forms.Label();
            this.register = new System.Windows.Forms.Button();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.pswdBox = new System.Windows.Forms.TextBox();
            this.namelbl = new System.Windows.Forms.Label();
            this.pswdlbl = new System.Windows.Forms.Label();
            this.welcomelbl = new System.Windows.Forms.Label();
            this.logout = new System.Windows.Forms.Button();
            this.conectserver = new System.Windows.Forms.Button();
            this.offlinelbl = new System.Windows.Forms.Label();
            this.createGAME = new System.Windows.Forms.Button();
            this.joinGame = new System.Windows.Forms.Button();
            this.DEV_closebtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numplayerslbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelTitulo = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.statsbtn = new System.Windows.Forms.Button();
            this.playersonlineGrid = new System.Windows.Forms.DataGridView();
            this.invitationsGrid = new System.Windows.Forms.DataGridView();
            this.invitationslbl = new System.Windows.Forms.Label();
            this.playersonlinelbl = new System.Windows.Forms.Label();
            this.unregisterbtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.playersonlineGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.invitationsGrid)).BeginInit();
            this.SuspendLayout();
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
            this.createGAME.Location = new System.Drawing.Point(340, 121);
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
            this.joinGame.Location = new System.Drawing.Point(340, 190);
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
            this.DEV_closebtn.Location = new System.Drawing.Point(761, 354);
            this.DEV_closebtn.Margin = new System.Windows.Forms.Padding(4);
            this.DEV_closebtn.Name = "DEV_closebtn";
            this.DEV_closebtn.Size = new System.Drawing.Size(160, 38);
            this.DEV_closebtn.TabIndex = 23;
            this.DEV_closebtn.Text = "CLOSE SERVER";
            this.DEV_closebtn.UseVisualStyleBackColor = true;
            this.DEV_closebtn.Click += new System.EventHandler(this.DEV_closebtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.unregisterbtn);
            this.groupBox1.Controls.Add(this.numplayerslbl);
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
            // numplayerslbl
            // 
            this.numplayerslbl.AutoSize = true;
            this.numplayerslbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numplayerslbl.Location = new System.Drawing.Point(16, 265);
            this.numplayerslbl.Name = "numplayerslbl";
            this.numplayerslbl.Size = new System.Drawing.Size(0, 20);
            this.numplayerslbl.TabIndex = 28;
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
            // labelTitulo
            // 
            this.labelTitulo.AutoSize = true;
            this.labelTitulo.BackColor = System.Drawing.Color.Transparent;
            this.labelTitulo.Font = new System.Drawing.Font("Brandish", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitulo.Location = new System.Drawing.Point(326, 12);
            this.labelTitulo.Name = "labelTitulo";
            this.labelTitulo.Size = new System.Drawing.Size(323, 81);
            this.labelTitulo.TabIndex = 25;
            this.labelTitulo.Tag = "04";
            this.labelTitulo.Text = "SYMBOLS";
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
            this.statsbtn.Location = new System.Drawing.Point(340, 262);
            this.statsbtn.Margin = new System.Windows.Forms.Padding(4);
            this.statsbtn.Name = "statsbtn";
            this.statsbtn.Size = new System.Drawing.Size(130, 39);
            this.statsbtn.TabIndex = 32;
            this.statsbtn.Text = "STATS";
            this.statsbtn.UseVisualStyleBackColor = true;
            this.statsbtn.Click += new System.EventHandler(this.statsbtn_Click);
            // 
            // playersonlineGrid
            // 
            this.playersonlineGrid.AllowUserToAddRows = false;
            this.playersonlineGrid.AllowUserToDeleteRows = false;
            this.playersonlineGrid.AllowUserToResizeColumns = false;
            this.playersonlineGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle25.BackColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle25.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle25.SelectionBackColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle25.SelectionForeColor = System.Drawing.Color.White;
            this.playersonlineGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle25;
            this.playersonlineGrid.BackgroundColor = System.Drawing.SystemColors.MenuText;
            this.playersonlineGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.playersonlineGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle26.BackColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle26.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle26.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle26.SelectionBackColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle26.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle26.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.playersonlineGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle26;
            this.playersonlineGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle27.BackColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle27.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle27.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle27.SelectionBackColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle27.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle27.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.playersonlineGrid.DefaultCellStyle = dataGridViewCellStyle27;
            this.playersonlineGrid.GridColor = System.Drawing.SystemColors.MenuText;
            this.playersonlineGrid.Location = new System.Drawing.Point(761, 55);
            this.playersonlineGrid.Name = "playersonlineGrid";
            dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle28.BackColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle28.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle28.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle28.SelectionBackColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle28.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle28.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.playersonlineGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle28;
            this.playersonlineGrid.RowHeadersVisible = false;
            this.playersonlineGrid.RowHeadersWidth = 51;
            this.playersonlineGrid.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.playersonlineGrid.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.MenuText;
            this.playersonlineGrid.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.playersonlineGrid.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.MenuText;
            this.playersonlineGrid.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.playersonlineGrid.RowTemplate.Height = 24;
            this.playersonlineGrid.Size = new System.Drawing.Size(160, 122);
            this.playersonlineGrid.TabIndex = 34;
            // 
            // invitationsGrid
            // 
            this.invitationsGrid.AllowUserToAddRows = false;
            this.invitationsGrid.AllowUserToDeleteRows = false;
            this.invitationsGrid.AllowUserToResizeColumns = false;
            this.invitationsGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle29.BackColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle29.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle29.SelectionBackColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle29.SelectionForeColor = System.Drawing.Color.White;
            this.invitationsGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle29;
            this.invitationsGrid.BackgroundColor = System.Drawing.SystemColors.MenuText;
            this.invitationsGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.invitationsGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle30.BackColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle30.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle30.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle30.SelectionBackColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle30.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle30.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.invitationsGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle30;
            this.invitationsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle31.BackColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle31.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle31.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle31.SelectionBackColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle31.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle31.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.invitationsGrid.DefaultCellStyle = dataGridViewCellStyle31;
            this.invitationsGrid.GridColor = System.Drawing.SystemColors.MenuText;
            this.invitationsGrid.Location = new System.Drawing.Point(761, 216);
            this.invitationsGrid.Name = "invitationsGrid";
            dataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle32.BackColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle32.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle32.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle32.SelectionBackColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle32.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle32.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.invitationsGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle32;
            this.invitationsGrid.RowHeadersVisible = false;
            this.invitationsGrid.RowHeadersWidth = 51;
            this.invitationsGrid.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.invitationsGrid.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.MenuText;
            this.invitationsGrid.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.invitationsGrid.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.MenuText;
            this.invitationsGrid.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.invitationsGrid.RowTemplate.Height = 24;
            this.invitationsGrid.Size = new System.Drawing.Size(160, 123);
            this.invitationsGrid.TabIndex = 35;
            this.invitationsGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.invitationsGrid_CellContentClick);
            // 
            // invitationslbl
            // 
            this.invitationslbl.AutoSize = true;
            this.invitationslbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.invitationslbl.Location = new System.Drawing.Point(772, 184);
            this.invitationslbl.Name = "invitationslbl";
            this.invitationslbl.Size = new System.Drawing.Size(139, 25);
            this.invitationslbl.TabIndex = 36;
            this.invitationslbl.Text = "INVITATIONS";
            // 
            // playersonlinelbl
            // 
            this.playersonlinelbl.AutoSize = true;
            this.playersonlinelbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playersonlinelbl.Location = new System.Drawing.Point(796, 26);
            this.playersonlinelbl.Name = "playersonlinelbl";
            this.playersonlinelbl.Size = new System.Drawing.Size(85, 25);
            this.playersonlinelbl.TabIndex = 37;
            this.playersonlinelbl.Text = "ONLINE";
            // 
            // unregisterbtn
            // 
            this.unregisterbtn.Location = new System.Drawing.Point(74, 308);
            this.unregisterbtn.Margin = new System.Windows.Forms.Padding(4);
            this.unregisterbtn.Name = "unregisterbtn";
            this.unregisterbtn.Size = new System.Drawing.Size(130, 37);
            this.unregisterbtn.TabIndex = 29;
            this.unregisterbtn.Text = "UNREGISTER";
            this.unregisterbtn.UseVisualStyleBackColor = true;
            this.unregisterbtn.Click += new System.EventHandler(this.unregisterbtn_Click);
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 624);
            this.Controls.Add(this.playersonlinelbl);
            this.Controls.Add(this.invitationslbl);
            this.Controls.Add(this.invitationsGrid);
            this.Controls.Add(this.playersonlineGrid);
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
            this.Controls.Add(this.Desconectar);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Client";
            this.Text = "Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Client_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.playersonlineGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.invitationsGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Desconectar;
        private System.Windows.Forms.Button login;
        private System.Windows.Forms.Label user_lbl;
        private System.Windows.Forms.Button register;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.TextBox pswdBox;
        private System.Windows.Forms.Label namelbl;
        private System.Windows.Forms.Label pswdlbl;
        private System.Windows.Forms.Label welcomelbl;
        private System.Windows.Forms.Button logout;
        private System.Windows.Forms.Button conectserver;
        private System.Windows.Forms.Label offlinelbl;
        private System.Windows.Forms.Button createGAME;
        private System.Windows.Forms.Button joinGame;
        private System.Windows.Forms.Button DEV_closebtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelTitulo;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button statsbtn;
        private System.Windows.Forms.Label numplayerslbl;
        private System.Windows.Forms.DataGridView playersonlineGrid;
        private System.Windows.Forms.DataGridView invitationsGrid;
        private System.Windows.Forms.Label invitationslbl;
        private System.Windows.Forms.Label playersonlinelbl;
        private System.Windows.Forms.Button unregisterbtn;
    }
}

