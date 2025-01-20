using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class PatenteDao:ConnectionSQL
    {
        public void InsertarExpedientePatente(string numExpediente, int idMarca, string tipo)
        {
            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("InsertarExpedientePatente", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue("@p_NumExpediente", numExpediente);
                    comando.Parameters.AddWithValue("@p_IdMarca", idMarca);
                    comando.Parameters.AddWithValue("@p_tipo", tipo);

                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }
        public void ActualizarExpedientePatente(int p_id, string p_expediente, DateTime fecha, string estado,
            string anotaciones, string usuario)
        {
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("expedientePatente", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                        comando.Parameters.AddWithValue("@p_id", p_id);
                        comando.Parameters.AddWithValue("@p_expediente", string.IsNullOrEmpty(p_expediente) ? DBNull.Value : (object)p_expediente);
                        comando.Parameters.AddWithValue("@p_fecha", string.IsNullOrEmpty(fecha.ToString()) ? DBNull.Value : (object)fecha);
                        comando.Parameters.AddWithValue("@p_estado", string.IsNullOrEmpty(estado) ? DBNull.Value : (object)estado);
                        comando.Parameters.AddWithValue("@p_anotaciones", string.IsNullOrEmpty(anotaciones) ? DBNull.Value : (object)anotaciones);
                        comando.Parameters.AddWithValue("@p_usuario", string.IsNullOrEmpty(usuario) ? DBNull.Value : (object)usuario);

                        conexion.Open();
                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar expediente de la patente: {ex.Message}");
            }
        }

        public int InsertarPatente(
        string caso,
        string expediente,
        string nombre,
        string estado,
        string tipo,
        int idTitular,
        int idAgente,
        DateTime fechaSolicitud,
        string registro,
        string folio,
        string libro,
        DateTime? fechaRegistro,
        DateTime? fechaVencimiento,
        string erenov,
        string etrasp,
        int anualidades,
        string pct,
        string comprobantePagos,
        string descripcion,
        string reivindicaciones,
        string dibujos,
        string resumen,
        string documentoCesion,
        string poderNombramiento)
        {
            using (MySqlConnection connection = GetConnection()) 
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("InsertarPatente", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@p_caso", caso);
                    command.Parameters.AddWithValue("@p_expediente", expediente);
                    command.Parameters.AddWithValue("@p_nombre", nombre);
                    command.Parameters.AddWithValue("@p_estado", estado);
                    command.Parameters.AddWithValue("@p_tipo", tipo);
                    command.Parameters.AddWithValue("@p_IdTitular", idTitular);
                    command.Parameters.AddWithValue("@p_IdAgente", idAgente);
                    command.Parameters.AddWithValue("@p_fecha_solicitud", fechaSolicitud);
                    command.Parameters.AddWithValue("@p_registro", registro);
                    command.Parameters.AddWithValue("@p_folio", folio);
                    command.Parameters.AddWithValue("@p_libro", libro);
                    command.Parameters.AddWithValue("@p_fecha_registro", fechaRegistro);
                    command.Parameters.AddWithValue("@p_fecha_vencimiento", fechaVencimiento);
                    command.Parameters.AddWithValue("@p_Erenov", erenov);
                    command.Parameters.AddWithValue("@p_Etrasp", etrasp);
                    command.Parameters.AddWithValue("@p_anualidades", anualidades);
                    command.Parameters.AddWithValue("@p_pct", pct);
                    command.Parameters.AddWithValue("@p_comprobante_pagos", comprobantePagos);
                    command.Parameters.AddWithValue("@p_descripcion", descripcion);
                    command.Parameters.AddWithValue("@p_reivindicaciones", reivindicaciones);
                    command.Parameters.AddWithValue("@p_dibujos", dibujos);
                    command.Parameters.AddWithValue("@p_resumen", resumen);
                    command.Parameters.AddWithValue("@p_documento_cesion", documentoCesion);
                    command.Parameters.AddWithValue("@p_poder_nombramiento", poderNombramiento);

                    MySqlParameter outputParam = new MySqlParameter("p_id_patente", MySqlDbType.Int32)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParam);

                    command.ExecuteNonQuery();
                    int idPatente = Convert.ToInt32(outputParam.Value);
                    return idPatente;
                }
            }
        }
        //patentes ingresadas
        public int GetTotalPatentesSinRegistro()
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetTotalPatentesSinRegistro", conexion))
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
        public int GetFilteredPatentesSinRegistroCount(string value)
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetFilteredPatentesSinRegistroCount", conexion))
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
        public DataTable GetAllPatentesEnTramite(int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("ObtenerPatentesSinRegistro", conexion))
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
                Console.WriteLine($"Error al obtener las patentes: {ex.Message}");
            }
            return tabla;
        }
        public DataTable FiltrarPatentesEnTramite(string filtro, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("FiltrarPatentesEnTramite", conexion))
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
                Console.WriteLine($"Error al obtener las patentes sin registro: {ex.Message}");
            }
            return tabla;
        }

        //patentes registradas
        public int GetTotalPatentesRegistradas()
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetTotalPatentesRegistradas", conexion))
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
        public int GetFilteredPatentesRegistradasCount(string value)
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetFilteredPatentesRegistradasCount", conexion))
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
        public DataTable GetAllPatentesRegistradas(int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("ObtenerPatentesRegistradas", conexion))
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
                Console.WriteLine($"Error al obtener las patentes: {ex.Message}");
            }
            return tabla;
        }
        public DataTable FiltrarPatentesRegistradas(string filtro, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("FiltrarPatentesRegistradas", conexion))
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
                Console.WriteLine($"Error al obtener las patentes sin registro: {ex.Message}");
            }
            return tabla;
        }

        //patentes en renovacion
        public int GetTotalPatentesRegistradasEnTramiteDeRenovacion()
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetTotalPatentesRegistradasEnTramiteDeRenovacion", conexion))
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
        public int GetFilteredPatentesRegistradasEnTramiteDeRenovacionCount(string value)
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetFilteredPatentesRegistradasEnTramiteDeRenovacionCount", conexion))
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
        public DataTable GetAllPatentesRegistradasEnTramiteDeRenovacion(int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("ObtenerPatentesEnTramiteRenovacion", conexion))
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
                Console.WriteLine($"Error al obtener las patentes: {ex.Message}");
            }
            return tabla;
        }
        public DataTable FiltrarPatentesRegistradasEnTramiteDeRenovacion(string filtro, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("FiltrarPatentesRegistradasEnTramiteDeRenovacion", conexion))
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
                Console.WriteLine($"Error al obtener las patentes sin registro: {ex.Message}");
            }
            return tabla;
        }
        //patentes en traspaso
        public int GetTotalPatentesRegistradasEnTramiteDeTraspaso()
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetTotalPatentesRegistradasEnTramiteDeTraspaso", conexion))
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
        public int GetFilteredPatentesRegistradasEnTramiteDeTraspasoCount(string value)
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetFilteredPatentesRegistradasEnTramiteDeTraspasoCount", conexion))
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
        public DataTable GetAllPatentesRegistradasEnTramiteDeTraspaso(int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("ObtenerPatentesEnTramiteTraspaso", conexion))
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
                Console.WriteLine($"Error al obtener las patentes: {ex.Message}");
            }
            return tabla;
        }
        public DataTable FiltrarPatentesRegistradasEnTramiteDeTraspaso(string filtro, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("FiltrarPatentesRegistradasEnTramiteDeTraspaso", conexion))
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
                Console.WriteLine($"Error al obtener las patentes sin registro: {ex.Message}");
            }
            return tabla;
        }

        //patentes en abandono
        public int GetTotalPatentesEnAbandono()
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetTotalPatentesEnAbandono", conexion))
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
        public int GetFilteredPatentesEnAbandonoCount(string value)
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetFilteredPatentesEnAbandonoCount", conexion))
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
        public DataTable GetAllPatentesEnAbandono(int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("ObtenerPatentesEnAbandono", conexion))
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
                Console.WriteLine($"Error al obtener las patentes: {ex.Message}");
            }
            return tabla;
        }
        public DataTable FiltrarPatentesEnAbandono(string filtro, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("FiltrarPatentesEnAbandono", conexion))
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
                Console.WriteLine($"Error al obtener las patentes sin registro: {ex.Message}");
            }
            return tabla;
        }

        public DataTable ObtenerPatentePorId(int idPatente)
        {
            DataTable patente = new DataTable();

            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("ObtenerPatentePorId", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@patenteId", idPatente);
                        conexion.Open();

                        using (MySqlDataReader reader = comando.ExecuteReader())
                        {
                            patente.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener la patente por ID: {ex.Message}");
            }

            return patente;
        }

        public bool EditarPatente(
            int id,
            string caso,
            string expediente,
            string nombre,
            string estado,
            string tipo,
            int idTitular,
            int idAgente,
            DateTime fechaSolicitud,
            string registro,
            string folio,
            string libro,
            DateTime? fechaRegistro,
            DateTime? fechaVencimiento,
            string erenov,
            string etrasp,
            int anualidades,
            string pct,
            string comprobantePagos,
            string descripcion,
            string reivindicaciones,
            string dibujos,
            string resumen,
            string documentoCesion,
            string poderNombramiento)
        {
            try
            {
                using (MySqlConnection connection = GetConnection())
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand("EditarPatente", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@p_id", id);
                        command.Parameters.AddWithValue("@p_caso", caso);
                        command.Parameters.AddWithValue("@p_expediente", expediente);
                        command.Parameters.AddWithValue("@p_nombre", nombre);
                        command.Parameters.AddWithValue("@p_estado", estado);
                        command.Parameters.AddWithValue("@p_tipo", tipo);
                        command.Parameters.AddWithValue("@p_IdTitular", idTitular);
                        command.Parameters.AddWithValue("@p_IdAgente", idAgente);
                        command.Parameters.AddWithValue("@p_fecha_solicitud", fechaSolicitud);
                        command.Parameters.AddWithValue("@p_registro", registro);
                        command.Parameters.AddWithValue("@p_folio", folio);
                        command.Parameters.AddWithValue("@p_libro", libro);
                        command.Parameters.AddWithValue("@p_fecha_registro", fechaRegistro);
                        command.Parameters.AddWithValue("@p_fecha_vencimiento", fechaVencimiento);
                        command.Parameters.AddWithValue("@p_Erenov", erenov);
                        command.Parameters.AddWithValue("@p_Etrasp", etrasp);
                        command.Parameters.AddWithValue("@p_anualidades", anualidades);
                        command.Parameters.AddWithValue("@p_pct", pct);
                        command.Parameters.AddWithValue("@p_comprobante_pagos", comprobantePagos);
                        command.Parameters.AddWithValue("@p_descripcion", descripcion);
                        command.Parameters.AddWithValue("@p_reivindicaciones", reivindicaciones);
                        command.Parameters.AddWithValue("@p_dibujos", dibujos);
                        command.Parameters.AddWithValue("@p_resumen", resumen);
                        command.Parameters.AddWithValue("@p_documento_cesion", documentoCesion);
                        command.Parameters.AddWithValue("@p_poder_nombramiento", poderNombramiento);

                      
                        command.ExecuteNonQuery();
                        return true; 
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al editar la patente: {ex.Message}");
                return false;
            }
        }



    }
}
