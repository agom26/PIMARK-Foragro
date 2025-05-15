using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Alertas
{
    public partial class FrmAlerta : Form
    {
        private int conteo;
        private Image ConvertByteArrayToImage(byte[] byteArray)
        {
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }

        public FrmAlerta(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            InitializeComponent();
            richTextBoxMensaje.Text = message;
            richTextBoxMensaje.SelectAll();
            richTextBoxMensaje.SelectionAlignment = HorizontalAlignment.Center;
            richTextBoxMensaje.DeselectAll();
            lblTitulo.Text = title;
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;

            this.Text = "";

            switch (icon)
            {
                case MessageBoxIcon.Information:
                    picIcon.Image = Properties.Resources.success;
                    break;
                case MessageBoxIcon.Warning:

                    picIcon.Image = Properties.Resources.warning_pica_pica;
                    break;
                case MessageBoxIcon.Error:
                    picIcon.Image = Properties.Resources.error_pica__2__pica;
                    break;
                case MessageBoxIcon.Question:
                    picIcon.Image = Properties.Resources.pregunta_artguru_artguru;
                    break;
                default:
                    picIcon.Image = Properties.Resources.informacion_artguru_artguru;
                    break;
            }


            if (buttons == MessageBoxButtons.OK)
            {

                btnYes2.Text = "OK";
                btnYes2.Width = 100;
                btnYes2.Left = (this.ClientSize.Width - btnYes2.Width) / 2;
                btnNo2.Visible = false;
            }
            else if (buttons == MessageBoxButtons.YesNo)
            {

                btnYes2.Text = "Sí";
                btnNo2.Text = "No";
                btnYes2.Width = 100;
                btnNo2.Width = 100;


                btnYes2.Left = (this.ClientSize.Width / 2) - btnYes2.Width - 10;
                btnNo2.Left = (this.ClientSize.Width / 2) + 10;
            }
            lblTitulo.Left = ((this.ClientSize.Width - lblTitulo.Width) / 2);

            //this.Load += FrmAlerta_Load;
            FrmAlerta_Resize(this, EventArgs.Empty);
        }
        private void FrmAlerta_Resize(object sender, EventArgs e)
        {
            //richTextBoxMensaje.Left = (this.ClientSize.Width - richTextBoxMensaje.Width) / 2;
            //lblTitulo.Left = (this.ClientSize.Width - lblTitulo.Width) / 2;


            //picIcon.Left = (this.ClientSize.Width - picIcon.Width) / 2;
        }


        private void btnNo2_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.No;
            this.Close();
        }

        private void btnYes2_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.Close();
        }

        private void FrmAlerta_Load(object sender, EventArgs e)
        {
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            conteo++;
            if (conteo == 60)
                this.Close();
        }

        private void FrmAlerta_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}