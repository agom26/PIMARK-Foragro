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
            dtgAgentes.DataSource = personaModel.GetAllAgentes();
            // Ocultar la columna 'id'
            if (dtgAgentes.Columns["id"] != null)
            {
                dtgAgentes.Columns["id"].Visible = false;
            }
        }

        private void LoadAgentes()
        {


            // Obtiene los agentes
            var titulares = personaModel.GetAllAgentes();

            Invoke(new Action(() =>
            {
                dtgAgentes.DataSource = titulares;

                if (dtgAgentes.Columns["id"] != null)
                {
                    dtgAgentes.Columns["id"].Visible = false;
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Verificar que una fila válida esté seleccionada
            {
                // Obtener el valor del 'id' de la fila seleccionada
                SeleccionarPersona.idPersona = Convert.ToInt32(dtgAgentes.Rows[e.RowIndex].Cells["id"].Value);

                // Usar el valor del 'id' como desees
                //MessageBox.Show("ID del usuario seleccionado: " + EditarPersona.idPersona);
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            string valor="%"+txtBuscar.Text+"%";
            var titulares = personaModel.GetPersonaByValue(valor);

            if (titulares != null )
            {
                dtgAgentes.DataSource = titulares;
                if (dtgAgentes.Columns["id"] != null)
                {
                    dtgAgentes.Columns["id"].Visible = false;
                    dtgAgentes.Columns["tipo"].Visible = false;
                }
            }
            else
            {
                MessageBox.Show("No se encontraron resultados para la búsqueda.");
            }
        }
    }
}
