﻿using System.Windows.Forms;

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
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnDesactivar = new System.Windows.Forms.Button();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.btnCargarStock = new System.Windows.Forms.Button();
            this.btnConsultarStock = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));

            // Ajustamos a 6 filas
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66F)); // btnAgregar
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66F)); // btnEditar
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66F)); // btnDesactivar
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66F)); // btnCerrarSesion
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66F)); // btnCargarStock
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66F)); // btnConsultarStock

            // 
            // btnAgregar
            // 
            this.btnAgregar.Text = "Añadir perfil";
            this.btnAgregar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);

            // 
            // btnEditar
            // 
            this.btnEditar.Text = "Editar perfil";
            this.btnEditar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);

            // 
            // btnDesactivar
            // 
            this.btnDesactivar.Text = "Desactivar perfil";
            this.btnDesactivar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDesactivar.Click += new System.EventHandler(this.btnDesactivar_Click);

            // 
            // btnCerrarSesion
            // 
            this.btnCerrarSesion.Text = "Cerrar sesión";
            this.btnCerrarSesion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCerrarSesion.Click += new System.EventHandler(this.btnCerrarSesion_Click);

            // 
            // btnCargarStock
            // 
            this.btnCargarStock.Text = "Cargar Stock";
            this.btnCargarStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCargarStock.Click += new System.EventHandler(this.btnCargarStock_Click);

            // 
            // btnConsultarStock
            // 
            this.btnConsultarStock.Text = "Consultar Stock";
            this.btnConsultarStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnConsultarStock.Click += new System.EventHandler(this.btnConsultarStock_Click);

            // Agregar botones al panel
            this.tableLayoutPanel1.Controls.Add(this.btnAgregar, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnEditar, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnDesactivar, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnCerrarSesion, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnCargarStock, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnConsultarStock, 0, 5);

            // 
            // AdminForm
            // 
            this.ClientSize = new System.Drawing.Size(300, 300);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "AdminForm";
            this.Text = "Panel de Administrador";
            this.ResumeLayout(false);
        }

        #endregion
    }
}
