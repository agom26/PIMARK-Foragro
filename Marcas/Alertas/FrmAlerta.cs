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
                    picIcon.Image = Properties.Resources.suc;
                    break;
                case MessageBoxIcon.Warning:
                    picIcon.Image = Properties.Resources.warning;
                    break;
                case MessageBoxIcon.Error:
                    picIcon.Image = Properties.Resources.error;
                    break;
                case MessageBoxIcon.Question:
                    picIcon.Image = Properties.Resources.question;
                    break;
                default:
                    picIcon.Image = Properties.Resources.info;
                    break;
            }


            if (buttons == MessageBoxButtons.OK)
            {

                btnYes.Text = "OK";
                btnYes.Width = 100;
                btnYes.Left = (this.ClientSize.Width - btnYes.Width) / 2;
                btnNo.Visible = false;
            }
            else if (buttons == MessageBoxButtons.YesNo)
            {

                btnYes.Text = "Sí";
                btnNo.Text = "No";
                btnYes.Width = 100;
                btnNo.Width = 100;


                btnYes.Left = (this.ClientSize.Width / 2) - btnYes.Width - 10;
                btnNo.Left = (this.ClientSize.Width / 2) + 10;
            }
            lblTitulo.Left = (this.ClientSize.Width - lblTitulo.Width) / 2;

            this.Load += FrmAlerta_Load;
            FrmAlerta_Resize(this, EventArgs.Empty);
        }
        private void FrmAlerta_Resize(object sender, EventArgs e)
        {
            //richTextBoxMensaje.Left = (this.ClientSize.Width - richTextBoxMensaje.Width) / 2;
            //lblTitulo.Left = (this.ClientSize.Width - lblTitulo.Width) / 2;


            //picIcon.Left = (this.ClientSize.Width - picIcon.Width) / 2;
        }


        private void btnNo_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.No;
            this.Close();
        }

        private void btnYes_Click(object sender, EventArgs e)
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
