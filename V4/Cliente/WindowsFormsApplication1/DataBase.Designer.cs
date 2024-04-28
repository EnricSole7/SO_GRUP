namespace WindowsFormsApplication1
{
    partial class DataBase
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
            this.jugadoresGrid = new System.Windows.Forms.DataGridView();
            this.serversGrid = new System.Windows.Forms.DataGridView();
            this.partidasGrid = new System.Windows.Forms.DataGridView();
            this.user_lbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.jugadoresGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.serversGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.partidasGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // jugadoresGrid
            // 
            this.jugadoresGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.jugadoresGrid.Location = new System.Drawing.Point(50, 58);
            this.jugadoresGrid.Name = "jugadoresGrid";
            this.jugadoresGrid.RowHeadersWidth = 51;
            this.jugadoresGrid.RowTemplate.Height = 24;
            this.jugadoresGrid.Size = new System.Drawing.Size(384, 363);
            this.jugadoresGrid.TabIndex = 0;
            // 
            // serversGrid
            // 
            this.serversGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.serversGrid.Location = new System.Drawing.Point(460, 58);
            this.serversGrid.Name = "serversGrid";
            this.serversGrid.RowHeadersWidth = 51;
            this.serversGrid.RowTemplate.Height = 24;
            this.serversGrid.Size = new System.Drawing.Size(246, 363);
            this.serversGrid.TabIndex = 1;
            // 
            // partidasGrid
            // 
            this.partidasGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.partidasGrid.Location = new System.Drawing.Point(737, 58);
            this.partidasGrid.Name = "partidasGrid";
            this.partidasGrid.RowHeadersWidth = 51;
            this.partidasGrid.RowTemplate.Height = 24;
            this.partidasGrid.Size = new System.Drawing.Size(620, 363);
            this.partidasGrid.TabIndex = 2;
            // 
            // user_lbl
            // 
            this.user_lbl.AutoSize = true;
            this.user_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.user_lbl.Location = new System.Drawing.Point(45, 30);
            this.user_lbl.Name = "user_lbl";
            this.user_lbl.Size = new System.Drawing.Size(103, 25);
            this.user_lbl.TabIndex = 10;
            this.user_lbl.Text = "PLAYERS";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(455, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 25);
            this.label1.TabIndex = 11;
            this.label1.Text = "SERVERS";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(732, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 25);
            this.label2.TabIndex = 12;
            this.label2.Text = "GAMES";
            // 
            // DataBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1391, 477);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.user_lbl);
            this.Controls.Add(this.partidasGrid);
            this.Controls.Add(this.serversGrid);
            this.Controls.Add(this.jugadoresGrid);
            this.Name = "DataBase";
            this.Text = "DataBase";
            this.Load += new System.EventHandler(this.FormLogin_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.jugadoresGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.serversGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.partidasGrid)).EndInit();
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
    }
}