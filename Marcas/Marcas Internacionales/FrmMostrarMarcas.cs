using Comun.Cache;
using DocumentFormat.OpenXml.Wordprocessing;
using Dominio;
using Presentacion.Alertas;
using System.Data;
using System.Threading.Tasks;

namespace Presentacion.Marcas_Nacionales
{
    public partial class FrmMostrarMarcas : Form
    {
        PersonaModel personaModel = new PersonaModel();
        MarcaModel marcaModel = new MarcaModel();
        private const int pageSize = 20;
        private int currentPageIndex = 1;
        private int totalPages = 0;
        private int totalRows = 0;
        private bool buscando = false;
        public FrmMostrarMarcas()
        {
            InitializeComponent();
            this.Load += FrmMostrarMarcas_Load; // Mueve la lógica de carga aquí
        }



        private void iconButton2_Click(object sender, EventArgs e)
        {
            SeleccionarMarcaOposicion.LimpiarMarcaOposicion();
            this.Close();
        }


        private async Task LoadMarcas()
        {
            totalRows = marcaModel.GetTotalMarcasInternacionales();
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
            // Obtiene las marcas 
            var marcas = await Task.Run(() => marcaModel.GetAllMarcasInternacionales(currentPageIndex, pageSize));

            Invoke(new Action(() =>
            {
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                dtgTitulares.DataSource = marcas;

                if (dtgTitulares.Columns["id"] != null)
                {
                    dtgTitulares.Columns["IdTitular"].Visible = false;
                    dtgTitulares.Columns["id"].Visible = false;
                }


            }));
        }

        public async void filtrar()
        {
            string buscar = txtBuscar.Text;
            if (buscar != "")
            {
                totalRows = marcaModel.GetFilteredMarcasInternacionalesCount(txtBuscar.Text);
                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                DataTable titulares = marcaModel.FiltrarMarcasInternacionales(buscar, currentPageIndex, pageSize);
                if (titulares.Rows.Count > 0)
                {
                    dtgTitulares.DataSource = titulares;
                    if (dtgTitulares.Columns["id"] != null)
                    {
                        dtgTitulares.Columns["id"].Visible = false;

                        dtgTitulares.Columns["IdTitular"].Visible = false;
                    }
                    dtgTitulares.ClearSelection();
                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("NO EXISTEN MARCAS NACIONALES CON ESOS DATOS", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                    alerta.ShowDialog();
                    //MessageBox.Show("No existen titulares con esos datos");
                    await LoadMarcas();
                }
            }
            else
            {
                await LoadMarcas();
            }
        }

        private async void FrmMostrarMarcas_Load(object sender, EventArgs e)
        {
            currentPageIndex = 1;
            lblCurrentPage.Text = currentPageIndex.ToString();
            // Cargar usuarios en segundo plano
            await Task.Run(() => LoadMarcas());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SeleccionarMarcaOposicion.LimpiarMarcaOposicion();
            this.Close();
        }

        private void dtgTitulares_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            buscando = true;
            currentPageIndex = 1;
            totalRows = marcaModel.GetFilteredMarcasInternacionalesCount(txtBuscar.Text);
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

            lblCurrentPage.Text = currentPageIndex.ToString();
            lblTotalPages.Text = totalPages.ToString();
            lblTotalRows.Text = totalRows.ToString();
            filtrar();
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
                // Usa DataBoundItem para acceder al objeto vinculado a la fila seleccionada
                var filaSeleccionada = dtgTitulares.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {

                    int id = Convert.ToInt32(dataRowView["id"]);
                    SeleccionarMarcaOposicion.idMarca = id;
                    SeleccionarMarcaOposicion.nombreSigno = dataRowView["SIGNO"].ToString();
                    SeleccionarMarcaOposicion.idTitularMarca = (int)dataRowView["IdTitular"];
                    SeleccionarMarcaOposicion.nombreTitular = dataRowView["TITULAR"].ToString();
                    this.Close();
                    /*
                    var detallesTitular = personaModel.GetPersonaById(id);

                    if (detallesTitular.Count > 0)
                    {
                        //MessageBox.Show("ID seleccionado: " + SeleccionarPersona.idPersona);

                        // Asignar los valores obtenidos a la clase SeleccionarPersona
                        SeleccionarPersona.nombre = detallesTitular[0].nombre;
                        SeleccionarPersona.direccion = detallesTitular[0].direccion;
                        SeleccionarPersona.correo = detallesTitular[0].correo;
                        SeleccionarPersona.contacto = detallesTitular[0].contacto;
                        SeleccionarPersona.pais = detallesTitular[0].pais;
                        SeleccionarPersona.nit = detallesTitular[0].nit;
                        SeleccionarPersona.telefono = detallesTitular[0].telefono;

                        this.Close(); // Cierra el formulario si todo fue correcto
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron detalles del titular", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }*/
                }
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("SELECCIONE UNA FILA", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                //MessageBox.Show("Por favor seleccione una fila", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnFirst_Click(object sender, EventArgs e)
        {
            currentPageIndex = 1;
            if (buscando == true)
            {
                filtrar();
            }
            else
            {
                await LoadMarcas();
            }

            lblCurrentPage.Text = currentPageIndex.ToString();
        }

        private async void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPageIndex > 1)
            {
                currentPageIndex--;
                if (buscando == true)
                {
                    filtrar();
                }
                else
                {
                    await LoadMarcas();
                }

                lblCurrentPage.Text = currentPageIndex.ToString();
            }
        }

        private async void btnNext_Click(object sender, EventArgs e)
        {
            if (currentPageIndex < totalPages)
            {
                currentPageIndex++;
                if (buscando == true)
                {
                    filtrar();
                }
                else
                {
                    await LoadMarcas();
                }

                lblCurrentPage.Text = currentPageIndex.ToString();
            }
        }

        private async void btnLast_Click(object sender, EventArgs e)
        {
            currentPageIndex = totalPages;
            if (buscando == true)
            {
                filtrar();
            }
            else
            {
                await LoadMarcas();
            }

            lblCurrentPage.Text = currentPageIndex.ToString();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buscando = true;
                currentPageIndex = 1;
                totalRows = marcaModel.GetFilteredMarcasInternacionalesCount(txtBuscar.Text);
                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                lblCurrentPage.Text = currentPageIndex.ToString();
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                filtrar();
            }
        }

        private async void iconButton7_Click(object sender, EventArgs e)
        {
            buscando = false;
            txtBuscar.Text = "";
            await LoadMarcas();
        }
    }
}
