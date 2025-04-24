using System;
using System.Windows.Forms;
namespace SistemaLogin
{
    partial class AgregarPerfilForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.TextBox txtContrasena;
        private System.Windows.Forms.CheckBox chkEsAdministrador;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.txtContrasena = new System.Windows.Forms.TextBox();
            this.chkEsAdministrador = new System.Windows.Forms.CheckBox();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(30, 30);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(200, 20);
            this.txtUsuario.TabIndex = 0;
            this.ConfigurePlaceholder(this.txtUsuario, "Usuario");
            // 
            // txtContrasena
            // 
            this.txtContrasena.Location = new System.Drawing.Point(30, 70);
            this.txtContrasena.Name = "txtContrasena";
            this.txtContrasena.Size = new System.Drawing.Size(200, 20);
            this.txtContrasena.TabIndex = 1;
            this.txtContrasena.UseSystemPasswordChar = true;
            this.ConfigurePlaceholder(this.txtContrasena, "Contraseña");
            // 
            // chkEsAdministrador
            // 
            this.chkEsAdministrador.Location = new System.Drawing.Point(30, 110);
            this.chkEsAdministrador.Name = "chkEsAdministrador";
            this.chkEsAdministrador.Size = new System.Drawing.Size(200, 20);
            this.chkEsAdministrador.TabIndex = 2;
            this.chkEsAdministrador.Text = "Es Administrador";
            this.chkEsAdministrador.UseVisualStyleBackColor = true;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(30, 150);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(90, 30);
            this.btnAceptar.TabIndex = 3;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(140, 150);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(90, 30);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // AgregarPerfilForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 211);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.chkEsAdministrador);
            this.Controls.Add(this.txtContrasena);
            this.Controls.Add(this.txtUsuario);
            this.Name = "AgregarPerfilForm";
            this.Text = "Agregar Perfil";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void ConfigurePlaceholder(TextBox txtBox, string placeholder)
        {
            // Establecer el texto por defecto del placeholder
            txtBox.ForeColor = System.Drawing.Color.Gray;
            txtBox.Text = placeholder;

            // Cuando el usuario hace clic (Enter) en el TextBox, se borra el placeholder
            txtBox.Enter += (sender, e) =>
            {
                if (txtBox.Text == placeholder)
                {
                    txtBox.Text = "";
                    txtBox.ForeColor = System.Drawing.Color.Black;
                }
            };

            // Cuando el usuario sale (Leave) del TextBox, si está vacío, se muestra el placeholder
            txtBox.Leave += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtBox.Text))
                {
                    txtBox.Text = placeholder;
                    txtBox.ForeColor = System.Drawing.Color.Gray;
                }
            };
        }
    }
}
