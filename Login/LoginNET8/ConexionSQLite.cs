using System.Data.SQLite;

namespace SistemaLogin
{
    public static class ConexionSQLite
    {
        private static string connectionString = "Data Source=miBaseDeDatos.sqlite;Version=3;";

        public static SQLiteConnection ObtenerConexion()
        {
            var conexion = new SQLiteConnection(connectionString);
            conexion.Open();
            return conexion;
        }
    }
}
