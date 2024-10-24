using Dominio;
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
    public partial class FrmMostrarAgentes : Form
    {
        PersonaModel personaModel = new PersonaModel();
        public FrmMostrarAgentes()
        {
            InitializeComponent();
            this.Load += FrmMostrarAgentes_Load; // Mueve la lógica de carga aquí
        }

        private void MostrarTitulares()
        {
            dataGridView1.DataSource = personaModel.GetAllAgentes();
            // Ocultar la columna 'id'
            if (dataGridView1.Columns["id"] != null)
            {
                dataGridView1.Columns["id"].Visible = false;
            }
        }

        private void LoadAgentes()
        {


            // Obtiene los agentes
            var titulares = personaModel.GetAllAgentes();

            Invoke(new Action(() =>
            {
                dataGridView1.DataSource = titulares;

                if (dataGridView1.Columns["id"] != null)
                {
                    dataGridView1.Columns["id"].Visible = false;
                }


            }));
        }
        private async void FrmMostrarAgentes_Load(object sender, EventArgs e)
        {
            // Cargar usuarios en segundo plano
            await Task.Run(() => LoadAgentes());
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
