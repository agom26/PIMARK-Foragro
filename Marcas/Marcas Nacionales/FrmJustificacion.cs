using Comun.Cache;
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
    public partial class FrmJustificacion : Form
    {
        public string Justificacion { get; private set; }
        public DateTime fecha { get; private set; }
        public string usuarioAbandono { get; private set; }

        public FrmJustificacion()
        {
            InitializeComponent();
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
            Justificacion = richTextBoxJustificacion.Text.Trim();
            fecha=dateTimePicker1.Value;
            if (string.IsNullOrEmpty(Justificacion))
            {
                MessageBox.Show("Por favor ingrese una justificación.", "Justificación requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void FrmJustificacion_Load(object sender, EventArgs e)
        {
            labelUsuarioAbandono.Text=UsuarioActivo.usuario;
            usuarioAbandono = UsuarioActivo.usuario;
        }
    }
}
