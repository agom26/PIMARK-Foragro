using Comun.Cache;
using Dominio;
using Presentacion.Alertas;

namespace Presentacion.Marcas_Nacionales
{
    public partial class FrmEnviarAOposicion : Form
    {
        public FrmEnviarAOposicion()
        {
            InitializeComponent();
            this.Load += FrmEnviarAOposicion_Load;
            txtSolicitante.Text = AgregarEtapa.solicitante;
            lblUser.Text = UsuarioActivo.usuario;
            lblUser.Visible = false;
        }

        private void ActualizarFechaVencimiento()
        {

        }

        private void ActualizarFechaVencimientoNueva()
        {


        }

        private void FrmEnviarAOposicion_Load(object sender, EventArgs e)
        {
            txtSolicitante.Text = AgregarEtapa.solicitante;
            lblUser.Text = UsuarioActivo.usuario;
            lblUser.Visible = false;
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
            OposicionModel oposicionModel = new OposicionModel();
            string anotaciones = richTextBox1.Text;
            string opositor=txtNombreOpositor.Text;
            string solicitante = txtSolicitante.Text;
            string situacion_actual = "EN TRÁMITE";
            AgregarEtapa.etapa = txtEstado.Text;
            AgregarEtapa.fecha = dateTimePicker1.Value;
            AgregarEtapa.usuario = UsuarioActivo.usuario;
            int idSolicitante = SeleccionarMarca.idPersonaTitular;


            //traspaso
            int idMarca = SeleccionarMarca.idInt;
            string nuevoTitular = txtSolicitante.Text;

            if (txtEstado.Text != "" && txtSolicitante.Text!="")
            {
                string fechaSinHora = dateTimePicker1.Value.ToShortDateString();
                string formato = fechaSinHora + " " +txtEstado.Text;
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
                    oposicionModel.CrearOposicion(SeleccionarMarca.expediente, SeleccionarMarca.nombre,
                        SeleccionarMarca.signoDistintivo, SeleccionarMarca.clase,
                        solicitante, SeleccionarMarca.idPersonaTitular, null, opositor, null,
                        situacion_actual, idMarca, null, SeleccionarMarca.logo);
                    historialModel.GuardarEtapa(idMarca, (DateTime)AgregarEtapa.fecha, AgregarEtapa.etapa, AgregarEtapa.anotaciones, AgregarEtapa.usuario, "TRÁMITE");
                    
                    FrmAlerta alerta = new FrmAlerta("MARCA ENVIADA A OPOSICIÓN", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alerta.ShowDialog();
                }
                catch (Exception ex){
                    MessageBox.Show("Error: "+ex.Message);
                }


            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR EL SOLICITANTE DEL SIGNO PRETENDIDO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
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
            

        }
    }
}
