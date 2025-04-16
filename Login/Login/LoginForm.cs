using Login;
using System;
using System.Data.SQLite;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace SistemaLogin
{
    public partial class LoginForm : Form
    {
        string connectionString = @"Data Source=Usuarios.db;Version=3;";

        public LoginForm()
        {
            InitializeComponent();
            this.Icon = new Icon("Logo2.ico");
            CrearBaseDeDatosYTabla();
            ConfigurarPlaceholder(txtUsuario, "Ingrese su usuario...");
            ConfigurarPlaceholder(txtContraseña, "Ingrese su contraseña...", true);
        }

        private void CrearBaseDeDatosYTabla()
        {
            // Verificamos si la base de datos existe
            using (var connection = new SQLiteConnection(connectionString))
            {
                if (!System.IO.File.Exists("Usuarios.db"))
                {
                    SQLiteConnection.CreateFile("Usuarios.db");
                }
            }

            // Ahora creamos la tabla Usuarios si no existe
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Crear la tabla si no existe
                string query = @"
                    CREATE TABLE IF NOT EXISTS Usuarios (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Usuario TEXT,
                        Contraseña TEXT,
                        EsAdministrador INTEGER,
                        Activo INTEGER
                    );
                ";

                using (var cmd = new SQLiteCommand(query, connection))
                {
                    cmd.ExecuteNonQuery();
                }

                // Verificar si el usuario admin existe y si no, lo creamos.
                string verificarAdmin = "SELECT COUNT(*) FROM Usuarios WHERE Usuario = 'admin'";
                using (var cmdVerificar = new SQLiteCommand(verificarAdmin, connection))
                {
                    long existe = (long)cmdVerificar.ExecuteScalar();
                    if (existe == 0)
                    {
                        // Crea un hash para la contraseña "admin123"
                        string hashAdmin = CalcularHash("admin123"); // Contraseña: admin123
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

        private string CalcularHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                foreach (var b in hashBytes)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

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
                            // Obtener la contraseña cifrada guardada en la base de datos
                            string contraseñaGuardada = reader["Contraseña"].ToString();
                            // Verificar si la contraseña ingresada coincide con la guardada
                            if (VerificarContraseña(contrasena, contraseñaGuardada))
                            {
                                bool estaActivo = Convert.ToBoolean(reader["Activo"]);
                                return estaActivo; // Retorna true si el usuario está activo
                            }
                        }
                    }
                }
            }
            return false; // Retorna false si no se encuentra el usuario o la contraseña es incorrecta
        }

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

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text;
            string contrasena = txtContraseña.Text;

            bool usuarioValido = VerificarUsuario(usuario, contrasena);
            if (usuarioValido)
            {
                bool esAdministrador = EsAdministrador(usuario);
                this.Hide();

                if (esAdministrador)
                {
                    using (AdminForm adminForm = new AdminForm())
                    {
                        adminForm.ShowDialog();
                    }
                }
                else
                {
                    using (UserForm userForm = new UserForm())
                    {
                        userForm.ShowDialog();
                    }
                }

                this.Close();
            }
            else
            {
                lblMensaje.Text = "Usuario o contraseña incorrectos.";
                lblMensaje.Visible = true;
            }
        }

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
                    return result != null && Convert.ToBoolean(result); // Devuelve true si el usuario es administrador
                }
            }
        }

        private void MostrarOpcionesUsuarioNormal()
        {
            // Mostrar opciones para usuario normal
            lblMensaje.Text = "Opciones de usuario normal";
            lblMensaje.Visible = true;
            // Aquí puedes agregar más opciones específicas para usuarios normales
            // Por ejemplo, abrir un formulario diferente o mostrar diferentes botones
        }

        private void TxtUsuario_GotFocus(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "Ingrese su usuario...")
            {
                txtUsuario.Text = "";
                txtUsuario.ForeColor = System.Drawing.Color.Black;
            }
        }

        
        private void TxtUsuario_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsuario.Text))
            {
                txtUsuario.Text = "Ingrese su usuario...";
                txtUsuario.ForeColor = System.Drawing.Color.Gray;
            }
        }

        
        private void TxtContraseña_GotFocus(object sender, EventArgs e)
        {
            if (txtContraseña.Text == "Ingrese su contraseña...")
            {
                txtContraseña.Text = "";
                txtContraseña.ForeColor = System.Drawing.Color.Black;
                txtContraseña.UseSystemPasswordChar = true; // Muestra asteriscos en el campo de contraseña
            }
        }

        
        private void TxtContraseña_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtContraseña.Text))
            {
                txtContraseña.Text = "Ingrese su contraseña...";
                txtContraseña.ForeColor = System.Drawing.Color.Gray;
                txtContraseña.UseSystemPasswordChar = false; // Desactiva los asteriscos
            }
        }

        private void ConfigurarPlaceholder(TextBox txtBox, string placeholder, bool esContraseña = false)
        {
            // Asigna el placeholder inicial
            txtBox.Text = placeholder;
            txtBox.ForeColor = System.Drawing.Color.Gray;

            // Configurar evento Enter
            txtBox.Enter += (sender, e) =>
            {
                if (txtBox.Text == placeholder)
                {
                    txtBox.Text = "";
                    txtBox.ForeColor = System.Drawing.Color.Black;
                    if (esContraseña)
                        txtBox.UseSystemPasswordChar = true;
                }
            };

            // Configurar evento Leave
            txtBox.Leave += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtBox.Text))
                {
                    txtBox.Text = placeholder;
                    txtBox.ForeColor = System.Drawing.Color.Gray;
                    if (esContraseña)
                        txtBox.UseSystemPasswordChar = false;
                }
            };
        }


    }
}
