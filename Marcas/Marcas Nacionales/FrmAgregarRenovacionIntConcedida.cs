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
