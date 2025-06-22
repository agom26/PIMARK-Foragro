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

namespace Presentacion.Reportes
{
    public partial class FrmMostrarAgentesReportes : Form
    {
        PersonaModel personaModel = new PersonaModel();
        private const int pageSize = 20;
        private int currentPageIndex = 1;
        private int totalPages = 0;
        private int totalRows = 0;
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        public static extern void ReleaseCapture();


        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        public FrmMostrarAgentesReportes()
        {
            InitializeComponent();
            this.Load += FrmMostrarAgentesReportes_Load;
        }

        private async Task LoadAgentes()
        {
            totalRows = personaModel.GetTotalAgentes();
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
            // Obtiene los usuarios
            var titulares = await Task.Run(() => personaModel.GetAllAgentes(currentPageIndex, pageSize));

            if (this.IsHandleCreated && !this.IsDisposed)
            {
                Invoke(new Action(() =>
                {
                    lblTotalPages.Text = totalPages.ToString();
                    lblTotalRows.Text = totalRows.ToString();
                    dtgAgentes.DataSource = titulares;

                    if (dtgAgentes.Columns["id"] != null)
                    {
                        dtgAgentes.Columns["id"].Visible = false;
                        dtgAgentes.ClearSelection();
                    }


                }));
            }

        }
        public async void filtrar()
        {
            string buscar = txtBuscar.Text;
            if (buscar != "")
            {
                totalRows = personaModel.GetFilteredAgentesCount(txtBuscar.Text);
                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                DataTable titulares = personaModel.GetAgenteByValue(buscar, currentPageIndex, pageSize);
                if (titulares.Rows.Count > 0)
                {
                    dtgAgentes.DataSource = titulares;
                    if (dtgAgentes.Columns["id"] != null)
                    {
                        dtgAgentes.Columns["id"].Visible = false;
                        dtgAgentes.Columns["tipo"].Visible = false;
                    }
                    dtgAgentes.ClearSelection();
                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("NO EXISTEN AGENTES CON ESOS DATOS", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                    alerta.ShowDialog();
                    //MessageBox.Show("No existen titulares con esos datos");
                    await LoadAgentes();
                }
            }
            else
            {
                await LoadAgentes();
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

        private async void FrmMostrarAgentesReportes_Load(object sender, EventArgs e)
        {
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;

            if (screenWidth <= 1105 && screenHeight <= 600)
            {
                this.Size = new Size(750, 540);
                this.StartPosition = FormStartPosition.CenterScreen;
                this.AutoScroll = true;

                panelSuperior.Width = this.ClientSize.Width - 20;
                panelInferior.Width = this.ClientSize.Width - 20;

                tblLayoutPrincipal.Dock = DockStyle.Fill;
                panelInferior.Dock = DockStyle.Fill;
                panel1.Dock = DockStyle.Fill;
                panel2.Dock = DockStyle.Fill;

                dtgAgentes.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Century Gothic", 8, FontStyle.Bold);

                dtgAgentes.DefaultCellStyle.Font = new System.Drawing.Font("Century Gothic", 8);

            }
            else
            {

                dtgAgentes.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Century Gothic", 10, FontStyle.Bold);
                dtgAgentes.DefaultCellStyle.Font = new System.Drawing.Font("Century Gothic", 10);
                // Pantalla grande → centrar los paneles
                CentrarPanel(panelSuperior);
                CentrarPanel(panelInferior);
            }
            CentrarControles();
            // Cargar usuarios en segundo plano
            await Task.Run(() => LoadAgentes());
            currentPageIndex = 1;
            lblCurrentPage.Text = currentPageIndex.ToString();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            SeleccionarPersonaReportes.LimpiarAgente();

            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SeleccionarPersonaReportes.LimpiarAgente();
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            filtrar();
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            if (dtgAgentes.RowCount <= 0)
            {
                FrmAlerta alerta = new FrmAlerta("NO HAY DATOS PARA SELECCIONAR", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                //MessageBox.Show("No hay datos para seleccionar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (dtgAgentes.SelectedRows.Count > 0)
            {

                var filaSeleccionada = dtgAgentes.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {

                    int id = Convert.ToInt32(dataRowView["id"]);
                    SeleccionarPersonaReportes.idAgente = id;


                    var detallesAgente = personaModel.GetPersonaById(id);
                    if (detallesAgente.Count > 0)
                    {
                        SeleccionarPersonaReportes.nombreAgente = detallesAgente[0].nombre;
                        //MessageBox.Show("ID seleccionado: " + SeleccionarPersona.idPersona);
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron detalles del agente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    this.Close();
                }
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("SELECCIONE UNA FILA", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                //MessageBox.Show("Por favor seleccione una fila", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            filtrar();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                filtrar();
            }
        }

        private async void btnFirst_Click(object sender, EventArgs e)
        {
            currentPageIndex = 1;
            if (txtBuscar.Text != "")
            {
                filtrar();
            }
            else
            {
                await LoadAgentes();
            }

            lblCurrentPage.Text = currentPageIndex.ToString();
        }

        private async void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPageIndex > 1)
            {
                currentPageIndex--;
                if (txtBuscar.Text != "")
                {
                    filtrar();
                }
                else
                {
                    await LoadAgentes();
                }

                lblCurrentPage.Text = currentPageIndex.ToString();
            }
        }

        private async void btnNext_Click(object sender, EventArgs e)
        {
            if (currentPageIndex < totalPages)
            {
                currentPageIndex++;
                if (txtBuscar.Text != "")
                {
                    filtrar();
                }
                else
                {
                    await LoadAgentes();
                }

                lblCurrentPage.Text = currentPageIndex.ToString();
            }
        }

        private async void btnLast_Click(object sender, EventArgs e)
        {
            currentPageIndex = totalPages;
            if (txtBuscar.Text != "")
            {
                filtrar();
            }
            else
            {
                await LoadAgentes();
            }

            lblCurrentPage.Text = currentPageIndex.ToString();
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

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBuscar_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                filtrar();
            }
        }

        private void btnX_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            filtrar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            filtrar();
        }
    }
}
