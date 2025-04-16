using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace SistemaLogin
{
    public partial class EditarPerfilForm : Form
    {
        private readonly string usuarioOriginal;
        private bool isDirty;
        private readonly ToolTip _tooltipContrasena;

        // Propiedades de salida
        public string Usuario { get; private set; }
        /// <summary>
        /// Si es null, no cambia la contraseña; de lo contrario, contiene la nueva contraseña en texto plano.
        /// </summary>
        public string Contrasena { get; private set; }
        public bool EsAdministrador { get; private set; }

        public EditarPerfilForm(string usuario)
        {
            InitializeComponent();
            usuarioOriginal = usuario;

            // Suscribir eventos para habilitar "Aceptar" sólo si hay cambios
            txtUsuario.TextChanged += InputChanged;
            txtContrasena.TextChanged += InputChanged;
            chkEsAdministrador.CheckedChanged += InputChanged;

            // Instanciar y configurar Tooltip en el campo de contraseña
            _tooltipContrasena = new ToolTip
            {
                ShowAlways = true,
                InitialDelay = 300,
                ReshowDelay = 100,
                AutoPopDelay = 5000
            };
            _tooltipContrasena.SetToolTip(txtContrasena, "Recomendación: mínimo 6 caracteres");

            // Al inicio no hay cambios
            isDirty = false;
            btnAceptar.Enabled = false;

            // Load
            Load += EditarPerfilForm_Load;
        }

        private void EditarPerfilForm_Load(object sender, EventArgs e)
        {
            // Carga los datos reales desde la base de datos
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

            // Deja la contraseña vacía para indicar "sin cambio"
            txtContrasena.Text = string.Empty;

            // Reset estado
            isDirty = false;
            btnAceptar.Enabled = false;
        }

        private void InputChanged(object sender, EventArgs e)
        {
            isDirty = true;
            btnAceptar.Enabled = true;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Validar usuario no vacío
            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                MessageBox.Show("Por favor, complete el nombre de usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar nombre único si cambió
            if (txtUsuario.Text != usuarioOriginal && UsuarioExiste(txtUsuario.Text))
            {
                MessageBox.Show("El nombre de usuario ya existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Asignar propiedades de salida
            Usuario = txtUsuario.Text;
            Contrasena = string.IsNullOrWhiteSpace(txtContrasena.Text)
                ? null
                : txtContrasena.Text;
            EsAdministrador = chkEsAdministrador.Checked;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        // Helper genérico para lecturas
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

        // Comprueba si ya existe un usuario (excluyendo el original)
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
