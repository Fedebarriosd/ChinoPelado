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
        // Diccionario de IDs con sus nombres
        private Dictionary<string, string> productos = new Dictionary<string, string>()
{
    { "01", "Pizza Peperoni" },
    { "02", "Pizza Muzzarella" },
    { "03", "Pizza Napolitana" },
    { "04", "Pizza Pollo con Catupiry" },
    { "05", "Pizza Palmito" },
    { "06", "Pizza Bacon" },
    { "07", "Pizza Mexicana" },
    { "08", "Pizza Carnibora" },
    { "09", "Pizza Margatira" },
    { "10", "Pizza Vegetariana" },
    { "007", "Pizza Espacial del chino" },

};

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
            string productoId = txtProducto.Text.Trim();
            string producto;

            if (!productos.TryGetValue(productoId, out producto))
            {
                MessageBox.Show("ID de producto no válido.");
                return;
            }
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
            int nroPedido = 1; // Contador para el número de pedido

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

                        // Si estás usando un diccionario de productos, podés traducir el ID a nombre
                        // Ejemplo: producto = productos.ContainsKey(producto) ? productos[producto] : producto;

                        var item = new ListViewItem(nroPedido.ToString()); // Nro. Pedido
                        item.SubItems.Add(producto);                       // Producto
                        item.SubItems.Add(total.ToString());               // Cantidad

                        lstVentas.Items.Add(item);
                        nroPedido++;
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
                MessageBox.Show("Selecciona un producto para eliminar.");
                return;
            }

            string producto = lstVentas.SelectedItems[0].SubItems[1].Text; // Columna 1 = Producto

            var confirmResult = MessageBox.Show($"¿Estás seguro de eliminar todas las ventas de '{producto}'?",
                                                "Confirmar eliminación",
                                                MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                using (var connection = new SQLiteConnection("Data Source=ventas.db"))
                {
                    connection.Open();
                    string deleteQuery = "DELETE FROM ventas WHERE producto = @producto";

                    using (var cmd = new SQLiteCommand(deleteQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@producto", producto);
                        cmd.ExecuteNonQuery();
                    }
                }

                CargarResumenVentas(); // Recarga el ListView después de eliminar
            }

        }
    }
}
