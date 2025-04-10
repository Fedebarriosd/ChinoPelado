using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace SistemaLogin
{
    public partial class BuscarPerfilForm : Form
    {
        public string UsuarioSeleccionado { get; private set; }
        private string connectionString = @"Data Source=Usuarios.db;Version=3;";

        public BuscarPerfilForm()
        {
            InitializeComponent();
        }

        private void BuscarPerfilForm_Load(object sender, EventArgs e)
        {
            // Cargar los usuarios disponibles en el ListBox
            lstUsuarios.Items.Clear(); // Limpiar el listado al cargar
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Usuario FROM Usuarios WHERE Activo = 1";  // Solo usuarios activos
                using (var cmd = new SQLiteCommand(query, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lstUsuarios.Items.Add(reader["Usuario"].ToString());
                        }
                    }
                }
            }
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            // Obtener el usuario seleccionado
            if (lstUsuarios.SelectedItem != null)
            {
                UsuarioSeleccionado = lstUsuarios.SelectedItem.ToString();
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un usuario.");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
