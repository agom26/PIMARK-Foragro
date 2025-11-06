
using System.Runtime.InteropServices;
using Dominio;
using Presentacion;
using System.IO.Compression;
using System.Net;
using System.Diagnostics;

namespace Marcas
{
    public partial class LoginForm : Form
    {

        private string dropboxFolderUrl = "https://www.dropbox.com/scl/fo/7hmngh6533qece4tqzm27/ALHAY7Mn_gNloTrOktB-stw?rlkey=l2rxzdne2pdynaz3w64z4tz54&st=ze9lawti&dl=1"; // Reemplaza con tu enlace directo de la carpeta
        private string tempFolder = @"C:\Temp\UpdateFiles";
        private string zipPath;

        private void CustomComponents()
        {
            txtUserName.AutoSize = false;
            txtUserName.Size = new Size(350, 38);
            txtPassword.AutoSize = false;
            txtPassword.Size = new Size(350, 38);
        }

        public LoginForm()
        {
            //zipPath = Path.Combine(tempFolder, "update.zip");
            //VerificarYActualizar();
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

        private async void btnGuardar_Click(object sender, EventArgs e)
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
           

            //LOGIN                                
            UserModel userModel = new UserModel();
            bool conexion = await userModel.ProbarConexion();
            if (!conexion)
            {
                MessageBox.Show("No se pudo establecer conexión con la base de datos.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            (bool validLogin, bool isAdmin) = await userModel.Login(txtUserName.Text, txtPassword.Text);
            try
            {
                if(validLogin == true)
                {
                    Form1 dashboard = new Form1(isAdmin);
                    dashboard.WindowState = FormWindowState.Maximized;
                    dashboard.Show();
                    dashboard.FormClosed += new FormClosedEventHandler(this.Logout);
                    this.Hide();
                    
                }
                else
                {
                    MessageBox.Show("Credenciales inválidas, usuario o contraseña inválidos");
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
