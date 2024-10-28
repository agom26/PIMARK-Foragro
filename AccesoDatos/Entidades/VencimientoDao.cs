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
        //Obtener correos de titulares y agentes por id
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

        public DataTable GetAllVencimientos()
        {
            DataTable tabla = new DataTable();
            using (MySqlConnection conexion = GetConnection()) // Asegura que la conexión se cierre al finalizar
            {
                using (MySqlCommand comando = new MySqlCommand("SELECT id, nombre as Nombre, fecha_vencimiento as Vencimiento, clase as Clase, registro as Registro, folio as Folio, libro as Libro, titular as Titular, agente as Agente, marcaID FROM Vencimientos;", conexion)) // Inicializa correctamente el comando
                {
                    conexion.Open();
                    using (MySqlDataReader leer = comando.ExecuteReader()) // Asegura que el lector se cierre
                    {
                        tabla.Load(leer);
                    }
                }
            }
            return tabla;

        }

        public DataTable GetVencimientoByValue(string value)
        {
            DataTable tabla = new DataTable();
            using (MySqlConnection conexion = GetConnection()) // Asegura que la conexión se cierre al finalizar
            {
                using (MySqlCommand comando = new MySqlCommand("SELECT * FROM Vencimientos WHERE (nombre LIKE @value OR fecha_vencimiento LIKE @value OR clase LIKE @value OR tipo LIKE @value OR registro LIKE @value OR folio LIKE @value OR libro LIKE @value OR titular LIKE @value OR agente LIKE @value)", conexion)) // Inicializa correctamente el comando
                {
                    comando.Parameters.AddWithValue("@value", value);
                    conexion.Open();
                    using (MySqlDataReader leer = comando.ExecuteReader()) // Asegura que el lector se cierre
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
                        command.ExecuteNonQuery(); // Ejecuta el procedimiento
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al ejecutar el procedimiento: {ex.Message}");
                        // Maneja la excepción como desees
                    }
                }
            }
        }
    }
}
