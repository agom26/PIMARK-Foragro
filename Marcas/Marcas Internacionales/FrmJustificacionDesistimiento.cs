using Comun.Cache;
using Presentacion.Alertas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Marcas_Nacionales
{
    public partial class FrmJustificacionDesistimiento : Form
    {
        public string Justificacion { get; private set; }
        public DateTime fecha { get; private set; }
        public string usuarioAbandono { get; private set; }

        public FrmJustificacionDesistimiento()
        {
            InitializeComponent();
            labelUsuarioAbandono.Visible = false;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void roundedButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void roundedButton1_Click(object sender, EventArgs e)
        {
            fecha = dateTimePicker1.Value;
            Justificacion = richTextBoxJustificacion.Text.Trim();

            if (string.IsNullOrEmpty(Justificacion))
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR UNA JUSTIFICACIÓN", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                return;
            }

            // Agrega la fecha con texto si aún no está en la justificación
            string fechaSinHora = fecha.ToString("dd/MM/yyyy");
            string formato = fechaSinHora + " Desistimiento";
            if (!Justificacion.Contains(formato))
            {
                Justificacion = formato + " " + Justificacion;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void FrmJustificacionDesistimiento_Load(object sender, EventArgs e)
        {
            labelUsuarioAbandono.Text = UsuarioActivo.usuario;
            labelUsuarioAbandono.Visible = false;
            usuarioAbandono = UsuarioActivo.usuario;

            richTextBoxJustificacion.Text = dateTimePicker1.Value.ToString("dd/MM/yyyy") + " " + "Desistimiento";
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            richTextBoxJustificacion.Text = dateTimePicker1.Value.ToString("dd/MM/yyyy") + " " + "Desistimiento";
        }
    }
}
