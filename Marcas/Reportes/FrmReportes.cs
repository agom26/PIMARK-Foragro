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

namespace Presentacion.Reportes
{
    public partial class FrmReportes : Form
    {
        MarcaModel marcaModel = new MarcaModel();
        public FrmReportes()
        {
            InitializeComponent();
            this.Load += FrmReportes_Load; // Mueve la lógica de carga aq
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            bool algunValor = false;
            string? estado = null;
            string? nombre = null;
            string? pais = null;
            string? folio = null;
            string? tomo = null;
            string? numRegistro = null;
            string? clase = null;
            string? titular = null;
            string? agente = null;
            string? fechaSolicitudInicio = null;
            string? fechaSolicitudFin = null;
            string? fechaRegistroInicio = null;
            string? fechaRegistroFin = null;
            string? fechaVencimientoInicio = null;
            string? fechaVencimientoFinal = null;

            if (chckEstado.Checked)
            {
                estado = comboBoxEstado.SelectedItem.ToString();
                algunValor = true;
            }
            else
            {
                estado = null;
            }

            if (chckNombre.Checked)
            {
                nombre = txtNombre.Text;
                algunValor = true;
            }
            else
            {
                nombre = null;
            }

            if (chckPais.Checked)
            {
                pais = comboBoxPais.SelectedIndex.ToString();
                algunValor = true;
            }
            else
            {
                pais = null;
            }

            if (chckFolio.Checked)
            {
                folio = txtFolio.Text;
                algunValor = true;
            }
            else
            {
                folio = null;
            }

            if (chckTomo.Checked)
            {
                tomo = txtTomo.Text;
                algunValor = true;
            }
            else
            {
                tomo = null;
            }

            if (chckNumRegistro.Checked)
            {
                numRegistro = txtNumRegistro.Text;
                algunValor = true;
            }
            else
            {
                numRegistro = null;
            }

            if (chckClase.Checked)
            {
                clase = txtClase.Text;
                algunValor = true;
            }
            else
            {
                clase = null;
            }

            if (chckTitular.Checked)
            {
                titular = txtTitular.Text;
                algunValor = true;
            }
            else
            {
                titular = null;
            }

            if (chckAgente.Checked)
            {
                agente = txtAgente.Text;
                algunValor = true;
            }
            else
            {
                agente = null;
            }

            if (chckFSolicitud.Checked)
            {
                fechaSolicitudInicio = dtpFRegistroInicial.Value.ToString("yyyy-MM-dd");
                algunValor = true;
                fechaSolicitudFin = dtpFechaRegistroFinal.Value.ToString("yyyy-MM-dd");
            }
            else
            {
                fechaSolicitudInicio = null;
                fechaSolicitudFin = null;
            }

            if (chckFRegistro.Checked)
            {
                fechaRegistroInicio = dtpFRegistroInicial.Value.ToString("yyyy-MM-dd");
                fechaRegistroFin = dtpFechaRegistroFinal.Value.ToString("yyyy-MM-dd");
                algunValor = true;
            }
            else
            {
                fechaRegistroInicio = null;
                fechaRegistroFin = null;
            }

            if (chckFVencimiento.Checked)
            {
                fechaVencimientoInicio = dtpVencimientoInicial.Value.ToString("yyyy-MM-dd");
                fechaVencimientoFinal = dtpVencimientoFinal.Value.ToString("yyyy-MM-dd");
                algunValor = true;
            }
            else
            {
                fechaVencimientoInicio = null;
                fechaVencimientoFinal = null;
            }

            if (algunValor == true)
            {/*
                // Llamada al método FiltrarMarcas con los parámetros procesados
                DataTable resultados = marcaModel.FiltrarMarcas(
                    estado, nombre, pais, folio, tomo, numRegistro, clase, titular, agente,
                    fechaSolicitudInicio, fechaSolicitudFin, fechaRegistroInicio, fechaRegistroFin,
                    fechaVencimientoInicio, fechaVencimientoFinal);
                dtgReportes.DataSource = resultados;
                if (dtgReportes.Columns["id"] != null)
                {
                    dtgReportes.Columns["id"].Visible = false;
                }
                dtgReportes.ClearSelection();*/
                
            }
            else
            {
                MessageBox.Show("Debe seleccionar por lo menos un filtro");
            }

            

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            dtgReportes.DataSource = null;
        }

        private void FrmReportes_Load(object sender, EventArgs e)
        {

        }
    }
}
