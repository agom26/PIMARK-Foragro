using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace AccesoDatos.Entidades
{
    public class MarcaDao:ConnectionSQL
    {
        
        public DataTable GetAllMarcasNacionalesEnTramite()
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection()) 
                {
                    using (MySqlCommand comando = new MySqlCommand("ObtenerMarcasSinRegistro", conexion))
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
                        (estado='Oposición');", conexion))
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
                    using (MySqlCommand comando = new MySqlCommand("ObtenerMarcasInternacionalesEnOposicion", conexion))
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
        public DataTable GetAllMarcasNacionalesRegistradas()
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
                        P2.nombre AS Agente,
                        M.Erenov,
                        M.Etrasp
                    FROM 
                        `Marcas` M
                    JOIN 
                        Personas AS P1 ON M.IdTitular = P1.id 
                    JOIN 
                        Personas AS P2 ON M.IdAgente = P2.id 
                    WHERE 
                        M.tipo = 'nacional' AND 
                        (estado='Abandono');", conexion))
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




        public int AddMarcaNacional(string expediente, string nombre, string signoDistintivo,string tipoSigno, string clase, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud)
        {
            using (var connection = GetConnection()) 
            {
                connection.Open();

                using (var command = new MySqlCommand(@"INSERT INTO Marcas (expediente, nombre, signo_distintivo,tipoSigno, clase, logo, idTitular, idAgente, fecha_solicitud, tipo) 
                                                  VALUES (@expediente, @nombre, @signoDistintivo,@tipoSigno, @clase, @logo, @idPersonaTitular, @idPersonaAgente, @fecha_solicitud, 'nacional'); 
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
                    
                    int idMarca = Convert.ToInt32(command.ExecuteScalar());
                    return idMarca; 
                }
            }
        }


        public int AddMarcaInternacional(string expediente, string nombre, string signoDistintivo, string tipoSigno, string clase, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud, string pais_de_registro, string tiene_poder, int idCliente)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var checkCommand = new MySqlCommand("SELECT COUNT(*) FROM Personas WHERE id = @idCliente", connection))
                {
                    checkCommand.Parameters.AddWithValue("@idCliente", idCliente);
                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (count == 0)
                    {
                        throw new Exception("El cliente proporcionado no existe en la base de datos.");
                    }
                }

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
                                      DateTime fechaRegistro, DateTime fechaVencimiento)
        {
            int idMarca = 0;
            using (var connection = GetConnection()) 
            {
                connection.Open();
                using (var command = new MySqlCommand(@"
                    INSERT INTO Marcas (expediente, nombre, signo_distintivo, tipoSigno, clase, folio, libro, logo, idTitular, idAgente, 
                                        fecha_solicitud, tipo, registro, fecha_registro, fecha_vencimiento) 
                    VALUES (@expediente, @nombre, @signoDistintivo, @tipoSigno, @clase, @folio, @libro, @logo, @idPersonaTitular, 
                            @idPersonaAgente, @fecha_solicitud, 'nacional', @registro, @fecha_registro, @fecha_vencimiento);
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

                    idMarca = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            return idMarca; 
        }


        public int AddMarcaInternacionalRegistrada(string expediente, string nombre, string signoDistintivo, string tipoSigno, string clase, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud, string pais_de_registro, string tiene_poder, int idCliente, string registro, string folio, string libro, DateTime fechaRegistro, DateTime fechaVencimiento)
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



        public bool EditMarcaNacional(int id, string expediente, string nombre, string signoDistintivo, string tipoSigno, string clase, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud)
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
                fecha_solicitud = @fecha_solicitud
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

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; 
                }
            }
        }


        public bool EditMarcaNacionalRegistrada(int id, string expediente, string nombre, string signoDistintivo, string tipoSigno, string clase, string folio, string libro, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud, string registro, DateTime fechaRegistro, DateTime fechaVencimiento, string erenov, string etrasp)
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

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;  
                }
            }
        }


        public bool EditarMarcaInternacional(int id, string expediente, string nombre, string signoDistintivo, string clase, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud, string paisRegistro, string tiene_poder, int idCliente)
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
        string clase, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fechaSolicitud,
        string paisRegistro, string tienePoder, int idCliente, string registro, string folio,
        string libro, DateTime fechaRegistro, DateTime fechaVencimiento)
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




    }
}
