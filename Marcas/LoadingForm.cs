using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class LoadingForm : Form
    {
        public LoadingForm()
        {
            InitializeComponent();
            // Establecer propiedades del formulario, como no redimensionable
            
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ControlBox = false; // Deshabilitar la caja de control

            // Configurar el ProgressBar
            progressBar1.Style = ProgressBarStyle.Marquee; // Establecer el estilo a Marquee
            progressBar1.MarqueeAnimationSpeed = 30; // Velocidad de la animación
            progressBar1.Visible = true; // Asegurarse de que sea visible
        }

        private void LoadingForm_Load(object sender, EventArgs e)
        {

        }
    }
}
