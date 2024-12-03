using Comun.Cache;
using Dominio;
using Presentacion.Alertas;
using System.Data;

namespace Presentacion.Marcas_Nacionales
{
    public partial class FrmMostrarMarcas : Form
    {
        PersonaModel personaModel = new PersonaModel();
        MarcaModel marcaModel = new MarcaModel();
        public FrmMostrarMarcas()
        {
            InitializeComponent();
            this.Load += FrmMostrarMarcas_Load; // Mueve la lógica de carga aquí
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            SeleccionarPersona.idPersonaT = 0;
            this.Close();
        }

     
        private void LoadMarcas()
        {
            // Obtiene las marcas 
            var marcas= marcaModel;

            Invoke(new Action(() =>
            {
                dtgTitulares.DataSource = marcas;

                if (dtgTitulares.Columns["id"] != null)
                {
                    dtgTitulares.Columns["id"].Visible = false;
                }


            }));
        }
        private async void FrmMostrarMarcas_Load(object sender, EventArgs e)
        {
            // Cargar usuarios en segundo plano
            await Task.Run(() => LoadMarcas());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SeleccionarPersona.idPersonaT = 0;
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
                // Usa DataBoundItem para acceder al objeto vinculado a la fila seleccionada
                var filaSeleccionada = dtgTitulares.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                    // Obtén el ID de la fila seleccionada
                    int id = Convert.ToInt32(dataRowView["id"]);
                    SeleccionarPersona.idPersonaT = id;

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
