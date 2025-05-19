using System.Windows.Forms;

namespace SistemaLogin
{
    partial class AdminForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnDesactivar;
        private System.Windows.Forms.Button btnCerrarSesion;
        private System.Windows.Forms.Button btnCargarStock;
        private System.Windows.Forms.Button btnConsultarStock;
        private System.Windows.Forms.Button btnRegistrarVenta;
        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            btnAgregar = new Button();
            btnEditar = new Button();
            btnDesactivar = new Button();
            btnCerrarSesion = new Button();
            btnCargarStock = new Button();
            btnConsultarStock = new Button();
            btnRegistrarVenta = new Button();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(btnAgregar, 0, 0);
            tableLayoutPanel1.Controls.Add(btnEditar, 0, 1);
            tableLayoutPanel1.Controls.Add(btnDesactivar, 0, 2);
            tableLayoutPanel1.Controls.Add(btnCerrarSesion, 0, 3);
            tableLayoutPanel1.Controls.Add(btnCargarStock, 0, 4);
            tableLayoutPanel1.Controls.Add(btnConsultarStock, 0, 5);
            tableLayoutPanel1.Controls.Add(btnRegistrarVenta, 0, 6);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 7;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857132F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857132F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857132F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857132F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857132F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857132F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857132F));
            tableLayoutPanel1.Size = new Size(324, 313);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // btnAgregar
            // 
            btnAgregar.Dock = DockStyle.Fill;
            btnAgregar.Location = new Point(3, 3);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(318, 38);
            btnAgregar.TabIndex = 0;
            btnAgregar.Text = "Añadir perfil";
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnEditar
            // 
            btnEditar.Dock = DockStyle.Fill;
            btnEditar.Location = new Point(3, 47);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(318, 38);
            btnEditar.TabIndex = 1;
            btnEditar.Text = "Editar perfil";
            btnEditar.Click += btnEditar_Click;
            // 
            // btnDesactivar
            // 
            btnDesactivar.Dock = DockStyle.Fill;
            btnDesactivar.Location = new Point(3, 91);
            btnDesactivar.Name = "btnDesactivar";
            btnDesactivar.Size = new Size(318, 38);
            btnDesactivar.TabIndex = 2;
            btnDesactivar.Text = "Desactivar perfil";
            btnDesactivar.Click += btnDesactivar_Click;
            // 
            // btnCerrarSesion
            // 
            btnCerrarSesion.Dock = DockStyle.Fill;
            btnCerrarSesion.Location = new Point(3, 135);
            btnCerrarSesion.Name = "btnCerrarSesion";
            btnCerrarSesion.Size = new Size(318, 38);
            btnCerrarSesion.TabIndex = 3;
            btnCerrarSesion.Text = "Cerrar sesión";
            btnCerrarSesion.Click += btnCerrarSesion_Click;
            // 
            // btnCargarStock
            // 
            btnCargarStock.Dock = DockStyle.Fill;
            btnCargarStock.Location = new Point(3, 179);
            btnCargarStock.Name = "btnCargarStock";
            btnCargarStock.Size = new Size(318, 38);
            btnCargarStock.TabIndex = 4;
            btnCargarStock.Text = "Cargar Stock";
            btnCargarStock.Click += btnCargarStock_Click;
            // 
            // btnConsultarStock
            // 
            btnConsultarStock.Dock = DockStyle.Fill;
            btnConsultarStock.Location = new Point(3, 223);
            btnConsultarStock.Name = "btnConsultarStock";
            btnConsultarStock.Size = new Size(318, 38);
            btnConsultarStock.TabIndex = 5;
            btnConsultarStock.Text = "Consultar Stock";
            btnConsultarStock.Click += btnConsultarStock_Click;
            // 
            // btnRegistrarVenta
            // 
            btnRegistrarVenta.Location = new Point(3, 267);
            btnRegistrarVenta.Name = "btnRegistrarVenta";
            btnRegistrarVenta.Size = new Size(318, 39);
            btnRegistrarVenta.TabIndex = 6;
            btnRegistrarVenta.Text = "Registrar Venta";
            btnRegistrarVenta.UseVisualStyleBackColor = true;
            // 
            // AdminForm
            // 
            ClientSize = new Size(324, 313);
            Controls.Add(tableLayoutPanel1);
            Name = "AdminForm";
            Text = "Panel de Administrador";
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        
    }
}
