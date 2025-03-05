using Comun.Cache;
using Dominio;
using Presentacion.Alertas;
using Presentacion.Marcas_Nacionales;
namespace Presentacion.Marcas_Internacionales
{
    public partial class FrmCrearTraspasoInt : Form
    {
        public FrmCrearTraspasoInt()
        {
            InitializeComponent();
        }

        private void ActualizarFechaVencimiento()
        {
            
        }

        private void ActualizarFechaVencimientoNueva()
        {
            
           
        }

        private void FrmCrearTraspasoInt_Load(object sender, EventArgs e)
        {
            lblUser.Text = UsuarioActivo.usuario;
            lblUser.Visible = false;
            txtNoExpediente.Text = SeleccionarMarca.etraspaso;
           txtNombreTitularA.Text=AgregarTraspaso.nombreTitulara;
            richTextBox1.Text= dateTimePicker1.Value.ToShortDateString()+" "+txtEstado.Text;
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
            AgregarTraspaso.LimpiarTraspaso();
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            // Instancias de modelos
            MarcaModel marcaModel = new MarcaModel();

            string anotaciones = richTextBox1.Text;
            AgregarEtapa.etapa = txtEstado.Text;
            AgregarEtapa.fecha = dateTimePicker1.Value;
            AgregarEtapa.usuario = UsuarioActivo.usuario;

            // Datos del traspaso
            string noExpediente = txtNoExpediente.Text;
            int idMarca = SeleccionarMarca.idN;
            string nuevoTitular = txtNombreTitularN.Text;

            if (!string.IsNullOrEmpty(txtEstado.Text) && !string.IsNullOrEmpty(txtNoExpediente.Text))
            {
                if (!string.IsNullOrEmpty(nuevoTitular))
                {
                    // Formatear anotaciones con fecha y etapa
                    string fechaSinHora = dateTimePicker1.Value.ToShortDateString();
                    string formato = fechaSinHora + " " + txtEstado.Text;
                    AgregarEtapa.anotaciones = anotaciones.Contains(formato) ? anotaciones : formato + " " + anotaciones;

                    try
                    {
                        int idTitularNuevo = AgregarTraspaso.idNuevoTitular;
                        int idTitularViejo = AgregarTraspaso.idTitularAnterior;

                        // Llamar al método con transacción en MarcaModel
                        marcaModel.InsertarTraspasoYHistorialMarca(
                            noExpediente, idMarca, idTitularViejo, idTitularNuevo,
                            (DateTime)AgregarEtapa.fecha, AgregarEtapa.etapa,
                            AgregarEtapa.anotaciones, AgregarEtapa.usuario
                        );

                        AgregarTraspaso.traspasoFinalizado = true;
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
                    FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR UN TITULAR Y NOMBRE NUEVO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    alerta.ShowDialog();
                }
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR EL NÚMERO DE EXPEDIENTE", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
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
            FrmMostrarTitularesTraspaso frmCrearTraspaso = new FrmMostrarTitularesTraspaso();
            frmCrearTraspaso.ShowDialog();

            if (AgregarTraspaso.idNuevoTitular != 0)
            {
                txtNombreTitularN.Text = AgregarTraspaso.nombreTitularN;

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
