using System.Security.Cryptography;
using System.Text;

namespace SistemaLogin
{
    /// <summary>
    /// Clase auxiliar para operaciones de seguridad como el cálculo de hashes.
    /// </summary>
    public static class SeguridadHelper
    {
        /// <summary>
        /// Calcula el hash SHA256 de una cadena de texto.
        /// </summary>
        /// <param name="input">Texto en plano</param>
        /// <returns>Hash en formato hexadecimal</returns>
        public static string CalcularHash(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                foreach (var b in hashBytes)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }
    }
}
