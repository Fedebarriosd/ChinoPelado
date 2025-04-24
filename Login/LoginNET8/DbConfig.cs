using System;

namespace SistemaLogin
{
    /// <summary>
    /// Contiene la configuración central de la base de datos del sistema.
    /// </summary>
    public static class DbConfig
    {
        /// <summary>
        /// Cadena de conexión utilizada para acceder a la base de datos SQLite del sistema.
        /// </summary>
        public const string ConnectionString = @"Data Source=Usuarios.db;Version=3;";
    }
}
