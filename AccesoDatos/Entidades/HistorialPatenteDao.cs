using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace AccesoDatos.Entidades
{
    public class HistorialPatenteDao : ConnectionSQL
    {
        // Método para insertar historial de patente
        public void InsertarHistorialPatente(
            DateTime fecha,
            string etapa,
            string anotaciones,
            string usuario,
            string usuarioEdicion,
            int idPatente)
        {
            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("InsertarHistorialPatente", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    
                    command.Parameters.AddWithValue("@p_fecha", fecha);
                    command.Parameters.AddWithValue("@p_etapa", etapa);
                    command.Parameters.AddWithValue("@p_anotaciones", anotaciones ?? (object)DBNull.Value); // Si es null, asignar DBNull
                    command.Parameters.AddWithValue("@p_usuario", usuario);
                    command.Parameters.AddWithValue("@p_usuarioEdicion", usuarioEdicion ?? (object)DBNull.Value); // Si es null, asignar DBNull
                    command.Parameters.AddWithValue("@p_IdPatente", idPatente);

                    
                    command.ExecuteNonQuery();
                }
            }
        }

        public DataTable GetAllEstadosByIdPatente(int idPatente)
        {
            DataTable tabla = new DataTable();
            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("ObtenerHistorialPatentePorId", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@p_IdPatente", idPatente);
                    conexion.Open();
                    using (MySqlDataReader leer = comando.ExecuteReader())
                    {
                        tabla.Load(leer);
                    }
                }
            }
            return tabla;
        }

        public DataTable GetHistorialById(int idHistorial)
        {
            DataTable tabla = new DataTable();
            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("ObtenerHistorialPatentePorIdHistorial", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@p_IdHistorial", idHistorial);

                    conexion.Open();
                    using (MySqlDataReader leer = comando.ExecuteReader())
                    {
                        tabla.Load(leer);
                    }
                }
            }
            return tabla;
        }

        public void EditarHistorialPatente(
            int idHistorial,
            DateTime fecha,
            string etapa,
            string anotaciones,
            string usuario,
            string usuarioEdicion)
        {
            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("EditarHistorialPatentePorId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@p_IdHistorial", idHistorial);
                    command.Parameters.AddWithValue("@p_Fecha", fecha);
                    command.Parameters.AddWithValue("@p_Etapa", etapa);
                    command.Parameters.AddWithValue("@p_Anotaciones", anotaciones ?? (object)DBNull.Value); // Si es null, asignar DBNull
                    command.Parameters.AddWithValue("@p_Usuario", usuario);
                    command.Parameters.AddWithValue("@p_UsuarioEdicion", usuarioEdicion ?? (object)DBNull.Value); // Si es null, asignar DBNull

                    command.ExecuteNonQuery();
                }
            }
        }


    }
}
