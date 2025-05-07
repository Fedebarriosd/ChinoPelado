using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace SistemaLogin
{
    public partial class ConsultaStockForm : Form
    {
        public ConsultaStockForm()
        {
            InitializeComponent();
            CargarDatosStock();
        }

        private void CargarDatosStock()
        {
            using (var conexion = ConexionSQLite.ObtenerConexion())
            {
                string query = "SELECT Descripcion, Cantidad FROM RegistroStock";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conexion);
                DataTable tabla = new DataTable();
                adapter.Fill(tabla);
                dataGridViewStock.DataSource = tabla;
            }
        }
    }
}
