using System.Drawing;
using System.Windows.Forms;
using System;

namespace SistemaLogin
{
    /// <summary>
    /// Clase parcial del formulario <see cref="RegistroStockForm"/> que contiene la definición de los controles y su inicialización.
    /// </summary>
    partial class RegistroStockForm
    {
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Libera los recursos utilizados por el formulario.
        /// </summary>
        /// <param name="disposing">true si se deben liberar los recursos administrados; de lo contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.TextBox txtFechaManual;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.NumericUpDown nudCantidad;
        private System.Windows.Forms.Button btnGuardar;

        /// <summary>
        /// Inicializa y configura los controles del formulario <see cref="RegistroStockForm"/>.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblFecha = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.txtFechaManual = new System.Windows.Forms.TextBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.nudCantidad = new System.Windows.Forms.NumericUpDown();
            this.btnGuardar = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // lblFecha
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(30, 20);
            this.lblFecha.Text = "Fecha (dd/MM/yyyy):";

            // txtFechaManual
            this.txtFechaManual.Location = new System.Drawing.Point(30, 40);
            this.txtFechaManual.Size = new System.Drawing.Size(200, 22);

            // lblDescripcion
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Location = new System.Drawing.Point(30, 70);
            this.lblDescripcion.Text = "Descripción:";

            // txtDescripcion
            this.txtDescripcion.Location = new System.Drawing.Point(30, 90);
            this.txtDescripcion.Size = new System.Drawing.Size(200, 22);

            // lblCantidad
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Location = new System.Drawing.Point(30, 120);
            this.lblCantidad.Text = "Cantidad:";

            // nudCantidad
            this.nudCantidad.Location = new System.Drawing.Point(30, 140);
            this.nudCantidad.Maximum = 10000;

            // btnGuardar
            this.btnGuardar.Location = new System.Drawing.Point(30, 180);
            this.btnGuardar.Size = new System.Drawing.Size(200, 30);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);

            // Agregá todos los controles al formulario
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.txtFechaManual);
            this.Controls.Add(this.lblDescripcion);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.lblCantidad);
            this.Controls.Add(this.nudCantidad);
            this.Controls.Add(this.btnGuardar);

            this.ClientSize = new System.Drawing.Size(280, 240);
            this.Text = "Registro de Stock";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }

}
