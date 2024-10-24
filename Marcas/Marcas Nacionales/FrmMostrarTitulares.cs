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
    public partial class FrmMostrarTitulares : Form
    {
        PersonaModel personaModel = new PersonaModel();
        public FrmMostrarTitulares()
        {
            InitializeComponent();
            this.Load += FrmMostrarTitulares_Load; // Mueve la lógica de carga aquí
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MostrarTitulares()
        {
            dtgTitulares.DataSource = personaModel.GetAllTitulares();
            // Ocultar la columna 'id'
            if (dtgTitulares.Columns["id"] != null)
            {
                dtgTitulares.Columns["id"].Visible = false;
            }
        }

        private void LoadTitulares()
        {


            // Obtiene los usuarios
            var titulares = personaModel.GetAllTitulares();

            Invoke(new Action(() =>
            {
                dtgTitulares.DataSource = titulares;

                if (dtgTitulares.Columns["id"] != null)
                {
                    dtgTitulares.Columns["id"].Visible = false;
                }


            }));
        }
        private async void FrmMostrarTitulares_Load(object sender, EventArgs e)
        {
            // Cargar usuarios en segundo plano
            await Task.Run(() => LoadTitulares());
        }
    }
}
