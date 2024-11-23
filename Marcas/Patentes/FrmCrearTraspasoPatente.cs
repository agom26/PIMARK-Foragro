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
            HistorialPatenteModel historialModel = new HistorialPatenteModel();
            TraspasosPatenteModel traspasosPatenteModel = new TraspasosPatenteModel();
            string anotaciones = richTextBox1.Text;
            AgregarEtapaPatente.etapa = txtEstado.Text;
            AgregarEtapaPatente.fecha = dateTimePicker1.Value;
            AgregarEtapaPatente.usuario = UsuarioActivo.usuario;

            //traspaso
            string noExpediente = txtNoExpediente.Text;
            int idPatente = SeleccionarPatente.id;
            string nuevoTitular = txtNombreTitularN.Text;

            if (txtEstado.Text != "")
            {
                if (nuevoTitular != "")
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
                    historialModel.CrearHistorialPatente( (DateTime)AgregarEtapaPatente.fecha, AgregarEtapaPatente.etapa, AgregarEtapaPatente.anotaciones, AgregarEtapaPatente.usuario,null,SeleccionarPatente.id);

                    try
                    {
                        int idTitularNuevo = AgregarTraspasoPatente.idNuevoTitular;
                        int idTitularviejo = AgregarTraspasoPatente.idTitularAnterior;
                        
                        //Agregar a traspasos
                        traspasosPatenteModel.AddTraspaso(noExpediente, idPatente, idTitularviejo, idTitularNuevo);
                        AgregarTraspasoPatente.traspasoFinalizado = true;
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        FrmAlerta alerta = new FrmAlerta(ex.Message.ToUpper(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        alerta.ShowDialog();
                        //MessageBox.Show(ex.Message);
                        this.Close();
                    }
                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR UN TITULAR NUEVO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    alerta.ShowDialog();
                    //MessageBox.Show("Debe ingresar un titular y un nombre nuevo");
                }
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
