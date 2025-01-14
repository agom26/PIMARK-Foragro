using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class VencimientoDao:ConnectionSQL
    {
        
        public (string CorreoTitular, string CorreoAgente) GetCorreosPorMarcaId(int id)
        {
            (string CorreoTitular, string CorreoAgente) correos = (null, null);

            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    conexion.Open();

                    string query = @"
                SELECT DISTINCT 
                    T.correo AS CorreoTitular, 
                    A.correo AS CorreoAgente 
                FROM 
                    Marcas M 
                JOIN 
                    Personas T ON M.IdTitular = T.id 
                JOIN 
                    Personas A ON M.IdAgente = A.id 
                WHERE 
                    M.id = @id;
            ";

                    using (MySqlCommand comando = new MySqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@id", id);

                        using (MySqlDataReader reader = comando.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                correos.CorreoTitular = reader.IsDBNull(reader.GetOrdinal("CorreoTitular"))
                                    ? null
                                    : reader.GetString("CorreoTitular");

                                correos.CorreoAgente = reader.IsDBNull(reader.GetOrdinal("CorreoAgente"))
                                    ? null
                                    : reader.GetString("CorreoAgente");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener correos por marca ID: {ex.Message}");
            }

            return correos;
        }
        //vencimientos normales
        public int GetTotalVencimientos()
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetTotalVencimientos", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    MySqlParameter paramTotalMarcas = new MySqlParameter("totalMarcas", MySqlDbType.Int32)
                    {
                        Direction = ParameterDirection.Output
                    };
                    comando.Parameters.Add(paramTotalMarcas);

                    conexion.Open();
                    comando.ExecuteNonQuery();
                    totalMarcas = Convert.ToInt32(paramTotalMarcas.Value);
                }
            }

            return totalMarcas;
        }
        public int GetFilteredVencimientosCount(string value)
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetFilteredVencimientosCount", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    // Parámetro de entrada
                    comando.Parameters.AddWithValue("@value", value);

                    // Parámetro de salida
                    MySqlParameter totalMarcasParam = new MySqlParameter("@totalMarcas", MySqlDbType.Int32);
                    totalMarcasParam.Direction = ParameterDirection.Output;
                    comando.Parameters.Add(totalMarcasParam);

                    conexion.Open();

                    comando.ExecuteNonQuery();

                    totalMarcas = Convert.ToInt32(totalMarcasParam.Value);
                }
            }

            return totalMarcas;
        }
        public DataTable GetAllVencimientosPaginados(int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("ObtenerVencimientosP", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        int registrosOmitidos = (currentPageIndex - 1) * pageSize;
                        // Agregar parámetros de entrada
                        comando.Parameters.AddWithValue("pageSize", pageSize);
                        comando.Parameters.AddWithValue("registrosOmitidos", registrosOmitidos);

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
                Console.WriteLine($"Error al obtener los vencimientos: {ex.Message}");
            }
            return tabla;
        }
        public DataTable FiltrarVencimientos(string filtro, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("FiltrarVencimientos", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        int registrosOmitidos = (currentPageIndex - 1) * pageSize;

                        comando.Parameters.AddWithValue("pageSize", pageSize);
                        comando.Parameters.AddWithValue("registrosOmitidos", registrosOmitidos);
                        comando.Parameters.AddWithValue("@p_valor", string.IsNullOrEmpty(filtro) ? DBNull.Value : (object)filtro);

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
                Console.WriteLine($"Error al obtener los vencimientos: {ex.Message}");
            }
            return tabla;
        }
        

        public DataTable GetVencimientoByValue(string value)
        {
            DataTable tabla = new DataTable();
            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("SELECT * FROM Vencimientos WHERE (nombre LIKE @value OR fecha_vencimiento LIKE @value OR clase LIKE @value OR tipo LIKE @value OR registro LIKE @value OR folio LIKE @value OR libro LIKE @value OR titular LIKE @value OR agente LIKE @value)", conexion)) // Inicializa correctamente el comando
                {
                    comando.Parameters.AddWithValue("@value", value);
                    conexion.Open();
                    using (MySqlDataReader leer = comando.ExecuteReader()) 
                    {

                        tabla.Load(leer);
                    }
                }
            }
            return tabla;
        }

        public void EjecutarProcedimientoInsertarVencimientos()
        {
            using (MySqlConnection conexion = GetConnection())
            {
                using (var command = new MySqlCommand("InsertarVencimientos", conexion))
                {
                    conexion.Open();
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        command.ExecuteNonQuery(); 
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al ejecutar el procedimiento: {ex.Message}");
                        
                    }
                }
            }
        }
        public void ActualizarNotificado(int id, string tipo)
        {
            using (MySqlConnection conexion = GetConnection())
            {
                using (var command = new MySqlCommand("ActualizarNotificado", conexion))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@p_id", id);
                    command.Parameters.AddWithValue("@p_tipo", tipo);
                    try
                    {
                        conexion.Open();
                        command.ExecuteNonQuery(); 
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al actualizar el estado de notificado: {ex.Message}");
                        
                    }
                }
            }
        }

        public void EditarTextoRtf(string tipo, string mensaje)
        {
            using (MySqlConnection conexion = GetConnection())
            {
                using (var command = new MySqlCommand("EditarTextoRtf", conexion))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@p_tipo", tipo);
                    command.Parameters.AddWithValue("@p_mensaje", mensaje);

                    try
                    {
                        conexion.Open();
                        command.ExecuteNonQuery(); 
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al ejecutar el procedimiento EditarTextoRtf: {ex.Message}");
                    }
                }
            }
        }

        public string ObtenerTextoRtfPorTipo(string tipo)
        {
            string mensajeRtf = null;

            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    conexion.Open();

                    using (MySqlCommand comando = new MySqlCommand("ObtenerTextoRtfPorTipo", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                        comando.Parameters.AddWithValue("@p_tipo", tipo);

                        MySqlParameter mensajeParam = new MySqlParameter("@p_mensaje", MySqlDbType.Text)
                        {
                            Direction = ParameterDirection.Output
                        };
                        comando.Parameters.Add(mensajeParam);

                        comando.ExecuteNonQuery();

                        mensajeRtf = mensajeParam.Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el texto RTF: {ex.Message}");
            }

            return mensajeRtf;
        }

        public DataTable ObtenerTodosLosVencimientosReporte()
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("ObtenerVencimientos", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

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
                Console.WriteLine($"Error al obtener los vencimientos: {ex.Message}");

            }
            return tabla;
        }

        public DataTable ObtenerTodosLosVencimientosFiltradosReporte(string valor)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("FiltrarVencimientosReporte", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@p_valor", valor);
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
                Console.WriteLine($"Error al obtener los vencimientos: {ex.Message}");

            }
            return tabla;
        }



    }
}
