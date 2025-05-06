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
        public bool InsertarLicenciaUso
        (
            int idMarca,
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

        public bool ExisteLicenciaUsoExclusiva(int idMarca)
        {
            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("ExisteLicenciaUsoExclusiva", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("p_IdMarca", idMarca);

                        MySqlParameter existeParam = new MySqlParameter("p_ExisteLicencia", MySqlDbType.Int32);
                        existeParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(existeParam);

                        cmd.ExecuteNonQuery();

                        int existe = Convert.ToInt32(existeParam.Value);

                        return existe == 1;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al verificar licencia de uso exclusiva: " + ex.Message);
                    throw;
                }
            }
        }


    }
}
