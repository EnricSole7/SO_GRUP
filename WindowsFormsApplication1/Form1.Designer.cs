namespace WindowsFormsApplication1
{
    partial class Form1
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
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
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(31, 29);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 31);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nombre";
            // 
            // nombre
            // 
            this.nombre.Location = new System.Drawing.Point(155, 38);
            this.nombre.Margin = new System.Windows.Forms.Padding(4);
            this.nombre.Name = "nombre";
            this.nombre.Size = new System.Drawing.Size(217, 22);
            this.nombre.TabIndex = 3;
            // 
            // Enviar_nombre
            // 
            this.Enviar_nombre.Location = new System.Drawing.Point(376, 38);
            this.Enviar_nombre.Margin = new System.Windows.Forms.Padding(4);
            this.Enviar_nombre.Name = "Enviar_nombre";
            this.Enviar_nombre.Size = new System.Drawing.Size(100, 22);
            this.Enviar_nombre.TabIndex = 5;
            this.Enviar_nombre.Text = "Enviar";
            this.Enviar_nombre.UseVisualStyleBackColor = true;
            this.Enviar_nombre.Click += new System.EventHandler(this.Enviar_nombre_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.Enviar_server);
            this.groupBox1.Controls.Add(this.server_box);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.Enviar_fecha);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.fecha);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.Enviar_nombre);
            this.groupBox1.Controls.Add(this.nombre);
            this.groupBox1.Location = new System.Drawing.Point(45, 32);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(506, 293);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Peticiones";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(150, 226);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(324, 64);
            this.label7.TabIndex = 17;
            this.label7.Text = "Devuelve el nombre de las personas que han jugado\r\nen este servidor\r\n\r\n\r\n";
            // 
            // Enviar_server
            // 
            this.Enviar_server.Location = new System.Drawing.Point(378, 200);
            this.Enviar_server.Margin = new System.Windows.Forms.Padding(4);
            this.Enviar_server.Name = "Enviar_server";
            this.Enviar_server.Size = new System.Drawing.Size(100, 22);
            this.Enviar_server.TabIndex = 16;
            this.Enviar_server.Text = "Enviar";
            this.Enviar_server.UseVisualStyleBackColor = true;
            this.Enviar_server.Click += new System.EventHandler(this.Enviar_server_Click);
            // 
            // server_box
            // 
            this.server_box.Location = new System.Drawing.Point(153, 200);
            this.server_box.Margin = new System.Windows.Forms.Padding(4);
            this.server_box.Name = "server_box";
            this.server_box.Size = new System.Drawing.Size(219, 22);
            this.server_box.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(152, 148);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(326, 48);
            this.label6.TabIndex = 14;
            this.label6.Text = "Devuelve el nombre de la persona que ha ganado en\r\nesta fecha\r\n\r\n";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(31, 191);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 31);
            this.label5.TabIndex = 13;
            this.label5.Text = "Server";
            // 
            // Enviar_fecha
            // 
            this.Enviar_fecha.Location = new System.Drawing.Point(378, 122);
            this.Enviar_fecha.Margin = new System.Windows.Forms.Padding(4);
            this.Enviar_fecha.Name = "Enviar_fecha";
            this.Enviar_fecha.Size = new System.Drawing.Size(100, 22);
            this.Enviar_fecha.TabIndex = 8;
            this.Enviar_fecha.Text = "Enviar";
            this.Enviar_fecha.UseVisualStyleBackColor = true;
            this.Enviar_fecha.Click += new System.EventHandler(this.Enviar_fecha_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(152, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(326, 32);
            this.label4.TabIndex = 12;
            this.label4.Text = "Devuelve la localización del server donde ha ganado\r\n esta persona\r\n";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(31, 113);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 31);
            this.label3.TabIndex = 11;
            this.label3.Text = "Fecha";
            // 
            // fecha
            // 
            this.fecha.Location = new System.Drawing.Point(155, 122);
            this.fecha.Margin = new System.Windows.Forms.Padding(4);
            this.fecha.Name = "fecha";
            this.fecha.Size = new System.Drawing.Size(219, 22);
            this.fecha.TabIndex = 10;
            // 
            // Desconectar
            // 
            this.Desconectar.Location = new System.Drawing.Point(608, 23);
            this.Desconectar.Margin = new System.Windows.Forms.Padding(4);
            this.Desconectar.Name = "Desconectar";
            this.Desconectar.Size = new System.Drawing.Size(112, 38);
            this.Desconectar.TabIndex = 7;
            this.Desconectar.Text = "desconectar";
            this.Desconectar.UseVisualStyleBackColor = true;
            this.Desconectar.Click += new System.EventHandler(this.Desconectar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 361);
            this.Controls.Add(this.Desconectar);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox nombre;
        private System.Windows.Forms.Button Enviar_nombre;
        private System.Windows.Forms.GroupBox groupBox1;
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
    }
}

