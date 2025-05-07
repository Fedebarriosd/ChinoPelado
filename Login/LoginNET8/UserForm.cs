using System;
using System.Windows.Forms;

namespace SistemaLogin
{
    public partial class UserForm : Form
    {
        public UserForm()
        {
            InitializeComponent(); // ← ESTO es lo que crea el diseño que pusimos
        }

        private void btnCargarStock_Click(object sender, EventArgs e)
        {
            RegistroStockForm registroStockForm = new RegistroStockForm();
            registroStockForm.ShowDialog();
        }

        private void btnConsultarStock_Click(object sender, EventArgs e)
        {
            ConsultaStockForm consultaStockForm = new ConsultaStockForm();
            consultaStockForm.ShowDialog();
        }
    }
}
