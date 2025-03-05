using Comun.Cache;
using Dominio;
using Presentacion.Alertas;
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
        private const int pageSize = 20;
        private int currentPageIndex = 1;
        private int totalPages = 0;
        private int totalRows = 0;
        private bool buscando = false;
        public FrmMostrarAgentes()
        {
            InitializeComponent();
            this.Load += FrmMostrarAgentes_Load; // Mueve la lógica de carga aquí
        }

        private async Task LoadAgentes()
        {
            totalRows = personaModel.GetTotalAgentes();
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
            // Obtiene los usuarios
            var titulares = await Task.Run(() => personaModel.GetAllAgentes(currentPageIndex, pageSize));

            Invoke(new Action(() =>
            {
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                dtgAgentes.DataSource = titulares;

                if (dtgAgentes.Columns["id"] != null)
                {
                    dtgAgentes.Columns["id"].Visible = false;
                    dtgAgentes.ClearSelection();
                }


            }));
        }
        public async void filtrar()
        {
            string buscar = txtBuscar.Text;
            if (buscar != "")
            {
                totalRows = personaModel.GetFilteredAgentesCount(txtBuscar.Text);
                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                DataTable titulares = personaModel.GetAgenteByValue(buscar, currentPageIndex, pageSize);
                if (titulares.Rows.Count > 0)
                {
                    dtgAgentes.DataSource = titulares;
                    if (dtgAgentes.Columns["id"] != null)
                    {
                        dtgAgentes.Columns["id"].Visible = false;
                        dtgAgentes.Columns["tipo"].Visible = false;
                    }
                    dtgAgentes.ClearSelection();
                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("NO EXISTEN AGENTES CON ESOS DATOS", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                    alerta.ShowDialog();
                    //MessageBox.Show("No existen titulares con esos datos");
                    await LoadAgentes();
                }
            }
            else
            {
                await LoadAgentes();
            }
        }



        private async void FrmMostrarAgentes_Load(object sender, EventArgs e)
        {
            // Cargar usuarios en segundo plano
            await Task.Run(() => LoadAgentes());
            currentPageIndex = 1;
            lblCurrentPage.Text = currentPageIndex.ToString();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            SeleccionarPersona.idPersonaA = 0;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SeleccionarPersona.idPersonaA = 0;
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            buscando = true;
            currentPageIndex = 1;
            totalRows = personaModel.GetFilteredAgentesCount(txtBuscar.Text);
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

            lblCurrentPage.Text = currentPageIndex.ToString();
            lblTotalPages.Text = totalPages.ToString();
            lblTotalRows.Text = totalRows.ToString();
            filtrar();
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            if (dtgAgentes.RowCount <= 0)
            {
                MessageBox.Show("No hay datos para seleccionar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (dtgAgentes.SelectedRows.Count > 0) // Verifica si hay filas seleccionadas
            {
                buscando = false;
                // Usa DataBoundItem para acceder al objeto vinculado a la fila seleccionada
                var filaSeleccionada = dtgAgentes.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                    // Obtén el ID de la fila seleccionada
                    int id = Convert.ToInt32(dataRowView["id"]);
                    SeleccionarPersona.idPersonaA = id;

                    // Obtén los detalles de la persona utilizando el ID
                    var detallesAgente = personaModel.GetPersonaById(id);
                    if (detallesAgente.Count > 0)
                    {
                        // Asignar los valores obtenidos a la clase SeleccionarPersona
                        SeleccionarPersona.nombre = detallesAgente[0].nombre;
                        SeleccionarPersona.direccion = detallesAgente[0].direccion;
                        SeleccionarPersona.correo = detallesAgente[0].correo;
                        SeleccionarPersona.contacto = detallesAgente[0].contacto;
                        SeleccionarPersona.pais = detallesAgente[0].pais;
                        SeleccionarPersona.nit = detallesAgente[0].nit;
                        SeleccionarPersona.telefono = detallesAgente[0].telefono;

                        //MessageBox.Show("ID seleccionado: " + SeleccionarPersona.idPersona);
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron detalles del agente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    this.Close(); // Cierra el formulario si todo fue correcto
                }
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("SELECCIONE UNA FILA", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                //MessageBox.Show("Por favor seleccione una fila", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            buscando = false;
            txtBuscar.Text = "";
            filtrar();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buscando = true;
                currentPageIndex = 1;
                totalRows = personaModel.GetFilteredAgentesCount(txtBuscar.Text);
                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                lblCurrentPage.Text = currentPageIndex.ToString();
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                filtrar();
            }
        }

        private async void btnFirst_Click(object sender, EventArgs e)
        {
            currentPageIndex = 1;
            if (buscando==true)
            {
                filtrar();
            }
            else
            {
                await LoadAgentes();
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
                    await LoadAgentes();
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
                    await LoadAgentes();
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
                await LoadAgentes();
            }

            lblCurrentPage.Text = currentPageIndex.ToString();
        }
    }
}
