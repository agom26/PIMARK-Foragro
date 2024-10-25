using Comun.Cache;
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtgTitulares_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Verificar que una fila válida esté seleccionada
            {
                // Obtener el valor del 'id' de la fila seleccionada
                SeleccionarPersona.idPersona = Convert.ToInt32(dtgTitulares.Rows[e.RowIndex].Cells["id"].Value);

                // Usar el valor del 'id' como desees
                //MessageBox.Show("ID del usuario seleccionado: " + EditarPersona.idPersona);
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
