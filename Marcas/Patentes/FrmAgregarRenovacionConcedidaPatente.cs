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

        public void RenovarMarca()
        {
            HistorialPatenteModel historialModel = new HistorialPatenteModel();
            RenovacionesPatenteModel renovacionModel = new RenovacionesPatenteModel();
            string anotaciones = richTextBox1.Text;
            AgregarEtapaPatente.etapa = txtEstado.Text;
            AgregarEtapaPatente.fecha = dateTimePicker1.Value;
            AgregarEtapaPatente.usuario = UsuarioActivo.usuario;

            //renovacion
            string noExpediente = txtNoExpediente.Text;
            AgregarRenovacionPatente.idPatente= SeleccionarPatente.id;
            AgregarRenovacionPatente.fechaVencimientoAntigua = dateFechVencAnt.Value;
            AgregarRenovacionPatente.fechaVencimientoNueva = dateFechVencNueva.Value;

            if (txtEstado.Text != "")
            {
                //Historial
                string fechaSinHora = dateTimePicker1.Value.ToShortDateString();
                string formato = fechaSinHora + " " + txtEstado.Text;
                if (anotaciones.Contains(formato))
                {
                    AgregarEtapaPatente.anotaciones = anotaciones;
                }
                else
                {
                    AgregarEtapaPatente.anotaciones = formato + " " + anotaciones;
                }
                historialModel.CrearHistorialPatente((DateTime)AgregarEtapaPatente.fecha, AgregarEtapaPatente.etapa, AgregarEtapaPatente.anotaciones, AgregarEtapaPatente.usuario, null, SeleccionarPatente.id);

                try
                {
                    //Agregar a renovaciones
                    renovacionModel.AddRenovacion(noExpediente, AgregarRenovacionPatente.idPatente, AgregarRenovacionPatente.fechaVencimientoAntigua, AgregarRenovacionPatente.fechaVencimientoNueva);

                    AgregarRenovacionPatente.renovacionTerminada = true;

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

        private void FrmAgregarRenovacionConcedidaPatente_Load(object sender, EventArgs e)
        {
            lblUser.Text = UsuarioActivo.usuario;
            lblUser.Visible = false;
            txtNoExpediente.Text = SeleccionarPatente.Erenov;
            dateFechVencAnt.Value = AgregarRenovacionPatente.fechaVencimientoAntigua;
            ActualizarFechaVencimientoNueva();

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
