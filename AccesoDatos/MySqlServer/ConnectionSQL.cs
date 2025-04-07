using MySql.Data.MySqlClient;
namespace AccesoDatos.MySqlServer
{
    public class ConnectionSQL
    {
        private string connectionString;

        public ConnectionSQL()
        {
            // Cadena de conexión para SQL Server
            connectionString = "server=bpa.com.es;port=3306;uid=bpaes_registrador;pwd=X*r@$Vh6VF@_;database=bpaes_marcas;";
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
                    Console.WriteLine("Conexión exitosa a la base de datos SQL Server.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar a la base de datos: {ex.Message}");
            }
        }
    }
}
