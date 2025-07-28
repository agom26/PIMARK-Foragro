using MySql.Data.MySqlClient;
namespace AccesoDatos.MySqlServer
{
    public class ConnectionSQL
    {
        private string connectionString;

        public ConnectionSQL()
        {
            
            connectionString = "server=mysql.foragro.com.es;port=3306;user=foragro_registrador;pwd=H*V#GjVX1N8?;database=foragro_pimark;";
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
