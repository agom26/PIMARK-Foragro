using Comun.Cache;
using Dominio;
using Presentacion.Alertas;
using System.Data;

namespace Presentacion.Marcas_Internacionales
{
    public partial class FrmMostrarMarcasN : Form
    {
        PersonaModel personaModel = new PersonaModel();
        MarcaModel marcaModel = new MarcaModel();
        private const int pageSize = 20;
        private int currentPageIndex = 1;
        private int totalPages = 0;
        private int totalRows = 0;
        public FrmMostrarMarcasN()
        {
            InitializeComponent();
            this.Load += FrmMostrarMarcasN_Load; // Mueve la lógica de carga aquí
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            SeleccionarMarcaOposicion.LimpiarMarcaOposicion();
            this.Close();
        }

        private async Task LoadMarcas()
        {
            totalRows = marcaModel.GetTotalMarcasNacionales();
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
            // Obtiene los usuarios
            var marcas = await Task.Run(() => marcaModel.GetAllMarcasNacionales(currentPageIndex, pageSize));

            Invoke(new Action(() =>
            {
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                dtgTitulares.DataSource = marcas;

                if (dtgTitulares.Columns["id"] != null)
                {
                    dtgTitulares.Columns["id"].Visible = false;
                    dtgTitulares.ClearSelection();
                }


            }));
        }
        public async void filtrar()
        {
            string buscar = txtBuscar.Text;
            if (buscar != "")
            {
                totalRows = marcaModel.GetFilteredMarcasNacionalesCount(txtBuscar.Text);
                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                DataTable titulares = marcaModel.FiltrarMarcasNacionales(buscar, currentPageIndex, pageSize);
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

        private async void FrmMostrarMarcasN_Load(object sender, EventArgs e)
        {
            // Cargar usuarios en segundo plano
            currentPageIndex = 1;
            lblCurrentPage.Text = currentPageIndex.ToString();
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
            filtrar();
            /*
            string valor = "%" + txtBuscar.Text + "%";
            var titulares = personaModel.GetTitularByValue(valor);

            if (titulares != null)
            {
                dtgTitulares.DataSource = titulares;
                if (dtgTitulares.Columns["id"] != null)
                {
                    dtgTitulares.Columns["id"].Visible = false;
                    dtgTitulares.Columns["IdTitular"].Visible = false;
                }
            }
            else
            {
                MessageBox.Show("No se encontraron resultados para la búsqueda.");
            }*/
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
                    SeleccionarMarcaOposicion.nombreSigno = dataRowView["nombre"].ToString();
                    SeleccionarMarcaOposicion.idTitularMarca = (int)dataRowView["IdTitular"];
                    SeleccionarMarcaOposicion.nombreTitular = dataRowView["titular"].ToString();
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
                FrmAlerta alerta = new FrmAlerta("SELECCIONE UNA MARCA", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                //MessageBox.Show("Por favor seleccione una fila", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPageIndex > 1)
            {
                currentPageIndex--;
                if (txtBuscar.Text != "")
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

        private async void btnFirst_Click(object sender, EventArgs e)
        {
            currentPageIndex = 1;
            if (txtBuscar.Text != "")
            {
                filtrar();
            }
            else
            {
                await LoadMarcas();
            }

            lblCurrentPage.Text = currentPageIndex.ToString();
        }

        private async void btnNext_Click(object sender, EventArgs e)
        {
            if (currentPageIndex < totalPages)
            {
                currentPageIndex++;
                if (txtBuscar.Text != "")
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
            if (txtBuscar.Text != "")
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
                filtrar();
            }
        }

        private void iconButton7_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            filtrar();
        }
    }
}
