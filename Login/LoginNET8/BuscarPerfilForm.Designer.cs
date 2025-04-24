using System;
using System.Windows.Forms;
namespace SistemaLogin
{
    partial class BuscarPerfilForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ListBox lstUsuarios;
        private System.Windows.Forms.Button btnSeleccionar;
        private System.Windows.Forms.Button btnCancelar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lstUsuarios = new System.Windows.Forms.ListBox();
            this.btnSeleccionar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstUsuarios
            // 
            this.lstUsuarios.FormattingEnabled = true;
            this.lstUsuarios.Location = new System.Drawing.Point(30, 30);
            this.lstUsuarios.Name = "lstUsuarios";
            this.lstUsuarios.Size = new System.Drawing.Size(220, 95);
            this.lstUsuarios.TabIndex = 0;
            // 
            // btnSeleccionar
            // 
            this.btnSeleccionar.Location = new System.Drawing.Point(30, 140);
            this.btnSeleccionar.Name = "btnSeleccionar";
            this.btnSeleccionar.Size = new System.Drawing.Size(90, 30);
            this.btnSeleccionar.TabIndex = 1;
            this.btnSeleccionar.Text = "Seleccionar";
            this.btnSeleccionar.UseVisualStyleBackColor = true;
            this.btnSeleccionar.Click += new System.EventHandler(this.btnSeleccionar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(160, 140);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(90, 30);
            this.btnCancelar.TabIndex = 2;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // BuscarPerfilForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 211);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnSeleccionar);
            this.Controls.Add(this.lstUsuarios);
            this.Name = "BuscarPerfilForm";
            this.Text = "Buscar Perfil";
            this.Load += new System.EventHandler(this.BuscarPerfilForm_Load);
            this.ResumeLayout(false);
        }
    }
}
