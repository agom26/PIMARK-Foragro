using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
    }
}
