using System.Xml.Serialization;
using System.Runtime.InteropServices;

namespace Marcas
{
    public partial class LoginForm : Form
    {
        private void CustomComponents()
        {
            txtUserName.AutoSize = false;
            txtUserName.Size = new Size(350, 38);
            txtPassword.AutoSize = false;
            txtPassword.Size = new Size(350, 38);
        }

          public LoginForm()
        {
            InitializeComponent();
            CustomComponents();
        }

        private void iconPictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {

        }

        private void iconPictureBox1_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        public static extern void ReleaseCapture();

        // Importar la función SendMessage de la user32.dll
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void TitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xF012, 0);
        }

        private void LoginForm_MouseMove(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xF012, 0);
        }
    }
}
