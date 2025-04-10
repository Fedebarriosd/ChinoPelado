using System;
using System.Windows.Forms;

namespace SistemaLogin
{
    public partial class EditarPerfilForm : Form
    {
        private string usuarioOriginal;

        public string Usuario { get; private set; }
        public string Contrasena { get; private set; }
        public bool EsAdministrador { get; private set; }

        public EditarPerfilForm(string usuario)
        {
            InitializeComponent();
            this.usuarioOriginal = usuario;
        }

        private void EditarPerfilForm_Load(object sender, EventArgs e)
        {
            // Cargar los datos del usuario a editar (esto debería ser de la base de datos)
            txtUsuario.Text = usuarioOriginal;
            // En un caso real, deberías cargar la contraseña y si es administrador desde la base de datos
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Validar que todos los campos estén completos
            if (string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrWhiteSpace(txtContrasena.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return;
            }

            // Asignar valores a las propiedades
            Usuario = txtUsuario.Text;
            Contrasena = txtContrasena.Text;
            EsAdministrador = chkEsAdministrador.Checked;

            // Cerrar el formulario
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
