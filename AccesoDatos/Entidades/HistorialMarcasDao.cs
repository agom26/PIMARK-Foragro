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
                using (MySqlCommand comando = new MySqlCommand("SELECT id, etapa as Etapa, fecha as Fecha, anotaciones as Anotaciones, usuario as Usuario FROM Historial WHERE  IdMarca=@idMarca;", conexion)) // Inicializa correctamente el comando
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

        public void GuardarEtapa(int idMarca, DateTime fecha, string etapa, string anotaciones, string usuario)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = @"INSERT INTO Historial (fecha, etapa, anotaciones, usuario, IdMarca) 
                         VALUES (@fecha, @etapa, @anotaciones, @usuario, @IdMarca)";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@fecha", fecha);
                cmd.Parameters.AddWithValue("@etapa", etapa);
                cmd.Parameters.AddWithValue("@anotaciones", anotaciones);
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@IdMarca", idMarca);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
