using System.Windows.Forms;

namespace SistemaLogin
{
    partial class UserForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnCargarStock;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCargarStock = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));

            // 
            // btnCargarStock
            // 
            this.btnCargarStock.Text = "Cargar Stock";
            this.btnCargarStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCargarStock.Click += new System.EventHandler(this.btnCargarStock_Click);

            // 
            // Agregar el botón al TableLayoutPanel
            // 
            this.tableLayoutPanel1.Controls.Add(this.btnCargarStock, 0, 0);

            // 
            // UserForm
            // 
            this.ClientSize = new System.Drawing.Size(300, 150);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UserForm";
            this.Text = "Panel de Usuario";
            this.ResumeLayout(false);

            this.btnConsultarStock = new System.Windows.Forms.Button();
            this.btnConsultarStock.Text = "Consultar Stock";
            this.btnConsultarStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnConsultarStock.Click += new System.EventHandler(this.btnConsultarStock_Click);

            // Configurar el layout para 2 filas
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Clear();
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));

            this.tableLayoutPanel1.Controls.Add(this.btnCargarStock, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnConsultarStock, 0, 1);

        }

        private System.Windows.Forms.Button btnConsultarStock;
        #endregion
    }
}
