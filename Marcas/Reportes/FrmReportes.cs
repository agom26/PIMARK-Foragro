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
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            string? estado = null;
            string? nombre = null;
            string? pais= null;
            string? folio = null;
            string? tomo= null;
            string? numRegistro= null;
            string? clase= null;
            string? titular= null;
            string? agente= null;
            DateTime? fechaSolicitudInicio = null;
            DateTime? fechaSolicitudFin= null;
            DateTime? fechaRegistroInicio = null;
            DateTime? fechaRegistroFin= null;
            DateTime? fechaVencimientoInicio= null;
            DateTime? fechaVencimientoFinal= null;

            if (chckEstado.Checked)
            {
                estado = comboBoxEstado.SelectedItem.ToString();
            }
            else
            {
                estado = null;
            }

            if (chckNombre.Checked)
            {
                nombre = txtNombre.Text;
            }
            else
            {
                nombre = null;
            }

            // Llamada al método FiltrarMarcas con los parámetros procesados
            DataTable resultados = marcaModel.FiltrarMarcas(
                estado, nombre, pais, folio, tomo, numRegistro, clase, titular, agente,
                fechaSolicitudInicio, fechaSolicitudFin, fechaRegistroInicio, fechaRegistroFin,
                fechaVencimientoInicio, fechaVencimientoFinal);
            dtgReportes.DataSource = resultados;

        }
    }
}
