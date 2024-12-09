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
        }


        private void ActualizarFechaVencimientoNueva()
        {
            DateTime fechaVencA = dateFechVencAnt.Value;
            DateTime fecha_vencimientoN = fechaVencA.AddYears(10);
            dateFechVencNueva.Value = fecha_vencimientoN;
            AgregarRenovacion.fechaVencimientoNueva = fecha_vencimientoN;
        }

        public void RenovarMarca()
        {
            HistorialModel historialModel = new HistorialModel();
            RenovacionesMarcaModel renovacionModel = new RenovacionesMarcaModel();
            string anotaciones = richTextBox1.Text;
            AgregarEtapa.etapa = txtEstado.Text;
            AgregarEtapa.fecha = dateTimePicker1.Value;
            AgregarEtapa.usuario = UsuarioActivo.usuario;

            //renovacion
            string noExpediente = txtNoExpediente.Text;
            AgregarRenovacion.idMarca = SeleccionarMarca.idInt;
            AgregarRenovacion.fechaVencimientoAntigua = dateFechVencAnt.Value;
            AgregarRenovacion.fechaVencimientoNueva = dateFechVencNueva.Value;

            if (txtEstado.Text != "")
            {
                //Historial
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
                historialModel.GuardarEtapa(SeleccionarMarca.idInt, (DateTime)AgregarEtapa.fecha, AgregarEtapa.etapa, AgregarEtapa.anotaciones, AgregarEtapa.usuario, "TRÁMITE");

                try
                {
                    //Agregar a renovaciones
                    renovacionModel.AddRenovacion(noExpediente, AgregarRenovacion.idMarca, AgregarRenovacion.fechaVencimientoAntigua, AgregarRenovacion.fechaVencimientoNueva);

                    AgregarRenovacion.renovacionTerminada = true;

                }
                catch (Exception ex)
                {
                    FrmAlerta alerta = new FrmAlerta(ex.Message.ToUpper(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    alerta.Show();
                    //MessageBox.Show(ex.Message);
                }


                this.Close();
            }
            else
            {
                //MessageBox.Show("No ha seleccionado ningun estado");
            }
        }

        private void FrmAgregarRenovacionIntConcedida_Load(object sender, EventArgs e)
        {
            lblUser.Text = UsuarioActivo.usuario;
            lblUser.Visible = false;
            txtNoExpediente.Text = SeleccionarMarca.erenov;
            dateFechVencAnt.Value = AgregarRenovacion.fechaVencimientoAntigua;
            ActualizarFechaVencimientoNueva();

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

        private void iconButton3_Click(object sender, EventArgs e)
        {
            FrmAlerta alerta = new FrmAlerta("¿ESTÁ SEGURO DE RENOVAR LA MARCA?", "PREGUNTA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            DialogResult resultado = alerta.ShowDialog();
            if (resultado == DialogResult.Yes)
            {
                RenovarMarca();
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
