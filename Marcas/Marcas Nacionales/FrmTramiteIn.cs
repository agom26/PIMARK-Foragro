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
    public partial class FrmTramiteIn : Form
    {
        public FrmTramiteIn()
        {
            InitializeComponent();
        }

        private void roundedButton1_Click(object sender, EventArgs e)
        {
            FrmMostrarTitulares frmMostrarTitulares = new FrmMostrarTitulares();
            frmMostrarTitulares.ShowDialog();
        }

        private void roundedButton2_Click(object sender, EventArgs e)
        {
            FrmMostrarAgentes frmMostrarAgentes= new FrmMostrarAgentes();
            frmMostrarAgentes.ShowDialog();

        }
    }
}
