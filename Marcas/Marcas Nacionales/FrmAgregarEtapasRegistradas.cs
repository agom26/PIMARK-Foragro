using Comun.Cache;
using Dominio;

namespace Presentacion.Marcas_Nacionales
{
    public partial class FrmAgregarEtapasRegistradas : Form
    {
        public FrmAgregarEtapasRegistradas()
        {
            InitializeComponent();
        }

        private void ActualizarFechaVencimiento()
        {
            DateTime fecha_registro = dateFechRegAntigua.Value;
            DateTime fecha_vencimiento = fecha_registro.AddYears(10).AddDays(-1);
            dateFechVencAnt.Value = fecha_vencimiento;
        }
        private void ActualizarFechaVencimientoNueva()
        {
            DateTime fecha_registro = dateFechRegNueva.Value;
            DateTime fecha_vencimiento = fecha_registro.AddYears(10).AddDays(-1);
            dateFechVencNueva.Value = fecha_vencimiento;
        }

        private void FrmAgregarEtapasRegistradas_Load(object sender, EventArgs e)
        {
            lblUser.Text = UsuarioActivo.usuario;
            txtNoExpediente.Text = SeleccionarMarca.erenov;
            dateFechRegAntigua.Value = SeleccionarMarca.fechaRegistro.Value;
            ActualizarFechaVencimiento();

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
            HistorialModel historialModel = new HistorialModel();
            RenovacionesMarcaModel renovacionModel = new RenovacionesMarcaModel();
            string anotaciones = richTextBox1.Text;
            AgregarEtapa.etapa = txtEstado.Text;
            AgregarEtapa.fecha = dateTimePicker1.Value;
            AgregarEtapa.usuario = UsuarioActivo.usuario;

            //renovacion
            string noExpediente = txtNoExpediente.Text;
            int idMarca = SeleccionarMarca.idN;
            DateTime fechaRegistrA = dateFechRegAntigua.Value;
            DateTime fechaRegistroN=dateFechRegNueva.Value;
            DateTime fechaVenA=dateFechVencAnt.Value;
            DateTime fechaVenN = dateFechVencNueva.Value;

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
                historialModel.GuardarEtapa(SeleccionarMarca.idN, (DateTime)AgregarEtapa.fecha, AgregarEtapa.etapa, AgregarEtapa.anotaciones, AgregarEtapa.usuario);

                try
                {
                    //Agregar a renovaciones
                    renovacionModel.AddRenovacion(noExpediente, idMarca, fechaRegistrA, fechaVenA, fechaRegistroN, fechaVenN);
                    MessageBox.Show("Renovacion actualizada");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                

                this.Close();
            }
            else
            {
                //MessageBox.Show("No ha seleccionado ningun estado");
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
            ActualizarFechaVencimientoNueva();
        }

        private void dateFechRegAntigua_ValueChanged(object sender, EventArgs e)
        {
            ActualizarFechaVencimiento();
        }
    }
}
