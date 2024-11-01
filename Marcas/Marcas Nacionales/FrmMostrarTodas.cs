using Comun.Cache;
using Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Marcas_Nacionales
{
    public partial class FrmMostrarTodas : Form
    {
        MarcaModel marcaModel = new MarcaModel();
        PersonaModel personaModel = new PersonaModel();
        public FrmMostrarTodas()
        {
            InitializeComponent();
        }

        private void EliminarTabPage(TabPage nombre)
        {
            if (tabControl1.TabPages.Contains(nombre))
            {
                tabControl1.TabPages.Remove(nombre);
            }
        }
        public void LimpiarCampos()
        {

        }
        private void MostrarAgentes()
        {
            dtgMarcasN.DataSource = marcaModel.GetAllMarcasNacionales();
            // Ocultar la columna 'id'
            if (dtgMarcasN.Columns["id"] != null)
            {
                dtgMarcasN.Columns["id"].Visible = false;

                // Desactiva la selección automática de la primera fila
                dtgMarcasN.ClearSelection();
            }
        }
        private void LoadMarcas()
        {
            // Obtiene los usuarios
            var marcasN = marcaModel.GetAllMarcasNacionalesEnTramite();

            // Invoca el método para actualizar el DataGridView en el hilo principal
            Invoke(new Action(() =>
            {
                dtgMarcasN.DataSource = marcasN;

                // Oculta la columna 'id'
                if (dtgMarcasN.Columns["id"] != null)
                {
                    dtgMarcasN.Columns["id"].Visible = false;
                    // Desactiva la selección automática de la primera fila
                    dtgMarcasN.ClearSelection();
                }
            }));
        }
        private void AnadirTabPage(TabPage nombre)
        {
            if (!tabControl1.TabPages.Contains(nombre))
            {
                tabControl1.TabPages.Add(nombre);
            }
            // Muestra el TabPage especificado (lo selecciona)
            tabControl1.SelectedTab = nombre;
        }

        // Método para mostrar el logo en un PictureBox
        public void MostrarLogoEnPictureBox(byte[] logo)
        {
            if (logo != null && logo.Length > 0) // Verificar que el logo no esté vacío
            {
                using (var ms = new MemoryStream(logo)) // Crear un MemoryStream a partir del array de bytes
                {
                    pictureBox1.Image = Image.FromStream(ms); // Convertir el MemoryStream a imagen y asignarlo al PictureBox
                }
            }
            else
            {
                pictureBox1.Image = null; // Si el logo es nulo, limpiar la imagen del PictureBox
            }
        }

        private async void EditarAsync()
        {
            if (dtgMarcasN.RowCount <= 0)
            {
                MessageBox.Show("No hay datos para seleccionar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dtgMarcasN.SelectedCells.Count >= 1)
            {
                var filaSeleccionada = dtgMarcasN.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                    int id = Convert.ToInt32(dataRowView["id"]);
                    SeleccionarMarca.idN = id;

                    // Obtén los detalles de la marca en segundo plano
                    var detallesMarcaN = await Task.Run(() => marcaModel.GetMarcaNacionalById(id));

                    if (detallesMarcaN.Count > 0)
                    {
                        // Verifica que el primer elemento de detallesMarcaN no sea nulo
                        if (detallesMarcaN[0].registro != null)
                        {
                            SeleccionarMarca.expediente = detallesMarcaN[0].expediente;
                            SeleccionarMarca.nombre = detallesMarcaN[0].nombre;
                            SeleccionarMarca.clase = detallesMarcaN[0].clase;
                            SeleccionarMarca.signoDistintivo = detallesMarcaN[0].signoDistintivo;
                            SeleccionarMarca.logo = detallesMarcaN[0].logo;
                            SeleccionarMarca.idPersonaTitular = detallesMarcaN[0].idTitular;
                            SeleccionarMarca.idPersonaAgente = detallesMarcaN[0].idAgente;
                            SeleccionarMarca.fecha_solicitud = (DateTime)detallesMarcaN[0].fechaSolicitud;
                            SeleccionarMarca.estado = detallesMarcaN[0].estado;

                            // Verifica si `registro` es distinto de `null`
                            if (detallesMarcaN[0].registro == null)
                            {
                                SeleccionarMarca.registro = detallesMarcaN[0].registro;
                                SeleccionarMarca.folio = detallesMarcaN[0].folio;
                                SeleccionarMarca.libro = detallesMarcaN[0].libro;
                                SeleccionarMarca.fechaRegistro = (DateTime)detallesMarcaN[0].fechaRegistro;
                                SeleccionarMarca.fechaVencimiento = (DateTime)detallesMarcaN[0].fechaVencimiento;

                                txtRegistro.Text = SeleccionarMarca.registro;
                                txtFolio.Text = SeleccionarMarca.folio;
                                txtLibro.Text = SeleccionarMarca.libro;
                                dateTimePFecha_Registro.Value = SeleccionarMarca.fechaRegistro;
                                dateTimePFecha_vencimiento.Value = SeleccionarMarca.fechaVencimiento;
                            }
                            else
                            {
                                //MessageBox.Show("El campo 'registro' es nulo.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                            // Cargar los detalles de titular y agente en segundo plano
                            var titular = await Task.Run(() => personaModel.GetPersonaById(SeleccionarMarca.idPersonaTitular));
                            var agente = await Task.Run(() => personaModel.GetPersonaById(SeleccionarMarca.idPersonaAgente));

                            if (titular.Count > 0)
                            {
                                txtNombreTitular.Text = titular[0].nombre;
                                txtDireccionTitular.Text = titular[0].direccion;
                                txtEntidadTitular.Text = titular[0].pais;
                            }

                            if (agente.Count > 0)
                            {
                                txtNombreAgente.Text = agente[0].nombre;
                            }

                            // Actualizar los controles de la interfaz con los datos obtenidos
                            AnadirTabPage(tabPageMarcaDetail);
                            txtExpediente.Text = SeleccionarMarca.expediente;
                            txtNombre.Text = SeleccionarMarca.nombre;
                            txtClase.Text = SeleccionarMarca.clase;
                            txtSignoDistintivo.Text = SeleccionarMarca.signoDistintivo;
                            MostrarLogoEnPictureBox(SeleccionarMarca.logo);
                            datePickerFechaSolicitud.Value = SeleccionarMarca.fecha_solicitud;
                            //cmbEstado.SelectedText = SeleccionarMarca.estado;
                        }
                        else
                        {
                            MessageBox.Show("No se encontró la marca seleccionada.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron detalles de la marca", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }


        private void ibtnAgregar_Click(object sender, EventArgs e)
        {

        }

        private async void FrmMostrarTodas_Load(object sender, EventArgs e)
        {
            // Cargar usuarios en segundo plano
            await Task.Run(() => LoadMarcas());

            // Eliminar la tabPage de detalle
            tabControl1.TabPages.Remove(tabPageMarcaDetail);
        }

        private void ibtnEditar_Click(object sender, EventArgs e)
        {
            EditarAsync();
        }

        private void ibtnEliminar_Click(object sender, EventArgs e)
        {

        }
    }
}
