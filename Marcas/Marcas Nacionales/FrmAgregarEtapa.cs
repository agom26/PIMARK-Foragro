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
    public partial class FrmAgregarEtapa : Form
    {
        public FrmAgregarEtapa()
        {
            InitializeComponent();
        }

        private void FrmAgregarEtapa_Load(object sender, EventArgs e)
        {
            lblUser.Text = UsuarioActivo.usuario;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AgregarEtapa.etapa = "";
            this.Close();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            AgregarEtapa.etapa = "";
            this.Close();
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex != -1)
            {
                AgregarEtapa.etapa=comboBox1.SelectedItem.ToString();
                AgregarEtapa.fecha=dateTimePicker1.ToString();
                AgregarEtapa.anotaciones = richTextBox1.Text;
                this.Close();
            }
            else
            {
                MessageBox.Show("No ha seleccionado ningun estatus");
            }
        }
    }
}
