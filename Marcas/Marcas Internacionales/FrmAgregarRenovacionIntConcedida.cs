using Comun.Cache;
using Dominio;
using Presentacion.Alertas;

namespace Presentacion.Marcas_Internacionales
{
    public partial class FrmAgregarRenovacionIntConcedida : Form
    {
        public FrmAgregarRenovacionIntConcedida()
        {
            InitializeComponent();
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
            HistorialModel historialModel = new HistorialModel();
            RenovacionesMarcaModel renovacionModel = new RenovacionesMarcaModel();
            string anotaciones = richTextBox1.Text;
            AgregarEtapa.etapa = txtEstado.Text;
            AgregarEtapa.fecha = dateTimePicker1.Value;
            AgregarEtapa.usuario = UsuarioActivo.usuario;

            // Renovación
            string noExpediente = txtNoExpediente.Text;
            AgregarRenovacion.idMarca = SeleccionarMarca.idN;
            AgregarRenovacion.fechaVencimientoAntigua = dateFechVencAnt.Value;
            AgregarRenovacion.fechaVencimientoNueva = dateFechVencNueva.Value;

            if (txtEstado.Text != "")
            {
                // Historial
                string fechaSinHora = dateTimePicker1.Value.ToShortDateString();
                string formato = fechaSinHora + " " + txtEstado.Text;
                if (anotaciones.Contains(formato))
                {
                    AgregarEtapa.anotaciones = anotaciones;
                }
                else
                {
                    AgregarEtapa.anotaciones = formato + " " + anotaciones;
                }

                try
                {
                    // Crear tareas
                    var guardarEtapaTask = Task.Run(() =>
                        historialModel.GuardarEtapa(
                            SeleccionarMarca.idN,
                            (DateTime)AgregarEtapa.fecha,
                            AgregarEtapa.etapa,
                            AgregarEtapa.anotaciones,
                            AgregarEtapa.usuario,
                            "TRÁMITE"
                        )
                    );

                    var addRenovacionTask = Task.Run(() =>
                        renovacionModel.AddRenovacion(
                            noExpediente,
                            AgregarRenovacion.idMarca,
                            AgregarRenovacion.fechaVencimientoAntigua,
                            AgregarRenovacion.fechaVencimientoNueva
                        )
                    );

                    await Task.WhenAll(guardarEtapaTask, addRenovacionTask);

                    AgregarRenovacion.renovacionTerminada = true;
                }
                catch (Exception ex)
                {
                    FrmAlerta alerta = new FrmAlerta(ex.Message.ToUpper(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    alerta.Show();
                }

                this.Close();
            }
            else
            {
                // MessageBox.Show("No ha seleccionado ningun estado");
            }
        }


        private void FrmAgregarRenovacionIntConcedida_Load(object sender, EventArgs e)
        {
            lblUser.Text = UsuarioActivo.usuario;
            lblUser.Visible = false;
            txtNoExpediente.Text = SeleccionarMarca.erenov;
            dateFechVencAnt.Value = AgregarRenovacion.fechaVencimientoAntigua;
            ActualizarFechaVencimientoNueva();
            richTextBox1.Text = dateTimePicker1.Value.ToShortDateString() + " " + txtEstado.Text;

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
            else
            {

            }
            
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
    }
}
