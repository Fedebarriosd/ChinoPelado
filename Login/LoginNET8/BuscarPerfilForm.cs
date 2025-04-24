using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;

namespace SistemaLogin
{
    /// <summary>
    /// Formulario para buscar y seleccionar un perfil de usuario activo.
    /// </summary>
    public partial class BuscarPerfilForm : Form
    {
        /// <summary>
        /// Nombre del usuario seleccionado en el formulario.
        /// </summary>
        public string UsuarioSeleccionado { get; private set; }

        /// <summary>
        /// Inicializa el formulario de búsqueda de perfiles.
        /// </summary>
        public BuscarPerfilForm()
        {
            InitializeComponent();
            lstUsuarios.SelectedIndexChanged += lstUsuarios_SelectedIndexChanged;
            btnSeleccionar.Enabled = false;
        }

        /// <summary>
        /// Carga los perfiles activos en el ListBox al iniciar el formulario.
        /// </summary>
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

        /// <summary>
        /// Devuelve una lista de nombres de usuarios activos.
        /// </summary>
        /// <returns>Lista de usuarios activos</returns>
        private List<string> ObtenerUsuariosActivos()
        {
            var lista = new List<string>();
            EjecutarQueryReader(
                "SELECT Usuario FROM Usuarios WHERE Activo = 1",
                cmd => { },
                reader =>
                {
                    while (reader.Read())
                        lista.Add(reader.GetString(0));
                });
            return lista;
        }

        /// <summary>
        /// Ejecuta una consulta de lectura a la base de datos.
        /// </summary>
        /// <param name="query">Consulta SQL a ejecutar</param>
        /// <param name="configurarParametros">Acción para configurar los parámetros del comando</param>
        /// <param name="procesarReader">Acción para procesar los resultados del lector</param>
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

        /// <summary>
        /// Habilita el botón seleccionar si hay un usuario elegido.
        /// </summary>
        private void lstUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSeleccionar.Enabled = lstUsuarios.SelectedItem != null;
        }

        /// <summary>
        /// Confirma la selección del usuario y cierra el formulario.
        /// </summary>
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

        /// <summary>
        /// Cancela la selección y cierra el formulario.
        /// </summary>
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
