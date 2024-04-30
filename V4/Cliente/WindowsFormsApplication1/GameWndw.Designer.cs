namespace WindowsFormsApplication1
{
    partial class GameWndw
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
            this.exitbtn = new System.Windows.Forms.Button();
            this.accept_invitation = new System.Windows.Forms.Button();
            this.peticionesBox = new System.Windows.Forms.GroupBox();
            this.checkTBD = new System.Windows.Forms.RadioButton();
            this.checkMaze = new System.Windows.Forms.RadioButton();
            this.checkSymbols = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.maze_lbl = new System.Windows.Forms.Label();
            this.symbols_lbl = new System.Windows.Forms.Label();
            this.playersonlineGrid = new System.Windows.Forms.DataGridView();
            this.server_lbl = new System.Windows.Forms.Label();
            this.server_value = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.invitationsgrid = new System.Windows.Forms.DataGridView();
            this.peticionesBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.playersonlineGrid)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.invitationsgrid)).BeginInit();
            this.SuspendLayout();
            // 
            // exitbtn
            // 
            this.exitbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitbtn.Location = new System.Drawing.Point(28, 687);
            this.exitbtn.Margin = new System.Windows.Forms.Padding(4);
            this.exitbtn.Name = "exitbtn";
            this.exitbtn.Size = new System.Drawing.Size(201, 59);
            this.exitbtn.TabIndex = 17;
            this.exitbtn.Text = "EXIT GAME";
            this.exitbtn.UseVisualStyleBackColor = true;
            this.exitbtn.Click += new System.EventHandler(this.exitbtn_Click);
            // 
            // accept_invitation
            // 
            this.accept_invitation.BackColor = System.Drawing.SystemColors.Control;
            this.accept_invitation.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accept_invitation.ForeColor = System.Drawing.SystemColors.MenuText;
            this.accept_invitation.Location = new System.Drawing.Point(295, 597);
            this.accept_invitation.Margin = new System.Windows.Forms.Padding(4);
            this.accept_invitation.Name = "accept_invitation";
            this.accept_invitation.Size = new System.Drawing.Size(109, 29);
            this.accept_invitation.TabIndex = 18;
            this.accept_invitation.Text = "ACCEPT";
            this.accept_invitation.UseVisualStyleBackColor = false;
            // 
            // peticionesBox
            // 
            this.peticionesBox.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.peticionesBox.Controls.Add(this.checkTBD);
            this.peticionesBox.Controls.Add(this.checkMaze);
            this.peticionesBox.Controls.Add(this.checkSymbols);
            this.peticionesBox.Controls.Add(this.label5);
            this.peticionesBox.Controls.Add(this.maze_lbl);
            this.peticionesBox.Controls.Add(this.symbols_lbl);
            this.peticionesBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.peticionesBox.Location = new System.Drawing.Point(28, 52);
            this.peticionesBox.Margin = new System.Windows.Forms.Padding(4);
            this.peticionesBox.Name = "peticionesBox";
            this.peticionesBox.Padding = new System.Windows.Forms.Padding(4);
            this.peticionesBox.Size = new System.Drawing.Size(201, 209);
            this.peticionesBox.TabIndex = 19;
            this.peticionesBox.TabStop = false;
            this.peticionesBox.Text = "MINIGAMES";
            // 
            // checkTBD
            // 
            this.checkTBD.AutoSize = true;
            this.checkTBD.Location = new System.Drawing.Point(16, 160);
            this.checkTBD.Name = "checkTBD";
            this.checkTBD.Size = new System.Drawing.Size(17, 16);
            this.checkTBD.TabIndex = 16;
            this.checkTBD.TabStop = true;
            this.checkTBD.UseVisualStyleBackColor = true;
            // 
            // checkMaze
            // 
            this.checkMaze.AutoSize = true;
            this.checkMaze.Location = new System.Drawing.Point(16, 109);
            this.checkMaze.Name = "checkMaze";
            this.checkMaze.Size = new System.Drawing.Size(17, 16);
            this.checkMaze.TabIndex = 15;
            this.checkMaze.TabStop = true;
            this.checkMaze.UseVisualStyleBackColor = true;
            // 
            // checkSymbols
            // 
            this.checkSymbols.AutoSize = true;
            this.checkSymbols.Location = new System.Drawing.Point(16, 53);
            this.checkSymbols.Name = "checkSymbols";
            this.checkSymbols.Size = new System.Drawing.Size(17, 16);
            this.checkSymbols.TabIndex = 14;
            this.checkSymbols.TabStop = true;
            this.checkSymbols.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(40, 152);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 29);
            this.label5.TabIndex = 13;
            this.label5.Text = "TBD";
            // 
            // maze_lbl
            // 
            this.maze_lbl.AutoSize = true;
            this.maze_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maze_lbl.Location = new System.Drawing.Point(40, 101);
            this.maze_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.maze_lbl.Name = "maze_lbl";
            this.maze_lbl.Size = new System.Drawing.Size(83, 29);
            this.maze_lbl.TabIndex = 11;
            this.maze_lbl.Text = "MAZE";
            // 
            // symbols_lbl
            // 
            this.symbols_lbl.AutoSize = true;
            this.symbols_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.symbols_lbl.Location = new System.Drawing.Point(40, 45);
            this.symbols_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.symbols_lbl.Name = "symbols_lbl";
            this.symbols_lbl.Size = new System.Drawing.Size(134, 29);
            this.symbols_lbl.TabIndex = 1;
            this.symbols_lbl.Text = "SYMBOLS";
            // 
            // playersonlineGrid
            // 
            this.playersonlineGrid.AllowUserToAddRows = false;
            this.playersonlineGrid.AllowUserToDeleteRows = false;
            this.playersonlineGrid.AllowUserToResizeColumns = false;
            this.playersonlineGrid.AllowUserToResizeRows = false;
            this.playersonlineGrid.BackgroundColor = System.Drawing.SystemColors.Control;
            this.playersonlineGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.playersonlineGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.playersonlineGrid.Location = new System.Drawing.Point(28, 280);
            this.playersonlineGrid.Name = "playersonlineGrid";
            this.playersonlineGrid.RowHeadersWidth = 51;
            this.playersonlineGrid.RowTemplate.Height = 24;
            this.playersonlineGrid.Size = new System.Drawing.Size(201, 165);
            this.playersonlineGrid.TabIndex = 20;
            this.playersonlineGrid.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.playersonlineGrid_CellContentDoubleClick);
            // 
            // server_lbl
            // 
            this.server_lbl.AutoSize = true;
            this.server_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.server_lbl.Location = new System.Drawing.Point(24, 18);
            this.server_lbl.Name = "server_lbl";
            this.server_lbl.Size = new System.Drawing.Size(82, 20);
            this.server_lbl.TabIndex = 21;
            this.server_lbl.Text = "SERVER:";
            // 
            // server_value
            // 
            this.server_value.AutoSize = true;
            this.server_value.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.server_value.Location = new System.Drawing.Point(112, 18);
            this.server_value.Name = "server_value";
            this.server_value.Size = new System.Drawing.Size(0, 20);
            this.server_value.TabIndex = 22;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.MenuText;
            this.groupBox1.Controls.Add(this.invitationsgrid);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Location = new System.Drawing.Point(28, 465);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 161);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "INVITATIONS";
            // 
            // invitationsgrid
            // 
            this.invitationsgrid.AllowUserToAddRows = false;
            this.invitationsgrid.AllowUserToDeleteRows = false;
            this.invitationsgrid.AllowUserToResizeColumns = false;
            this.invitationsgrid.AllowUserToResizeRows = false;
            this.invitationsgrid.BackgroundColor = System.Drawing.SystemColors.MenuText;
            this.invitationsgrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.invitationsgrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.invitationsgrid.ColumnHeadersVisible = false;
            this.invitationsgrid.GridColor = System.Drawing.SystemColors.MenuText;
            this.invitationsgrid.Location = new System.Drawing.Point(6, 25);
            this.invitationsgrid.Name = "invitationsgrid";
            this.invitationsgrid.RowHeadersWidth = 51;
            this.invitationsgrid.RowTemplate.Height = 24;
            this.invitationsgrid.Size = new System.Drawing.Size(188, 130);
            this.invitationsgrid.TabIndex = 24;
            // 
            // GameWndw
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1500, 778);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.accept_invitation);
            this.Controls.Add(this.server_value);
            this.Controls.Add(this.server_lbl);
            this.Controls.Add(this.playersonlineGrid);
            this.Controls.Add(this.peticionesBox);
            this.Controls.Add(this.exitbtn);
            this.Name = "GameWndw";
            this.Text = "GameWndw";
            this.Load += new System.EventHandler(this.GameWndw_Load);
            this.peticionesBox.ResumeLayout(false);
            this.peticionesBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.playersonlineGrid)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.invitationsgrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button exitbtn;
        private System.Windows.Forms.Button accept_invitation;
        private System.Windows.Forms.GroupBox peticionesBox;
        private System.Windows.Forms.RadioButton checkTBD;
        private System.Windows.Forms.RadioButton checkMaze;
        private System.Windows.Forms.RadioButton checkSymbols;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label maze_lbl;
        private System.Windows.Forms.Label symbols_lbl;
        private System.Windows.Forms.DataGridView playersonlineGrid;
        private System.Windows.Forms.Label server_lbl;
        private System.Windows.Forms.Label server_value;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView invitationsgrid;
    }
}