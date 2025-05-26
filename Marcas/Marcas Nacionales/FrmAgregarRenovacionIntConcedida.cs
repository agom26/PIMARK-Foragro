using Comun.Cache;
using DocumentFormat.OpenXml.Wordprocessing;
using Dominio;
using Presentacion.Alertas;
using System.Data;
using System.Threading.Tasks;

namespace Presentacion.Marcas_Internacionales
{
    public partial class FrmAgregarRenovacionIntConcedida : Form
    {
        public FrmAgregarRenovacionIntConcedida()
        {
            InitializeComponent();
            EliminarTabPage(tabPageListarLicencias);
            EliminarTabPage(tabPageRenovarLicencia);
            this.Load += FrmAgregarRenovacionIntConcedida_Load;
        }


        private void ActualizarFechaVencimientoNueva()
        {
            DateTime fechaVencA = dateFechVencAnt.Value;
            DateTime fecha_vencimientoN = fechaVencA.AddYears(10);
            dateFechVencNueva.Value = fecha_vencimientoN;
            AgregarRenovacion.fechaVencimientoNueva = fecha_vencimientoN;
        }

        public async Task RenovarMarca()
        {
            MarcaModel marcaModel = new MarcaModel();

            string anotaciones = richTextBox1.Text;
            string noExpediente = txtNoExpediente.Text;

            int idMarca = SeleccionarMarca.idN;
            DateTime fechaVencAnt = dateFechVencAnt.Value;
            DateTime fechaVencNueva = dateFechVencNueva.Value;
            DateTime fecha = dateTimePicker1.Value;
            string etapa = txtEstado.Text;
            string usuario = UsuarioActivo.usuario;

            if (string.IsNullOrEmpty(etapa))
            {
                MessageBox.Show("No ha seleccionado ningún estado.");
                return;
            }

            // Construcción de anotaciones
            string fechaSinHora = fecha.ToShortDateString();
            string formato = fechaSinHora + " " + etapa;
            string anotacionesFinales = anotaciones.Contains(formato) ? anotaciones : formato + " " + anotaciones;

            try
            {
                bool resultado = await Task.Run(() =>
                    marcaModel.RenovarMarca(noExpediente, idMarca, fechaVencAnt, fechaVencNueva, fecha, etapa, anotacionesFinales, usuario)
                );

                if (resultado)
                {

                    AgregarRenovacion.renovacionTerminada = true;
                }
                else
                {
                    MessageBox.Show("Error al realizar la renovación.");
                }
            }
            catch (Exception ex)
            {
                FrmAlerta alerta = new FrmAlerta(ex.Message.ToUpper(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.Show();
            }

            this.Close();
        }
        private void EliminarTabPage(TabPage nombre)
        {
            if (tabControl1.TabPages.Contains(nombre))
            {
                tabControl1.TabPages.Remove(nombre);
            }
        }
        private void AnadirTabPage(TabPage nombre)
        {
            if (!tabControl1.TabPages.Contains(nombre))
            {
                tabControl1.TabPages.Add(nombre);
            }

            tabControl1.SelectedTab = nombre;
        }

        private void FrmAgregarRenovacionIntConcedida_Load(object sender, EventArgs e)
        {
            lblUser.Text = UsuarioActivo.usuario;
            lblUser.Visible = false;
            txtNoExpediente.Text = SeleccionarMarca.erenov;
            dateFechVencAnt.Value = AgregarRenovacion.fechaVencimientoAntigua;
            ActualizarFechaVencimientoNueva();
            richTextBox1.Text = dateTimePicker1.Value.ToShortDateString() + " " + txtEstado.Text;
            EliminarTabPage(tabPageListarLicencias);
            EliminarTabPage(tabPageRenovarLicencia);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AgregarEtapa.LimpiarEtapa();
            this.Close();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {

            this.Close();
            AgregarEtapa.LimpiarEtapa();
        }

        private async void iconButton3_Click(object sender, EventArgs e)
        {
            FrmAlerta alerta = new FrmAlerta("¿ESTÁ SEGURO DE RENOVAR LA MARCA?", "PREGUNTA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            DialogResult resultado = alerta.ShowDialog();
            if (resultado == DialogResult.Yes)
            {
                await RenovarMarca();
            }


        }

        private async Task CargarLicencias(int idMarca)
        {
            var licenciaModel = new LicenciaUsoModel();
            var dtLicencias = await Task.Run(() => licenciaModel.ObtenerLicenciasPorMarca(idMarca));
            dtgLicencias.DataSource = dtLicencias;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = dateTimePicker1.Value.ToShortDateString() + " " + txtEstado.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateFechRegNueva_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateFechRegAntigua_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateFechVencAnt_ValueChanged(object sender, EventArgs e)
        {
            ActualizarFechaVencimientoNueva();
        }

        private void Fechas_Enter(object sender, EventArgs e)
        {

        }

        private async void iconButton1_Click(object sender, EventArgs e)
        {
            await CargarLicencias(SeleccionarMarca.idN);
            AnadirTabPage(tabPageListarLicencias);
        }

        private void iconButton18_Click(object sender, EventArgs e)
        {
            AnadirTabPage(tabPageRenovarMarca);
            EliminarTabPage(tabPageListarLicencias);
        }

        public static void DiferenciaAnioMesDia(DateTime fechaInicio, DateTime fechaFin, out int años, out int meses, out int dias)
        {
            if (fechaFin < fechaInicio)
            {
                DiferenciaAnioMesDia(fechaFin, fechaInicio, out años, out meses, out dias);
                años = -años;
                meses = -meses;
                dias = -dias;
                return;
            }
            
            años = fechaFin.Year - fechaInicio.Year;
            meses = fechaFin.Month - fechaInicio.Month;
            dias = fechaFin.Day - fechaInicio.Day;

            if (dias == 1)
            {
                dias = 0;
            }
            else if (dias == -1)
            {
                dias = 0;
            }
            else if (dias < 0)
            {
                meses--;
                var fechaMesAnterior = fechaFin.AddMonths(-1);
                dias += DateTime.DaysInMonth(fechaMesAnterior.Year, fechaMesAnterior.Month);
                dias += 1;
            }
            

            if (meses < 0)
            {
                años--;
                meses += 12;
            }

            
        }


        private void EdicionLicencia()
        {
            if (dtgLicencias.SelectedRows.Count == 0)
            {
                FrmAlerta alerta = new FrmAlerta("DEBE SELECCIONAR UNA LICENCIA DE USO", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                return;
            }

            var filaSeleccionada = dtgLicencias.SelectedRows[0];

            if (!(filaSeleccionada.DataBoundItem is DataRowView dataRowView))
            {
                FrmAlerta alerta = new FrmAlerta("ERROR AL OBTENER LA INFORMACIÓN DE LA LICENCIA DE USO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
                return;
            }

            // Validar que la columna 'id' exista y no sea DBNull
            if (!dataRowView.DataView.Table.Columns.Contains("id") || dataRowView["id"] == DBNull.Value)
            {
                FrmAlerta alerta = new FrmAlerta("EL ID DE LA LICENCIA NO ESTÁ DISPONIBLE.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
                return;
            }

            int idLicencia = Convert.ToInt32(dataRowView["id"]);

            var licenciaModel = new LicenciaUsoModel();
            DataTable dtDatosRenovacion = licenciaModel.ObtenerDatosRenovacionLicencia(idLicencia);

            if (dtDatosRenovacion == null || dtDatosRenovacion.Rows.Count == 0)
            {
                FrmAlerta alerta = new FrmAlerta("NO SE ENCONTRARON DATOS DE RENOVACIÓN PARA ESTA LICENCIA.", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                return;
            }

            DataRow filaRenovacion = dtDatosRenovacion.Rows[0];

            txtTituloVerifica.Text = filaRenovacion["titulo"].ToString();
            int anios = Convert.ToInt32(filaRenovacion["Anios"].ToString());
            int meses = Convert.ToInt32(filaRenovacion["Meses"].ToString());
            int dias = Convert.ToInt32(filaRenovacion["Dias"].ToString());
            if(dias == -1 || dias == 1)
            {
                dias = 0;
            }

            DateTime fechaInicioActual = filaRenovacion["fecha_inicio"] == DBNull.Value ? DateTime.Today : Convert.ToDateTime(filaRenovacion["fecha_inicio"]);
            DateTime fechaFinActual = filaRenovacion["fecha_fin"] == DBNull.Value ? DateTime.Today : Convert.ToDateTime(filaRenovacion["fecha_fin"]);

            dtpFechaFinAntigua.Value = fechaFinActual;

            DateTime nuevaFechaFin = fechaFinActual.AddYears(anios).AddMonths(meses).AddDays(dias);
            dtpFechaFinNueva.Value = nuevaFechaFin;

            AgregarRenovacionLicencia.idLicencia = idLicencia;
            AgregarRenovacionLicencia.fechaVencimientoAntigua = fechaFinActual;
            AgregarRenovacionLicencia.fechaVencimientoNueva = dtpFechaFinNueva.Value;
            AgregarRenovacionLicencia.numExpediente = filaRenovacion["expediente"].ToString();

            AnadirTabPage(tabPageRenovarLicencia);
        }



        private void btnAgregarArchivo_Click(object sender, EventArgs e)
        {
            EdicionLicencia();
        }

        private void dtgLicencias_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dtgLicencias.Columns["id"] != null)
            {
                dtgLicencias.Columns["id"].Visible = false;
                dtgLicencias.Columns["IdTitular"].Visible = false;
                dtgLicencias.Columns["IdMarca"].Visible = false;
            }
            dtgLicencias.ClearSelection();
        }

        private void btnCancelarH_Click(object sender, EventArgs e)
        {
            EliminarTabPage(tabPageRenovarLicencia);
            AnadirTabPage(tabPageListarLicencias);
        }

        private async void btnEditarH_Click(object sender, EventArgs e)
        {
            int idLicencia = AgregarRenovacionLicencia.idLicencia;
            string numExpediente =  AgregarRenovacionLicencia.numExpediente;
            DateTime fechaAntigua = dtpFechaFinAntigua.Value;
            DateTime fechaNueva = dtpFechaFinNueva.Value;
            var licenciaModel = new LicenciaUsoModel();
            bool renovada = licenciaModel.RenovarLicenciaUso(idLicencia, numExpediente, fechaAntigua, fechaNueva);

            if (renovada)
            {
                AgregarRenovacionLicencia.idLicencia = 0;
                AgregarRenovacionLicencia.fechaVencimientoAntigua = DateTime.Now;
                AgregarRenovacionLicencia.fechaVencimientoNueva = DateTime.Now;
                AgregarRenovacionLicencia.numExpediente = "";

                MessageBox.Show("LICENCIA DE USO RENOVADA CORRECTAMENTE", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarLicencias(SeleccionarMarca.idN);
                AnadirTabPage(tabPageListarLicencias);
                EliminarTabPage(tabPageRenovarLicencia);
            }
            else
            {
                MessageBox.Show("OCURRIÓ UN ERROR AL RENOVAR LA LICENCIA DE USO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
