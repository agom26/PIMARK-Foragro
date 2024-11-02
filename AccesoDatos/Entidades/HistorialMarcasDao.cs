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

        public bool EditHistorialById(int id, string etapa, DateTime fecha, string anotaciones, string usuario, string usuarioEditor)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(@"
                    UPDATE Historial 
                    SET etapa = @etapa, 
                        fecha = @fecha, 
                        anotaciones = @anotaciones, 
                        usuario = @usuario, 
                        usuarioEdicion = @usuarioEdicion
                    WHERE id = @id;", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@etapa", etapa);
                    command.Parameters.AddWithValue("@fecha", fecha);
                    command.Parameters.AddWithValue("@anotaciones", anotaciones);
                    command.Parameters.AddWithValue("@usuario", usuario);
                    command.Parameters.AddWithValue("@usuarioEdicion", usuarioEditor);

                    // Ejecuta el comando y devuelve el número de filas afectadas
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Retorna true si se actualizó al menos una fila
                }
            }
        }
        public DataTable GetHistorialById(int id)
        {
            DataTable tabla = new DataTable();
            using (MySqlConnection conexion = GetConnection()) // Asegura que la conexión se cierre al finalizar
            {
                using (MySqlCommand comando = new MySqlCommand("SELECT * FROM Historial WHERE id=@id;", conexion)) // Inicializa correctamente el comando
                {
                    comando.Parameters.AddWithValue("@id", id);
                    conexion.Open();
                    using (MySqlDataReader leer = comando.ExecuteReader()) // Asegura que el lector se cierre
                    {
                        tabla.Load(leer);
                    }
                }
            }
            return tabla;
        }
        public DataTable GetAllEtapasByIdMarca(int idMarca)
        {
            DataTable tabla = new DataTable();
            using (MySqlConnection conexion = GetConnection()) // Asegura que la conexión se cierre al finalizar
            {
                using (MySqlCommand comando = new MySqlCommand("SELECT id, etapa as Etapa, fecha as Fecha, anotaciones as Anotaciones, usuario as Usuario_creador, usuarioEdicion As Usuario_editor FROM Historial WHERE  IdMarca=@idMarca;", conexion)) // Inicializa correctamente el comando
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

        public bool EliminarRegistroHistorial(int idHistorial)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("EliminarRegistroHistorial", connection))
                {
                    command.CommandType = CommandType.StoredProcedure; // Especifica que es un procedimiento almacenado
                    command.Parameters.AddWithValue("historialId", idHistorial); // Nombre del parámetro según el procedimiento almacenado

                    // Ejecuta el comando y devuelve true si se eliminó al menos un registro
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

    }
}
