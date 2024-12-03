using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class OposicionesDao: ConnectionSQL
    {
        public int AddOposicion(
            string expediente,
            string signoPretendido,
            string signoDistintivo,
            string clase,
            string solicitanteSignoPretendido,
            int idOpositor,
            string signoOpositor,
            string observaciones,
            string estado,
            string situacionActual,
            int? idMarca,
            byte?[] logoOpositor,
            byte?[] logoSignoPretendido)
        {
            int rowsAffected = 0;



            using (var connection = GetConnection())
            {
                try
                {
                    connection.Open();

                    using (MySqlCommand cmd = new MySqlCommand("InsertarOposicion", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@p_expediente", expediente);
                        cmd.Parameters.AddWithValue("@p_signo_pretendido", signoPretendido);
                        cmd.Parameters.AddWithValue("@p_signo_distintivo", signoDistintivo);
                        cmd.Parameters.AddWithValue("@p_clase", clase);
                        cmd.Parameters.AddWithValue("@p_solicitante_signo_pretendido", solicitanteSignoPretendido);
                        cmd.Parameters.AddWithValue("@p_opositor", idOpositor);
                        cmd.Parameters.AddWithValue("@p_signo_opositor", signoOpositor);
                        cmd.Parameters.AddWithValue("@p_observaciones", observaciones ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@p_estado", estado ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@p_situacion_actual", situacionActual);
                        cmd.Parameters.AddWithValue("@p_idMarca", idMarca ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@p_logo_opositor", logoOpositor ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@p_logo_signo_pretendido", logoSignoPretendido ?? (object)DBNull.Value);

                        rowsAffected = cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }
            }

            return rowsAffected;
        }

    }
}
