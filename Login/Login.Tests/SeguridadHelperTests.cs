using System;
using Xunit;
using SistemaLogin;

namespace SistemaLogin;

/// <summary>
/// Pruebas unitarias para la clase SeguridadHelper.
/// </summary>
public class SeguridadHelperTests
{
    [Fact]
    public void CalcularHash_DeberiaSerDeterministico()
    {
        // Arrange
        string texto = "clave123";

        // Act
        string hash1 = SeguridadHelper.CalcularHash(texto);
        string hash2 = SeguridadHelper.CalcularHash(texto);

        // Assert
        Assert.Equal(hash1, hash2); // mismo texto, mismo hash
    }

    [Fact]
    public void CalcularHash_DeberiaDiferirParaTextosDistintos()
    {
        // Arrange
        string texto1 = "admin123";
        string texto2 = "usuario456";

        // Act
        string hash1 = SeguridadHelper.CalcularHash(texto1);
        string hash2 = SeguridadHelper.CalcularHash(texto2);

        // Assert
        Assert.NotEqual(hash1, hash2); // diferentes textos, hashes distintos
    }

    [Fact]
    public void CalcularHash_DeberiaDevolverHashHexadecimalValido()
    {
        // Arrange
        string texto = "test123";

        // Act
        string hash = SeguridadHelper.CalcularHash(texto);

        // Assert
        Assert.Matches("^[a-f0-9]{64}$", hash); // hash SHA256 debe tener 64 caracteres hexadecimales
    }
}
