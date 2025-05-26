using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class LicenciaUso : ConnectionSQL
    {
        
        public DataTable FiltrarLicenciasUso(
            string tipoLicencia,
            string expediente,
            string titulo,
            string signo,
            string signoDistintivo,
            string estado,
            string clase,
            string origen,
            string nombreRazonSocial,
            string titular)
        {
            DataTable dataTable = new DataTable();

            using (MySqlConnection conexion = GetConnection())
            {
                conexion.Open();
                using (MySqlCommand cmd = new MySqlCommand("FiltrarLicenciasUso", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("p_tipo_licencia", tipoLicencia ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_expediente", expediente ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_titulo", titulo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_signo", signo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_signo_distintivo", signoDistintivo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_estado", estado ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_clase", clase ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_origen", origen ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_nombre_razon_social", nombreRazonSocial ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_titular", titular ?? (object)DBNull.Value);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }



        public bool InsertarLicenciaUso
        (
            int idMarca,
            int idTitular,
            string titulo,
            string tipo,
            DateTime fechaInicio,
            DateTime fechaFin,
            string territorio,
            string nombreRazonSocial,
            string direccion,
            string domicilio,
            string nacionalidad,
            string apoderadoRepresentanteLegal,
            string estado,
            string origen
        )
        {
            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("InsertarLicenciaUso", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros
                        cmd.Parameters.AddWithValue("p_IdMarca", idMarca);
                        cmd.Parameters.AddWithValue("p_IdTitular", idTitular);
                        cmd.Parameters.AddWithValue("p_titulo", titulo);
                        cmd.Parameters.AddWithValue("p_tipo", tipo);
                        cmd.Parameters.AddWithValue("p_fecha_inicio", fechaInicio);
                        cmd.Parameters.AddWithValue("p_fecha_fin", fechaFin);
                        cmd.Parameters.AddWithValue("p_territorio", territorio);
                        cmd.Parameters.AddWithValue("p_nombre_razon_social", nombreRazonSocial);
                        cmd.Parameters.AddWithValue("p_direccion", direccion);
                        cmd.Parameters.AddWithValue("p_domicilio", domicilio);
                        cmd.Parameters.AddWithValue("p_nacionalidad", nacionalidad);
                        cmd.Parameters.AddWithValue("p_apoderado_representante_legal", apoderadoRepresentanteLegal);
                        cmd.Parameters.AddWithValue("p_estado", estado);
                        cmd.Parameters.AddWithValue("p_origen", origen);

                        cmd.ExecuteNonQuery();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al insertar licencia de uso: " + ex.Message);
                    return false;
                }
            }
        }

        public bool EditarLicenciaUso(
            int id,
            int idMarca,
            int idTitular,
            string titulo,
            string tipo,
            DateTime fechaInicio,
            DateTime fechaFin,
            string territorio,
            string nombreRazonSocial,
            string direccion,
            string domicilio,
            string nacionalidad,
            string apoderadoRepresentanteLegal,
            string estado,
            string origen
        )
        {
            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("EditarLicenciaUso", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros
                        cmd.Parameters.AddWithValue("p_id", id);
                        cmd.Parameters.AddWithValue("p_IdMarca", idMarca);
                        cmd.Parameters.AddWithValue("p_IdTitular", idTitular);
                        cmd.Parameters.AddWithValue("p_titulo", titulo);
                        cmd.Parameters.AddWithValue("p_tipo", tipo);
                        cmd.Parameters.AddWithValue("p_fecha_inicio", fechaInicio);
                        cmd.Parameters.AddWithValue("p_fecha_fin", fechaFin);
                        cmd.Parameters.AddWithValue("p_territorio", territorio);
                        cmd.Parameters.AddWithValue("p_nombre_razon_social", nombreRazonSocial);
                        cmd.Parameters.AddWithValue("p_direccion", direccion);
                        cmd.Parameters.AddWithValue("p_domicilio", domicilio);
                        cmd.Parameters.AddWithValue("p_nacionalidad", nacionalidad);
                        cmd.Parameters.AddWithValue("p_apoderado_representante_legal", apoderadoRepresentanteLegal);
                        cmd.Parameters.AddWithValue("p_estado", estado);
                        cmd.Parameters.AddWithValue("p_origen", origen);

                        cmd.ExecuteNonQuery();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al editar licencia de uso: " + ex.Message);
                    return false;
                }
            }
        }

        public bool ExisteOtraLicenciaUsoNoExclusiva(int idMarca, int idLicenciaExcluir, string origen)
{
            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("ExisteOtraLicenciaUsoNoExclusiva", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("p_IdMarca", idMarca);
                        cmd.Parameters.AddWithValue("p_IdLicenciaExcluir", idLicenciaExcluir);
                        cmd.Parameters.AddWithValue("p_origen", origen);

                        // Parámetro de salida
                        var existeLicenciaParam = new MySqlParameter("p_ExisteLicencia", MySqlDbType.Int32);
                        existeLicenciaParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(existeLicenciaParam);

                        cmd.ExecuteNonQuery();

                        int existeLicencia = Convert.ToInt32(existeLicenciaParam.Value);
                        return existeLicencia == 1;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error verificando licencia no exclusiva: " + ex.Message);
                    return false;
                }
    }
}


        public DataTable ObtenerLicenciasUsoNacionalesExclusivas(string estadoFiltro, int currentPageIndex, int pageSize)
        {
            DataTable dt = new DataTable();

            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("ObtenerLicenciasUsoNacionalesExclusivas", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        int registrosOmitidos = (currentPageIndex - 1) * pageSize;
                        cmd.Parameters.AddWithValue("estadoFiltro", estadoFiltro);
                        cmd.Parameters.AddWithValue("pageSize", pageSize);
                        cmd.Parameters.AddWithValue("registrosOmitidos", registrosOmitidos);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener licencias nacionales exclusivas: " + ex.Message);
                }
            }

            return dt;
        }

        public int GetTotalLicenciasUsoNacionalesExclusivas(string situacion)
        {
            int total = 0;

            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("GetTotalLicenciasUsoNacionalesExclusivas", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("situacion", situacion);

                        MySqlParameter outputParam = new MySqlParameter("totalMarcas", MySqlDbType.Int32);
                        outputParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(outputParam);

                        cmd.ExecuteNonQuery();

                        total = Convert.ToInt32(outputParam.Value);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener total de licencias nacionales exclusivas: " + ex.Message);
                }
            }

            return total;
        }

        public DataTable FiltrarLicenciasUsoNacionalesExclusivas(string valor, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();

            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("filtrarLicenciasUsoNacionalesExclusivas", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        int registrosOmitidos = (currentPageIndex - 1) * pageSize;
                        cmd.Parameters.AddWithValue("p_valor", valor);
                        cmd.Parameters.AddWithValue("pageSize", pageSize);
                        cmd.Parameters.AddWithValue("registrosOmitidos", registrosOmitidos);

                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            da.Fill(tabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al filtrar licencias nacionales exclusivas: " + ex.Message);
                }
            }

            return tabla;
        }

        public int GetFilteredLicenciasUsoNacionalesExclusivasCount(string valor)
        {
            int total = 0;

            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("GetFilteredLicenciasUsoNacionalesExclusivasCount", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("p_valor", valor);

                        // Parámetro de salida
                        MySqlParameter outputParam = new MySqlParameter("totalMarcas", MySqlDbType.Int32);
                        outputParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(outputParam);

                        cmd.ExecuteNonQuery();

                        total = Convert.ToInt32(outputParam.Value);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener el total de licencias filtradas: " + ex.Message);
                }
            }

            return total;
        }

        // no exclusivas
        public DataTable ObtenerLicenciasUsoNacionalesNoExclusivas(string estadoFiltro, int currentPageIndex, int pageSize)
        {
            DataTable dt = new DataTable();

            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("ObtenerLicenciasUsoNacionalesNoExclusivas", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        int registrosOmitidos = (currentPageIndex - 1) * pageSize;
                        cmd.Parameters.AddWithValue("estadoFiltro", estadoFiltro);
                        cmd.Parameters.AddWithValue("pageSize", pageSize);
                        cmd.Parameters.AddWithValue("registrosOmitidos", registrosOmitidos);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener licencias nacionales exclusivas: " + ex.Message);
                }
            }

            return dt;
        }

        public int GetTotalLicenciasUsoNacionalesNoExclusivas(string situacion)
        {
            int total = 0;

            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("GetTotalLicenciasUsoNacionalesNoExclusivas", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("situacion", situacion);

                        MySqlParameter outputParam = new MySqlParameter("totalMarcas", MySqlDbType.Int32);
                        outputParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(outputParam);

                        cmd.ExecuteNonQuery();

                        total = Convert.ToInt32(outputParam.Value);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener total de licencias nacionales exclusivas: " + ex.Message);
                }
            }

            return total;
        }

        public DataTable FiltrarLicenciasUsoNacionalesNoExclusivas(string valor, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();

            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("filtrarLicenciasUsoNacionalesNoExclusivas", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        int registrosOmitidos = (currentPageIndex - 1) * pageSize;
                        cmd.Parameters.AddWithValue("p_valor", valor);
                        cmd.Parameters.AddWithValue("pageSize", pageSize);
                        cmd.Parameters.AddWithValue("registrosOmitidos", registrosOmitidos);

                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            da.Fill(tabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al filtrar licencias nacionales exclusivas: " + ex.Message);
                }
            }

            return tabla;
        }

        public int GetFilteredLicenciasUsoNacionalesNoExclusivasCount(string valor)
        {
            int total = 0;

            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("GetFilteredLicenciasUsoNacionalesNoExclusivasCount", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("p_valor", valor);

                        // Parámetro de salida
                        MySqlParameter outputParam = new MySqlParameter("totalMarcas", MySqlDbType.Int32);
                        outputParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(outputParam);

                        cmd.ExecuteNonQuery();

                        total = Convert.ToInt32(outputParam.Value);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener el total de licencias filtradas: " + ex.Message);
                }
            }

            return total;
        }

        public bool VerificarCompatibilidadLicenciaUso(int idMarca, string tipoLicencia, string origen)
        {
            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("VerificarCompatibilidadLicenciaUso", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("p_IdMarca", idMarca);
                        cmd.Parameters.AddWithValue("p_TipoLicencia", tipoLicencia);
                        cmd.Parameters.AddWithValue("p_origen", origen);

                        var conflictoParam = new MySqlParameter("p_ExisteConflicto", MySqlDbType.Int32);
                        conflictoParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(conflictoParam);

                        cmd.ExecuteNonQuery();

                        int conflicto = Convert.ToInt32(conflictoParam.Value);
                        return conflicto == 1; 
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error verificando compatibilidad de licencia: " + ex.Message);
                    return true;
                }
            }
        }



        public DataTable ObtenerLicenciaUsoPorId(int idLicencia)
        {
            DataTable dt = new DataTable();

            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("ObtenerLicenciaPorId", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_id", idLicencia);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener la licencia de uso por ID: " + ex.Message);
                }
            }

            if (dt.Rows.Count > 0)
                return dt; // Devuelve la primera (y única) fila encontrada
            else
                return null; // No se encontró ninguna licencia
        }


        public (int total, DataTable datos) ObtenerLicenciasUsoNacionalesExclusivasCombinado(string estadoFiltro, int currentPageIndex, int pageSize)
        {
            int total = 0;
            DataTable datos = new DataTable();

            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("GetLicenciasUsoNacionalesExclusivasCombinado", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        int registrosOmitidos = (currentPageIndex - 1) * pageSize;
                        cmd.Parameters.AddWithValue("estadoFiltro", estadoFiltro);
                        cmd.Parameters.AddWithValue("pageSize", pageSize);
                        cmd.Parameters.AddWithValue("registrosOmitidos", registrosOmitidos);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            // 1. Primer resultado: total de registros
                            if (reader.Read())
                            {
                                total = reader.GetInt32("totalMarcas");
                            }

                            // 2. Avanzar al segundo resultado
                            if (reader.NextResult())
                            {
                                datos.Load(reader);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener licencias nacionales exclusivas combinadas: " + ex.Message);
                }
            }

            return (total, datos);
        }

        public (int total, DataTable datos) ObtenerLicenciasUsoNacionalesNoExclusivasCombinado(string estadoFiltro, int currentPageIndex, int pageSize)
        {
            int total = 0;
            DataTable datos = new DataTable();

            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("GetLicenciasUsoNacionalesNoExclusivasCombinado", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        int registrosOmitidos = (currentPageIndex - 1) * pageSize;
                        cmd.Parameters.AddWithValue("estadoFiltro", estadoFiltro);
                        cmd.Parameters.AddWithValue("pageSize", pageSize);
                        cmd.Parameters.AddWithValue("registrosOmitidos", registrosOmitidos);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            // 1. Primer resultado: total de registros
                            if (reader.Read())
                            {
                                total = reader.GetInt32("totalMarcas");
                            }

                            // 2. Avanzar al segundo resultado
                            if (reader.NextResult())
                            {
                                datos.Load(reader);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener licencias nacionales no exclusivas combinadas: " + ex.Message);
                }
            }

            return (total, datos);
        }

       

    public string CambiarEstadoLicencia(int idLicencia)
    {
        string resultado = "";

        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();

            using (var cmd = new MySqlCommand("CambiarEstadoLicencia", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("p_id", idLicencia);

                var output = new MySqlParameter("p_mensaje", MySqlDbType.VarChar, 100);
                output.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(output);

                cmd.ExecuteNonQuery();

                resultado = output.Value.ToString();
            }
        }

        return resultado;
    }


}
}
