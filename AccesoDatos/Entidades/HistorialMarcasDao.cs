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

        public bool EliminarRegistroHistorialYLog(int idHistorial, string deletedBy)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                // Begin a transaction to ensure both actions succeed or fail together
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {

                        // Log the deletion
                        using (var command = new MySqlCommand("LogHistorialDeletion", connection, transaction))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("p_idHistorial", idHistorial);
                            command.Parameters.AddWithValue("p_usuario", deletedBy);

                            command.ExecuteNonQuery(); // Execute the logging
                            
                        }


                        // Delete the history record
                        using (var command = new MySqlCommand("EliminarRegistroHistorial", connection, transaction))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("historialId", idHistorial);

                            int rowsAffected = command.ExecuteNonQuery();

                            // If no rows were affected, the deletion did not happen
                            if (rowsAffected == 0)
                            {
                                transaction.Rollback(); // Rollback if deletion failed
                                return false;
                            }
                        }

                       
                        // Commit the transaction if both actions succeeded
                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        // Rollback the transaction in case of an error
                        transaction.Rollback();
                        throw; // Optionally rethrow the exception for further handling
                    }
                }
            }
        }



    }
}
