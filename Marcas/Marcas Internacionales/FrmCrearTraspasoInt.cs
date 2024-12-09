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
            txtNombreMarcaA.Text = AgregarTraspaso.antiguoNombre;
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
            HistorialModel historialModel = new HistorialModel();
            TraspasosMarcaModel traspasosMarcaModel = new TraspasosMarcaModel();
            string anotaciones = richTextBox1.Text;
            AgregarEtapa.etapa = txtEstado.Text;
            AgregarEtapa.fecha = dateTimePicker1.Value;
            AgregarEtapa.usuario = UsuarioActivo.usuario;

            //traspaso
            string noExpediente = txtNoExpediente.Text;
            int idMarca = SeleccionarMarca.idInt;
            string nombreNuevoMarca = txtNombreMarcaN.Text;
            string nuevoTitular = txtNombreTitularN.Text;

            if (txtEstado.Text != "")
            {
                if (nombreNuevoMarca != "" || nuevoTitular !="")
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
                    historialModel.GuardarEtapa(idMarca, (DateTime)AgregarEtapa.fecha, AgregarEtapa.etapa, AgregarEtapa.anotaciones, AgregarEtapa.usuario, "TRÁMITE");

                    try
                    {
                        int idTitularNuevo = AgregarTraspaso.idNuevoTitular;
                        int idTitularviejo = AgregarTraspaso.idTitularAnterior;
                        string nombreViejo = txtNombreMarcaA.Text;
                        //Agregar a traspasos
                        traspasosMarcaModel.AddTraspaso(noExpediente, idMarca, idTitularviejo, idTitularNuevo);
                        AgregarTraspaso.traspasoFinalizado = true;
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        this.Close();
                    }
                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR UN TITULAR Y NOMBRE NUEVO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
