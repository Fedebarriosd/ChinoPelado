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
            btnRegistrar = new Button();
            lstVentas = new ListView();
            Id = new ColumnHeader();
            PRODUCTO = new ColumnHeader();
            CANTIDAD = new ColumnHeader();
            btnsalir = new Button();
            btnEliminar = new Button();
            ((System.ComponentModel.ISupportInitialize)numCantidad).BeginInit();
            SuspendLayout();
            // 
            // lblProducto
            // 
            lblProducto.AutoSize = true;
            lblProducto.BackColor = SystemColors.ActiveBorder;
            lblProducto.BorderStyle = BorderStyle.Fixed3D;
            lblProducto.Location = new Point(35, 58);
            lblProducto.Name = "lblProducto";
            lblProducto.Size = new Size(58, 17);
            lblProducto.TabIndex = 0;
            lblProducto.Text = "Producto";
            // 
            // txtProducto
            // 
            txtProducto.Location = new Point(133, 55);
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
            lblCantidad.Location = new Point(35, 106);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(55, 15);
            lblCantidad.TabIndex = 2;
            lblCantidad.Text = "Cantidad";
            // 
            // numCantidad
            // 
            numCantidad.Location = new Point(133, 104);
            numCantidad.Name = "numCantidad";
            numCantidad.Size = new Size(120, 23);
            numCantidad.TabIndex = 3;
            // 
            // btnRegistrar
            // 
            btnRegistrar.Location = new Point(35, 186);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(75, 23);
            btnRegistrar.TabIndex = 4;
            btnRegistrar.Text = "Registrar Venta";
            btnRegistrar.UseVisualStyleBackColor = true;
            btnRegistrar.Click += btnRegistrar_Click;
            // 
            // lstVentas
            // 
            lstVentas.Columns.AddRange(new ColumnHeader[] { Id, PRODUCTO, CANTIDAD });
            lstVentas.Location = new Point(303, 55);
            lstVentas.Name = "lstVentas";
            lstVentas.Size = new Size(293, 264);
            lstVentas.TabIndex = 6;
            lstVentas.UseCompatibleStateImageBehavior = false;
            lstVentas.View = View.Details;
            // 
            // Id
            // 
            Id.Text = "Nro Pedido";
            Id.Width = 80;
            // 
            // PRODUCTO
            // 
            PRODUCTO.Text = "Producto ";
            PRODUCTO.Width = 150;
            // 
            // CANTIDAD
            // 
            CANTIDAD.Text = "Cantidad";
            CANTIDAD.Width = 80;
            // 
            // btnsalir
            // 
            btnsalir.Location = new Point(158, 186);
            btnsalir.Name = "btnsalir";
            btnsalir.Size = new Size(75, 23);
            btnsalir.TabIndex = 7;
            btnsalir.Text = "Salir";
            btnsalir.UseVisualStyleBackColor = true;
            btnsalir.Click += btnsalir_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(35, 232);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(75, 23);
            btnEliminar.TabIndex = 8;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // VentasForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(636, 361);
            Controls.Add(btnEliminar);
            Controls.Add(btnsalir);
            Controls.Add(lstVentas);
            Controls.Add(btnRegistrar);
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
        private Button btnRegistrar;
        private ListView lstVentas;
        private ColumnHeader PRODUCTO;
        private ColumnHeader CANTIDAD;
        private Button btnsalir;
        private Button btnEliminar;
        private ColumnHeader Id;
    }
}
