using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Esf;
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
        public DataTable FiltrarOposiciones(string tipo_filtro,
            string expediente, string solicitante, string signoPretendido, 
            string signoDistintivo, string clase, string opositor, string signoOpositor, 
            string estado, string situacionActual, string tipo, string tipoOposicion
        )
        {
            DataTable dataTable = new DataTable();

            using (MySqlConnection conexion = GetConnection())
            {
                conexion.Open();
                using (MySqlCommand cmd = new MySqlCommand("FiltrarOposiciones", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("tipo_filtro", tipo_filtro);
                    cmd.Parameters.AddWithValue("p_expediente", expediente ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_solicitante_signoP", solicitante ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_signo_pretendido", signoPretendido ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_signo_distintivo", signoDistintivo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_clase", clase ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_opositor", opositor ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_signoO", signoOpositor ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_estadoA", estado ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_situacion_actual", situacionActual ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_tipo", tipo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_tipo_op", tipoOposicion ?? (object)DBNull.Value);
                    

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }
        public DataTable GetAllOposicionesInternacionalesInterpuestas(string situacionActual)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("ObtenerOposicionesInternacionalesInterpuestas", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                        comando.Parameters.AddWithValue("p_situacion_actual", situacionActual);

                        conexion.Open();
                        using (MySqlDataReader leer = comando.ExecuteReader())
                        {
                            tabla.Load(leer);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener las oposiciones internacionales: {ex.Message}");
            }
            return tabla;
        }

        public DataTable GetAllOposicionesInternacionales(string situacionActual)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand(" ObtenerOposicionesInternacionalesRecibidas ", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                        comando.Parameters.AddWithValue("p_situacion_actual", situacionActual);

                        conexion.Open();
                        using (MySqlDataReader leer = comando.ExecuteReader())
                        {
                            tabla.Load(leer);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener las oposiciones internacionales: {ex.Message}");
            }
            return tabla;
        }

        public DataTable GetAllOposicionesNacionales(string situacionActual)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("ObtenerOposicionesNacionalesRecibidas", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                        comando.Parameters.AddWithValue("p_situacion_actual", situacionActual);

                        conexion.Open();
                        using (MySqlDataReader leer = comando.ExecuteReader())
                        {
                            tabla.Load(leer);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener las oposiciones nacionales: {ex.Message}");
            }
            return tabla;
        }

        public DataTable GetAllOposicionesNacionalesInterpuestas(string situacionActual)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("ObtenerOposicionesNacionalesInterpuestas", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                        comando.Parameters.AddWithValue("p_situacion_actual", situacionActual);

                        conexion.Open();
                        using (MySqlDataReader leer = comando.ExecuteReader())
                        {
                            tabla.Load(leer);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener las oposiciones nacionales: {ex.Message}");
            }
            return tabla;
        }

        public int AddOposicion(
            string expediente,
            string signoPretendido,
            string signoDistintivo,
            string clase,
            string solicitanteSignoPretendido,
            int? idSolicitante,
            int? idOpositor,
            string opositor,
            string signoOpositor,
            string situacionActual,
            int? idMarca,
            byte[] logoOpositor, 
            byte[] logoSignoPretendido,
            string tipo,
            string tipoOposicion)
        {
            int lastInsertedId = 0;
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
                        cmd.Parameters.AddWithValue("@p_idSolicitante", idSolicitante ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@p_idopositor", idOpositor ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@p_opositor", opositor);
                        cmd.Parameters.AddWithValue("@p_signo_opositor", signoOpositor);
                        cmd.Parameters.AddWithValue("@p_situacion_actual", situacionActual);
                        cmd.Parameters.AddWithValue("@p_idMarca", idMarca ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@p_logo_opositor", logoOpositor ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@p_logo_signo_pretendido", logoSignoPretendido ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@p_tipo", tipo);
                        cmd.Parameters.AddWithValue("@p_tipo_oposicion", tipoOposicion);
                        // Leer el ID del último registro insertado
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                lastInsertedId = Convert.ToInt32(reader["idOposicion"]);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }
            }

            return lastInsertedId;
        }

        public DataTable GetOposicionPorId(int idOposicion)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    
                    using (MySqlCommand comando = new MySqlCommand("ObtenerOposicionPorId", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                       
                        comando.Parameters.AddWithValue("@oposicion_id", idOposicion);

                        conexion.Open();
                        using (MySqlDataReader leer = comando.ExecuteReader())
                        {
                          
                            tabla.Load(leer);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener la oposición por ID: {ex.Message}");
            }
            return tabla;
        }

        public bool EditOposicion(
        int idOposicion,
        string expediente,
        string signoPretendido,
        string signoDistintivo,
        string clase,
        string solicitanteSignoPretendido,
        int? idOpositor,
        string signoOpositor,
        string situacionActual,
        int? idMarca,
        byte[] logoOpositor,
        byte[] logoSignoPretendido,
        string opositor,
        int? idSolicitante)
        {
            try
            {
                using (MySqlConnection connection = GetConnection())
                {
                    connection.Open();

                    using (MySqlCommand cmd = new MySqlCommand("EditarOposicion", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@p_idOposicion", idOposicion);
                        cmd.Parameters.AddWithValue("@p_expediente", expediente);
                        cmd.Parameters.AddWithValue("@p_signo_pretendido", signoPretendido);
                        cmd.Parameters.AddWithValue("@p_signo_distintivo", signoDistintivo);
                        cmd.Parameters.AddWithValue("@p_clase", clase);
                        cmd.Parameters.AddWithValue("@p_solicitante_signo_pretendido", solicitanteSignoPretendido);
                        cmd.Parameters.AddWithValue("@p_idopositor", idOpositor);
                        cmd.Parameters.AddWithValue("@p_signo_opositor", signoOpositor);
                        cmd.Parameters.AddWithValue("@p_situacion_actual", situacionActual);
                        cmd.Parameters.AddWithValue("@p_idMarca", idMarca);
                        cmd.Parameters.AddWithValue("@p_logo_opositor", logoOpositor ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@p_logo_signo_pretendido", logoSignoPretendido ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@p_opositor", opositor);
                        cmd.Parameters.AddWithValue("@p_idSolicitante", idSolicitante);

                        var resultParam = new MySqlParameter("@p_result", MySqlDbType.Bit)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(resultParam);

                        cmd.ExecuteNonQuery();

                        return Convert.ToBoolean(resultParam.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al editar la oposición: {ex.Message}");
                throw;
            }
        }

        public bool CambiarSituacionActualATerminada(int id)
        {
            bool resultado = false;

            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("CambiarSituacionActualATerminada", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                        comando.Parameters.AddWithValue("p_id", id);

                        var resultadoParam = new MySqlParameter("p_resultado", MySqlDbType.Bit)
                        {
                            Direction = ParameterDirection.Output
                        };
                        comando.Parameters.Add(resultadoParam);

                      
                        conexion.Open();
                        comando.ExecuteNonQuery();

                       
                        resultado = Convert.ToBoolean(resultadoParam.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cambiar la situación actual: {ex.Message}");
            }

            return resultado;
        }




    }
}
