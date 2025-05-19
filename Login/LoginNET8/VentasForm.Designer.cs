namespace LoginNET8
{

    partial class VentasForm
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
            lblProducto = new Label();
            txtProducto = new TextBox();
            lblCantidad = new Label();
            numCantidad = new NumericUpDown();
            btnRegistrarVenta = new Button();
            lstVentas = new ListBox();
            ((System.ComponentModel.ISupportInitialize)numCantidad).BeginInit();
            SuspendLayout();
            // 
            // lblProducto
            // 
            lblProducto.AutoSize = true;
            lblProducto.BackColor = SystemColors.ActiveBorder;
            lblProducto.BorderStyle = BorderStyle.Fixed3D;
            lblProducto.Location = new Point(126, 117);
            lblProducto.Name = "lblProducto";
            lblProducto.Size = new Size(58, 17);
            lblProducto.TabIndex = 0;
            lblProducto.Text = "Producto";
            // 
            // txtProducto
            // 
            txtProducto.Location = new Point(211, 117);
            txtProducto.Name = "txtProducto";
            txtProducto.Size = new Size(100, 23);
            txtProducto.TabIndex = 1;
            // 
            // lblCantidad
            // 
            lblCantidad.AutoSize = true;
            lblCantidad.BackColor = SystemColors.ActiveBorder;
            lblCantidad.FlatStyle = FlatStyle.Popup;
            lblCantidad.ImageAlign = ContentAlignment.MiddleRight;
            lblCantidad.Location = new Point(126, 160);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(55, 15);
            lblCantidad.TabIndex = 2;
            lblCantidad.Text = "Cantidad";
            lblCantidad.Click += lblcantidad_Click;
            // 
            // numCantidad
            // 
            numCantidad.Location = new Point(213, 159);
            numCantidad.Name = "numCantidad";
            numCantidad.Size = new Size(120, 23);
            numCantidad.TabIndex = 3;
            // 
            // btnRegistrarVenta
            // 
            btnRegistrarVenta.Location = new Point(120, 209);
            btnRegistrarVenta.Name = "btnRegistrarVenta";
            btnRegistrarVenta.Size = new Size(75, 23);
            btnRegistrarVenta.TabIndex = 4;
            btnRegistrarVenta.Text = "Registrar Venta";
            btnRegistrarVenta.UseVisualStyleBackColor = true;
            // 
            // lstVentas
            // 
            lstVentas.FormattingEnabled = true;
            lstVentas.ItemHeight = 15;
            lstVentas.Location = new Point(392, 29);
            lstVentas.Name = "lstVentas";
            lstVentas.Size = new Size(316, 274);
            lstVentas.TabIndex = 5;
            // 
            // VentasForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lstVentas);
            Controls.Add(btnRegistrarVenta);
            Controls.Add(numCantidad);
            Controls.Add(lblCantidad);
            Controls.Add(txtProducto);
            Controls.Add(lblProducto);
            Name = "VentasForm";
            Text = "VentasForm";
            Load += VentasForm_Load;
            ((System.ComponentModel.ISupportInitialize)numCantidad).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblProducto;
        private TextBox txtProducto;
        private Label lblCantidad;
        private NumericUpDown numCantidad;
        private Button btnRegistrarVenta;
        private ListBox lstVentas;
    }
}
