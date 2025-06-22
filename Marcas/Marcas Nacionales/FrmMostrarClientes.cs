using Comun.Cache;
using DocumentFormat.OpenXml.Wordprocessing;
using Dominio;
using Presentacion.Alertas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Marcas_Internacionales
{
    public partial class FrmMostrarClientes : Form
    {
        PersonaModel personaModel = new PersonaModel();
        private const int pageSize = 20;
        private int currentPageIndex = 1;
        private int totalPages = 0;
        private int totalRows = 0;
        private bool buscando = false;
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        public static extern void ReleaseCapture();


        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        public FrmMostrarClientes()
        {
            InitializeComponent();
            this.Load += FrmMostrarClientes_Load;
        }

        private async Task LoadClientes()
        {
            totalRows = personaModel.GetTotalClientes();
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
            // Obtiene los usuarios
            var titulares = await Task.Run(() => personaModel.GetAllClientes(currentPageIndex, pageSize));

            Invoke(new Action(() =>
            {
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                dtgClientes.DataSource = titulares;

                if (dtgClientes.Columns["id"] != null)
                {
                    dtgClientes.Columns["id"].Visible = false;
                    dtgClientes.ClearSelection();
                }


            }));
        }
        public async void filtrar()
        {
            string buscar = txtBuscar.Text;
            if (buscar != "")
            {
                totalRows = personaModel.GetFilteredClientesCount(txtBuscar.Text);
                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                DataTable titulares = personaModel.GetClienteByValue(buscar, currentPageIndex, pageSize);
                if (titulares.Rows.Count > 0)
                {
                    dtgClientes.DataSource = titulares;
                    if (dtgClientes.Columns["id"] != null)
                    {
                        dtgClientes.Columns["id"].Visible = false;
                        dtgClientes.Columns["tipo"].Visible = false;
                    }
                    dtgClientes.ClearSelection();
                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("NO EXISTEN CLIENTES CON ESOS DATOS", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                    alerta.ShowDialog();
                    //MessageBox.Show("No existen titulares con esos datos");
                    await LoadClientes();
                }
            }
            else
            {
                await LoadClientes();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SeleccionarPersona.idPersonaC = 0;
            this.Close();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            SeleccionarPersona.idPersonaC = 0;
            this.Close();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            buscando = true;
            currentPageIndex = 1;
            totalRows = personaModel.GetFilteredClientesCount(txtBuscar.Text);
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

            lblCurrentPage.Text = currentPageIndex.ToString();
            lblTotalPages.Text = totalPages.ToString();
            lblTotalRows.Text = totalRows.ToString();
            filtrar();
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            if (dtgClientes.RowCount <= 0)
            {
                FrmAlerta alerta = new FrmAlerta("NO HAY DATOS PARA SELECCIONAR", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                //MessageBox.Show("No hay datos para seleccionar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dtgClientes.SelectedRows.Count > 0)
            {
                buscando = false;
                var filaSeleccionada = dtgClientes.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {

                    int id = Convert.ToInt32(dataRowView["id"]);
                    SeleccionarPersona.idPersonaC = id;
                    //MessageBox.Show("id" + SeleccionarPersona.idPersonaC);
                    var detallesCliente = personaModel.GetPersonaById(id);

                    if (detallesCliente.Count > 0)
                    {
                        //MessageBox.Show("ID seleccionado: " + SeleccionarPersona.idPersona);

                        SeleccionarPersona.nombre = detallesCliente[0].nombre;
                        SeleccionarPersona.direccion = detallesCliente[0].direccion;
                        SeleccionarPersona.correo = detallesCliente[0].correo;
                        SeleccionarPersona.contacto = detallesCliente[0].contacto;
                        SeleccionarPersona.pais = detallesCliente[0].pais;
                        SeleccionarPersona.nit = detallesCliente[0].nit;
                        SeleccionarPersona.telefono = detallesCliente[0].telefono;

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron detalles del clente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("SELECCIONE UNA FILA", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                //MessageBox.Show("Por favor seleccione una fila", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void CentrarPanel(Panel panel)
        {
            int anchoMinimo = panel.Width + 100;

            if (this.ClientSize.Width >= anchoMinimo)
            {
                panel.Anchor = AnchorStyles.Top;
                int x = (this.ClientSize.Width - panel.Width) / 2;
                int y = panel.Location.Y;
                panel.Location = new Point(x, y);
            }
            else
            {
                panel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                panel.Location = new Point(0, panel.Location.Y);
            }
        }

        private void CentrarControles()
        {
            int espacioEntreControles = 5;

            int totalWidth = txtBuscar.Width + espacioEntreControles +
                             btnX.Width + espacioEntreControles +
                             btnBuscar.Width;

            int startX = (panel1.Width - totalWidth) / 2;
            int centerY = (panel1.Height - txtBuscar.Height) / 2;

            txtBuscar.Location = new Point(startX, centerY);
            btnX.Location = new Point(txtBuscar.Right + espacioEntreControles, centerY);
            btnBuscar.Location = new Point(btnX.Right + espacioEntreControles, centerY);
        }


        private async void FrmMostrarClientes_Load(object sender, EventArgs e)
        {
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;

            if (screenWidth <= 1105 && screenHeight <= 600)
            {
                this.Size = new Size(750, 540);
                this.StartPosition = FormStartPosition.CenterScreen;
                this.AutoScroll = true;

                panelSuperior.Width = this.ClientSize.Width - 20;
                BtnSalir.Dock = DockStyle.Right;
                panelInferior.Width = this.ClientSize.Width - 20;

                tblLayoutPrincipal.Dock = DockStyle.Fill;
                panel1.Dock = DockStyle.Fill;
                panel2.Dock = DockStyle.Fill;
                panel3.Dock = DockStyle.Fill;
                CentrarControles();
                dtgClientes.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Century Gothic", 8, FontStyle.Bold);

                dtgClientes.DefaultCellStyle.Font = new System.Drawing.Font("Century Gothic", 8);

            }
            else
            {
                CentrarControles();
                dtgClientes.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Century Gothic", 10, FontStyle.Bold);
                dtgClientes.DefaultCellStyle.Font = new System.Drawing.Font("Century Gothic", 10);
                // Pantalla grande → centrar los paneles
                CentrarPanel(panelSuperior);
                CentrarPanel(panelInferior);
            }

            await Task.Run(() => LoadClientes());
            currentPageIndex = 1;
            lblCurrentPage.Text = currentPageIndex.ToString();
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            buscando = false;
            txtBuscar.Text = "";
            filtrar();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buscando = true;
                currentPageIndex = 1;
                totalRows = personaModel.GetFilteredClientesCount(txtBuscar.Text);
                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                lblCurrentPage.Text = currentPageIndex.ToString();
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                filtrar();
            }
        }

        private async void btnFirst_Click(object sender, EventArgs e)
        {
            currentPageIndex = 1;
            if (buscando == true)
            {
                filtrar();
            }
            else
            {
                await LoadClientes();
            }

            lblCurrentPage.Text = currentPageIndex.ToString();
        }

        private async void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPageIndex > 1)
            {
                currentPageIndex--;
                if (buscando == true)
                {
                    filtrar();
                }
                else
                {
                    await LoadClientes();
                }

                lblCurrentPage.Text = currentPageIndex.ToString();
            }
        }

        private async void btnNext_Click(object sender, EventArgs e)
        {
            if (currentPageIndex < totalPages)
            {
                currentPageIndex++;
                if (buscando == true)
                {
                    filtrar();
                }
                else
                {
                    await LoadClientes();
                }

                lblCurrentPage.Text = currentPageIndex.ToString();
            }
        }

        private async void btnLast_Click(object sender, EventArgs e)
        {
            currentPageIndex = totalPages;
            if (buscando == true)
            {
                filtrar();
            }
            else
            {
                await LoadClientes();
            }

            lblCurrentPage.Text = currentPageIndex.ToString();
        }

        private void panelSuperior_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelSuperior_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xF012, 0);
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_NCCALCSIZE = 0x83;
            if (m.Msg == WM_NCCALCSIZE && m.WParam.ToInt32() == 1)
            {
                m.Result = new IntPtr(0xF0);   // Align client area to all borders
                return;
            }
            base.WndProc(ref m);
        }
    }
}
