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

namespace Presentacion.Vencimientos
{
    public partial class FrmVencimientos : Form
    {
        VencimientoModel vencimientoModel = new VencimientoModel();
        public FrmVencimientos()
        {
            InitializeComponent();
            this.Load += FrmVencimientos_Load; // Mueve la lógica de carga aquí
        }

        private void MostrarTitulares()
        {
            dtgVencimientos.DataSource = vencimientoModel.GetAllVencimientos();
            // Ocultar la columna 'id'
            if (dtgVencimientos.Columns["id"] != null)
            {
                dtgVencimientos.Columns["id"].Visible = false;
                // Desactiva la selección automática de la primera fila
                dtgVencimientos.ClearSelection();
            }
        }

        private void LoadVencimientos()
        {


            // Obtiene los usuarios
            var titulares = vencimientoModel.GetAllVencimientos();

            Invoke(new Action(() =>
            {
                dtgVencimientos.DataSource = titulares;

                if (dtgVencimientos.Columns["id"] != null)
                {
                    dtgVencimientos.Columns["id"].Visible = false;
                }


            }));
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            string valor = "%" + txtBuscar.Text + "%";
            var vencimientos = vencimientoModel.GetVencimientoByValue(valor);

            if (vencimientos != null)
            {
                dtgVencimientos.DataSource = vencimientos;
                if (dtgVencimientos.Columns["id"] != null)
                {
                    dtgVencimientos.Columns["id"].Visible = false;
                }
            }
            else
            {
                MessageBox.Show("No se encontraron resultados para la búsqueda.");
            }
        }

        private async void FrmVencimientos_Load(object sender, EventArgs e)
        {
            // Cargar usuarios en segundo plano
            await Task.Run(() => LoadVencimientos());
        }
    }
}
