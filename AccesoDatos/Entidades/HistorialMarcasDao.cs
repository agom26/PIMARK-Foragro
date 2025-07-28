


using System;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class HistorialMarcasDao
    {
        private readonly string urlApi = "https://foragro.com.es/peticiones/historial_marcas.php";

        private async Task<JsonDocument> PostAsync(object data)
        {
            using var client = new HttpClient();
            string json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(urlApi, content);

            if (!response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error HTTP {response.StatusCode}: {errorContent}");
            }

            string responseBody = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(responseBody))
                throw new Exception("Respuesta vacía del servidor.");

            return JsonDocument.Parse(responseBody);
        }

        public async Task<bool> EditHistorialById(int id, string etapa, DateTime fecha, string anotaciones, string usuario, string usuarioEditor, DateTime? fechaVencimiento)
        {
            var data = new
            {
                action = "editar",
                id,
                etapa,
                fecha = fecha.ToString("yyyy-MM-dd"),
                anotaciones,
                usuario,
                usuarioEditor,
                fechaVencimiento = fechaVencimiento.HasValue ? fechaVencimiento.Value.ToString("yyyy-MM-dd") : null
            };

            var jsonDoc = await PostAsync(data);
            return jsonDoc.RootElement.TryGetProperty("success", out var success) && success.GetBoolean();
        }

        public async Task<DataTable> GetHistorialById(int id)
        {
            var data = new { action = "obtener_por_id", id };
            var jsonDoc = await PostAsync(data);

            var tabla = new DataTable();

            if (jsonDoc.RootElement.ValueKind == JsonValueKind.Array)
            {
                foreach (var elem in jsonDoc.RootElement.EnumerateArray())
                {
                    if (tabla.Columns.Count == 0)
                    {
                        foreach (var prop in elem.EnumerateObject())
                        {
                            // Agrega columna si no existe aún
                            if (!tabla.Columns.Contains(prop.Name))
                                tabla.Columns.Add(prop.Name);
                        }
                    }

                    var row = tabla.NewRow();
                    foreach (var prop in elem.EnumerateObject())
                    {
                        if (tabla.Columns.Contains(prop.Name))
                            row[prop.Name] = prop.Value.ToString();
                    }
                    tabla.Rows.Add(row);
                }
            }

            return tabla;
        }




        public async Task<DataTable> GetAllEtapasByIdMarca(int idMarca)
        {
            var data = new { action = "obtener_etapas", idMarca };
            var jsonDoc = await PostAsync(data);

            var tabla = new DataTable();

            if (jsonDoc.RootElement.TryGetProperty("historial", out var historialArray) &&
                historialArray.ValueKind == JsonValueKind.Array)
            {
                foreach (var elem in historialArray.EnumerateArray())
                {
                    if (tabla.Columns.Count == 0)
                    {
                        foreach (var prop in elem.EnumerateObject())
                            tabla.Columns.Add(prop.Name);
                    }

                    var row = tabla.NewRow();
                    foreach (var prop in elem.EnumerateObject())
                        row[prop.Name] = prop.Value.ToString();

                    tabla.Rows.Add(row);
                }
            }

            return tabla;
        }


        public async Task<bool> GuardarEtapa(int idMarca, DateTime fecha, string etapa, string anotaciones, string usuario, string origen, DateTime? fechaVencimiento)
        {
            var data = new
            {
                action = "guardar",
                idMarca,
                fecha = fecha.ToString("yyyy-MM-dd"),
                etapa,
                anotaciones,
                usuario,
                origen,
                fechaVencimiento = fechaVencimiento.HasValue ? fechaVencimiento.Value.ToString("yyyy-MM-dd") : null
            };

            var jsonDoc = await PostAsync(data);
            return jsonDoc.RootElement.TryGetProperty("success", out var success) && success.GetBoolean();
        }

        public async Task<bool> EliminarRegistroHistorialYLog(int idHistorial, string deletedBy)
        {
            var data = new
            {
                action = "eliminar",
                idHistorial,
                deletedBy
            };

            var jsonDoc = await PostAsync(data);
            return jsonDoc.RootElement.TryGetProperty("success", out var success) && success.GetBoolean();
        }
    }
}


