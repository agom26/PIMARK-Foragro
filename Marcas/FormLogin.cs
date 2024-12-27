using System.Xml.Serialization;
using System.Runtime.InteropServices;
using Dominio;
using Presentacion;
using System.Runtime.CompilerServices;
using MySql.Data.MySqlClient;
using System.Windows.Forms.PropertyGridInternal;
using Comun.Cache;
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

        public void Logout(object sender, FormClosedEventArgs e)
        {
            if (Presentacion.Properties.Settings.Default.Recordar == true)
            {
                txtUserName.Text = Presentacion.Properties.Settings.Default.Usuario;
                txtPassword.Text = Presentacion.Properties.Settings.Default.Contrasena;
                checkBoxRememberme.Checked = Presentacion.Properties.Settings.Default.Recordar;
                this.Show();
                //btnLogin.Focus();
            }
            else
            {
                txtUserName.Text = "";
                txtPassword.Text = "";
                checkBoxRememberme.Checked = false;
                this.Show();
                txtUserName.Focus();
            }


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
            if (Presentacion.Properties.Settings.Default.Recordar == true)
            {
                txtUserName.Text = Presentacion.Properties.Settings.Default.Usuario;
                txtPassword.Text = Presentacion.Properties.Settings.Default.Contrasena;
                checkBoxRememberme.Checked = Presentacion.Properties.Settings.Default.Recordar;
                btnGuardar.Focus();
            }
            else
            {
                txtUserName.Text = "";
                txtPassword.Text = "";
                checkBoxRememberme.Checked = false;
            }
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

        private void TitleBar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {



        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //recordar sesion
            if (checkBoxRememberme.Checked)
            {
                Presentacion.Properties.Settings.Default.Usuario = txtUserName.Text;
                Presentacion.Properties.Settings.Default.Contrasena = txtPassword.Text;
                Presentacion.Properties.Settings.Default.Recordar = checkBoxRememberme.Checked;
                Presentacion.Properties.Settings.Default.Save();
                Presentacion.Properties.Settings.Default.Reload();
            }
            else
            {

            }

            //LOGIN                                
            UserModel userModel = new UserModel();
            (bool validLogin, bool isAdmin) = userModel.Login(txtUserName.Text, txtPassword.Text);
            try
            {
                if(validLogin == true)
                {
                    Form1 dashboard = new Form1(isAdmin);
                    dashboard.Show();
                    dashboard.FormClosed += new FormClosedEventHandler(this.Logout);
                    this.Hide();
                    
                }
                else
                {
                    //MessageBox.Show("aqui se quedo");
                }
                
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
                
            }

            
        }
    }
}
