using MySql.Data.MySqlClient;
using System;
using System.Configuration;

namespace AccesoDatos
{
    public class ConnectionSQL
    {
        private string connectionString;

        public ConnectionSQL()
        {
            //connectionString = "server=mysql.foragro.com.es;port=3306;uid=foragro;pwd=gqL8ygtSv6Z8;database=foragro_pimark;";
            connectionString = ConfigurationManager.ConnectionStrings["Foragro"].ConnectionString;
        }

        protected MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        public void TestConnection()
        {
            try
            {
                using (MySqlConnection connection = GetConnection())
                {
                    connection.Open();
                    Console.WriteLine("Conexión exitosa a la base de datos MySQL.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar a la base de datos: {ex.Message}");
            }
        }
    }
}

