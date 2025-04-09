namespace Login
{
    partial class Form1
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.TextBoxUsuario = new System.Windows.Forms.TextBox();
            this.ButtonLogin = new System.Windows.Forms.Button();
            this.TextBoxContraseña = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(115, 22);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // TextBoxUsuario
            // 
            this.TextBoxUsuario.BackColor = System.Drawing.Color.Bisque;
            this.TextBoxUsuario.Font = new System.Drawing.Font("Georgia", 8F);
            this.TextBoxUsuario.Location = new System.Drawing.Point(12, 12);
            this.TextBoxUsuario.Name = "TextBoxUsuario";
            this.TextBoxUsuario.Size = new System.Drawing.Size(292, 23);
            this.TextBoxUsuario.TabIndex = 1;
            this.TextBoxUsuario.Text = "Ingrese su usuario";
            this.TextBoxUsuario.Enter += new System.EventHandler(this.TextBoxUsuario_Enter);
            this.TextBoxUsuario.Leave += new System.EventHandler(this.TextBoxUsuario_Leave);
            // 
            // ButtonLogin
            // 
            this.ButtonLogin.BackColor = System.Drawing.Color.Chocolate;
            this.ButtonLogin.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonLogin.Location = new System.Drawing.Point(12, 69);
            this.ButtonLogin.Name = "ButtonLogin";
            this.ButtonLogin.Size = new System.Drawing.Size(292, 29);
            this.ButtonLogin.TabIndex = 2;
            this.ButtonLogin.Text = "Iniciar sesión";
            this.ButtonLogin.UseVisualStyleBackColor = false;
            this.ButtonLogin.Click += new System.EventHandler(this.ButtonLogin_Click);
            // 
            // TextBoxContraseña
            // 
            this.TextBoxContraseña.BackColor = System.Drawing.Color.Bisque;
            this.TextBoxContraseña.Font = new System.Drawing.Font("Georgia", 8F);
            this.TextBoxContraseña.Location = new System.Drawing.Point(12, 40);
            this.TextBoxContraseña.Name = "TextBoxContraseña";
            this.TextBoxContraseña.Size = new System.Drawing.Size(292, 23);
            this.TextBoxContraseña.TabIndex = 3;
            this.TextBoxContraseña.Text = "Ingrese su contraseña";
            this.TextBoxContraseña.UseSystemPasswordChar = true;
            this.TextBoxContraseña.Enter += new System.EventHandler(this.TextBoxContraseña_Enter);
            this.TextBoxContraseña.Leave += new System.EventHandler(this.TextBoxContraseña_Leave);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 529);
            this.Controls.Add(this.TextBoxContraseña);
            this.Controls.Add(this.ButtonLogin);
            this.Controls.Add(this.TextBoxUsuario);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox TextBoxUsuario;
        private System.Windows.Forms.Button ButtonLogin;
        private System.Windows.Forms.TextBox TextBoxContraseña;
    }
}

