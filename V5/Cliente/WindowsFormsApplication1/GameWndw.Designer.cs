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
            this.players_lbl = new System.Windows.Forms.Label();
            this.playersBox = new System.Windows.Forms.GroupBox();
            this.player5_lbl = new System.Windows.Forms.Label();
            this.player4_lbl = new System.Windows.Forms.Label();
            this.player3_lbl = new System.Windows.Forms.Label();
            this.player2_lbl = new System.Windows.Forms.Label();
            this.player1_lbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.form_value = new System.Windows.Forms.Label();
            this.form_lbl = new System.Windows.Forms.Label();
            this.peticionesBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.playersonlineGrid)).BeginInit();
            this.playersBox.SuspendLayout();
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
            this.peticionesBox.Location = new System.Drawing.Point(28, 79);
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
            this.playersonlineGrid.BackgroundColor = System.Drawing.Color.White;
            this.playersonlineGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.playersonlineGrid.Location = new System.Drawing.Point(28, 373);
            this.playersonlineGrid.Name = "playersonlineGrid";
            this.playersonlineGrid.RowHeadersWidth = 51;
            this.playersonlineGrid.RowTemplate.Height = 24;
            this.playersonlineGrid.Size = new System.Drawing.Size(201, 204);
            this.playersonlineGrid.TabIndex = 20;
            this.playersonlineGrid.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.playersonlineGrid_CellContentDoubleClick);
            // 
            // server_lbl
            // 
            this.server_lbl.AutoSize = true;
            this.server_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.server_lbl.Location = new System.Drawing.Point(24, 41);
            this.server_lbl.Name = "server_lbl";
            this.server_lbl.Size = new System.Drawing.Size(82, 20);
            this.server_lbl.TabIndex = 21;
            this.server_lbl.Text = "SERVER:";
            // 
            // server_value
            // 
            this.server_value.AutoSize = true;
            this.server_value.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.server_value.Location = new System.Drawing.Point(112, 41);
            this.server_value.Name = "server_value";
            this.server_value.Size = new System.Drawing.Size(0, 20);
            this.server_value.TabIndex = 22;
            // 
            // players_lbl
            // 
            this.players_lbl.AutoSize = true;
            this.players_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.players_lbl.Location = new System.Drawing.Point(361, 26);
            this.players_lbl.Name = "players_lbl";
            this.players_lbl.Size = new System.Drawing.Size(122, 29);
            this.players_lbl.TabIndex = 24;
            this.players_lbl.Text = "PLAYERS";
            // 
            // playersBox
            // 
            this.playersBox.Controls.Add(this.player5_lbl);
            this.playersBox.Controls.Add(this.player4_lbl);
            this.playersBox.Controls.Add(this.player3_lbl);
            this.playersBox.Controls.Add(this.player2_lbl);
            this.playersBox.Controls.Add(this.player1_lbl);
            this.playersBox.Location = new System.Drawing.Point(513, 13);
            this.playersBox.Name = "playersBox";
            this.playersBox.Size = new System.Drawing.Size(856, 48);
            this.playersBox.TabIndex = 25;
            this.playersBox.TabStop = false;
            // 
            // player5_lbl
            // 
            this.player5_lbl.AutoSize = true;
            this.player5_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player5_lbl.Location = new System.Drawing.Point(716, 13);
            this.player5_lbl.Name = "player5_lbl";
            this.player5_lbl.Size = new System.Drawing.Size(125, 29);
            this.player5_lbl.TabIndex = 30;
            this.player5_lbl.Text = "<<<<>>>>";
            // 
            // player4_lbl
            // 
            this.player4_lbl.AutoSize = true;
            this.player4_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player4_lbl.Location = new System.Drawing.Point(526, 13);
            this.player4_lbl.Name = "player4_lbl";
            this.player4_lbl.Size = new System.Drawing.Size(125, 29);
            this.player4_lbl.TabIndex = 29;
            this.player4_lbl.Text = "<<<<>>>>";
            // 
            // player3_lbl
            // 
            this.player3_lbl.AutoSize = true;
            this.player3_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player3_lbl.Location = new System.Drawing.Point(346, 13);
            this.player3_lbl.Name = "player3_lbl";
            this.player3_lbl.Size = new System.Drawing.Size(125, 29);
            this.player3_lbl.TabIndex = 28;
            this.player3_lbl.Text = "<<<<>>>>";
            // 
            // player2_lbl
            // 
            this.player2_lbl.AutoSize = true;
            this.player2_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player2_lbl.Location = new System.Drawing.Point(168, 13);
            this.player2_lbl.Name = "player2_lbl";
            this.player2_lbl.Size = new System.Drawing.Size(125, 29);
            this.player2_lbl.TabIndex = 27;
            this.player2_lbl.Text = "<<<<>>>>";
            // 
            // player1_lbl
            // 
            this.player1_lbl.AutoSize = true;
            this.player1_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player1_lbl.Location = new System.Drawing.Point(6, 13);
            this.player1_lbl.Name = "player1_lbl";
            this.player1_lbl.Size = new System.Drawing.Size(125, 29);
            this.player1_lbl.TabIndex = 26;
            this.player1_lbl.Text = "<<<<>>>>";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(39, 345);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 25);
            this.label1.TabIndex = 26;
            this.label1.Text = "PLAYERS ONLINE";
            // 
            // form_value
            // 
            this.form_value.AutoSize = true;
            this.form_value.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.form_value.Location = new System.Drawing.Point(93, 13);
            this.form_value.Name = "form_value";
            this.form_value.Size = new System.Drawing.Size(0, 20);
            this.form_value.TabIndex = 28;
            // 
            // form_lbl
            // 
            this.form_lbl.AutoSize = true;
            this.form_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.form_lbl.Location = new System.Drawing.Point(24, 13);
            this.form_lbl.Name = "form_lbl";
            this.form_lbl.Size = new System.Drawing.Size(63, 20);
            this.form_lbl.TabIndex = 27;
            this.form_lbl.Text = "FORM:";
            // 
            // GameWndw
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1500, 778);
            this.Controls.Add(this.form_value);
            this.Controls.Add(this.form_lbl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.playersBox);
            this.Controls.Add(this.players_lbl);
            this.Controls.Add(this.server_value);
            this.Controls.Add(this.server_lbl);
            this.Controls.Add(this.playersonlineGrid);
            this.Controls.Add(this.peticionesBox);
            this.Controls.Add(this.exitbtn);
            this.Name = "GameWndw";
            this.Text = "GameWndw";
            this.Load += new System.EventHandler(this.GameWndw_Load);
            this.Shown += new System.EventHandler(this.GameWndw_Shown);
            this.peticionesBox.ResumeLayout(false);
            this.peticionesBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.playersonlineGrid)).EndInit();
            this.playersBox.ResumeLayout(false);
            this.playersBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button exitbtn;
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
        private System.Windows.Forms.Label players_lbl;
        private System.Windows.Forms.GroupBox playersBox;
        private System.Windows.Forms.Label player5_lbl;
        private System.Windows.Forms.Label player4_lbl;
        private System.Windows.Forms.Label player3_lbl;
        private System.Windows.Forms.Label player2_lbl;
        private System.Windows.Forms.Label player1_lbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label form_value;
        private System.Windows.Forms.Label form_lbl;
    }
}