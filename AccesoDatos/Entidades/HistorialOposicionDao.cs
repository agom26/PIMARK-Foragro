using System;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class HistorialOposicionDao
    {
        private readonly string urlApi = "https://bpa.com.es/peticiones/historial_oposicion.php";

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

        public async Task InsertarHistorialOposicion(DateTime fecha, string etapa, string anotaciones, string usuario, string usuarioEdicion, string origen, int idOposicion)
        {
            var data = new
            {
                action = "insertar_historial",
                fecha = fecha.ToString("yyyy-MM-dd"),
                etapa,
                anotaciones,
                usuario,
                usuarioEdicion,
                origen,
                idOposicion
            };

            var jsonDoc = await PostAsync(data);
            // Opcional: puedes checar respuesta success aquí
        }

        public async Task<DataTable> ObtenerHistorialOposicion(int idOposicion)
        {
            var data = new
            {
                action = "obtener_historial_por_oposicion",
                idOposicion
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

        public async Task<DataTable> ObtenerHistorialOposicionPorId(int historialId)
        {
            var data = new
            {
                action = "obtener_historial_por_id",
                historialId
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

        public async Task<bool> EditarHistorialOposicion(int historialId, string nuevaEtapa, DateTime nuevaFecha, string nuevasAnotaciones, string nuevoUsuario, string nuevoUsuarioEdicion)
        {
            var data = new
            {
                action = "editar_historial",
                historialId,
                nuevaEtapa,
                nuevaFecha = nuevaFecha.ToString("yyyy-MM-dd"),
                nuevasAnotaciones,
                nuevoUsuario,
                nuevoUsuarioEdicion
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
    public class HistorialOposicionDao:ConnectionSQL
    {
        public void InsertarHistorialOposicion(DateTime fecha, string etapa, string anotaciones, string usuario, string usuarioEdicion, string origen, int idOposicion)
        {
            using (MySqlConnection connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("InsertarHistorialOposicion", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                      
                        command.Parameters.AddWithValue("p_fecha", fecha);
                        command.Parameters.AddWithValue("p_etapa", string.IsNullOrEmpty(etapa) ? (object)DBNull.Value : etapa);
                        command.Parameters.AddWithValue("p_anotaciones", string.IsNullOrEmpty(anotaciones) ? (object)DBNull.Value : anotaciones);
                        command.Parameters.AddWithValue("p_usuario", usuario);
                        command.Parameters.AddWithValue("p_usuarioEdicion", string.IsNullOrEmpty(usuarioEdicion) ? (object)DBNull.Value : usuarioEdicion);
                        command.Parameters.AddWithValue("p_origen", origen);
                        command.Parameters.AddWithValue("p_IdOposicion", idOposicion);

                       
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                   
                    throw new Exception("Error al insertar el historial de oposición: " + ex.Message, ex);
                }
            }
        }

        public DataTable ObtenerHistorialOposicion(int idOposicion)
        {
            using (MySqlConnection connection = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand("ObtenerHistorialOposicion", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id_oposicion", idOposicion);

                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                dataAdapter.Fill(dt);

                return dt;
            }
        }

        public DataTable ObtenerHistorialOposicionPorId(int historialId)
        {
            using (MySqlConnection connection = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand("ObtenerHistorialOposicionPorId", connection);
                cmd.CommandType = CommandType.StoredProcedure;

               
                cmd.Parameters.AddWithValue("historial_id", historialId);

                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();

               
                dataAdapter.Fill(dt);

                return dt;
            }
        }

        public bool EditarHistorialOposicion(int historialId, string nuevaEtapa, DateTime nuevaFecha, string nuevasAnotaciones, string nuevoUsuario, string nuevoUsuarioEdicion)
        {
            using (MySqlConnection connection = GetConnection())
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand("EditarHistorialOposicion", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                
                    cmd.Parameters.AddWithValue("historial_id", historialId);
                    cmd.Parameters.AddWithValue("nueva_etapa", nuevaEtapa);
                    cmd.Parameters.AddWithValue("nueva_fecha", nuevaFecha);
                    cmd.Parameters.AddWithValue("nuevas_anotaciones", nuevasAnotaciones);
                    cmd.Parameters.AddWithValue("nuevo_usuario", nuevoUsuario);
                    cmd.Parameters.AddWithValue("nuevo_usuario_edicion", nuevoUsuarioEdicion);

                   
                    MySqlParameter resultadoParam = new MySqlParameter("resultado", MySqlDbType.Byte)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(resultadoParam);

                    connection.Open();
                    cmd.ExecuteNonQuery();

                    
                    bool resultado = Convert.ToBoolean(resultadoParam.Value);
                    return resultado;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al editar el historial de oposición: " + ex.Message, ex);
                }
            }
        }

    }
}*/
