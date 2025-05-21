using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginNET8
{
    public partial class VentasForm : Form
    {
        private string connectionString = "Data Source=ventas.db";

        public VentasForm()
        {
            InitializeComponent();
        }

        private void VentasForm_Load(object sender, EventArgs e)
        {
            using (var connection = new SQLiteConnection("Data Source=ventas.db"))
            {
                connection.Open();
                string crearTabla = @"CREATE TABLE IF NOT EXISTS ventas (
                                id INTEGER PRIMARY KEY AUTOINCREMENT,
                                fecha_hora TEXT,
                                producto TEXT,
                                cantidad INTEGER
                              );";
                using (var cmd = new SQLiteCommand(crearTabla, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }

            CargarResumenVentas(); // para cargar los datos al abrir el form
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            string producto = txtProducto.Text;
            int cantidad = (int)numCantidad.Value;

            if (string.IsNullOrWhiteSpace(producto))
            {
                MessageBox.Show("Ingrese un producto.");
                return;
            }

            using (var connection = new SQLiteConnection("Data Source=ventas.db"))
            {
                connection.Open();
                string insert = "INSERT INTO ventas (fecha_hora, producto, cantidad) VALUES (@fecha, @producto, @cantidad)";
                using (var cmd = new SQLiteCommand(insert, connection))
                {
                    cmd.Parameters.AddWithValue("@fecha", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@producto", producto);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.ExecuteNonQuery();
                }
            }

            txtProducto.Clear();
            numCantidad.Value = 1;
            CargarResumenVentas();
        }
        private void CargarResumenVentas()
        {
            lstVentas.Items.Clear();

            using (var connection = new SQLiteConnection("Data Source=ventas.db"))
            {
                connection.Open();
                string query = "SELECT producto, SUM(cantidad) as total FROM ventas GROUP BY producto";
                using (var cmd = new SQLiteCommand(query, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string producto = reader["producto"].ToString();
                        int total = Convert.ToInt32(reader["total"]);

                        var item = new ListViewItem(producto);
                        item.SubItems.Add(total.ToString());

                        lstVentas.Items.Add(item);
                    }
                }
            }
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (lstVentas.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecciona un producto del listado para eliminar.");
                return;
            }

            string productoSeleccionado = lstVentas.SelectedItems[0].Text;

            DialogResult result = MessageBox.Show($"¿Seguro que quieres eliminar las ventas del producto '{productoSeleccionado}'?",
                                                  "Confirmar eliminación",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string deleteQuery = "DELETE FROM ventas WHERE producto = @producto";

                    using (var cmd = new SQLiteCommand(deleteQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@producto", productoSeleccionado);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Eliminado correctamente.");
                CargarResumenVentas(); // Recarga el resumen
            }

        }
    }
}
