using System;
using System.Windows.Forms;

namespace SistemaLogin
{
    public partial class AgregarPerfilForm : Form
    {
        public string Usuario { get; private set; }
        public string Contrasena { get; private set; }
        public bool EsAdministrador { get; private set; }

        public AgregarPerfilForm()
        {
            InitializeComponent();
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
