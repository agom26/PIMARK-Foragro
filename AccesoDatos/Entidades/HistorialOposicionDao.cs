using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class HistorialOposicionDao:ConnectionSQL
    {
        public void InsertarHistorialOposicion(DateTime fecha, string etapa, string anotaciones, string usuario, string usuarioEdicion, string origen, int idOposicion)
        {
            using (MySqlConnection connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("InsertarHistorialOposicion", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                      
                        command.Parameters.AddWithValue("p_fecha", fecha);
                        command.Parameters.AddWithValue("p_etapa", string.IsNullOrEmpty(etapa) ? (object)DBNull.Value : etapa);
                        command.Parameters.AddWithValue("p_anotaciones", string.IsNullOrEmpty(anotaciones) ? (object)DBNull.Value : anotaciones);
                        command.Parameters.AddWithValue("p_usuario", usuario);
                        command.Parameters.AddWithValue("p_usuarioEdicion", string.IsNullOrEmpty(usuarioEdicion) ? (object)DBNull.Value : usuarioEdicion);
                        command.Parameters.AddWithValue("p_origen", origen);
                        command.Parameters.AddWithValue("p_IdOposicion", idOposicion);

                       
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                   
                    throw new Exception("Error al insertar el historial de oposición: " + ex.Message, ex);
                }
            }
        }

        public DataTable ObtenerHistorialOposicion(int idOposicion)
        {
            using (MySqlConnection connection = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand("ObtenerHistorialOposicion", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id_oposicion", idOposicion);

                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                dataAdapter.Fill(dt);

                return dt;
            }
        }

        public DataTable ObtenerHistorialOposicionPorId(int historialId)
        {
            using (MySqlConnection connection = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand("ObtenerHistorialOposicionPorId", connection);
                cmd.CommandType = CommandType.StoredProcedure;

               
                cmd.Parameters.AddWithValue("historial_id", historialId);

                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();

               
                dataAdapter.Fill(dt);

                return dt;
            }
        }

        public bool EditarHistorialOposicion(int historialId, string nuevaEtapa, DateTime nuevaFecha, string nuevasAnotaciones, string nuevoUsuario, string nuevoUsuarioEdicion)
        {
            using (MySqlConnection connection = GetConnection())
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand("EditarHistorialOposicion", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                
                    cmd.Parameters.AddWithValue("historial_id", historialId);
                    cmd.Parameters.AddWithValue("nueva_etapa", nuevaEtapa);
                    cmd.Parameters.AddWithValue("nueva_fecha", nuevaFecha);
                    cmd.Parameters.AddWithValue("nuevas_anotaciones", nuevasAnotaciones);
                    cmd.Parameters.AddWithValue("nuevo_usuario", nuevoUsuario);
                    cmd.Parameters.AddWithValue("nuevo_usuario_edicion", nuevoUsuarioEdicion);

                   
                    MySqlParameter resultadoParam = new MySqlParameter("resultado", MySqlDbType.Byte)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(resultadoParam);

                    connection.Open();
                    cmd.ExecuteNonQuery();

                    
                    bool resultado = Convert.ToBoolean(resultadoParam.Value);
                    return resultado;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al editar el historial de oposición: " + ex.Message, ex);
                }
            }
        }

    }
}
