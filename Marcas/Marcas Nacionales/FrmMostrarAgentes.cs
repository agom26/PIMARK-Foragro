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
                // Desactiva la selección automática de la primera fila
                dtgAgentes.ClearSelection();
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
            SeleccionarPersona.idPersona = 0;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SeleccionarPersona.idPersona = 0;
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            string valor = "%" + txtBuscar.Text + "%";
            var agentes = personaModel.GetAgenteByValue(valor);

            if (agentes != null)
            {
                dtgAgentes.DataSource = agentes;
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

        private void iconButton3_Click(object sender, EventArgs e)
        {
            if (dtgAgentes.RowCount <= 0)
            {
                MessageBox.Show("No hay datos para seleccionar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (dtgAgentes.SelectedRows.Count > 0) // Verifica si hay filas seleccionadas
            {
                // Usa DataBoundItem para acceder al objeto vinculado a la fila seleccionada
                var filaSeleccionada = dtgAgentes.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                    // Obtén el ID de la fila seleccionada
                    int id = Convert.ToInt32(dataRowView["id"]);
                    SeleccionarPersona.idPersona = id;

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

                        MessageBox.Show("ID seleccionado: " + SeleccionarPersona.idPersona);
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
                MessageBox.Show("Por favor seleccione una fila", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
    }
}
