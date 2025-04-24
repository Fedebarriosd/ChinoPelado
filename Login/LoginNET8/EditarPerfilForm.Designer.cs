namespace SistemaLogin
{
    partial class EditarPerfilForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label lblContrasena;
        private System.Windows.Forms.TextBox txtContrasena;
        private System.Windows.Forms.CheckBox chkEsAdministrador;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.lblContrasena = new System.Windows.Forms.Label();
            this.txtContrasena = new System.Windows.Forms.TextBox();
            this.chkEsAdministrador = new System.Windows.Forms.CheckBox();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            // 
            // lblUsuario
            // 
            this.lblUsuario.Text = "Usuario:";
            this.lblUsuario.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblUsuario.AutoSize = true;
            this.tableLayoutPanel1.Controls.Add(this.lblUsuario, 0, 0);
            // 
            // txtUsuario
            // 
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.tableLayoutPanel1.Controls.Add(this.txtUsuario, 1, 0);
            // 
            // lblContrasena
            // 
            this.lblContrasena.Text = "Contraseña:";
            this.lblContrasena.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblContrasena.AutoSize = true;
            this.tableLayoutPanel1.Controls.Add(this.lblContrasena, 0, 1);
            // 
            // txtContrasena
            // 
            this.txtContrasena.Name = "txtContrasena";
            this.txtContrasena.UseSystemPasswordChar = true;
            this.txtContrasena.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.tableLayoutPanel1.Controls.Add(this.txtContrasena, 1, 1);
            // 
            // chkEsAdministrador
            // 
            this.chkEsAdministrador.Text = "Es Administrador";
            this.chkEsAdministrador.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanel1.SetColumnSpan(this.chkEsAdministrador, 2);
            this.tableLayoutPanel1.Controls.Add(this.chkEsAdministrador, 0, 2);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            this.tableLayoutPanel1.Controls.Add(this.btnAceptar, 0, 3);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            this.tableLayoutPanel1.Controls.Add(this.btnCancelar, 1, 3);
            // 
            // EditarPerfilForm
            // 
            this.ClientSize = new System.Drawing.Size(350, 200);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "EditarPerfilForm";
            this.Text = "Editar Perfil";
            this.Load += new System.EventHandler(this.EditarPerfilForm_Load);
            this.ResumeLayout(false);
        }

        #endregion
    }
}
