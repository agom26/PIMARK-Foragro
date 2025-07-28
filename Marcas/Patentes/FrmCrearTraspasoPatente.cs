using Comun.Cache;
using Dominio;
using Presentacion.Alertas;

namespace Presentacion.Patentes
{
    public partial class FrmCrearTraspasoPatente : Form
    {
        public FrmCrearTraspasoPatente()
        {
            InitializeComponent();
            this.Load += FrmCrearTraspasoPatente_Load;
        }

        private void ActualizarFechaVencimiento()
        {

        }

        private void ActualizarFechaVencimientoNueva()
        {


        }

        private void FrmCrearTraspasoPatente_Load(object sender, EventArgs e)
        {
            lblUser.Text = UsuarioActivo.usuario;
            lblUser.Visible = false;
            txtNoExpediente.Text = SeleccionarPatente.Etrasp;
            txtNombreTitularA.Text = AgregarTraspasoPatente.nombreTitulara;
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
            AgregarTraspasoPatente.LimpiarTraspaso();
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            PatenteModel patenteModel = new PatenteModel();
            string anotaciones = richTextBox1.Text;
            string etapa = txtEstado.Text;
            DateTime fecha = dateTimePicker1.Value;
            string usuario = UsuarioActivo.usuario;

            // Datos para el traspaso
            string noExpediente = txtNoExpediente.Text;
            int idPatente = SeleccionarPatente.id;
            string nuevoTitular = txtNombreTitularN.Text;

            if (!string.IsNullOrEmpty(etapa)) // Verifica si se ha ingresado un estado
            {
                if (!string.IsNullOrEmpty(nuevoTitular)) // Verifica si hay un titular nuevo
                {
                    // Formatear anotaciones con fecha y etapa
                    string fechaSinHora = fecha.ToString("dd/MM/yyyy");
                    string formato = fechaSinHora + " " + etapa;
                    if (!anotaciones.Contains(formato))
                    {
                        anotaciones = formato + " " + anotaciones;
                    }

                    try
                    {
                        int idTitularNuevo = AgregarTraspasoPatente.idNuevoTitular;
                        int idTitularviejo = AgregarTraspasoPatente.idTitularAnterior;

                        // Llamar al método que ejecuta la transacción
                        patenteModel.InsertarTraspasoYHistorial(
                            noExpediente, idPatente, idTitularviejo, idTitularNuevo,
                            fecha, etapa, anotaciones, usuario, null
                        );

                        AgregarTraspasoPatente.traspasoFinalizado = true;
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        FrmAlerta alerta = new FrmAlerta(ex.Message.ToUpper(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        alerta.ShowDialog();
                        this.Close();
                    }
                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR UN TITULAR NUEVO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    alerta.ShowDialog();
                }
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
            ActualizarFechaVencimientoNueva();
        }

        private void dateFechRegAntigua_ValueChanged(object sender, EventArgs e)
        {
            ActualizarFechaVencimiento();
        }

        private void roundedButton2_Click(object sender, EventArgs e)
        {
            FrmMostrarTitularesTraspasoPatente frmCrearTraspaso = new FrmMostrarTitularesTraspasoPatente();
            frmCrearTraspaso.ShowDialog();

            if (AgregarTraspasoPatente.idNuevoTitular != 0)
            {
                txtNombreTitularN.Text = AgregarTraspasoPatente.nombreTitularN;

            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("NO SELECCIONÓ UN TITULAR NUEVO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                //MessageBox.Show("No selecciono un nuevo titular");
            }

        }
    }
}
