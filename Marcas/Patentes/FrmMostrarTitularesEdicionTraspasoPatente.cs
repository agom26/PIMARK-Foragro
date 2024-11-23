using Comun.Cache;
using Dominio;
using Presentacion.Alertas;
using System.Data;

namespace Presentacion.Patentes
{
    public partial class FrmMostrarTitularesEdicionTraspasoPatente : Form
    {
        PersonaModel personaModel = new PersonaModel();
        public FrmMostrarTitularesEdicionTraspasoPatente()
        {
            InitializeComponent();
            this.Load += FrmMostrarTitularesEdicionTraspasoPatente_Load; // Mueve la lógica de carga aquí
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            SeleccionarPersonaPatente.idPersonaT = 0;
            this.Close();
        }

        private void MostrarTitulares()
        {
            dtgTitulares.DataSource = personaModel.GetAllTitulares();
            // Ocultar la columna 'id'
            if (dtgTitulares.Columns["id"] != null)
            {
                dtgTitulares.Columns["id"].Visible = false;
                // Desactiva la selección automática de la primera fila
                dtgTitulares.ClearSelection();
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
        private async void FrmMostrarTitularesEdicionTraspasoPatente_Load(object sender, EventArgs e)
        {
            // Cargar usuarios en segundo plano
            await Task.Run(() => LoadTitulares());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SeleccionarPersonaPatente.idPersonaT = 0;
            this.Close();
        }

        private void dtgTitulares_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            string valor = "%" + txtBuscar.Text + "%";
            var titulares = personaModel.GetTitularByValue(valor);

            if (titulares != null)
            {
                dtgTitulares.DataSource = titulares;
                if (dtgTitulares.Columns["id"] != null)
                {
                    dtgTitulares.Columns["id"].Visible = false;
                    dtgTitulares.Columns["tipo"].Visible = false;
                }
            }
            else
            {
                MessageBox.Show("No se encontraron resultados para la búsqueda.");
            }
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            if (dtgTitulares.RowCount <= 0)
            {
                MessageBox.Show("No hay datos para seleccionar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dtgTitulares.SelectedRows.Count > 0) // Verifica si hay filas seleccionadas
            {

                var filaSeleccionada = dtgTitulares.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {

                    int id = Convert.ToInt32(dataRowView["id"]);


                    var detallesTitular = personaModel.GetPersonaById(id);

                    if (detallesTitular.Count > 0)
                    {
                        SeleccionarTraspasoPatente.idComodin = id;
                        SeleccionarTraspasoPatente.nombreComodin = detallesTitular[0].nombre;

                        this.Close();
                    }
                    else
                    {
                        SeleccionarTraspasoPatente.idComodin = 0;
                        MessageBox.Show("No se encontraron detalles del titular", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Close();
                    }
                }
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("SELECCIONE UNA FILA", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                //MessageBox.Show("Por favor seleccione una fila", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
