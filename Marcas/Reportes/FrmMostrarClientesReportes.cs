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

namespace Presentacion.Reportes
{
    public partial class FrmMostrarClientesReportes : Form
    {
        PersonaModel personaModel = new PersonaModel();
        public FrmMostrarClientesReportes()
        {
            InitializeComponent();
            this.Load += FrmMostrarClientesReportes_Load; 
        }

     

        private void LoadClientes()
        {
            
            var clientes = personaModel.GetAllClientes();

            Invoke(new Action(() =>
            {
                dtgClientes.DataSource = clientes;

                if (dtgClientes.Columns["id"] != null)
                {
                    dtgClientes.Columns["id"].Visible = false;
                }


            }));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SeleccionarPersonaReportes.LimpiarCliente();
            this.Close();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            SeleccionarPersonaReportes.LimpiarCliente() ;
            this.Close();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            string valor = "%" + txtBuscar.Text + "%";
            var agentes = personaModel.GetClienteByValue(valor);

            if (agentes != null)
            {
                dtgClientes.DataSource = agentes;
                if (dtgClientes.Columns["id"] != null)
                {
                    dtgClientes.Columns["id"].Visible = false;
                    dtgClientes.Columns["tipo"].Visible = false;
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
            if (dtgClientes.RowCount <= 0)
            {
                FrmAlerta alerta = new FrmAlerta("NO HAY DATOS PARA SELECCIONAR", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                //MessageBox.Show("No hay datos para seleccionar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dtgClientes.SelectedRows.Count > 0) 
            {
               
                var filaSeleccionada = dtgClientes.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                   
                    int id = Convert.ToInt32(dataRowView["id"]);
                    SeleccionarPersonaReportes.idCliente= id;
                    //MessageBox.Show("id" + SeleccionarPersona.idPersonaC);
                    var detallesCliente = personaModel.GetPersonaById(id);

                    if (detallesCliente.Count > 0)
                    {
                        //MessageBox.Show("ID seleccionado: " + SeleccionarPersona.idPersona);

                        SeleccionarPersonaReportes.nombreCliente= detallesCliente[0].nombre;
                        

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron detalles del clente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private async void FrmMostrarClientesReportes_Load(object sender, EventArgs e)
        {
            await Task.Run(() => LoadClientes());
        }
    }
}
