using SistemaLogin;
using Xunit;

namespace LoginNET8.Tests
{
    /// <summary>
    /// Pruebas unitarias para la clase SeguridadHelper.
    /// </summary>
    public class SeguridadHelperTests
    {
        [Fact]
        public void CalcularHash_DeberiaDevolverHashHexadecimalValido()
        {
            // Arrange
            string entrada = "prueba123";

            // Act
            string hash = SeguridadHelper.CalcularHash(entrada);

            // Assert
            Assert.False(string.IsNullOrWhiteSpace(hash)); // No debe ser nulo o vacío
            Assert.Equal(64, hash.Length); // SHA-256 produce 64 caracteres hexadecimales
            Assert.Matches("^[0-9a-f]+$", hash); // Solo caracteres hexadecimales (0-9, a-f)
        }
    }
}
