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
        


        public DataTable GetAllMarcasNacionales()
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection()) // Asegura que la conexión se cierre al finalizar
                {
                    using (MySqlCommand comando = new MySqlCommand(@"
                SELECT  
                    M.id,
                    M.nombre AS Nombre, 
                    M.clase AS Clase, 
                    M.registro AS Registro, 
                    M.folio AS Folio, 
                    M.libro AS Libro, 
                    P1.nombre AS Titular, 
                    P2.nombre AS Agente,
                    M.estado AS Estado
                FROM 
                    `Marcas` M
                JOIN 
                    Personas AS P1 ON M.IdTitular = P1.id 
                JOIN 
                    Personas AS P2 ON M.IdAgente = P2.id 
                WHERE 
                    M.tipo = 'nacional';", conexion))
                    {
                        conexion.Open(); // Abre la conexión a la base de datos
                        using (MySqlDataReader leer = comando.ExecuteReader()) // Asegura que el lector se cierre
                        {
                            tabla.Load(leer); // Carga los datos en el DataTable
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener las marcas nacionales: {ex.Message}");
                // Maneja la excepción según sea necesario
            }
            return tabla; // Devuelve el DataTable con los resultados
        }

        public bool AddMarcaNacional(string expediente, string nombre, string signoDistintivo, string clase, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud, string estado)
        {
            using (var connection = GetConnection()) // Asegúrate de que GetConnection esté implementado
            {
                connection.Open();
                using (var command = new MySqlCommand("INSERT INTO Marcas (expediente, nombre, signo_distintivo, clase, logo, idTitular, idAgente, fecha_solicitud, estado,tipo) VALUES (@expediente, @nombre, @signoDistintivo, @clase, @logo, @idPersonaTitular, @idPersonaAgente, @fecha_solicitud, @estado,'nacional')", connection))
                {
                    command.Parameters.AddWithValue("@expediente", expediente);
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@signoDistintivo", signoDistintivo);
                    command.Parameters.AddWithValue("@clase", clase);
                    command.Parameters.AddWithValue("@logo", logo); // Asignar el logo
                    command.Parameters.AddWithValue("@idPersonaTitular", idPersonaTitular);
                    command.Parameters.AddWithValue("@idPersonaAgente", idPersonaAgente);
                    command.Parameters.AddWithValue("@fecha_solicitud", fecha_solicitud);
                    command.Parameters.AddWithValue("@estado", estado);

                    // Ejecuta el comando y devuelve el número de filas afectadas
                    int rowsAffected = command.ExecuteNonQuery();

                    // Si se insertó al menos una fila, la operación fue exitosa
                    return rowsAffected > 0;
                }
            }
        }

        public bool AddMarcaInternacional(string expediente, string nombre, string signoDistintivo, string clase, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud, string estado, string pais_de_registro, string tiene_poder, DateTime presentacion, DateTime ultimo_pago, DateTime vencimiento, int idCliente)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                // Verificar si idCliente existe en la tabla Personas
                using (var checkCommand = new MySqlCommand("SELECT COUNT(*) FROM Personas WHERE id = @idCliente", connection))
                {
                    checkCommand.Parameters.AddWithValue("@idCliente", idCliente);
                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (count == 0)
                    {
                        throw new Exception("El cliente proporcionado no existe en la base de datos.");
                    }
                }

                using (var command = new MySqlCommand("INSERT INTO Marcas (expediente, nombre, signo_distintivo, clase, logo, idTitular, idAgente, fecha_solicitud, estado, pais_de_registro, tiene_poder, presentacion, ultimo_pago, vencimiento, idCliente, tipo) VALUES (@expediente, @nombre, @signoDistintivo, @clase, @logo, @idPersonaTitular, @idPersonaAgente, @fecha_solicitud, @estado, @pais_de_registro, @tiene_poder, @presentacion, @ultimo_pago, @vencimiento, @idCliente, 'internacional')", connection))
                {
                    command.Parameters.AddWithValue("@expediente", expediente);
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@signoDistintivo", signoDistintivo);
                    command.Parameters.AddWithValue("@clase", clase);
                    command.Parameters.AddWithValue("@logo", logo);
                    command.Parameters.AddWithValue("@idPersonaTitular", idPersonaTitular);
                    command.Parameters.AddWithValue("@idPersonaAgente", idPersonaAgente);
                    command.Parameters.AddWithValue("@fecha_solicitud", fecha_solicitud);
                    command.Parameters.AddWithValue("@estado", estado);
                    command.Parameters.AddWithValue("@pais_de_registro", pais_de_registro);
                    command.Parameters.AddWithValue("@tiene_poder", tiene_poder);
                    command.Parameters.AddWithValue("@presentacion", presentacion);
                    command.Parameters.AddWithValue("@ultimo_pago", ultimo_pago);
                    command.Parameters.AddWithValue("@vencimiento", vencimiento);
                    command.Parameters.AddWithValue("@idCliente", idCliente);

                    // Ejecuta el comando y devuelve el número de filas afectadas
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }


        public bool AddMarcaNacionalRegistrada(string expediente, string nombre, string signoDistintivo, string clase, string folio, string libro, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud, string estado, string registro, DateTime fechaRegistro, DateTime fechaVencimiento)
        {
            using (var connection = GetConnection()) // Asegúrate de que GetConnection esté implementado
            {
                connection.Open();
                using (var command = new MySqlCommand("INSERT INTO Marcas (expediente, nombre, signo_distintivo, clase, folio, libro, logo, idTitular, idAgente, fecha_solicitud, estado,tipo, registro, fecha_registro, fecha_vencimiento) VALUES (@expediente, @nombre, @signoDistintivo, @clase, @folio, @libro, @logo, @idPersonaTitular, @idPersonaAgente, @fecha_solicitud, @estado,'nacional',@registro, @fecha_registro, @fecha_vencimiento)", connection))
                {
                    command.Parameters.AddWithValue("@expediente", expediente);
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@signoDistintivo", signoDistintivo);
                    command.Parameters.AddWithValue("@clase", clase);
                    command.Parameters.AddWithValue("@folio", folio);
                    command.Parameters.AddWithValue("@libro", libro);
                    command.Parameters.AddWithValue("@logo", logo); // Asignar el logo
                    command.Parameters.AddWithValue("@idPersonaTitular", idPersonaTitular);
                    command.Parameters.AddWithValue("@idPersonaAgente", idPersonaAgente);
                    command.Parameters.AddWithValue("@fecha_solicitud", fecha_solicitud);
                    command.Parameters.AddWithValue("@estado", estado);
                    command.Parameters.AddWithValue("@registro", registro);
                    command.Parameters.AddWithValue("@fecha_registro", fechaRegistro);
                    command.Parameters.AddWithValue("@fecha_vencimiento", fechaVencimiento);

                    // Ejecuta el comando y devuelve el número de filas afectadas
                    int rowsAffected = command.ExecuteNonQuery();

                    // Si se insertó al menos una fila, la operación fue exitosa
                    return rowsAffected > 0;
                }
            }
        }

        public bool AddMarcaInternacionalRegistrada(string expediente, string nombre, string signoDistintivo, string clase, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud, string estado, string pais_de_registro, string tiene_poder, DateTime presentacion, DateTime ultimo_pago, DateTime vencimiento, int idCliente, string registro, string folio, string libro, DateTime fechaRegistro, DateTime fechaVencimiento)
        {
            using (var connection = GetConnection()) // Asegúrate de que GetConnection esté implementado
            {
                connection.Open();
                using (var command = new MySqlCommand("INSERT INTO Marcas (expediente, nombre, signo_distintivo, clase, logo, idTitular, idAgente, fecha_solicitud, estado, pais_de_registro, tiene_poder, presentacion, ultimo_pago, vencimiento, idCliente, tipo, registro, folio, libro, fecha_registro, fecha_vencimiento) VALUES (@expediente, @nombre, @signoDistintivo, @clase, @logo, @idPersonaTitular, @idPersonaAgente, @fecha_solicitud, @estado, @pais_de_registro, @tiene_poder, @presentacion, @ultimo_pago, @vencimiento, @idCliente, 'internacional', @registro, @folio, @libro, @fecha_de_registro, @fecha_vencimiento)", connection))
                {
                    command.Parameters.AddWithValue("@expediente", expediente);
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@signoDistintivo", signoDistintivo);
                    command.Parameters.AddWithValue("@clase", clase);
                    command.Parameters.AddWithValue("@logo", logo);
                    command.Parameters.AddWithValue("@idPersonaTitular", idPersonaTitular);
                    command.Parameters.AddWithValue("@idPersonaAgente", idPersonaAgente);
                    command.Parameters.AddWithValue("@fecha_solicitud", fecha_solicitud);
                    command.Parameters.AddWithValue("@estado", estado);
                    command.Parameters.AddWithValue("@pais_de_registro", pais_de_registro);
                    command.Parameters.AddWithValue("@tiene_poder", tiene_poder);
                    command.Parameters.AddWithValue("@presentacion", presentacion);
                    command.Parameters.AddWithValue("@ultimo_pago", ultimo_pago);
                    command.Parameters.AddWithValue("@vencimiento", vencimiento);
                    command.Parameters.AddWithValue("@idCliente", idCliente);
                    command.Parameters.AddWithValue("@registro", registro);
                    command.Parameters.AddWithValue("@folio", folio);
                    command.Parameters.AddWithValue("@libro", libro);
                    command.Parameters.AddWithValue("@fecha_de_registro", fechaRegistro);
                    command.Parameters.AddWithValue("@fecha_vencimiento", fechaVencimiento);

                    // Ejecuta el comando y devuelve el número de filas afectadas
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public List<(int id, string expediente, string nombre, string signoDistintivo, string clase, string folio, string libro, byte[] logo, string estado, string registro, DateTime? fechaSolicitud, DateTime? fechaRegistro, DateTime? fechaVencimiento, int idTitular, int idAgente)> GetMarcaNacionalById(int id)
        {
            var marca = new List<(int id, string expediente, string nombre, string signoDistintivo, string clase, string folio, string libro, byte[] logo, string estado, string registro, DateTime? fechaSolicitud, DateTime? fechaRegistro, DateTime? fechaVencimiento, int idTitular, int idAgente)>();

            using (MySqlConnection conexion = GetConnection()) // Asegura que la conexión se cierre al finalizar
            {
                using (MySqlCommand comando = new MySqlCommand(@"SELECT 
                M.id, 
                M.expediente, 
                M.nombre, 
                M.signo_distintivo AS signoDistintivo, 
                M.clase, 
                M.folio, 
                M.libro, 
                M.logo, 
                M.estado, 
                M.registro, 
                M.fecha_solicitud AS fechaSolicitud, 
                M.fecha_registro AS fechaRegistro, 
                M.fecha_vencimiento AS fechaVencimiento,
                M.idTitular,  
                M.idAgente
            FROM 
                Marcas M
            WHERE 
                M.tipo = 'nacional' 
                AND M.id = @id;", conexion))
                {
                    comando.Parameters.AddWithValue("@id", id);
                    conexion.Open();

                    using (MySqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            marca.Add((
                                reader.GetInt32("id"),
                                reader.IsDBNull(reader.GetOrdinal("expediente")) ? "" : reader.GetString("expediente"),
                                reader.IsDBNull(reader.GetOrdinal("nombre")) ? "" : reader.GetString("nombre"),
                                reader.IsDBNull(reader.GetOrdinal("signoDistintivo")) ? "" : reader.GetString("signoDistintivo"),
                                reader.IsDBNull(reader.GetOrdinal("clase")) ? "" : reader.GetString("clase"),
                                reader.IsDBNull(reader.GetOrdinal("folio")) ? "" : reader.GetString("folio"),
                                reader.IsDBNull(reader.GetOrdinal("libro")) ? "" : reader.GetString("libro"),
                                reader.IsDBNull(reader.GetOrdinal("logo")) ? null : (byte[])reader["logo"],
                                reader.IsDBNull(reader.GetOrdinal("estado")) ? "" : reader.GetString("estado"),
                                reader.IsDBNull(reader.GetOrdinal("registro")) ? "" : reader.GetString("registro"),
                                reader.IsDBNull(reader.GetOrdinal("fechaSolicitud")) ? (DateTime?)null : reader.GetDateTime("fechaSolicitud"),
                                reader.IsDBNull(reader.GetOrdinal("fechaRegistro")) ? (DateTime?)null : reader.GetDateTime("fechaRegistro"),
                                reader.IsDBNull(reader.GetOrdinal("fechaVencimiento")) ? (DateTime?)null : reader.GetDateTime("fechaVencimiento"),
                                reader.IsDBNull(reader.GetOrdinal("idTitular")) ? 0 : reader.GetInt32("idTitular"),
                                reader.IsDBNull(reader.GetOrdinal("idAgente")) ? 0 : reader.GetInt32("idAgente")
                            ));
                        }
                    }
                }
            }
            return marca; // Devuelve la lista con los detalles de la marca (contiene solo un elemento)
        }





    }
}
