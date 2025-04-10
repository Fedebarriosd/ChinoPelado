using System;
using System.Data.SQLite;
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
            CrearBaseDeDatosYTabla();
            ConfigurePlaceholder(txtUsuario, "Ingrese su usuario...");
            ConfigurePlaceholder(txtContraseña, "Ingrese su contraseña...");
            InicializarPlaceholders();
        }

        private void CrearBaseDeDatosYTabla()
        {
            // Verificamos si la base de datos existe
            using (var connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                }
                catch (SQLiteException)
                {
                    // Si ocurre un error, significa que la base de datos no existe, así que la creamos.
                    SQLiteConnection.CreateFile("Usuarios.db"); // Esto crea la base de datos vacía
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
            using (var sha256 = new SHA256Managed())
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

            // Verificar si el usuario y la contraseña son correctos
            bool usuarioValido = VerificarUsuario(usuario, contrasena);

            if (usuarioValido)
            {
                // Ocultar los controles de inicio de sesión
                txtUsuario.Visible = false;
                txtContraseña.Visible = false;
                btnIniciarSesion.Visible = false;
                lblUsuario.Visible = false;
                lblContraseña.Visible = false;

                // Si el usuario es válido, mostrar un mensaje de bienvenida
                lblMensaje.Text = "Bienvenido";
                lblMensaje.Visible = true;

                // Verificar si es un administrador
                bool esAdministrador = EsAdministrador(usuario);

                if (esAdministrador)
                {
                    // Si es administrador, abrir el formulario de administrador
                    AdminForm adminForm = new AdminForm();
                    adminForm.Show();
                    this.Hide(); // Oculta el formulario de inicio de sesión
                }
                else
                {
                    MostrarOpcionesUsuarioNormal();
                }
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

        // Configurar placeholder en el TextBox
        private void ConfigurePlaceholder(TextBox txtBox, string placeholder)
        {
            // Establecer el texto por defecto del placeholder
            txtBox.ForeColor = System.Drawing.Color.Gray;
            txtBox.Text = placeholder;

            // Cuando el usuario hace clic (Enter) en el TextBox, se borra el placeholder
            txtBox.Enter += (sender, e) =>
            {
                if (txtBox.Text == placeholder)
                {
                    txtBox.Text = "";
                    txtBox.ForeColor = System.Drawing.Color.Black;
                }
            };

            // Cuando el usuario sale (Leave) del TextBox, si está vacío, se muestra el placeholder
            txtBox.Leave += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtBox.Text))
                {
                    txtBox.Text = placeholder;
                    txtBox.ForeColor = System.Drawing.Color.Gray;
                }
            };
        }

        // Función para inicializar los placeholders
        private void InicializarPlaceholders()
        {
            // Para el campo de usuario
            txtUsuario.Text = "Ingrese su usuario...";
            txtUsuario.ForeColor = System.Drawing.Color.Gray;
            txtUsuario.GotFocus += TxtUsuario_GotFocus;
            txtUsuario.LostFocus += TxtUsuario_LostFocus;

            // Para el campo de contraseña
            txtContraseña.Text = "Ingrese su contraseña...";
            txtContraseña.ForeColor = System.Drawing.Color.Gray;
            txtContraseña.GotFocus += TxtContraseña_GotFocus;
            txtContraseña.LostFocus += TxtContraseña_LostFocus;
        }

        // Cuando el campo de usuario recibe foco (se hace clic o se selecciona)
        private void TxtUsuario_GotFocus(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "Ingrese su usuario...")
            {
                txtUsuario.Text = "";
                txtUsuario.ForeColor = System.Drawing.Color.Black;
            }
        }

        // Cuando el campo de usuario pierde el foco (cuando el usuario deja el campo vacío)
        private void TxtUsuario_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsuario.Text))
            {
                txtUsuario.Text = "Ingrese su usuario...";
                txtUsuario.ForeColor = System.Drawing.Color.Gray;
            }
        }

        // Cuando el campo de contraseña recibe foco
        private void TxtContraseña_GotFocus(object sender, EventArgs e)
        {
            if (txtContraseña.Text == "Ingrese su contraseña...")
            {
                txtContraseña.Text = "";
                txtContraseña.ForeColor = System.Drawing.Color.Black;
                txtContraseña.UseSystemPasswordChar = true; // Muestra asteriscos en el campo de contraseña
            }
        }

        // Cuando el campo de contraseña pierde el foco
        private void TxtContraseña_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtContraseña.Text))
            {
                txtContraseña.Text = "Ingrese su contraseña...";
                txtContraseña.ForeColor = System.Drawing.Color.Gray;
                txtContraseña.UseSystemPasswordChar = false; // Desactiva los asteriscos
            }
        }

    }
}
