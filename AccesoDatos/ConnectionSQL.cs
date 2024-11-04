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
            connectionString = ConfigurationManager.ConnectionStrings["BPA"].ConnectionString;
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

