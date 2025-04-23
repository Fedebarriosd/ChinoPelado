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
            using (AgregarPerfilForm agregarForm = new AgregarPerfilForm())
            {
                if (agregarForm.ShowDialog() == DialogResult.OK)
                {
                    // Obtenemos los datos ingresados
                    string usuario = agregarForm.Usuario;
                    string contrasena = agregarForm.Contrasena;
                    bool esAdministrador = agregarForm.EsAdministrador;

                    try
                    {
                        EjecutarQuery("INSERT INTO Usuarios (Usuario, Contraseña, EsAdministrador, Activo) VALUES (@usuario, @contrasena, @esAdministrador, 1)",
                            cmd =>
                            {
                                cmd.Parameters.AddWithValue("@usuario", usuario);
                                cmd.Parameters.AddWithValue("@contrasena", SeguridadHelper.CalcularHash(contrasena));
                                cmd.Parameters.AddWithValue("@esAdministrador", esAdministrador);
                            });
                        MessageBox.Show("Perfil agregado correctamente.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al agregar el perfil: " + ex.Message);
                    }
                }
            }
        }

        // Función para editar un perfil
        private void btnEditar_Click(object sender, EventArgs e)
        {
            using (BuscarPerfilForm buscarForm = new BuscarPerfilForm())
            {
                if (buscarForm.ShowDialog() == DialogResult.OK)
                {
                    string usuarioOriginal = buscarForm.UsuarioSeleccionado;
                    using (EditarPerfilForm editarForm = new EditarPerfilForm(usuarioOriginal))
                    {
                        if (editarForm.ShowDialog() == DialogResult.OK)
                        {
                            // Pasamos usuario original, nuevo nombre, nuevaPass (puede ser null) y rol
                            EditarPerfil(
                                usuarioOriginal,
                                editarForm.Usuario,
                                editarForm.Contrasena,
                                editarForm.EsAdministrador
                            );
                        }
                    }
                }
            }
        }

        // Función que maneja la edición de un perfil
        /// <summary>
        /// Actualiza un perfil. Si nuevaContraseña es null, no toca la columna Contraseña.
        /// </summary>
        private void EditarPerfil(string usuarioOriginal,
                                 string nuevoUsuario,
                                 string nuevaContraseña,
                                 bool nuevoEsAdministrador)
        {
            // Si se proporcionó nueva contraseña, actualizamos también ese campo
            if (nuevaContraseña != null)
            {
                EjecutarQuery(
                    "UPDATE Usuarios " +
                    "SET Usuario = @usuario, Contraseña = @contrasena, EsAdministrador = @esAdministrador " +
                    "WHERE Usuario = @usuarioOriginal",
                    cmd =>
                    {
                        cmd.Parameters.AddWithValue("@usuario", nuevoUsuario);
                        cmd.Parameters.AddWithValue("@contrasena", SeguridadHelper.CalcularHash(nuevaContraseña));
                        cmd.Parameters.AddWithValue("@esAdministrador", nuevoEsAdministrador);
                        cmd.Parameters.AddWithValue("@usuarioOriginal", usuarioOriginal);
                    });
            }
            else
            {
                // Sin contraseña nueva: sólo nombre y rol
                EjecutarQuery(
                    "UPDATE Usuarios " +
                    "SET Usuario = @usuario, EsAdministrador = @esAdministrador " +
                    "WHERE Usuario = @usuarioOriginal",
                    cmd =>
                    {
                        cmd.Parameters.AddWithValue("@usuario", nuevoUsuario);
                        cmd.Parameters.AddWithValue("@esAdministrador", nuevoEsAdministrador);
                        cmd.Parameters.AddWithValue("@usuarioOriginal", usuarioOriginal);
                    });
            }

            // Mensaje único de confirmación
            MessageBox.Show("Perfil editado correctamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Función para desactivar un perfil
        private void btnDesactivar_Click(object sender, EventArgs e)
        {
            using (BuscarPerfilForm buscarForm = new BuscarPerfilForm())
            {
                if (buscarForm.ShowDialog() == DialogResult.OK)
                {
                    string usuarioSeleccionado = buscarForm.UsuarioSeleccionado;
                    DesactivarPerfil(usuarioSeleccionado);
                }
            }
        }

        // Función que actualiza el perfil en la base de datos para marcarlo como inactivo
        private void DesactivarPerfil(string usuario)
        {
            try
            {
                EjecutarQuery("UPDATE Usuarios SET Activo = 0 WHERE Usuario = @usuario",
                    cmd =>
                    {
                        cmd.Parameters.AddWithValue("@usuario", usuario);
                    });
                MessageBox.Show("Perfil desactivado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al desactivar el perfil: " + ex.Message);
            }
        }

        // Método auxiliar para ejecutar consultas SQL
        private void EjecutarQuery(string query, Action<SQLiteCommand> configurarParametros)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var cmd = new SQLiteCommand(query, connection))
                {
                    configurarParametros(cmd);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            // Indicamos al llamador que solicitamos un "logout"
            this.DialogResult = DialogResult.Retry;
            this.Close();
        }

    }

    // Clase auxiliar para manejar operaciones de seguridad (calcular hash de contraseñas)
    public static class SeguridadHelper
    {
        public static string CalcularHash(string input)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
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
