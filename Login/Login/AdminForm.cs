using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace SistemaLogin
{
    public partial class AdminForm : Form
    {
        private string connectionString = @"Data Source=Usuarios.db;Version=3;";

        public AdminForm()
        {
            InitializeComponent();
        }

        // Función para agregar un nuevo perfil
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Mostrar un formulario para ingresar los datos
            using (AgregarPerfilForm agregarForm = new AgregarPerfilForm())
            {
                if (agregarForm.ShowDialog() == DialogResult.OK)
                {
                    // Obtenemos los datos ingresados
                    string usuario = agregarForm.Usuario;
                    string contrasena = agregarForm.Contrasena;
                    bool esAdministrador = agregarForm.EsAdministrador;

                    // Guardar el nuevo perfil en la base de datos
                    using (var connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();
                        string query = "INSERT INTO Usuarios (Usuario, Contraseña, EsAdministrador, Activo) VALUES (@usuario, @contrasena, @esAdministrador, 1)";
                        using (var cmd = new SQLiteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@usuario", usuario);
                            cmd.Parameters.AddWithValue("@contrasena", CalcularHash(contrasena)); // Asegúrate de cifrar la contraseña
                            cmd.Parameters.AddWithValue("@esAdministrador", esAdministrador);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Perfil agregado correctamente.");
                }
            }
        }

        // Función para editar un perfil
        private void btnEditar_Click(object sender, EventArgs e)
        {
            // Mostrar un formulario de búsqueda para seleccionar el perfil a editar
            using (BuscarPerfilForm buscarForm = new BuscarPerfilForm())
            {
                if (buscarForm.ShowDialog() == DialogResult.OK)
                {
                    string usuarioSeleccionado = buscarForm.UsuarioSeleccionado;
                    EditarPerfil(usuarioSeleccionado);
                }
            }
        }

        // Función que maneja la edición de un perfil
        private void EditarPerfil(string usuario)
        {
            // Mostrar los datos del perfil en un formulario editable
            using (EditarPerfilForm editarForm = new EditarPerfilForm(usuario))
            {
                if (editarForm.ShowDialog() == DialogResult.OK)
                {
                    string nuevoUsuario = editarForm.Usuario;
                    string nuevaContraseña = editarForm.Contrasena;
                    bool nuevoEsAdministrador = editarForm.EsAdministrador;

                    // Actualizar el perfil en la base de datos
                    using (var connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();
                        string query = "UPDATE Usuarios SET Usuario = @usuario, Contraseña = @contrasena, EsAdministrador = @esAdministrador WHERE Usuario = @usuarioOriginal";
                        using (var cmd = new SQLiteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@usuario", nuevoUsuario);
                            cmd.Parameters.AddWithValue("@contrasena", CalcularHash(nuevaContraseña)); // Cifra la nueva contraseña
                            cmd.Parameters.AddWithValue("@esAdministrador", nuevoEsAdministrador);
                            cmd.Parameters.AddWithValue("@usuarioOriginal", usuario);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Perfil editado correctamente.");
                }
            }
        }

        // Función para desactivar un perfil
        private void btnDesactivar_Click(object sender, EventArgs e)
        {
            // Mostrar un formulario para buscar el perfil a desactivar
            using (BuscarPerfilForm buscarForm = new BuscarPerfilForm())
            {
                if (buscarForm.ShowDialog() == DialogResult.OK)
                {
                    string usuarioSeleccionado = buscarForm.UsuarioSeleccionado;
                    DesactivarPerfil(usuarioSeleccionado);
                }
            }
        }

        // Función para desactivar un perfil
        private void DesactivarPerfil(string usuario)
        {
            // Actualizar el perfil en la base de datos para marcarlo como inactivo
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Usuarios SET Activo = 0 WHERE Usuario = @usuario";
                using (var cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Perfil desactivado correctamente.");
        }

        // Función para calcular el hash de una contraseña
        private string CalcularHash(string input)
        {
            using (System.Security.Cryptography.SHA256 sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (var b in hashBytes)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }
    }
}
