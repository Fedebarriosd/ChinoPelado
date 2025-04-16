using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace SistemaLogin
{
    public partial class AgregarPerfilForm : Form
    {
        public string Usuario { get; private set; }
        public string Contrasena { get; private set; }
        public bool EsAdministrador { get; private set; }

        private readonly ToolTip _tooltipContrasena;

        public AgregarPerfilForm()
        {
            InitializeComponent();

            // Asignar botones de teclado
            AcceptButton = btnAceptar;
            CancelButton = btnCancelar;

            // Inicialmente deshabilitar "Aceptar"
            btnAceptar.Enabled = false;
            txtUsuario.TextChanged += ValidarEntradas;
            txtContrasena.TextChanged += ValidarEntradas;

            // Instanciar y configurar Tooltip en el campo de contraseña
            _tooltipContrasena = new ToolTip
            {
                ShowAlways = true,
                InitialDelay = 300,
                ReshowDelay = 100,
                AutoPopDelay = 5000
            };
            _tooltipContrasena.SetToolTip(txtContrasena, "Recomendación: mínimo 6 caracteres");
        }

        private void ValidarEntradas(object sender, EventArgs e)
        {
            btnAceptar.Enabled =
                !string.IsNullOrWhiteSpace(txtUsuario.Text) &&
                !string.IsNullOrWhiteSpace(txtContrasena.Text);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Trim de espacios
            var usuarioTrim = txtUsuario.Text.Trim();
            var contrasenaTrim = txtContrasena.Text.Trim();

            // Validar que los campos estén completos
            if (string.IsNullOrWhiteSpace(usuarioTrim) || string.IsNullOrWhiteSpace(contrasenaTrim))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar unicidad de usuario
            if (UsuarioExiste(usuarioTrim))
            {
                MessageBox.Show($"El usuario '{usuarioTrim}' ya existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Asignar propiedades de salida
            Usuario = usuarioTrim;
            Contrasena = contrasenaTrim;
            EsAdministrador = chkEsAdministrador.Checked;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        // Helper genérico para lecturas desde la base de datos
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

        // Comprueba si un usuario ya existe en la base de datos
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
