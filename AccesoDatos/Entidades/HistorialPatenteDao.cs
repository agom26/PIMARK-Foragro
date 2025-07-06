using System;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class HistorialPatenteDao
    {
        private readonly string urlApi = "https://bpa.com.es/peticiones/historial_patente.php";

        private async Task<JsonDocument> PostAsync(object data)
        {
            using var client = new HttpClient();
            string json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(urlApi, content);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            return JsonDocument.Parse(responseBody);
        }

        public async Task InsertarHistorialPatente(DateTime fecha, string etapa, string anotaciones, string usuario, string usuarioEdicion, int idPatente)
        {
            var data = new
            {
                action = "insertar_historial_patente",
                fecha = fecha.ToString("yyyy-MM-dd"),
                etapa,
                anotaciones,
                usuario,
                usuarioEdicion,
                idPatente
            };

            await PostAsync(data);
        }

        public async Task<DataTable> ObtenerHistorialPatentePorIdPatente(int idPatente)
        {
            var data = new
            {
                action = "obtener_historial_por_patente",
                idPatente
            };

            var jsonDoc = await PostAsync(data);

            var tabla = new DataTable();
            if (jsonDoc.RootElement.ValueKind == JsonValueKind.Array)
            {
                foreach (var elem in jsonDoc.RootElement.EnumerateArray())
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

        public async Task<DataTable> ObtenerHistorialPatentePorId(int idHistorial)
        {
            var data = new
            {
                action = "obtener_historial_por_id",
                idHistorial
            };

            var jsonDoc = await PostAsync(data);

            var tabla = new DataTable();
            if (jsonDoc.RootElement.ValueKind == JsonValueKind.Array)
            {
                foreach (var elem in jsonDoc.RootElement.EnumerateArray())
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

        public async Task<bool> EditarHistorialPatente(int idHistorial, DateTime fecha, string etapa, string anotaciones, string usuario, string usuarioEdicion)
        {
            var data = new
            {
                action = "editar_historial_patente",
                idHistorial,
                fecha = fecha.ToString("yyyy-MM-dd"),
                etapa,
                anotaciones,
                usuario,
                usuarioEdicion
            };

            var jsonDoc = await PostAsync(data);

            return jsonDoc.RootElement.TryGetProperty("success", out var success) && success.GetBoolean();
        }
    }
}


/*
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace AccesoDatos.Entidades
{
    public class HistorialPatenteDao : ConnectionSQL
    {
        // Método para insertar historial de patente
        public void InsertarHistorialPatente(
            DateTime fecha,
            string etapa,
            string anotaciones,
            string usuario,
            string usuarioEdicion,
            int idPatente)
        {
            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("InsertarHistorialPatente", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    
                    command.Parameters.AddWithValue("@p_fecha", fecha);
                    command.Parameters.AddWithValue("@p_etapa", etapa);
                    command.Parameters.AddWithValue("@p_anotaciones", anotaciones ?? (object)DBNull.Value); // Si es null, asignar DBNull
                    command.Parameters.AddWithValue("@p_usuario", usuario);
                    command.Parameters.AddWithValue("@p_usuarioEdicion", usuarioEdicion ?? (object)DBNull.Value); // Si es null, asignar DBNull
                    command.Parameters.AddWithValue("@p_IdPatente", idPatente);

                    
                    command.ExecuteNonQuery();
                }
            }
        }

        public DataTable GetAllEstadosByIdPatente(int idPatente)
        {
            DataTable tabla = new DataTable();
            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("ObtenerHistorialPatentePorId", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@p_IdPatente", idPatente);
                    conexion.Open();
                    using (MySqlDataReader leer = comando.ExecuteReader())
                    {
                        tabla.Load(leer);
                    }
                }
            }
            return tabla;
        }

        public DataTable GetHistorialById(int idHistorial)
        {
            DataTable tabla = new DataTable();
            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("ObtenerHistorialPatentePorIdHistorial", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@p_IdHistorial", idHistorial);

                    conexion.Open();
                    using (MySqlDataReader leer = comando.ExecuteReader())
                    {
                        tabla.Load(leer);
                    }
                }
            }
            return tabla;
        }

        public void EditarHistorialPatente(
            int idHistorial,
            DateTime fecha,
            string etapa,
            string anotaciones,
            string usuario,
            string usuarioEdicion)
        {
            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("EditarHistorialPatentePorId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@p_IdHistorial", idHistorial);
                    command.Parameters.AddWithValue("@p_Fecha", fecha);
                    command.Parameters.AddWithValue("@p_Etapa", etapa);
                    command.Parameters.AddWithValue("@p_Anotaciones", anotaciones ?? (object)DBNull.Value); // Si es null, asignar DBNull
                    command.Parameters.AddWithValue("@p_Usuario", usuario);
                    command.Parameters.AddWithValue("@p_UsuarioEdicion", usuarioEdicion ?? (object)DBNull.Value); // Si es null, asignar DBNull

                    command.ExecuteNonQuery();
                }
            }
        }


    }
}*/
