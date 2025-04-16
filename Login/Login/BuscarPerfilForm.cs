using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;

namespace SistemaLogin
{
    public partial class BuscarPerfilForm : Form
    {
        public string UsuarioSeleccionado { get; private set; }

        public BuscarPerfilForm()
        {
            InitializeComponent();
            lstUsuarios.SelectedIndexChanged += lstUsuarios_SelectedIndexChanged;
            btnSeleccionar.Enabled = false;
        }

        private void BuscarPerfilForm_Load(object sender, EventArgs e)
        {
            try
            {
                var usuarios = ObtenerUsuariosActivos();
                lstUsuarios.DataSource = usuarios;
                lstUsuarios.ClearSelected();
                btnSeleccionar.Enabled = usuarios.Count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando perfiles: " + ex.Message);
                btnSeleccionar.Enabled = false;
            }
        }

        // Punto 1 & 2: helper para obtener la lista de usuarios activos y DataBinding
        private List<string> ObtenerUsuariosActivos()
        {
            var lista = new List<string>();
            EjecutarQueryReader(
                "SELECT Usuario FROM Usuarios WHERE Activo = 1",
                cmd => { /* no parameters */ },
                reader =>
                {
                    while (reader.Read())
                        lista.Add(reader.GetString(0));
                });
            return lista;
        }

        // Punto 1: helper genérico para lecturas
        private void EjecutarQueryReader(
            string query,
            Action<SQLiteCommand> configurarParametros,
            Action<SQLiteDataReader> procesarReader)
        {
            using (var conn = new SQLiteConnection(DbConfig.ConnectionString))
            using (var cmd = new SQLiteCommand(query, conn))
            {
                configurarParametros(cmd);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    procesarReader(reader);
                }
            }
        }

        // Punto 3: habilitar/deshabilitar botón según selección
        private void lstUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSeleccionar.Enabled = lstUsuarios.SelectedItem != null;
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
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
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
