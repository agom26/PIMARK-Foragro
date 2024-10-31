using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class HistorialMarcasDao:ConnectionSQL
    {

        public DataTable GetAllEtapasByIdMarca(int idMarca)
        {
            DataTable tabla = new DataTable();
            using (MySqlConnection conexion = GetConnection()) // Asegura que la conexión se cierre al finalizar
            {
                using (MySqlCommand comando = new MySqlCommand("SELECT fecha, etapa, anotaciones,usuario FROM Historial WHERE  IdMarca=@idMarca;", conexion)) // Inicializa correctamente el comando
                {
                    comando.Parameters.AddWithValue("@idMarca", idMarca);
                    conexion.Open();
                    using (MySqlDataReader leer = comando.ExecuteReader()) // Asegura que el lector se cierre
                    {
                        tabla.Load(leer);
                    }
                }
            }
            return tabla;
        }

        public bool AddEstatusHistorial(DateTime fecha, string etapa, string anotaciones, string usuario, int idMarca)
        {
            using (var connection = GetConnection()) // Asegúrate de que GetConnection esté implementado
            {
                connection.Open();
                using (var command = new MySqlCommand("INSERT INTO Historial (fecha, etapa, anotaciones, usuario, IdMarca) VALUES (@fecha, @etapa, @anotaciones, @usuario, @IdMarca)", connection))
                {
                    command.Parameters.AddWithValue("@fecha", fecha);
                    command.Parameters.AddWithValue("@etapa", etapa);
                    command.Parameters.AddWithValue("@anotaciones", anotaciones);
                    command.Parameters.AddWithValue("@usuario", usuario);
                    command.Parameters.AddWithValue("@IdMarca", idMarca);

                    // Ejecuta el comando y devuelve el número de filas afectadas
                    int rowsAffected = command.ExecuteNonQuery();

                    // Si se insertó al menos una fila, la operación fue exitosa
                    return rowsAffected > 0;
                }
            }
        }
    }
}
