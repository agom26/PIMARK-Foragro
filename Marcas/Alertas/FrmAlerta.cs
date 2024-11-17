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
            lblMessage.Text = message;
            lblMessage.TextAlign = ContentAlignment.MiddleCenter;

            lblTitulo.Text = title;
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;

            this.Text = "";

            switch (icon)
            {
                case MessageBoxIcon.Information:
                    picIcon.Image = ConvertByteArrayToImage(Properties.Resources.SucessIcon);
                    break;
                case MessageBoxIcon.Warning:
                    picIcon.Image = ConvertByteArrayToImage(Properties.Resources.WarningIcon);
                    break;
                case MessageBoxIcon.Error:
                    picIcon.Image = ConvertByteArrayToImage(Properties.Resources.ErrorIcon);
                    break;
                case MessageBoxIcon.Question:
                    picIcon.Image = ConvertByteArrayToImage(Properties.Resources.QuestionIcon);
                    break;
                default:
                    picIcon.Image = ConvertByteArrayToImage(Properties.Resources.InfoIcon);
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


            this.Resize += FrmAlerta_Resize;
            FrmAlerta_Resize(this, EventArgs.Empty);
        }
        private void FrmAlerta_Resize(object sender, EventArgs e)
        {

            lblMessage.Left = (this.ClientSize.Width - lblMessage.Width) / 2;
            lblTitulo.Left = (this.ClientSize.Width - lblTitulo.Width) / 2;


            picIcon.Left = (this.ClientSize.Width - picIcon.Width) / 2;
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
    }
}
