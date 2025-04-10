using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Login
{
    public partial class Form1 : Form
    {
        // Colores para los placeholders
        Color placeholderColor = Color.Gray;
        Color textColor = Color.Black;

        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }

        public Form1()
        {
            InitializeComponent();

            // Asignar eventos a los TextBox
            TextBoxUsuario.Enter += new EventHandler(TextBoxUsuario_Enter);
            TextBoxUsuario.Leave += new EventHandler(TextBoxUsuario_Leave);

            TextBoxContraseña.Enter += new EventHandler(TextBoxContraseña_Enter);
            TextBoxContraseña.Leave += new EventHandler(TextBoxContraseña_Leave);

            // Inicializar placeholders
            SetPlaceholder(TextBoxUsuario, "Ingrese su usuario");
            SetPlaceholder(TextBoxContraseña, "Ingrese su contraseña");
            TextBoxContraseña.UseSystemPasswordChar = false;
        }

        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            // Evitar usar el texto placeholder como usuario o contraseña
            if (TextBoxUsuario.Text == "Ingrese su usuario" || TextBoxContraseña.Text == "Ingrese su contraseña")
            {
                MessageBox.Show("Por favor, ingrese su usuario y contraseña.");
                return;
            }

            string connectionString = "Data Source=\"C:\\Users\\Federico\\Documents\\Americana\\6to_Semestre\\Taller de Ing. de Software\\ChinoPelado\\Login\\usuarios.db\";Version=3;";
            string usuario = TextBoxUsuario.Text;
            string contraseña = HashPassword(TextBoxContraseña.Text);

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM usuarios WHERE usuario = @usuario AND contraseña = @contraseña";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@usuario", usuario);
                        cmd.Parameters.AddWithValue("@contraseña", contraseña);

                        int result = Convert.ToInt32(cmd.ExecuteScalar());

                        if (result > 0)
                        {
                            MessageBox.Show("¡Login exitoso!");
                        }
                        else
                        {
                            MessageBox.Show("Usuario o contraseña incorrectos.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        // Métodos para los placeholders
        private void SetPlaceholder(TextBox textBox, string placeholder)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.ForeColor = placeholderColor;
                textBox.Text = placeholder;
            }
        }

        private void RemovePlaceholder(TextBox textBox, string placeholder)
        {
            if (textBox.Text == placeholder)
            {
                textBox.Text = "";
                textBox.ForeColor = textColor;
            }
        }

        // Eventos del TextBoxUsuario
        private void TextBoxUsuario_Enter(object sender, EventArgs e)
        {
            RemovePlaceholder(TextBoxUsuario, "Ingrese su usuario");
        }

        private void TextBoxUsuario_Leave(object sender, EventArgs e)
        {
            SetPlaceholder(TextBoxUsuario, "Ingrese su usuario");
        }

        // Eventos del TextBoxContraseña
        private void TextBoxContraseña_Enter(object sender, EventArgs e)
        {
            RemovePlaceholder(TextBoxContraseña, "Ingrese su contraseña");
            TextBoxContraseña.UseSystemPasswordChar = true;
        }

        private void TextBoxContraseña_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBoxContraseña.Text))
            {
                TextBoxContraseña.UseSystemPasswordChar = false;
                SetPlaceholder(TextBoxContraseña, "Ingrese su contraseña");
            }
        }

        // Si no usás estos eventos, podés borrarlos
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e) { }
    }
}
