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

namespace Presentacion.Patentes
{
    public partial class FrmMostrarAgentesPatente : Form
    {
        PersonaModel personaModel = new PersonaModel();
        public FrmMostrarAgentesPatente()
        {
            InitializeComponent();
            this.Load += FrmMostrarAgentesPatente_Load; 
        }

        private void MostrarTitulares()
        {
            dtgAgentes.DataSource = personaModel.GetAllAgentes();
            
            if (dtgAgentes.Columns["id"] != null)
            {
                dtgAgentes.Columns["id"].Visible = false;
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




        private async void FrmMostrarAgentesPatente_Load(object sender, EventArgs e)
        {
            // Cargar usuarios en segundo plano
            await Task.Run(() => LoadAgentes());
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            SeleccionarPersonaPatente.LimpiarPersona();
           
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SeleccionarPersonaPatente.LimpiarPersona();
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
                FrmAlerta alerta = new FrmAlerta("NO SE ENCONTRARON RESULTADOS PARA LA BÚSQUEDA", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                //MessageBox.Show("No se encontraron resultados para la búsqueda.");
            }
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            if (dtgAgentes.RowCount <= 0)
            {
                FrmAlerta alerta = new FrmAlerta("NO HAY DATOS PARA SELECCIONAR", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                //MessageBox.Show("No hay datos para seleccionar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (dtgAgentes.SelectedRows.Count > 0) 
            {
                
                var filaSeleccionada = dtgAgentes.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                  
                    int id = Convert.ToInt32(dataRowView["id"]);
                    SeleccionarPersonaPatente.idPersonaA = id;

                  
                    var detallesAgente = personaModel.GetPersonaById(id);
                    if (detallesAgente.Count > 0)
                    {
                       
                        SeleccionarPersonaPatente.nombre = detallesAgente[0].nombre;
                        SeleccionarPersonaPatente.direccion = detallesAgente[0].direccion;
                        SeleccionarPersonaPatente.correo = detallesAgente[0].correo;
                        SeleccionarPersonaPatente.contacto = detallesAgente[0].contacto;
                        SeleccionarPersonaPatente.pais = detallesAgente[0].pais;
                        SeleccionarPersonaPatente.nit = detallesAgente[0].nit;
                        SeleccionarPersonaPatente.telefono = detallesAgente[0].telefono;

                        //MessageBox.Show("ID seleccionado: " + SeleccionarPersona.idPersona);
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron detalles del agente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    this.Close(); 
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
