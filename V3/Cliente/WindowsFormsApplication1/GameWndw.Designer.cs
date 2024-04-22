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
            this.database = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.peticionesBox = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.maze_lbl = new System.Windows.Forms.Label();
            this.symbols_lbl = new System.Windows.Forms.Label();
            this.checkSymbols = new System.Windows.Forms.RadioButton();
            this.checkMaze = new System.Windows.Forms.RadioButton();
            this.checkTBD = new System.Windows.Forms.RadioButton();
            this.InvitePlayersBOX = new System.Windows.Forms.CheckedListBox();
            this.peticionesBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // database
            // 
            this.database.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.database.Location = new System.Drawing.Point(28, 299);
            this.database.Margin = new System.Windows.Forms.Padding(4);
            this.database.Name = "database";
            this.database.Size = new System.Drawing.Size(213, 76);
            this.database.TabIndex = 16;
            this.database.Text = "INVITE PLAYER";
            this.database.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(28, 591);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(213, 76);
            this.button1.TabIndex = 17;
            this.button1.Text = "EXIT LOBBY";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(345, 348);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(213, 76);
            this.button2.TabIndex = 18;
            this.button2.Text = "INVITE PLAYER";
            this.button2.UseVisualStyleBackColor = true;
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
            this.peticionesBox.Location = new System.Drawing.Point(393, 33);
            this.peticionesBox.Margin = new System.Windows.Forms.Padding(4);
            this.peticionesBox.Name = "peticionesBox";
            this.peticionesBox.Padding = new System.Windows.Forms.Padding(4);
            this.peticionesBox.Size = new System.Drawing.Size(459, 268);
            this.peticionesBox.TabIndex = 19;
            this.peticionesBox.TabStop = false;
            this.peticionesBox.Text = "MINIGAMES";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(72, 196);
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
            this.maze_lbl.Location = new System.Drawing.Point(72, 125);
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
            this.symbols_lbl.Location = new System.Drawing.Point(72, 53);
            this.symbols_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.symbols_lbl.Name = "symbols_lbl";
            this.symbols_lbl.Size = new System.Drawing.Size(134, 29);
            this.symbols_lbl.TabIndex = 1;
            this.symbols_lbl.Text = "SYMBOLS";
            // 
            // checkSymbols
            // 
            this.checkSymbols.AutoSize = true;
            this.checkSymbols.Location = new System.Drawing.Point(45, 61);
            this.checkSymbols.Name = "checkSymbols";
            this.checkSymbols.Size = new System.Drawing.Size(17, 16);
            this.checkSymbols.TabIndex = 14;
            this.checkSymbols.TabStop = true;
            this.checkSymbols.UseVisualStyleBackColor = true;
            // 
            // checkMaze
            // 
            this.checkMaze.AutoSize = true;
            this.checkMaze.Location = new System.Drawing.Point(45, 133);
            this.checkMaze.Name = "checkMaze";
            this.checkMaze.Size = new System.Drawing.Size(17, 16);
            this.checkMaze.TabIndex = 15;
            this.checkMaze.TabStop = true;
            this.checkMaze.UseVisualStyleBackColor = true;
            // 
            // checkTBD
            // 
            this.checkTBD.AutoSize = true;
            this.checkTBD.Location = new System.Drawing.Point(45, 204);
            this.checkTBD.Name = "checkTBD";
            this.checkTBD.Size = new System.Drawing.Size(17, 16);
            this.checkTBD.TabIndex = 16;
            this.checkTBD.TabStop = true;
            this.checkTBD.UseVisualStyleBackColor = true;
            // 
            // InvitePlayersBOX
            // 
            this.InvitePlayersBOX.FormattingEnabled = true;
            this.InvitePlayersBOX.Location = new System.Drawing.Point(28, 391);
            this.InvitePlayersBOX.Name = "InvitePlayersBOX";
            this.InvitePlayersBOX.Size = new System.Drawing.Size(213, 89);
            this.InvitePlayersBOX.TabIndex = 20;
            // 
            // GameWndw
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1392, 701);
            this.Controls.Add(this.InvitePlayersBOX);
            this.Controls.Add(this.peticionesBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.database);
            this.Name = "GameWndw";
            this.Text = "GameWndw";
            this.Load += new System.EventHandler(this.GameWndw_Load);
            this.peticionesBox.ResumeLayout(false);
            this.peticionesBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button database;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox peticionesBox;
        private System.Windows.Forms.RadioButton checkTBD;
        private System.Windows.Forms.RadioButton checkMaze;
        private System.Windows.Forms.RadioButton checkSymbols;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label maze_lbl;
        private System.Windows.Forms.Label symbols_lbl;
        private System.Windows.Forms.CheckedListBox InvitePlayersBOX;
    }
}