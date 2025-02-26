using System.Xml.Serialization;
using System.Runtime.InteropServices;
using Dominio;
using Presentacion;
using System.Runtime.CompilerServices;
using System.Windows.Forms.PropertyGridInternal;

using MySql.Data.MySqlClient;
using Comun.Cache;
using System.IO.Compression;
using System.IO;
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
            zipPath = Path.Combine(tempFolder, "update.zip");
            VerificarYActualizar();
            InitializeComponent();
            
            CustomComponents();
            
        }

        private void VerificarYActualizar()
        {
            try
            {
                if (!Directory.Exists(tempFolder))
                {
                    Directory.CreateDirectory(tempFolder);
                }

                string localVersionPath = Path.Combine(tempFolder, "version.txt");
                string tempVersionPath = Path.Combine(tempFolder, "new_version.txt");

                using (WebClient client = new WebClient())
                {
                    // 🔹 Descargar SOLO el archivo de versión
                    string versionUrl = "https://www.dropbox.com/scl/fi/avkx6y4ummvdwg9qi49mg/version.txt?rlkey=uc81tz5erfjax8is6h8nenvlm&st=yrqcw91t&dl=1";
                    client.DownloadFile(versionUrl, tempVersionPath);

                    string localVersion = File.Exists(localVersionPath) ? File.ReadAllText(localVersionPath).Trim() : "0.0.0";
                    string onlineVersion = File.ReadAllText(tempVersionPath).Trim();

                    // 🔹 Si ya tenemos la última versión, no hacemos nada
                    if (localVersion == onlineVersion)
                    {
                        File.Delete(tempVersionPath); // Eliminar el archivo temporal
                        return;
                    }

                    // 🔹 Si hay una nueva versión, proceder con la actualización
                    File.WriteAllText(localVersionPath, onlineVersion);
                    MessageBox.Show($"Nueva versión disponible: {onlineVersion}. Descargando actualización...", "Actualización", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 🔹 Descargar el ZIP con la actualización
                    client.DownloadFile(dropboxFolderUrl, zipPath);
                    MessageBox.Show("Extrayendo archivos...", "Actualización", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    using (ZipArchive archive = ZipFile.OpenRead(zipPath))
                    {
                        foreach (ZipArchiveEntry entry in archive.Entries)
                        {
                            if (string.IsNullOrEmpty(entry.Name))
                                continue;

                            string destinationPath = Path.Combine(tempFolder, entry.Name);

                            if (!destinationPath.StartsWith(tempFolder, StringComparison.OrdinalIgnoreCase))
                            {
                                MessageBox.Show("Se detectó un intento de extraer archivos fuera del directorio permitido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            entry.ExtractToFile(destinationPath, true);
                        }
                    }

                    // 🔹 Ejecutar setup.exe si existe
                    string setupPath = Path.Combine(tempFolder, "setup.exe");
                    if (File.Exists(setupPath))
                    {
                        Process.Start(setupPath);
                        Application.Exit();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en la actualización: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