/*
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class HistorialMarcasDao:ConnectionSQL
    {
        
        public bool EditHistorialById(int id, string etapa, DateTime fecha, string anotaciones, string usuario, string usuarioEditor)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(@"
                    UPDATE Historial 
                    SET etapa = @etapa, 
                        fecha = @fecha, 
                        anotaciones = @anotaciones, 
                        usuario = @usuario, 
                        usuarioEdicion = @usuarioEdicion
                    WHERE id = @id;", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@etapa", etapa);
                    command.Parameters.AddWithValue("@fecha", fecha);
                    command.Parameters.AddWithValue("@anotaciones", anotaciones);
                    command.Parameters.AddWithValue("@usuario", usuario);
                    command.Parameters.AddWithValue("@usuarioEdicion", usuarioEditor);

                    // Ejecuta el comando y devuelve el número de filas afectadas
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Retorna true si se actualizó al menos una fila
                }
            }
        }
        public DataTable GetHistorialById(int id)
        {
            DataTable tabla = new DataTable();
            using (MySqlConnection conexion = GetConnection()) 
            {
                using (MySqlCommand comando = new MySqlCommand("SELECT * FROM Historial WHERE id=@id;", conexion)) // Inicializa correctamente el comando
                {
                    comando.Parameters.AddWithValue("@id", id);
                    conexion.Open();
                    using (MySqlDataReader leer = comando.ExecuteReader()) 
                    {
                        tabla.Load(leer);
                    }
                }
            }
            return tabla;
        }
        public DataTable GetAllEtapasByIdMarca(int idMarca)
        {
            DataTable tabla = new DataTable();
            using (MySqlConnection conexion = GetConnection()) 
            {
                using (MySqlCommand comando = new MySqlCommand("SELECT id, etapa as Etapa, DATE_FORMAT(fecha, '%Y-%m-%d') as Fecha, anotaciones as Anotaciones,origen as Origen, usuario as Usuario_creador, usuarioEdicion As Usuario_editor FROM Historial WHERE IdMarca=@idMarca;", conexion))
                {
                    comando.Parameters.AddWithValue("@idMarca", idMarca);
                    conexion.Open();
                    using (MySqlDataReader leer = comando.ExecuteReader())
                    {
                        tabla.Load(leer);
                    }
                }
            }
            return tabla;
        }

        public void GuardarEtapa(int idMarca, DateTime fecha, string etapa, string anotaciones, string usuario, string origen)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = @"INSERT INTO Historial (fecha, etapa, anotaciones, usuario, IdMarca, origen) 
                         VALUES (@fecha, @etapa, @anotaciones, @usuario, @IdMarca,@origen)";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@fecha", fecha);
                cmd.Parameters.AddWithValue("@etapa", etapa);
                cmd.Parameters.AddWithValue("@anotaciones", anotaciones);
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@IdMarca", idMarca);
                cmd.Parameters.AddWithValue("@origen", origen);
                cmd.ExecuteNonQuery();
            }
        }

        public bool EliminarRegistroHistorialYLog(int idHistorial, string deletedBy)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var command = new MySqlCommand("LogHistorialDeletion", connection, transaction))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("p_idHistorial", idHistorial);
                            command.Parameters.AddWithValue("p_usuario", deletedBy);

                            command.ExecuteNonQuery(); 
                            
                        }


                        
                        using (var command = new MySqlCommand("EliminarRegistroHistorial", connection, transaction))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("historialId", idHistorial);

                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected == 0)
                            {
                                transaction.Rollback(); 
                                return false;
                            }
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw; 
                    }
                }
            }
        }


        
    }
}*/
