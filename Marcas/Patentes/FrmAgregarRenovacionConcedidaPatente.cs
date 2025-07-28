using Comun.Cache;
using Dominio;
using Presentacion.Alertas;

namespace Presentacion.Patentes
{
    public partial class FrmAgregarRenovacionConcedidaPatente : Form
    {
        public FrmAgregarRenovacionConcedidaPatente()
        {
            InitializeComponent();
            this.Load += FrmAgregarRenovacionConcedidaPatente_Load;
        }


        private void ActualizarFechaVencimientoNueva()
        {
            DateTime fechaVencA = dateFechVencAnt.Value;
            DateTime fecha_vencimientoN = fechaVencA.AddYears(20);
            dateFechVencNueva.Value = fecha_vencimientoN;
            AgregarRenovacionPatente.fechaVencimientoNueva = fecha_vencimientoN;
        }

        public async Task RenovarMarca()
        {
            PatenteModel patenteModel= new PatenteModel();

            string anotaciones = richTextBox1.Text;
            string noExpediente = txtNoExpediente.Text;

            int idPatente = SeleccionarPatente.id;
            DateTime fechaVencAnt = dateFechVencAnt.Value;
            DateTime fechaVencNueva = dateFechVencNueva.Value;
            DateTime fecha = dateTimePicker1.Value;
            string etapa = txtEstado.Text;
            string usuario = UsuarioActivo.usuario;
            AgregarRenovacionPatente.idPatente = SeleccionarPatente.id;
            AgregarRenovacionPatente.fechaVencimientoAntigua = dateFechVencAnt.Value;
            AgregarRenovacionPatente.fechaVencimientoNueva = dateFechVencNueva.Value;

            if (string.IsNullOrEmpty(etapa))
            {
                MessageBox.Show("No ha seleccionado ningún estado.");
                return;
            }

            // Construcción de anotaciones
            string fechaSinHora = fecha.ToString("dd/MM/yyyy");
            string formato = fechaSinHora + " " + etapa;
            string anotacionesFinales = anotaciones.Contains(formato) ? anotaciones : formato + " " + anotaciones;

            try
            {
                bool resultado = await Task.Run(() =>
                    patenteModel.RenovarPatente(noExpediente, idPatente, fechaVencAnt, fechaVencNueva, fecha, etapa, anotacionesFinales, usuario)
                );

                if (resultado)
                {

                    AgregarRenovacionPatente.renovacionTerminada = true;
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


        private void FrmAgregarRenovacionConcedidaPatente_Load(object sender, EventArgs e)
        {
            lblUser.Text = UsuarioActivo.usuario;
            lblUser.Visible = false;
            txtNoExpediente.Text = SeleccionarPatente.Erenov;
            dateFechVencAnt.Value = AgregarRenovacionPatente.fechaVencimientoAntigua;
            ActualizarFechaVencimientoNueva();
            richTextBox1.Text = dateTimePicker1.Value.ToString("dd/MM/yyyy") + " " + txtEstado.Text;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AgregarEtapaPatente.LimpiarEtapa();
            this.Close();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {

            this.Close();
            AgregarEtapaPatente.LimpiarEtapa();
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = dateTimePicker1.Value.ToString("dd/MM/yyyy") + " " + txtEstado.Text;
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
