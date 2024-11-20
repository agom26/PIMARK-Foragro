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
    public partial class FrmMostrarTitularesPatente : Form
    {
        PersonaModel personaModel = new PersonaModel();
        public FrmMostrarTitularesPatente()
        {
            InitializeComponent();
            this.Load += FrmMostrarTitularesPatente_Load; 
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            SeleccionarPersonaPatente.idPersonaT = 0;
            this.Close();
        }

        private void MostrarTitulares()
        {
            dtgTitulares.DataSource = personaModel.GetAllTitulares();
           
            if (dtgTitulares.Columns["id"] != null)
            {
                dtgTitulares.Columns["id"].Visible = false;
                
                dtgTitulares.ClearSelection();
            }
        }

        private void LoadTitulares()
        {


           
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
        private async void FrmMostrarTitularesPatente_Load(object sender, EventArgs e)
        {
            
            await Task.Run(() => LoadTitulares());
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

            if (dtgTitulares.SelectedRows.Count > 0) 
            {
               
                var filaSeleccionada = dtgTitulares.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                   
                    int id = Convert.ToInt32(dataRowView["id"]);
                    SeleccionarPersonaPatente.idPersonaT = id;

                    var detallesTitular = personaModel.GetPersonaById(id);

                    if (detallesTitular.Count > 0)
                    {
                        //MessageBox.Show("ID seleccionado: " + SeleccionarPersona.idPersona);

                        SeleccionarPersonaPatente.nombre = detallesTitular[0].nombre;
                        SeleccionarPersonaPatente.direccion = detallesTitular[0].direccion;
                        SeleccionarPersonaPatente.correo = detallesTitular[0].correo;
                        SeleccionarPersonaPatente.contacto = detallesTitular[0].contacto;
                        SeleccionarPersonaPatente.pais = detallesTitular[0].pais;
                        SeleccionarPersonaPatente.nit = detallesTitular[0].nit;
                        SeleccionarPersonaPatente.telefono = detallesTitular[0].telefono;

                        this.Close(); 
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
