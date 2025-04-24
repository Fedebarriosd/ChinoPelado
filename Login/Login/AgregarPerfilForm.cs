using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace SistemaLogin
{
    /// <summary>
    /// Formulario para agregar un nuevo perfil de usuario al sistema.
    /// </summary>
    public partial class AgregarPerfilForm : Form
    {
        /// <summary>
        /// Nombre de usuario ingresado.
        /// </summary>
        public string Usuario { get; private set; }

        /// <summary>
        /// Contraseña ingresada por el usuario.
        /// </summary>
        public string Contrasena { get; private set; }

        /// <summary>
        /// Indica si el nuevo perfil es de administrador.
        /// </summary>
        public bool EsAdministrador { get; private set; }

        private readonly ToolTip _tooltipContrasena;

        /// <summary>
        /// Inicializa el formulario y configura el comportamiento de los controles.
        /// </summary>
        public AgregarPerfilForm()
        {
            InitializeComponent();

            AcceptButton = btnAceptar;
            CancelButton = btnCancelar;

            btnAceptar.Enabled = false;
            txtUsuario.TextChanged += ValidarEntradas;
            txtContrasena.TextChanged += ValidarEntradas;

            _tooltipContrasena = new ToolTip
            {
                ShowAlways = true,
                InitialDelay = 300,
                ReshowDelay = 100,
                AutoPopDelay = 5000
            };
            _tooltipContrasena.SetToolTip(txtContrasena, "Recomendación: mínimo 6 caracteres");
        }

        /// <summary>
        /// Valida si los campos están completos para habilitar el botón Aceptar.
        /// </summary>
        private void ValidarEntradas(object sender, EventArgs e)
        {
            btnAceptar.Enabled =
                !string.IsNullOrWhiteSpace(txtUsuario.Text) &&
                !string.IsNullOrWhiteSpace(txtContrasena.Text);
        }

        /// <summary>
        /// Crea el nuevo perfil si los datos ingresados son válidos y no duplicados.
        /// </summary>
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            var usuarioTrim = txtUsuario.Text.Trim();
            var contrasenaTrim = txtContrasena.Text.Trim();

            if (string.IsNullOrWhiteSpace(usuarioTrim) || string.IsNullOrWhiteSpace(contrasenaTrim))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (UsuarioExiste(usuarioTrim))
            {
                MessageBox.Show($"El usuario '{usuarioTrim}' ya existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Usuario = usuarioTrim;
            Contrasena = contrasenaTrim;
            EsAdministrador = chkEsAdministrador.Checked;

            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Cancela el formulario y descarta los cambios.
        /// </summary>
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Ejecuta una consulta de lectura contra la base de datos.
        /// </summary>
        /// <param name="query">Consulta SQL</param>
        /// <param name="configurarParametros">Acción que configura los parámetros</param>
        /// <param name="procesarReader">Acción que procesa los resultados</param>
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
                    procesarReader(reader);
            }
        }

        /// <summary>
        /// Verifica si ya existe un usuario con el nombre especificado.
        /// </summary>
        /// <param name="usuario">Nombre de usuario a verificar</param>
        /// <returns>True si el usuario ya existe</returns>
        private bool UsuarioExiste(string usuario)
        {
            bool existe = false;
            EjecutarQueryReader(
                "SELECT COUNT(1) FROM Usuarios WHERE Usuario = @usuario",
                cmd => cmd.Parameters.AddWithValue("@usuario", usuario),
                reader =>
                {
                    if (reader.Read())
                        existe = reader.GetInt32(0) > 0;
                });
            return existe;
        }
    }
}
