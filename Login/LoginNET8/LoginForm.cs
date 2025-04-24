using Login;
using System;
using System.Data.SQLite;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace SistemaLogin
{
    /// <summary>
    /// Formulario de inicio de sesión principal del sistema.
    /// </summary>
    public partial class LoginForm : Form
    {
        private readonly string connectionString = @"Data Source=Usuarios.db;Version=3;";

        /// <summary>
        /// Inicializa el formulario de inicio de sesión y configura la base de datos.
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();
            this.Icon = new Icon("Logo2.ico");
            CrearBaseDeDatosYTabla();
            ConfigurarPlaceholder(txtUsuario, "Ingrese su usuario...");
            ConfigurarPlaceholder(txtContraseña, "Ingrese su contraseña...", true);
        }

        /// <summary>
        /// Crea la base de datos y la tabla de usuarios si no existen.
        /// </summary>
        private void CrearBaseDeDatosYTabla()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                if (!System.IO.File.Exists("Usuarios.db"))
                {
                    SQLiteConnection.CreateFile("Usuarios.db");
                }
            }

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = @"
                    CREATE TABLE IF NOT EXISTS Usuarios (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Usuario TEXT,
                        Contraseña TEXT,
                        EsAdministrador INTEGER,
                        Activo INTEGER
                    );";

                using (var cmd = new SQLiteCommand(query, connection))
                {
                    cmd.ExecuteNonQuery();
                }

                string verificarAdmin = "SELECT COUNT(*) FROM Usuarios WHERE Usuario = 'admin'";
                using (var cmdVerificar = new SQLiteCommand(verificarAdmin, connection))
                {
                    long existe = (long)cmdVerificar.ExecuteScalar();
                    if (existe == 0)
                    {
                        string hashAdmin = SeguridadHelper.CalcularHash("admin123");
                        string insertarAdmin = "INSERT INTO Usuarios (Usuario, Contraseña, EsAdministrador, Activo) VALUES ('admin', @contrasena, 1, 1)";
                        using (var cmdInsertar = new SQLiteCommand(insertarAdmin, connection))
                        {
                            cmdInsertar.Parameters.AddWithValue("@contrasena", hashAdmin);
                            cmdInsertar.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Verifica si el usuario y la contraseña son válidos.
        /// </summary>
        /// <param name="usuario">Nombre de usuario</param>
        /// <param name="contrasena">Contraseña en texto plano</param>
        /// <returns>True si las credenciales son válidas y el usuario está activo</returns>
        private bool VerificarUsuario(string usuario, string contrasena)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Id, Usuario, Contraseña, EsAdministrador, Activo FROM Usuarios WHERE Usuario = @usuario";

                using (var cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@usuario", usuario);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string contraseñaGuardada = reader["Contraseña"].ToString();
                            if (VerificarContraseña(contrasena, contraseñaGuardada))
                            {
                                bool estaActivo = Convert.ToBoolean(reader["Activo"]);
                                return estaActivo;
                            }
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Compara una contraseña ingresada con su versión hasheada.
        /// </summary>
        /// <param name="contrasenaIngresada">Contraseña en texto plano</param>
        /// <param name="contraseñaGuardada">Hash almacenado</param>
        /// <returns>True si coinciden</returns>
        private bool VerificarContraseña(string contrasenaIngresada, string contraseñaGuardada)
        {
            using (var sha256 = SHA256.Create())
            {
                var passwordBytes = Encoding.UTF8.GetBytes(contrasenaIngresada);
                var hashBytes = sha256.ComputeHash(passwordBytes);
                string hashIngresado = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                return hashIngresado == contraseñaGuardada;
            }
        }

        /// <summary>
        /// Maneja el inicio de sesión al presionar el botón correspondiente.
        /// </summary>
        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text;
            string contrasena = txtContraseña.Text;

            bool usuarioValido = VerificarUsuario(usuario, contrasena);
            if (!usuarioValido)
            {
                lblMensaje.Text = "Usuario o contraseña incorrectos.";
                lblMensaje.Visible = true;
                return;
            }

            bool esAdministrador = EsAdministrador(usuario);
            this.Hide();

            DialogResult dr;
            if (esAdministrador)
            {
                using (var adminForm = new AdminForm())
                    dr = adminForm.ShowDialog();
            }
            else
            {
                using (var userForm = new UserForm())
                    dr = userForm.ShowDialog();
            }

            if (dr == DialogResult.Retry)
            {
                this.Show();
                lblMensaje.Visible = false;
                return;
            }

            this.Close();
        }

        /// <summary>
        /// Determina si un usuario es administrador.
        /// </summary>
        /// <param name="usuario">Nombre de usuario</param>
        /// <returns>True si es administrador</returns>
        private bool EsAdministrador(string usuario)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT EsAdministrador FROM Usuarios WHERE Usuario = @usuario";

                using (var cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@usuario", usuario);

                    object result = cmd.ExecuteScalar();
                    return result != null && Convert.ToBoolean(result);
                }
            }
        }

        /// <summary>
        /// Placeholder para mostrar opciones de usuario normal (no implementado).
        /// </summary>
        private void MostrarOpcionesUsuarioNormal()
        {
            lblMensaje.Text = "Opciones de usuario normal";
            lblMensaje.Visible = true;
        }

        // Métodos auxiliares para gestionar el enfoque de los campos de texto

        private void TxtUsuario_GotFocus(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "Ingrese su usuario...")
            {
                txtUsuario.Text = "";
                txtUsuario.ForeColor = Color.Black;
            }
        }

        private void TxtUsuario_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsuario.Text))
            {
                txtUsuario.Text = "Ingrese su usuario...";
                txtUsuario.ForeColor = Color.Gray;
            }
        }

        private void TxtContraseña_GotFocus(object sender, EventArgs e)
        {
            if (txtContraseña.Text == "Ingrese su contraseña...")
            {
                txtContraseña.Text = "";
                txtContraseña.ForeColor = Color.Black;
                txtContraseña.UseSystemPasswordChar = true;
            }
        }

        private void TxtContraseña_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtContraseña.Text))
            {
                txtContraseña.Text = "Ingrese su contraseña...";
                txtContraseña.ForeColor = Color.Gray;
                txtContraseña.UseSystemPasswordChar = false;
            }
        }

        /// <summary>
        /// Configura un placeholder personalizado para un TextBox.
        /// </summary>
        /// <param name="txtBox">Control TextBox a configurar</param>
        /// <param name="placeholder">Texto del placeholder</param>
        /// <param name="esContraseña">True si se trata de un campo de contraseña</param>
        private void ConfigurarPlaceholder(TextBox txtBox, string placeholder, bool esContraseña = false)
        {
            txtBox.Text = placeholder;
            txtBox.ForeColor = Color.Gray;

            txtBox.Enter += (sender, e) =>
            {
                if (txtBox.Text == placeholder)
                {
                    txtBox.Text = "";
                    txtBox.ForeColor = Color.Black;
                    if (esContraseña)
                        txtBox.UseSystemPasswordChar = true;
                }
            };

            txtBox.Leave += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtBox.Text))
                {
                    txtBox.Text = placeholder;
                    txtBox.ForeColor = Color.Gray;
                    if (esContraseña)
                        txtBox.UseSystemPasswordChar = false;
                }
            };
        }
    }
}
