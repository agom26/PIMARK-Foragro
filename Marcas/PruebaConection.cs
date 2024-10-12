using MySql.Data.MySqlClient;
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
    public partial class PruebaConection : Form
    {
        public PruebaConection()
        {
            InitializeComponent();
        }

        string conexion = "server=bpa.com.es;port=3306;uid=bpaes_registrador;pwd='X*r@$Vh6VF@_';database=bpaes_marcas;";
        private void button1_Click(object sender, EventArgs e)
        {
            string conexion = "server=serverlo;port=3306;uid=user;pwd='password';database=database;";
            MySqlConnection conectado=new MySqlConnection(conexion);

            try
            {
                conectado.Open();
                MessageBox.Show("Conectado correctamente");
                conectado.Close();
            }
            catch 
            {
                MessageBox.Show("Error al conectarse");
            }

        }
    }
}
