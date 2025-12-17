using System;
using System.Windows.Forms;

namespace Proyecto_IS_Sistema_De_Tickets
{
    public partial class FormSobreProyecto : Form
    {
        public FormSobreProyecto()
        {
            InitializeComponent();
        }

        private void btnVerModulos_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Arquitectura en capas\n" +
                "Seguridad (login/bloqueo/hash)\n" +
                "Bitácora\n" +
                "Control de cambios\n" +
                "Integridad (DVH/DVV)\n" +
                "Idiomas dinámicos (Observer)",
                "Módulos", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCerrarSobre_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
