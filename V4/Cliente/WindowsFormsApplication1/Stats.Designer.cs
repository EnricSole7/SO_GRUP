namespace WindowsFormsApplication1
{
    partial class Stats
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Stats));
            this.jugadoresGrid = new System.Windows.Forms.DataGridView();
            this.serversGrid = new System.Windows.Forms.DataGridView();
            this.partidasGrid = new System.Windows.Forms.DataGridView();
            this.user_lbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.statsBox = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.date1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.gameslbl = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.playertxt = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.check1 = new System.Windows.Forms.Button();
            this.check2 = new System.Windows.Forms.Button();
            this.date2 = new System.Windows.Forms.TextBox();
            this.check3 = new System.Windows.Forms.Button();
            this.grid = new System.Windows.Forms.DataGridView();
            this.returnbtn = new System.Windows.Forms.Button();
            this.infoPictureBox_cons2 = new System.Windows.Forms.PictureBox();
            this.infoPictureBox_cons3 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.jugadoresGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.serversGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.partidasGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoPictureBox_cons2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoPictureBox_cons3)).BeginInit();
            this.SuspendLayout();
            // 
            // jugadoresGrid
            // 
            this.jugadoresGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.jugadoresGrid.Location = new System.Drawing.Point(1277, 324);
            this.jugadoresGrid.Name = "jugadoresGrid";
            this.jugadoresGrid.RowHeadersWidth = 51;
            this.jugadoresGrid.RowTemplate.Height = 24;
            this.jugadoresGrid.Size = new System.Drawing.Size(98, 55);
            this.jugadoresGrid.TabIndex = 0;
            // 
            // serversGrid
            // 
            this.serversGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.serversGrid.Location = new System.Drawing.Point(1277, 184);
            this.serversGrid.Name = "serversGrid";
            this.serversGrid.RowHeadersWidth = 51;
            this.serversGrid.RowTemplate.Height = 24;
            this.serversGrid.Size = new System.Drawing.Size(101, 70);
            this.serversGrid.TabIndex = 1;
            // 
            // partidasGrid
            // 
            this.partidasGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.partidasGrid.Location = new System.Drawing.Point(1277, 67);
            this.partidasGrid.Name = "partidasGrid";
            this.partidasGrid.RowHeadersWidth = 51;
            this.partidasGrid.RowTemplate.Height = 24;
            this.partidasGrid.Size = new System.Drawing.Size(71, 72);
            this.partidasGrid.TabIndex = 2;
            // 
            // user_lbl
            // 
            this.user_lbl.AutoSize = true;
            this.user_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.user_lbl.Location = new System.Drawing.Point(1272, 284);
            this.user_lbl.Name = "user_lbl";
            this.user_lbl.Size = new System.Drawing.Size(103, 25);
            this.user_lbl.TabIndex = 10;
            this.user_lbl.Text = "PLAYERS";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1272, 156);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 25);
            this.label1.TabIndex = 11;
            this.label1.Text = "SERVERS";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1272, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 25);
            this.label2.TabIndex = 12;
            this.label2.Text = "GAMES";
            // 
            // statsBox
            // 
            this.statsBox.BackColor = System.Drawing.SystemColors.Control;
            this.statsBox.Location = new System.Drawing.Point(1040, 108);
            this.statsBox.Margin = new System.Windows.Forms.Padding(4);
            this.statsBox.Name = "statsBox";
            this.statsBox.Padding = new System.Windows.Forms.Padding(4);
            this.statsBox.Size = new System.Drawing.Size(167, 109);
            this.statsBox.TabIndex = 13;
            this.statsBox.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(52, 257);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(314, 16);
            this.label7.TabIndex = 17;
            this.label7.Text = "Returns the games played between these two dates";
            // 
            // date1
            // 
            this.date1.Location = new System.Drawing.Point(139, 229);
            this.date1.Margin = new System.Windows.Forms.Padding(4);
            this.date1.Name = "date1";
            this.date1.Size = new System.Drawing.Size(107, 22);
            this.date1.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(52, 159);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(366, 16);
            this.label6.TabIndex = 14;
            this.label6.Text = "Returns the results of the games you played with this player/s";
            // 
            // gameslbl
            // 
            this.gameslbl.AutoSize = true;
            this.gameslbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gameslbl.Location = new System.Drawing.Point(46, 229);
            this.gameslbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.gameslbl.Name = "gameslbl";
            this.gameslbl.Size = new System.Drawing.Size(85, 25);
            this.gameslbl.TabIndex = 13;
            this.gameslbl.Text = "GAMES";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(52, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(294, 16);
            this.label4.TabIndex = 12;
            this.label4.Text = "Returns a list of the players you have played with";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(45, 131);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 25);
            this.label3.TabIndex = 11;
            this.label3.Text = "RESULTS";
            // 
            // playertxt
            // 
            this.playertxt.Location = new System.Drawing.Point(157, 134);
            this.playertxt.Margin = new System.Windows.Forms.Padding(4);
            this.playertxt.Name = "playertxt";
            this.playertxt.Size = new System.Drawing.Size(261, 22);
            this.playertxt.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(46, 39);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 25);
            this.label8.TabIndex = 1;
            this.label8.Text = "PLAYERS";
            // 
            // check1
            // 
            this.check1.BackColor = System.Drawing.Color.Bisque;
            this.check1.FlatAppearance.BorderColor = System.Drawing.Color.OrangeRed;
            this.check1.Font = new System.Drawing.Font("Barlow Condensed", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check1.ForeColor = System.Drawing.Color.OrangeRed;
            this.check1.Location = new System.Drawing.Point(504, 39);
            this.check1.Margin = new System.Windows.Forms.Padding(4);
            this.check1.Name = "check1";
            this.check1.Size = new System.Drawing.Size(115, 44);
            this.check1.TabIndex = 30;
            this.check1.Text = "CHECK";
            this.check1.UseVisualStyleBackColor = false;
            this.check1.Click += new System.EventHandler(this.check1_Click);
            // 
            // check2
            // 
            this.check2.BackColor = System.Drawing.Color.Bisque;
            this.check2.FlatAppearance.BorderColor = System.Drawing.Color.OrangeRed;
            this.check2.Font = new System.Drawing.Font("Barlow Condensed", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check2.ForeColor = System.Drawing.Color.OrangeRed;
            this.check2.Location = new System.Drawing.Point(504, 131);
            this.check2.Margin = new System.Windows.Forms.Padding(4);
            this.check2.Name = "check2";
            this.check2.Size = new System.Drawing.Size(115, 44);
            this.check2.TabIndex = 31;
            this.check2.Text = "CHECK";
            this.check2.UseVisualStyleBackColor = false;
            this.check2.Click += new System.EventHandler(this.check2_Click);
            // 
            // date2
            // 
            this.date2.Location = new System.Drawing.Point(259, 229);
            this.date2.Margin = new System.Windows.Forms.Padding(4);
            this.date2.Name = "date2";
            this.date2.Size = new System.Drawing.Size(107, 22);
            this.date2.TabIndex = 32;
            // 
            // check3
            // 
            this.check3.BackColor = System.Drawing.Color.Bisque;
            this.check3.FlatAppearance.BorderColor = System.Drawing.Color.OrangeRed;
            this.check3.Font = new System.Drawing.Font("Barlow Condensed", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check3.ForeColor = System.Drawing.Color.OrangeRed;
            this.check3.Location = new System.Drawing.Point(504, 229);
            this.check3.Margin = new System.Windows.Forms.Padding(4);
            this.check3.Name = "check3";
            this.check3.Size = new System.Drawing.Size(115, 44);
            this.check3.TabIndex = 33;
            this.check3.Text = "CHECK";
            this.check3.UseVisualStyleBackColor = false;
            this.check3.Click += new System.EventHandler(this.check3_Click);
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.AllowUserToResizeColumns = false;
            this.grid.AllowUserToResizeRows = false;
            this.grid.BackgroundColor = System.Drawing.Color.Coral;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.NavajoWhite;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.OrangeRed;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grid.DefaultCellStyle = dataGridViewCellStyle2;
            this.grid.Location = new System.Drawing.Point(652, 39);
            this.grid.Name = "grid";
            this.grid.RowHeadersWidth = 51;
            this.grid.RowTemplate.Height = 24;
            this.grid.Size = new System.Drawing.Size(201, 234);
            this.grid.TabIndex = 34;
            // 
            // returnbtn
            // 
            this.returnbtn.Location = new System.Drawing.Point(652, 308);
            this.returnbtn.Margin = new System.Windows.Forms.Padding(4);
            this.returnbtn.Name = "returnbtn";
            this.returnbtn.Size = new System.Drawing.Size(130, 39);
            this.returnbtn.TabIndex = 35;
            this.returnbtn.Text = "RETURN";
            this.returnbtn.UseVisualStyleBackColor = true;
            // 
            // infoPictureBox_cons2
            // 
            this.infoPictureBox_cons2.Image = ((System.Drawing.Image)(resources.GetObject("infoPictureBox_cons2.Image")));
            this.infoPictureBox_cons2.Location = new System.Drawing.Point(446, 136);
            this.infoPictureBox_cons2.Name = "infoPictureBox_cons2";
            this.infoPictureBox_cons2.Size = new System.Drawing.Size(40, 39);
            this.infoPictureBox_cons2.TabIndex = 36;
            this.infoPictureBox_cons2.TabStop = false;
            this.infoPictureBox_cons2.Click += new System.EventHandler(this.infoPictureBox_cons2_Click);
            // 
            // infoPictureBox_cons3
            // 
            this.infoPictureBox_cons3.Image = ((System.Drawing.Image)(resources.GetObject("infoPictureBox_cons3.Image")));
            this.infoPictureBox_cons3.Location = new System.Drawing.Point(446, 234);
            this.infoPictureBox_cons3.Name = "infoPictureBox_cons3";
            this.infoPictureBox_cons3.Size = new System.Drawing.Size(40, 39);
            this.infoPictureBox_cons3.TabIndex = 37;
            this.infoPictureBox_cons3.TabStop = false;
            this.infoPictureBox_cons3.Click += new System.EventHandler(this.infoPictureBox_cons3_Click);
            // 
            // Stats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1398, 405);
            this.Controls.Add(this.infoPictureBox_cons3);
            this.Controls.Add(this.infoPictureBox_cons2);
            this.Controls.Add(this.returnbtn);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.check3);
            this.Controls.Add(this.date2);
            this.Controls.Add(this.check2);
            this.Controls.Add(this.check1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.statsBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.date1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.user_lbl);
            this.Controls.Add(this.gameslbl);
            this.Controls.Add(this.partidasGrid);
            this.Controls.Add(this.serversGrid);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.jugadoresGrid);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.playertxt);
            this.Name = "Stats";
            this.Text = "DataBase";
            this.Load += new System.EventHandler(this.FormLogin_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.jugadoresGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.serversGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.partidasGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoPictureBox_cons2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoPictureBox_cons3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView jugadoresGrid;
        private System.Windows.Forms.DataGridView serversGrid;
        private System.Windows.Forms.DataGridView partidasGrid;
        private System.Windows.Forms.Label user_lbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox statsBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox date1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label gameslbl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox playertxt;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button check1;
        private System.Windows.Forms.Button check2;
        private System.Windows.Forms.TextBox date2;
        private System.Windows.Forms.Button check3;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.Button returnbtn;
        private System.Windows.Forms.PictureBox infoPictureBox_cons2;
        private System.Windows.Forms.PictureBox infoPictureBox_cons3;
    }
}