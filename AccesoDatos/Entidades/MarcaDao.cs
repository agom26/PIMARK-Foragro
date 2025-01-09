using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Google.Protobuf.WellKnownTypes;

namespace AccesoDatos.Entidades
{
    public class MarcaDao:ConnectionSQL
    {
        public int GetTotalMarcasSinRegistro()
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetTotalMarcasSinRegistro", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    MySqlParameter paramTotalMarcas = new MySqlParameter("totalMarcas", MySqlDbType.Int32)
                    {
                        Direction = ParameterDirection.Output
                    };
                    comando.Parameters.Add(paramTotalMarcas);

                    conexion.Open();
                    comando.ExecuteNonQuery();  // Ejecutar el procedimiento almacenado

                    // Obtener el valor de totalUsuarios desde el parámetro de salida
                    totalMarcas = Convert.ToInt32(paramTotalMarcas.Value);
                }
            }

            return totalMarcas;
        }
        public int GetFilteredMarcasSinRegistroCount(string value)
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetFilteredMarcasSinRegistroCount", conexion))
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
        public void ActualizarExpedienteMarca(int p_id, string p_expediente, DateTime fecha, string estado, 
            string anotaciones, string usuario)
        {
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("expedienteMarca", conexion))
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
                Console.WriteLine($"Error al actualizar expediente de la marca: {ex.Message}");
            }
        }

        public DataTable Filtrar(string tipo_filtro,
        string? estado, string? nombre, string? pais, string? folio, string? libro,
        string? registro, string? clase, 
        string? fechaSolicitudInicio, string? fechaSolicitudFin,
        string? fechaRegistroInicio, string? fechaRegistroFin,
        string? fechaVencimientoInicio, string? fechaVencimientoFin,
        string? titular, string? agente,string? cliente)
        {
            DataTable dataTable = new DataTable();

            using (MySqlConnection conexion = GetConnection())
            {
                conexion.Open();
                using (MySqlCommand cmd = new MySqlCommand("Filtrar", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("tipo_filtro", tipo_filtro);
                    cmd.Parameters.AddWithValue("estado_filtro", estado ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_nombre", nombre ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_pais", pais ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_folio", folio ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_tomo", libro ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_registro", registro ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_clase", clase ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("P_solicitud_inicio", fechaSolicitudInicio ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_solicitud_fin", fechaSolicitudFin ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_registro_inicio", fechaRegistroInicio?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_registro_fin", fechaRegistroFin?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_vencimiento_inicio", fechaVencimientoInicio ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_vencimiento_fin", fechaVencimientoFin ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_titular", titular ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_agente", agente ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_cliente", cliente ?? (object)DBNull.Value);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(dataTable);  
                    }
                }
            }

            return dataTable; 
        }
        



        public DataTable FiltrarMarcasNacionalesEnTramite(string filtro, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("filtrarMarcasSinRegistro", conexion))
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
                Console.WriteLine($"Error al obtener las marcas sin registro: {ex.Message}");
            }
            return tabla;
        }

        public DataTable FiltrarMarcasNacionalesEnOposicion(string filtro)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("filtrarMarcasRecibidasEnOposicion", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

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
                Console.WriteLine($"Error al obtener las marcas : {ex.Message}");
            }
            return tabla;
        }

        public DataTable FiltrarMarcasNacionalesEnOposicionInterpuestas(string filtro)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("filtrarMarcasInterpuestasEnOposicion", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

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
                Console.WriteLine($"Error al obtener las marcas : {ex.Message}");
            }
            return tabla;
        }

        public DataTable FiltrarMarcasInternacionalesEnOposicion(string filtro)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand(" filtrarMarcasInternacionalesRecibidasEnOposicion ", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

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
                Console.WriteLine($"Error al obtener las marcas : {ex.Message}");
            }
            return tabla;
        }

        public DataTable FiltrarMarcasInternacionalesEnOposicionInterpuestas(string filtro)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("filtrarMarcasInternacionalesInterpuestasEnOposicion", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

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
                Console.WriteLine($"Error al obtener las marcas : {ex.Message}");
            }
            return tabla;
        }


        public DataTable FiltrarMarcasNacionalesRegistradas(string filtro, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("filtrarMarcasRegistradas", conexion))
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
                Console.WriteLine($"Error al obtener las marcas sin registro: {ex.Message}");
            }
            return tabla;
        }
        

            public DataTable FiltrarMarcasNacionalesEnTramiteDeRenovacion(string filtro)
            {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("filtrarMarcasEnRenovacion", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

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
                Console.WriteLine($"Error al obtener las marcas sin registro: {ex.Message}");
            }
            return tabla;
        }
        public DataTable FiltrarMarcasNacionalesEnTramiteDeTraspaso(string filtro)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("filtrarMarcaEnTramiteDeTraspaso", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

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
                Console.WriteLine($"Error al obtener las marcas sin registro: {ex.Message}");
            }
            return tabla;
        }

        public DataTable GetAllMarcasInternacionales()
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("ObtenerMarcasInternacionales", conexion))
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
                Console.WriteLine($"Error al obtener las marcas internacionales: {ex.Message}");

            }
            return tabla;
        }

        public DataTable GetAllMarcasNacionales()
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("ObtenerMarcasNacionales", conexion))
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
                Console.WriteLine($"Error al obtener las marcas internacionales: {ex.Message}");

            }
            return tabla;
        }
        public DataTable GetAllMarcasNacionalesEnTramite(int currentPageIndex, int pageSize)
        {
           
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection()) 
                {
                    using (MySqlCommand comando = new MySqlCommand("ObtenerMarcasSinRegistro", conexion))
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
                Console.WriteLine($"Error al obtener las marcas sin registro: {ex.Message}");
               
            }
            return tabla; 
        }

        public DataTable GetAllMarcasInternacionalesIngresadas()
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection()) 
                {
                    using (MySqlCommand comando = new MySqlCommand("ObtenerMarcasInternacionalesSinRegistro", conexion))
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
                Console.WriteLine($"Error al obtener las marcas sin registro: {ex.Message}");

            }
            return tabla;
        }

        public DataTable GetAllMarcasNacionalesEnOposicion()
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection()) 
                {
                    using (MySqlCommand comando = new MySqlCommand(@"
                    SELECT  
                        M.id,
                        M.nombre AS Nombre, 
                        M.estado AS Estado,
                        M.expediente As Expediente,
                        M.clase AS Clase,  
                        P1.nombre AS Titular, 
                        P2.nombre AS Agente
                    FROM 
                        `Marcas` M
                    JOIN 
                        Personas AS P1 ON M.IdTitular = P1.id 
                    JOIN 
                        Personas AS P2 ON M.IdAgente = P2.id 
                    WHERE 
                        M.tipo = 'nacional' AND 
                        (estado='Oposición')
                    ORDER BY 
                        M.id DESC;", conexion))
                    {
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
                Console.WriteLine($"Error al obtener las marcas nacionales: {ex.Message}");
                
            }
            return tabla;
        }
        public DataTable GetAllMarcasInternacionalesEnOposicion()
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection()) 
                {
                    using (MySqlCommand comando = new MySqlCommand("ObtenerOposicionesInternacionalesRecibidas", conexion))
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
                Console.WriteLine($"Error al obtener las marcas sin registro: {ex.Message}");

            }
            return tabla;
        }

        public DataTable GetAllMarcasInternacionalesEnOposicionInterpuestas()
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("ObtenerOposicionesInternacionalesInterpuestas", conexion))
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
                Console.WriteLine($"Error al obtener las marcas sin registro: {ex.Message}");

            }
            return tabla;
        }
        public int GetTotalMarcasRegistradas()
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetTotalMarcasRegistradas", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    MySqlParameter paramTotalMarcas = new MySqlParameter("totalMarcas", MySqlDbType.Int32)
                    {
                        Direction = ParameterDirection.Output
                    };
                    comando.Parameters.Add(paramTotalMarcas);

                    conexion.Open();
                    comando.ExecuteNonQuery();  // Ejecutar el procedimiento almacenado

                    // Obtener el valor de totalUsuarios desde el parámetro de salida
                    totalMarcas = Convert.ToInt32(paramTotalMarcas.Value);
                }
            }

            return totalMarcas;
        }
        public int GetFilteredMarcasRegistradasCount(string value)
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetFilteredMarcasRegistradasCount", conexion))
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
        public DataTable GetAllMarcasNacionalesRegistradas(int currentPageIndex, int pageSize)
        {
            string estadoFiltro = "Registrada";
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection()) 
                {
                    using (MySqlCommand comando = new MySqlCommand("ObtenerMarcasRegistradas", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        int registrosOmitidos = (currentPageIndex - 1) * pageSize;
                        // Agregar parámetros de entrada
                        comando.Parameters.AddWithValue("pageSize", pageSize);
                        comando.Parameters.AddWithValue("registrosOmitidos", registrosOmitidos);
                        comando.Parameters.AddWithValue("@estadoFiltro", estadoFiltro);

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
                Console.WriteLine($"Error al obtener las marcas nacionales: {ex.Message}");
                
            }
            return tabla; 
        }

        public DataTable GetAllMarcasInternacionalesRegistradas()
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("ObtenerMarcasInternacionalesRegistradas", conexion))
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
                Console.WriteLine($"Error al obtener las marcas internacionales: {ex.Message}");
            }
            return tabla;
        }


        public DataTable GetAllMarcasNacionalesEnAbandono()
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection()) 
                {
                    using (MySqlCommand comando = new MySqlCommand(@"
                    SELECT  
                        M.id,
                        M.nombre AS Nombre, 
                        M.estado AS Estado,
                        M.Observaciones as Observaciones,
                        M.expediente As Expediente,
                        M.clase AS Clase,  
                        P1.nombre AS Titular, 
                        P2.nombre AS Agente
                        
                    FROM 
                        `Marcas` M
                    JOIN 
                        Personas AS P1 ON M.IdTitular = P1.id 
                    JOIN 
                        Personas AS P2 ON M.IdAgente = P2.id 
                    WHERE 
                        M.tipo = 'nacional' AND 
                        (estado='Abandono')
                    ORDER BY 
                        M.id DESC;", conexion))
                    {
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
                Console.WriteLine($"Error al obtener las marcas nacionales: {ex.Message}");
                
            }
            return tabla; 
        }

       
        public DataTable GetAllMarcasInternacionalesEnAbandono()
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("ObtenerMarcasInternacionalesEnAbandono", conexion))
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
                Console.WriteLine($"Error al obtener las marcas nacionales en abandono: {ex.Message}");
            }
            return tabla;
        }



                          
        public int AddMarcaNacional(string expediente, string nombre, string signoDistintivo,string tipoSigno, string clase, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud, int? idCliente)
        {
            using (var connection = GetConnection()) 
            {
                connection.Open();

                using (var command = new MySqlCommand(@"INSERT INTO Marcas (expediente, nombre, signo_distintivo,tipoSigno, clase, logo, idTitular, idAgente, fecha_solicitud, tipo, idCliente) 
                                                  VALUES (@expediente, @nombre, @signoDistintivo,@tipoSigno, @clase, @logo, @idPersonaTitular, @idPersonaAgente, @fecha_solicitud, 'nacional',@idCliente); 
                                                  SELECT LAST_INSERT_ID();", connection))
                {
                    command.Parameters.AddWithValue("@expediente", expediente);
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@signoDistintivo", signoDistintivo);
                    command.Parameters.AddWithValue("@tipoSigno", tipoSigno);
                    command.Parameters.AddWithValue("@clase", clase);
                    command.Parameters.AddWithValue("@logo", logo); 
                    command.Parameters.AddWithValue("@idPersonaTitular", idPersonaTitular);
                    command.Parameters.AddWithValue("@idPersonaAgente", idPersonaAgente);
                    command.Parameters.AddWithValue("@fecha_solicitud", fecha_solicitud);
                    command.Parameters.AddWithValue("@idCliente", idCliente);

                    int idMarca = Convert.ToInt32(command.ExecuteScalar());
                    return idMarca; 
                }
            }
        }


        public int AddMarcaInternacional(string expediente, string nombre, string signoDistintivo, string tipoSigno, string clase, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud, string pais_de_registro, string tiene_poder, int? idCliente)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                
                using (var command = new MySqlCommand(@"INSERT INTO Marcas (expediente, nombre, signo_distintivo, tipoSigno, clase, logo, idTitular, idAgente, fecha_solicitud, pais_de_registro, tiene_poder, idCliente, tipo) 
                                                  VALUES (@expediente, @nombre, @signoDistintivo, @tipoSigno, @clase, @logo, @idPersonaTitular, @idPersonaAgente, @fecha_solicitud, @pais_de_registro, @tiene_poder, @idCliente, 'internacional'); 
                                                  SELECT LAST_INSERT_ID();", connection))
                {
                    command.Parameters.AddWithValue("@expediente", expediente);
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@signoDistintivo", signoDistintivo);
                    command.Parameters.AddWithValue("@tipoSigno", tipoSigno);
                    command.Parameters.AddWithValue("@clase", clase);
                    command.Parameters.AddWithValue("@logo", logo);
                    command.Parameters.AddWithValue("@idPersonaTitular", idPersonaTitular);
                    command.Parameters.AddWithValue("@idPersonaAgente", idPersonaAgente);
                    command.Parameters.AddWithValue("@fecha_solicitud", fecha_solicitud);
                    command.Parameters.AddWithValue("@pais_de_registro", pais_de_registro);
                    command.Parameters.AddWithValue("@tiene_poder", tiene_poder);
                    command.Parameters.AddWithValue("@idCliente", idCliente);

                    int idMarca = Convert.ToInt32(command.ExecuteScalar());
                    return idMarca;
                }
            }
        }


        
        public int AddMarcaNacionalRegistrada(string expediente, string nombre, string signoDistintivo, string tipoSigno, string clase, string folio,
                                      string libro, byte[] logo, int idPersonaTitular, int idPersonaAgente,
                                      DateTime fecha_solicitud, string registro,
                                      DateTime fechaRegistro, DateTime fechaVencimiento, int? idCliente)
        {
            int idMarca = 0;
            using (var connection = GetConnection()) 
            {
                connection.Open();
                using (var command = new MySqlCommand(@"
                    INSERT INTO Marcas (expediente, nombre, signo_distintivo, tipoSigno, clase, folio, libro, logo, idTitular, idAgente, 
                                        fecha_solicitud, tipo, registro, fecha_registro, fecha_vencimiento, idCliente) 
                    VALUES (@expediente, @nombre, @signoDistintivo, @tipoSigno, @clase, @folio, @libro, @logo, @idPersonaTitular, 
                            @idPersonaAgente, @fecha_solicitud, 'nacional', @registro, @fecha_registro, @fecha_vencimiento, @idCliente);
                    SELECT LAST_INSERT_ID();", connection))
                {
                    command.Parameters.AddWithValue("@expediente", expediente);
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@signoDistintivo", signoDistintivo);
                    command.Parameters.AddWithValue("@tipoSigno", tipoSigno);
                    command.Parameters.AddWithValue("@clase", clase);
                    command.Parameters.AddWithValue("@folio", folio);
                    command.Parameters.AddWithValue("@libro", libro);
                    command.Parameters.AddWithValue("@logo", logo); 
                    command.Parameters.AddWithValue("@idPersonaTitular", idPersonaTitular);
                    command.Parameters.AddWithValue("@idPersonaAgente", idPersonaAgente);
                    command.Parameters.AddWithValue("@fecha_solicitud", fecha_solicitud);
                    command.Parameters.AddWithValue("@registro", registro);
                    command.Parameters.AddWithValue("@fecha_registro", fechaRegistro);
                    command.Parameters.AddWithValue("@fecha_vencimiento", fechaVencimiento);
                    command.Parameters.AddWithValue("@idCliente", idCliente);

                    idMarca = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            return idMarca; 
        }


        public int AddMarcaInternacionalRegistrada(string expediente, string nombre, string signoDistintivo, string tipoSigno, string clase, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud, string pais_de_registro, string tiene_poder, int? idCliente, string registro, string folio, string libro, DateTime fechaRegistro, DateTime fechaVencimiento)
        {
            int idMarca = 0;
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(@"
                    INSERT INTO Marcas (expediente, nombre, signo_distintivo, tipoSigno, clase, logo, idTitular, idAgente, 
                                        fecha_solicitud, pais_de_registro, tiene_poder, idCliente, tipo, 
                                        registro, folio, libro, fecha_registro, fecha_vencimiento) 
                    VALUES (@expediente, @nombre, @signoDistintivo, @tipoSigno, @clase, @logo, @idPersonaTitular, 
                            @idPersonaAgente, @fecha_solicitud, @pais_de_registro, @tiene_poder, @idCliente, 
                            'internacional', @registro, @folio, @libro, @fecha_registro, @fecha_vencimiento);
                    SELECT LAST_INSERT_ID();", connection))
                {
                    command.Parameters.AddWithValue("@expediente", expediente);
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@signoDistintivo", signoDistintivo);
                    command.Parameters.AddWithValue("@tipoSigno", tipoSigno);
                    command.Parameters.AddWithValue("@clase", clase);
                    command.Parameters.AddWithValue("@logo", logo);
                    command.Parameters.AddWithValue("@idPersonaTitular", idPersonaTitular);
                    command.Parameters.AddWithValue("@idPersonaAgente", idPersonaAgente);
                    command.Parameters.AddWithValue("@fecha_solicitud", fecha_solicitud);
                    command.Parameters.AddWithValue("@pais_de_registro", pais_de_registro);
                    command.Parameters.AddWithValue("@tiene_poder", tiene_poder);
                    command.Parameters.AddWithValue("@idCliente", idCliente);
                    command.Parameters.AddWithValue("@registro", registro);
                    command.Parameters.AddWithValue("@folio", folio);
                    command.Parameters.AddWithValue("@libro", libro);
                    command.Parameters.AddWithValue("@fecha_registro", fechaRegistro);
                    command.Parameters.AddWithValue("@fecha_vencimiento", fechaVencimiento);

                    idMarca = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            return idMarca; 
        }


        public DataTable GetMarcaNacionalById(int id)
        {
            DataTable marcaTable = new DataTable();

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("ObtenerMarcaNacionalPorId", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@marcaId", id);

                    conexion.Open();

                    using (MySqlDataReader reader = comando.ExecuteReader())
                    {
                        marcaTable.Load(reader);
                    }
                }
            }

            return marcaTable;
        }



        public bool EditMarcaNacional(int id, string expediente, string nombre, string signoDistintivo, string tipoSigno, string clase, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud, int? idCliente)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(@"
            UPDATE Marcas 
            SET expediente = @expediente, 
                nombre = @nombre, 
                signo_distintivo = @signoDistintivo, 
                tipoSigno=@tipoSigno,
                clase = @clase, 
                logo = @logo, 
                idTitular = @idPersonaTitular, 
                idAgente = @idPersonaAgente, 
                fecha_solicitud = @fecha_solicitud,
                idCliente=@idCliente
            WHERE (id = @id) AND (tipo = 'nacional');", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@expediente", expediente);
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@signoDistintivo", signoDistintivo);
                    command.Parameters.AddWithValue("@tipoSigno", tipoSigno);
                    command.Parameters.AddWithValue("@clase", clase);
                    command.Parameters.AddWithValue("@logo", logo); 
                    command.Parameters.AddWithValue("@idPersonaTitular", idPersonaTitular);
                    command.Parameters.AddWithValue("@idPersonaAgente", idPersonaAgente);
                    command.Parameters.AddWithValue("@fecha_solicitud", fecha_solicitud);
                    command.Parameters.AddWithValue("@idCliente", idCliente);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; 
                }
            }
        }


        public bool EditMarcaNacionalRegistrada(int id, string expediente, string nombre, string signoDistintivo, string tipoSigno, string clase, string folio, string libro, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud, string registro, DateTime fechaRegistro, DateTime fechaVencimiento, string erenov, string etrasp, int? idCliente)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                
                using (var command = new MySqlCommand("EditMarcaNacionalRegistrada", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@p_id", id);
                    command.Parameters.AddWithValue("@p_expediente", expediente);
                    command.Parameters.AddWithValue("@p_nombre", nombre);
                    command.Parameters.AddWithValue("@p_signoDistintivo", signoDistintivo);
                    command.Parameters.AddWithValue("@p_tipoSigno", tipoSigno);
                    command.Parameters.AddWithValue("@p_clase", clase);
                    command.Parameters.AddWithValue("@p_folio", folio);
                    command.Parameters.AddWithValue("@p_libro", libro);
                    command.Parameters.AddWithValue("@p_logo", logo);
                    command.Parameters.AddWithValue("@p_idPersonaTitular", idPersonaTitular);
                    command.Parameters.AddWithValue("@p_idPersonaAgente", idPersonaAgente);
                    command.Parameters.AddWithValue("@p_fechaSolicitud", fecha_solicitud);
                    command.Parameters.AddWithValue("@p_registro", registro);
                    command.Parameters.AddWithValue("@p_fechaRegistro", fechaRegistro);
                    command.Parameters.AddWithValue("@p_fechaVencimiento", fechaVencimiento);
                    command.Parameters.AddWithValue("@p_erenov", erenov);
                    command.Parameters.AddWithValue("@p_etrasp", etrasp);
                    command.Parameters.AddWithValue("@p_idCliente", idCliente);


                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;  
                }
            }
        }


        public bool EditarMarcaInternacional(int id, string expediente, string nombre, string signoDistintivo,string tipoSigno, string clase, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud, string paisRegistro, string tiene_poder, int? idCliente)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("EditarMarcaInternacional", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("p_id", id);
                    command.Parameters.AddWithValue("p_expediente", expediente);
                    command.Parameters.AddWithValue("p_nombre", nombre);
                    command.Parameters.AddWithValue("p_signo_distintivo", signoDistintivo);
                    command.Parameters.AddWithValue("p_tipoSigno", tipoSigno);
                    command.Parameters.AddWithValue("p_clase", clase);
                    command.Parameters.AddWithValue("p_logo", logo);
                    command.Parameters.AddWithValue("p_idPersonaTitular", idPersonaTitular);
                    command.Parameters.AddWithValue("p_idPersonaAgente", idPersonaAgente);
                    command.Parameters.AddWithValue("p_fecha_solicitud", fecha_solicitud);
                    command.Parameters.AddWithValue("p_pais_de_registro", paisRegistro);
                    command.Parameters.AddWithValue("p_tiene_poder", tiene_poder);
                    command.Parameters.AddWithValue("p_idCliente", idCliente);

                   
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public bool EditarMarcaInternacionalRegistrada(int id, string expediente, string nombre, string signoDistintivo,
            string tipoSigno,
        string clase, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fechaSolicitud,
        string paisRegistro, string tienePoder, int? idCliente, string registro, string folio,
        string libro, DateTime fechaRegistro, DateTime fechaVencimiento, string erenov, string etrasp)
        {
            int resultado;

            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new MySqlCommand("EditarMarcaInternacionalRegistrada", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("p_id", id);
                    command.Parameters.AddWithValue("p_expediente", expediente);
                    command.Parameters.AddWithValue("p_nombre", nombre);
                    command.Parameters.AddWithValue("p_signo_distintivo", signoDistintivo);
                    command.Parameters.AddWithValue("p_tipoSigno", tipoSigno);
                    command.Parameters.AddWithValue("p_clase", clase);
                    command.Parameters.AddWithValue("p_logo", logo);
                    command.Parameters.AddWithValue("p_idPersonaTitular", idPersonaTitular);
                    command.Parameters.AddWithValue("p_idPersonaAgente", idPersonaAgente);
                    command.Parameters.AddWithValue("p_fecha_solicitud", fechaSolicitud);
                    command.Parameters.AddWithValue("p_pais_de_registro", paisRegistro);
                    command.Parameters.AddWithValue("p_tiene_poder", tienePoder);
                    command.Parameters.AddWithValue("p_idCliente", idCliente);
                    command.Parameters.AddWithValue("p_registro", registro);
                    command.Parameters.AddWithValue("p_folio", folio);
                    command.Parameters.AddWithValue("p_libro", libro);
                    command.Parameters.AddWithValue("p_fecha_registro", fechaRegistro);
                    command.Parameters.AddWithValue("p_fecha_vencimiento", fechaVencimiento);
                    command.Parameters.AddWithValue("@p_erenov", erenov);
                    command.Parameters.AddWithValue("@p_etrasp", etrasp);

                    MySqlParameter resultadoParam = new MySqlParameter("p_resultado", MySqlDbType.Int32)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    };
                    command.Parameters.Add(resultadoParam);

                    command.ExecuteNonQuery();

                    resultado = Convert.ToInt32(resultadoParam.Value);
                }
            }

            return resultado == 0;
        }

        public DataTable GetMarcaInternacionalById(int id)
        {
            var dataTable = new DataTable();

            using (var conexion = GetConnection())
            {
                using (var comando = new MySqlCommand("GetMarcaInternacionalById", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("p_id", id);

                    conexion.Open();

                    using (var adapter = new MySqlDataAdapter(comando))
                    {
                       
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }

        public DataTable ObtenerMarcasRegistradasEnTramiteDeRenovacion()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("ObtenerMarcasRegistradasRenovaciones", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        DataTable resultado = new DataTable();
                        adapter.Fill(resultado);
                        return resultado;
                    }
                }
            }
        }

        public DataTable ObtenerMarcasInternacionalesRegistradasEnTramiteDeRenovacion()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("ObtenerMarcasInternacionalesRegistradasRenovaciones", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        DataTable resultado = new DataTable();
                        adapter.Fill(resultado);
                        return resultado;
                    }
                }
            }
        }

        public DataTable ObtenerMarcasRegistradasEnTramiteDeTraspaso()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("ObtenerMarcasRegistradasEnTramiteDeTraspaso", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        DataTable resultado = new DataTable();
                        adapter.Fill(resultado);
                        return resultado;
                    }
                }
            }
        }

        public DataTable ObtenerMarcasInternacionalesRegistradasEnTramiteDeTraspaso()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("ObtenerMarcasInternacionalesRegistradasEnTramiteDeTraspaso", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        DataTable resultado = new DataTable();
                        adapter.Fill(resultado);
                        return resultado;
                    }
                }
            }
        }

        public DataTable ObtenerTipoMarca(int id)
        {
            var dataTable = new DataTable();

            using (var conexion = GetConnection())
            {
                using (var comando = new MySqlCommand("ObtenerTipoMarca", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("p_id", id);

                    conexion.Open();

                    using (var adapter = new MySqlDataAdapter(comando))
                    {

                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }




    }
}
