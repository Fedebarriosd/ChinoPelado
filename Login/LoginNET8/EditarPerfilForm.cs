using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace SistemaLogin
{
    /// <summary>
    /// Formulario para editar un perfil de usuario existente.
    /// </summary>
    public partial class EditarPerfilForm : Form
    {
        private readonly string usuarioOriginal;
        private bool isDirty;
        private readonly ToolTip _tooltipContrasena;

        /// <summary>
        /// Nombre de usuario editado.
        /// </summary>
        public string Usuario { get; private set; }

        /// <summary>
        /// Si es null, no se modifica la contraseña; de lo contrario, contiene la nueva contraseña en texto plano.
        /// </summary>
        public string Contrasena { get; private set; }

        /// <summary>
        /// Indica si el usuario editado es administrador.
        /// </summary>
        public bool EsAdministrador { get; private set; }

        /// <summary>
        /// Inicializa el formulario de edición con el nombre del usuario original.
        /// </summary>
        /// <param name="usuario">Nombre original del usuario a editar</param>
        public EditarPerfilForm(string usuario)
        {
            InitializeComponent();
            usuarioOriginal = usuario;

            txtUsuario.TextChanged += InputChanged;
            txtContrasena.TextChanged += InputChanged;
            chkEsAdministrador.CheckedChanged += InputChanged;

            _tooltipContrasena = new ToolTip
            {
                ShowAlways = true,
                InitialDelay = 300,
                ReshowDelay = 100,
                AutoPopDelay = 5000
            };
            _tooltipContrasena.SetToolTip(txtContrasena, "Recomendación: mínimo 6 caracteres");

            isDirty = false;
            btnAceptar.Enabled = false;

            Load += EditarPerfilForm_Load;
        }

        /// <summary>
        /// Carga los datos actuales del perfil en los campos del formulario.
        /// </summary>
        private void EditarPerfilForm_Load(object sender, EventArgs e)
        {
            EjecutarQueryReader(
                "SELECT Usuario, EsAdministrador FROM Usuarios WHERE Usuario = @usuario",
                cmd => cmd.Parameters.AddWithValue("@usuario", usuarioOriginal),
                reader =>
                {
                    if (reader.Read())
                    {
                        txtUsuario.Text = reader.GetString(0);
                        chkEsAdministrador.Checked = reader.GetBoolean(1);
                    }
                });

            txtContrasena.Text = string.Empty;
            isDirty = false;
            btnAceptar.Enabled = false;
        }

        /// <summary>
        /// Habilita el botón Aceptar cuando se detectan cambios.
        /// </summary>
        private void InputChanged(object sender, EventArgs e)
        {
            isDirty = true;
            btnAceptar.Enabled = true;
        }

        /// <summary>
        /// Aplica los cambios realizados al perfil del usuario.
        /// </summary>
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                MessageBox.Show("Por favor, complete el nombre de usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtUsuario.Text != usuarioOriginal && UsuarioExiste(txtUsuario.Text))
            {
                MessageBox.Show("El nombre de usuario ya existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Usuario = txtUsuario.Text;
            Contrasena = string.IsNullOrWhiteSpace(txtContrasena.Text) ? null : txtContrasena.Text;
            EsAdministrador = chkEsAdministrador.Checked;

            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Cancela la edición y cierra el formulario.
        /// </summary>
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Ejecuta una consulta SELECT con parámetros personalizados.
        /// </summary>
        /// <param name="query">Consulta SQL</param>
        /// <param name="configurarParametros">Acción que define los parámetros del comando</param>
        /// <param name="procesarReader">Acción que maneja los resultados del lector</param>
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
        /// Verifica si ya existe un usuario con el nombre especificado, excluyendo al original.
        /// </summary>
        /// <param name="usuario">Nombre del usuario a verificar</param>
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
