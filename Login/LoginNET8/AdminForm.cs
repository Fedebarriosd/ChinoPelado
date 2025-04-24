using System;
using System.Data.SQLite;
using System.Windows.Forms;
using SistemaLogin;


namespace SistemaLogin
{
    /// <summary>
    /// Formulario principal del administrador para gestionar perfiles de usuario.
    /// </summary>
    public partial class AdminForm : Form
    {
        private string connectionString = @"Data Source=Usuarios.db;Version=3;";

        /// <summary>
        /// Inicializa el formulario de administrador.
        /// </summary>
        public AdminForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Maneja la acción de agregar un nuevo perfil.
        /// </summary>
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            using (AgregarPerfilForm agregarForm = new AgregarPerfilForm())
            {
                if (agregarForm.ShowDialog() == DialogResult.OK)
                {
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

        /// <summary>
        /// Maneja la acción de buscar y editar un perfil.
        /// </summary>
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

        /// <summary>
        /// Actualiza los datos de un perfil. Si no se proporciona nueva contraseña, se mantiene la actual.
        /// </summary>
        /// <param name="usuarioOriginal">Nombre de usuario actual</param>
        /// <param name="nuevoUsuario">Nuevo nombre de usuario</param>
        /// <param name="nuevaContraseña">Nueva contraseña (null si no se cambia)</param>
        /// <param name="nuevoEsAdministrador">Nuevo estado de administrador</param>
        private void EditarPerfil(string usuarioOriginal,
                                 string nuevoUsuario,
                                 string nuevaContraseña,
                                 bool nuevoEsAdministrador)
        {
            if (nuevaContraseña != null)
            {
                EjecutarQuery(
                    "UPDATE Usuarios SET Usuario = @usuario, Contraseña = @contrasena, EsAdministrador = @esAdministrador WHERE Usuario = @usuarioOriginal",
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
                EjecutarQuery(
                    "UPDATE Usuarios SET Usuario = @usuario, EsAdministrador = @esAdministrador WHERE Usuario = @usuarioOriginal",
                    cmd =>
                    {
                        cmd.Parameters.AddWithValue("@usuario", nuevoUsuario);
                        cmd.Parameters.AddWithValue("@esAdministrador", nuevoEsAdministrador);
                        cmd.Parameters.AddWithValue("@usuarioOriginal", usuarioOriginal);
                    });
            }

            MessageBox.Show("Perfil editado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Maneja la acción de desactivar un perfil existente.
        /// </summary>
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

        /// <summary>
        /// Marca un perfil como inactivo en la base de datos.
        /// </summary>
        /// <param name="usuario">Nombre del usuario a desactivar</param>
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

        /// <summary>
        /// Ejecuta una consulta SQL utilizando parámetros personalizados.
        /// </summary>
        /// <param name="query">Consulta SQL a ejecutar</param>
        /// <param name="configurarParametros">Acción para configurar los parámetros del comando</param>
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

        /// <summary>
        /// Cierra la sesión actual y retorna al formulario de login.
        /// </summary>
        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Retry;
            this.Close();
        }
    }
}
