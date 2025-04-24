using SistemaLogin;
using System;
using System.Windows.Forms;

namespace LoginApp
{
    /// <summary>
    /// Clase principal de la aplicación. Contiene el punto de entrada.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal de la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Habilita los estilos visuales para la aplicación (look moderno de Windows).
            Application.EnableVisualStyles();

            // Establece el estilo de renderizado de texto compatible por defecto.
            Application.SetCompatibleTextRenderingDefault(false);

            // Inicia la aplicación y muestra el formulario de inicio de sesión.
            Application.Run(new LoginForm());
        }
    }
}
