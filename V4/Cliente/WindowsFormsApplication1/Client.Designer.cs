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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Client));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.unregisterbtn = new System.Windows.Forms.Button();
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
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.playersonlineGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.invitationsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // Desconectar
            // 
            this.Desconectar.BackColor = System.Drawing.Color.Bisque;
            this.Desconectar.Font = new System.Drawing.Font("Barlow Condensed", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Desconectar.ForeColor = System.Drawing.Color.OrangeRed;
            this.Desconectar.Location = new System.Drawing.Point(340, 313);
            this.Desconectar.Margin = new System.Windows.Forms.Padding(4);
            this.Desconectar.Name = "Desconectar";
            this.Desconectar.Size = new System.Drawing.Size(197, 56);
            this.Desconectar.TabIndex = 7;
            this.Desconectar.Text = "CLOSE";
            this.Desconectar.UseVisualStyleBackColor = false;
            this.Desconectar.Click += new System.EventHandler(this.Desconectar_Click);
            // 
            // login
            // 
            this.login.BackColor = System.Drawing.Color.Bisque;
            this.login.Font = new System.Drawing.Font("Barlow Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.login.ForeColor = System.Drawing.Color.OrangeRed;
            this.login.Location = new System.Drawing.Point(20, 186);
            this.login.Margin = new System.Windows.Forms.Padding(4);
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(112, 43);
            this.login.TabIndex = 8;
            this.login.Text = "LOGIN";
            this.login.UseVisualStyleBackColor = false;
            this.login.Click += new System.EventHandler(this.login_Click);
            // 
            // user_lbl
            // 
            this.user_lbl.AutoSize = true;
            this.user_lbl.BackColor = System.Drawing.Color.Transparent;
            this.user_lbl.Font = new System.Drawing.Font("Barlow Condensed", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.user_lbl.ForeColor = System.Drawing.Color.OrangeRed;
            this.user_lbl.Location = new System.Drawing.Point(6, 15);
            this.user_lbl.Name = "user_lbl";
            this.user_lbl.Size = new System.Drawing.Size(80, 46);
            this.user_lbl.TabIndex = 9;
            this.user_lbl.Text = "USER";
            // 
            // register
            // 
            this.register.BackColor = System.Drawing.Color.Bisque;
            this.register.Font = new System.Drawing.Font("Barlow Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.register.ForeColor = System.Drawing.Color.OrangeRed;
            this.register.Location = new System.Drawing.Point(150, 186);
            this.register.Margin = new System.Windows.Forms.Padding(4);
            this.register.Name = "register";
            this.register.Size = new System.Drawing.Size(112, 43);
            this.register.TabIndex = 10;
            this.register.Text = "REGISTER";
            this.register.UseVisualStyleBackColor = false;
            this.register.Click += new System.EventHandler(this.register_Click);
            // 
            // nameBox
            // 
            this.nameBox.BackColor = System.Drawing.Color.SeaShell;
            this.nameBox.Font = new System.Drawing.Font("Barlow Condensed", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameBox.Location = new System.Drawing.Point(123, 72);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(137, 27);
            this.nameBox.TabIndex = 11;
            // 
            // pswdBox
            // 
            this.pswdBox.BackColor = System.Drawing.Color.SeaShell;
            this.pswdBox.Font = new System.Drawing.Font("Barlow Condensed", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pswdBox.Location = new System.Drawing.Point(123, 121);
            this.pswdBox.Name = "pswdBox";
            this.pswdBox.Size = new System.Drawing.Size(137, 27);
            this.pswdBox.TabIndex = 12;
            // 
            // namelbl
            // 
            this.namelbl.AutoSize = true;
            this.namelbl.BackColor = System.Drawing.Color.Transparent;
            this.namelbl.Font = new System.Drawing.Font("Barlow Condensed", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.namelbl.Location = new System.Drawing.Point(6, 68);
            this.namelbl.Name = "namelbl";
            this.namelbl.Size = new System.Drawing.Size(70, 33);
            this.namelbl.TabIndex = 13;
            this.namelbl.Text = "NAME :";
            // 
            // pswdlbl
            // 
            this.pswdlbl.AutoSize = true;
            this.pswdlbl.BackColor = System.Drawing.Color.Transparent;
            this.pswdlbl.Font = new System.Drawing.Font("Barlow Condensed", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pswdlbl.Location = new System.Drawing.Point(5, 117);
            this.pswdlbl.Name = "pswdlbl";
            this.pswdlbl.Size = new System.Drawing.Size(113, 33);
            this.pswdlbl.TabIndex = 14;
            this.pswdlbl.Text = "PASSWORD :";
            // 
            // welcomelbl
            // 
            this.welcomelbl.AutoSize = true;
            this.welcomelbl.BackColor = System.Drawing.Color.Transparent;
            this.welcomelbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.welcomelbl.Location = new System.Drawing.Point(24, 101);
            this.welcomelbl.Name = "welcomelbl";
            this.welcomelbl.Size = new System.Drawing.Size(0, 29);
            this.welcomelbl.TabIndex = 17;
            // 
            // logout
            // 
            this.logout.BackColor = System.Drawing.Color.Bisque;
            this.logout.Font = new System.Drawing.Font("Barlow Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logout.ForeColor = System.Drawing.Color.OrangeRed;
            this.logout.Location = new System.Drawing.Point(74, 186);
            this.logout.Margin = new System.Windows.Forms.Padding(4);
            this.logout.Name = "logout";
            this.logout.Size = new System.Drawing.Size(130, 43);
            this.logout.TabIndex = 18;
            this.logout.Text = "LOGOUT";
            this.logout.UseVisualStyleBackColor = false;
            this.logout.Click += new System.EventHandler(this.logout_Click);
            // 
            // conectserver
            // 
            this.conectserver.BackColor = System.Drawing.Color.Bisque;
            this.conectserver.Font = new System.Drawing.Font("Barlow Condensed", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.conectserver.ForeColor = System.Drawing.Color.OrangeRed;
            this.conectserver.Location = new System.Drawing.Point(746, 8);
            this.conectserver.Margin = new System.Windows.Forms.Padding(4);
            this.conectserver.Name = "conectserver";
            this.conectserver.Size = new System.Drawing.Size(201, 100);
            this.conectserver.TabIndex = 19;
            this.conectserver.Text = "CONNECT TO SERVER";
            this.conectserver.UseVisualStyleBackColor = false;
            this.conectserver.Click += new System.EventHandler(this.conectserver_Click);
            // 
            // offlinelbl
            // 
            this.offlinelbl.AutoSize = true;
            this.offlinelbl.Font = new System.Drawing.Font("Barlow Condensed", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.offlinelbl.ForeColor = System.Drawing.Color.Red;
            this.offlinelbl.Location = new System.Drawing.Point(792, 112);
            this.offlinelbl.Name = "offlinelbl";
            this.offlinelbl.Size = new System.Drawing.Size(101, 40);
            this.offlinelbl.TabIndex = 20;
            this.offlinelbl.Text = "OFFLINE";
            // 
            // createGAME
            // 
            this.createGAME.BackColor = System.Drawing.Color.Bisque;
            this.createGAME.Font = new System.Drawing.Font("Barlow Condensed", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createGAME.ForeColor = System.Drawing.Color.OrangeRed;
            this.createGAME.Location = new System.Drawing.Point(340, 121);
            this.createGAME.Margin = new System.Windows.Forms.Padding(4);
            this.createGAME.Name = "createGAME";
            this.createGAME.Size = new System.Drawing.Size(197, 56);
            this.createGAME.TabIndex = 21;
            this.createGAME.Text = "NEW GAME";
            this.createGAME.UseVisualStyleBackColor = false;
            this.createGAME.Click += new System.EventHandler(this.createGAME_Click);
            // 
            // joinGame
            // 
            this.joinGame.BackColor = System.Drawing.Color.Bisque;
            this.joinGame.Font = new System.Drawing.Font("Barlow Condensed", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.joinGame.ForeColor = System.Drawing.Color.OrangeRed;
            this.joinGame.Location = new System.Drawing.Point(340, 185);
            this.joinGame.Margin = new System.Windows.Forms.Padding(4);
            this.joinGame.Name = "joinGame";
            this.joinGame.Size = new System.Drawing.Size(197, 56);
            this.joinGame.TabIndex = 22;
            this.joinGame.Text = "JOIN GAME";
            this.joinGame.UseVisualStyleBackColor = false;
            this.joinGame.Click += new System.EventHandler(this.joinGame_Click);
            // 
            // DEV_closebtn
            // 
            this.DEV_closebtn.BackColor = System.Drawing.Color.Bisque;
            this.DEV_closebtn.Font = new System.Drawing.Font("Barlow Condensed", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DEV_closebtn.ForeColor = System.Drawing.Color.OrangeRed;
            this.DEV_closebtn.Location = new System.Drawing.Point(545, 313);
            this.DEV_closebtn.Margin = new System.Windows.Forms.Padding(4);
            this.DEV_closebtn.Name = "DEV_closebtn";
            this.DEV_closebtn.Size = new System.Drawing.Size(162, 50);
            this.DEV_closebtn.TabIndex = 23;
            this.DEV_closebtn.Text = "CLOSE SERVER";
            this.DEV_closebtn.UseVisualStyleBackColor = false;
            this.DEV_closebtn.Click += new System.EventHandler(this.DEV_closebtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.SandyBrown;
            this.groupBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("groupBox1.BackgroundImage")));
            this.groupBox1.Controls.Add(this.label2);
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
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(284, 369);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            // 
            // unregisterbtn
            // 
            this.unregisterbtn.BackColor = System.Drawing.Color.Bisque;
            this.unregisterbtn.Font = new System.Drawing.Font("Barlow Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.unregisterbtn.ForeColor = System.Drawing.Color.OrangeRed;
            this.unregisterbtn.Location = new System.Drawing.Point(74, 300);
            this.unregisterbtn.Margin = new System.Windows.Forms.Padding(4);
            this.unregisterbtn.Name = "unregisterbtn";
            this.unregisterbtn.Size = new System.Drawing.Size(130, 42);
            this.unregisterbtn.TabIndex = 29;
            this.unregisterbtn.Text = "UNREGISTER";
            this.unregisterbtn.UseVisualStyleBackColor = false;
            this.unregisterbtn.Click += new System.EventHandler(this.unregisterbtn_Click);
            // 
            // numplayerslbl
            // 
            this.numplayerslbl.AutoSize = true;
            this.numplayerslbl.BackColor = System.Drawing.Color.Transparent;
            this.numplayerslbl.Font = new System.Drawing.Font("Barlow Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numplayerslbl.Location = new System.Drawing.Point(24, 265);
            this.numplayerslbl.Name = "numplayerslbl";
            this.numplayerslbl.Size = new System.Drawing.Size(0, 27);
            this.numplayerslbl.TabIndex = 28;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.OrangeRed;
            this.label1.Location = new System.Drawing.Point(86, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 16);
            this.label1.TabIndex = 26;
            this.label1.Text = "___________________";
            // 
            // labelTitulo
            // 
            this.labelTitulo.AutoSize = true;
            this.labelTitulo.BackColor = System.Drawing.Color.Transparent;
            this.labelTitulo.Font = new System.Drawing.Font("Harlow Solid Italic", 50F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitulo.ForeColor = System.Drawing.Color.Maroon;
            this.labelTitulo.Location = new System.Drawing.Point(317, 6);
            this.labelTitulo.Name = "labelTitulo";
            this.labelTitulo.Size = new System.Drawing.Size(320, 106);
            this.labelTitulo.TabIndex = 25;
            this.labelTitulo.Tag = "04";
            this.labelTitulo.Text = "Symbols";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(438, 94);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(167, 16);
            this.label11.TabIndex = 30;
            this.label11.Text = "____________________";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(438, 83);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(247, 16);
            this.label8.TabIndex = 31;
            this.label8.Text = "______________________________";
            // 
            // statsbtn
            // 
            this.statsbtn.BackColor = System.Drawing.Color.Bisque;
            this.statsbtn.Font = new System.Drawing.Font("Barlow Condensed", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statsbtn.ForeColor = System.Drawing.Color.OrangeRed;
            this.statsbtn.Location = new System.Drawing.Point(340, 249);
            this.statsbtn.Margin = new System.Windows.Forms.Padding(4);
            this.statsbtn.Name = "statsbtn";
            this.statsbtn.Size = new System.Drawing.Size(197, 56);
            this.statsbtn.TabIndex = 32;
            this.statsbtn.Text = "STATS";
            this.statsbtn.UseVisualStyleBackColor = false;
            this.statsbtn.Click += new System.EventHandler(this.statsbtn_Click);
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
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.playersonlineGrid.DefaultCellStyle = dataGridViewCellStyle3;
            this.playersonlineGrid.GridColor = System.Drawing.SystemColors.MenuText;
            this.playersonlineGrid.Location = new System.Drawing.Point(761, 64);
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
            this.playersonlineGrid.Size = new System.Drawing.Size(160, 122);
            this.playersonlineGrid.TabIndex = 34;
            // 
            // invitationsGrid
            // 
            this.invitationsGrid.AllowUserToAddRows = false;
            this.invitationsGrid.AllowUserToDeleteRows = false;
            this.invitationsGrid.AllowUserToResizeColumns = false;
            this.invitationsGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            this.invitationsGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.invitationsGrid.BackgroundColor = System.Drawing.SystemColors.MenuText;
            this.invitationsGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.invitationsGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.invitationsGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.invitationsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.invitationsGrid.DefaultCellStyle = dataGridViewCellStyle7;
            this.invitationsGrid.GridColor = System.Drawing.SystemColors.MenuText;
            this.invitationsGrid.Location = new System.Drawing.Point(761, 235);
            this.invitationsGrid.Name = "invitationsGrid";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.invitationsGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
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
            this.invitationslbl.Font = new System.Drawing.Font("Barlow Condensed", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.invitationslbl.Location = new System.Drawing.Point(779, 196);
            this.invitationslbl.Name = "invitationslbl";
            this.invitationslbl.Size = new System.Drawing.Size(114, 33);
            this.invitationslbl.TabIndex = 36;
            this.invitationslbl.Text = "INVITATIONS";
            // 
            // playersonlinelbl
            // 
            this.playersonlinelbl.AutoSize = true;
            this.playersonlinelbl.Font = new System.Drawing.Font("Barlow Condensed", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playersonlinelbl.Location = new System.Drawing.Point(793, 27);
            this.playersonlinelbl.Name = "playersonlinelbl";
            this.playersonlinelbl.Size = new System.Drawing.Size(75, 33);
            this.playersonlinelbl.TabIndex = 37;
            this.playersonlinelbl.Text = "ONLINE";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.OrangeRed;
            this.label2.Location = new System.Drawing.Point(86, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 16);
            this.label2.TabIndex = 30;
            this.label2.Text = "___________________";
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.ClientSize = new System.Drawing.Size(960, 409);
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
        private System.Windows.Forms.Label label2;
    }
}

