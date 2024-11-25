using Dominio;
using Microsoft.Win32;
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
    public partial class FrmReportesMarcasPatentes : Form
    {
        MarcaModel marcamodel = new MarcaModel();
        public FrmReportesMarcasPatentes()
        {
            InitializeComponent();
            this.Load += FrmReportesMarcasPatentes_Load;

        }


        public void Filtrar()
        {
            string objeto = null;
            string? estado = null;
            string? nombre = null;
            string? pais = null;
            string? folio = null;
            string? tomo = null;
            string? numRegistro = null;
            string? clase = null;
            string? fechaSolicitudInicio = null;
            string? fechaSolicitudFin = null;
            string? fechaRegistroInicio = null;
            string? fechaRegistroFin = null;
            string? fechaVencimientoInicio = null;
            string? fechaVencimientoFinal = null;
            string? titular = null;
            string? agente = null;
            string? cliente = null;


            switch (comboBoxObjeto.SelectedIndex)
            {
                case 0:
                    objeto = "nacional";
                    break;
                case 1:
                    objeto = "internacional";
                    break;
                case 2:
                    objeto = "marca";
                    break;
                case 3:
                    objeto = "patentes";
                    break;
            }

            if (checkBoxEstado.Checked)
            {
                estado = comboBoxEstado.SelectedItem.ToString();
            }
            else
            {
                estado = null;
            }

            if (checkBoxNombre.Checked)
            {
                nombre = txtNombre.Text;
            }
            else
            {
                nombre = null;
            }

            if (checkBoxPais.Checked)
            {
                pais = comboBoxPais.SelectedIndex.ToString();
            }
            else
            {
                pais = null;
            }

            if (checkBoxFolio.Checked)
            {
                folio = txtFolio.Text;
            }
            else
            {
                folio = null;
            }

            if (checkBoxTomo.Checked)
            {
                tomo = txtTomo.Text;
            }
            else
            {
                tomo = null;
            }

            if (checkBoxReigstro.Checked)
            {
                numRegistro = txtRegistro.Text;
            }
            else
            {
                numRegistro = null;
            }

            if (checkBoxClase.Checked)
            {
                clase = txtClase.Text;
            }
            else
            {
                clase = null;
            }

            if (checkBoxTitular.Checked)
            {
                titular = richTextBoxTitular.Text;
            }
            else
            {
                titular = null;
            }

            if (checkBoxAgente.Checked)
            {
                agente = richTextBoxAgente.Text;
            }
            else
            {
                agente = null;
            }

            if (checkBoxClase.Checked)
            {
                cliente = richTextBoxCliente.Text;
            }
            else
            {
                cliente = null;
            }

            if (checkBoxSolicitud.Checked)
            {
                fechaSolicitudInicio = dtpFRegistroInicial.Value.ToString("yyyy-MM-dd");
                fechaSolicitudFin = dtpFechaRegistroFinal.Value.ToString("yyyy-MM-dd");
            }
            else
            {
                fechaSolicitudInicio = null;
                fechaSolicitudFin = null;
            }

            if (checkBoxRegistro.Checked)
            {
                fechaRegistroInicio = dtpFRegistroInicial.Value.ToString("yyyy-MM-dd");
                fechaRegistroFin = dtpFechaRegistroFinal.Value.ToString("yyyy-MM-dd");
            }
            else
            {
                fechaRegistroInicio = null;
                fechaRegistroFin = null;
            }

            if (checkBoxVencimiento.Checked)
            {
                fechaVencimientoInicio = dtpVencimientoInicial.Value.ToString("yyyy-MM-dd");
                fechaVencimientoFinal = dtpVencimientoFinal.Value.ToString("yyyy-MM-dd");
            }
            else
            {
                fechaVencimientoInicio = null;
                fechaVencimientoFinal = null;
            }

            dtgReportes.DataSource = marcamodel.Filtrar(objeto, estado, nombre, pais,
                folio, tomo, numRegistro, clase, fechaSolicitudInicio, fechaSolicitudFin,
                fechaRegistroInicio, fechaRegistroFin, fechaVencimientoInicio, fechaVencimientoFinal,
                titular, agente, cliente
                );
            dtgReportes.ClearSelection();

        }
        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void txtRegistro_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Filtrar();
        }

        private void comboBoxObjeto_SelectedIndexChanged(object sender, EventArgs e)
        {
            string seleccion = comboBoxObjeto.SelectedItem.ToString();
            if (seleccion == "Marcas nacionales" || seleccion == "Marcas internacionales" || seleccion == "Marcas nacionales e internacionales")
            {
                comboBoxEstado.Items.Clear();
                comboBoxEstado.Items.Add("");
                comboBoxEstado.Items.Add("Ingresada");
                comboBoxEstado.Items.Add("Examen de forma");
                comboBoxEstado.Items.Add("Examen de fondo");
                comboBoxEstado.Items.Add("Requerimiento");
                comboBoxEstado.Items.Add("Objeción");
                comboBoxEstado.Items.Add("Edicto");
                comboBoxEstado.Items.Add("Publicación");
                comboBoxEstado.Items.Add("Oposición");
                comboBoxEstado.Items.Add("Orden de pago");
                comboBoxEstado.Items.Add("Abandono");
                comboBoxEstado.Items.Add("Registrada");
                comboBoxEstado.Items.Add("Licencia de uso");
                comboBoxEstado.Items.Add("Trámite de renovación");
                comboBoxEstado.Items.Add("Trámite de traspaso");

            }
            else if (seleccion == "Patentes")
            {

                comboBoxEstado.Items.Clear();
                comboBoxEstado.Items.Add("");
                comboBoxEstado.Items.Add("Ingresada");
                comboBoxEstado.Items.Add("Examen de forma");
                comboBoxEstado.Items.Add("Examen de publicación");
                comboBoxEstado.Items.Add("Edicto");
                comboBoxEstado.Items.Add("Examen de fondo");
                comboBoxEstado.Items.Add("Prórroga");
                comboBoxEstado.Items.Add("Rechazo");
                comboBoxEstado.Items.Add("Registro/concesión");
                comboBoxEstado.Items.Add("Modificación");
                comboBoxEstado.Items.Add("Conversión de solicitud");
                comboBoxEstado.Items.Add("Corrección del certificado o inscripción");
                comboBoxEstado.Items.Add("Trámite de renovación");
                comboBoxEstado.Items.Add("Trámite de traspaso");
            }
        }

        private void FrmReportesMarcasPatentes_Load(object sender, EventArgs e)
        {
            comboBoxObjeto.SelectedIndex = 0;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            dtgReportes.DataSource = null;
            dtgReportes.ClearSelection();
        }
    }
}
