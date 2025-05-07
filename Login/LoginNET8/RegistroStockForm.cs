using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace SistemaLogin
{
    public partial class RegistroStockForm : Form
    {
        private SQLiteConnection conexion;

        /// <summary>
        /// Inicializa una nueva instancia del formulario <see cref="RegistroStockForm"/>.
        /// </summary>
        public RegistroStockForm()
        {
            InitializeComponent();
            this.Load += RegistroStockForm_Load;
        }

        private void RegistroStockForm_Load(object sender, EventArgs e)
        {
            string cadenaConexion = "Data Source=miBaseDeDatos.sqlite;Version=3;";
            conexion = new SQLiteConnection(cadenaConexion);
            conexion.Open();

            string crearTabla = @"CREATE TABLE IF NOT EXISTS RegistroStock (
                                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    Fecha TEXT NOT NULL,
                                    Descripcion TEXT NOT NULL,
                                    Cantidad INTEGER NOT NULL
                                  );";

            using (var comando = new SQLiteCommand(crearTabla, conexion))
            {
                comando.ExecuteNonQuery();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string descripcion = txtDescripcion.Text;
            int cantidad = (int)nudCantidad.Value;
            string fechaTexto = txtFechaManual.Text;

            if (!DateTime.TryParseExact(fechaTexto, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime fecha))
            {
                MessageBox.Show("La fecha ingresada no es válida. Debe tener el formato dd/MM/yyyy.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string fechaFormateada = fecha.ToString("yyyy-MM-dd");

            string insertar = "INSERT INTO RegistroStock (Fecha, Descripcion, Cantidad) VALUES (@fecha, @descripcion, @cantidad)";

            using (var comando = new SQLiteCommand(insertar, conexion))
            {
                comando.Parameters.AddWithValue("@fecha", fechaFormateada);
                comando.Parameters.AddWithValue("@descripcion", descripcion);
                comando.Parameters.AddWithValue("@cantidad", cantidad);

                comando.ExecuteNonQuery();
            }

            MessageBox.Show("Registro guardado exitosamente");

            // Limpiar campos
            txtFechaManual.Text = "";
            txtDescripcion.Text = "";
            nudCantidad.Value = 0;
        }



        /// <summary>
        /// Se ejecuta cuando el formulario se cierra.
        /// </summary>
        /// <param name="e">Argumentos del evento de cierre del formulario.</param>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (conexion != null)
                conexion.Close();

            base.OnFormClosed(e);
        }
    }
}
