using System;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class VencimientoDao
    {
        private readonly string urlApi = "https://foragro.com.es/peticiones/vencimientos.php";
        private static readonly JsonSerializerOptions JsonOpts = new() { PropertyNameCaseInsensitive = true };

        /* ---------------- HTTP Helper ---------------- */
        private async Task<JsonDocument> PostAsync(object data)
        {
            using var client = new HttpClient();
            string json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = await client.PostAsync(urlApi, content);
            resp.EnsureSuccessStatusCode();

            string body = await resp.Content.ReadAsStringAsync();
            return JsonDocument.Parse(body);
        }

        private static void ThrowIfNotOk(JsonElement root)
        {
            if (root.TryGetProperty("ok", out var okProp) && okProp.ValueKind == JsonValueKind.False)
            {
                var msg = root.TryGetProperty("error", out var err) ? err.GetString() : "Operación no OK";
                throw new InvalidOperationException(msg ?? "Operación no OK");
            }
        }

        private static DataTable JsonArrayToDataTable(JsonElement arr)
        {
            var tabla = new DataTable();
            bool schemaBuilt = false;

            foreach (var elem in arr.EnumerateArray())
            {
                if (!schemaBuilt)
                {
                    foreach (var prop in elem.EnumerateObject())
                        if (!tabla.Columns.Contains(prop.Name))
                            tabla.Columns.Add(prop.Name);
                    schemaBuilt = true;
                }

                var row = tabla.NewRow();
                foreach (var prop in elem.EnumerateObject())
                    row[prop.Name] = prop.Value.ValueKind == JsonValueKind.Null ? DBNull.Value : prop.Value.ToString();

                tabla.Rows.Add(row);
            }
            return tabla;
        }

        /* ---------------- Métodos ---------------- */

        // 2) GetTotalVencimientos
        public async Task<int> GetTotalVencimientosAsync()
        {
            var payload = new { action = "get_total_vencimientos" };
            using var doc = await PostAsync(payload);
            var root = doc.RootElement;
            ThrowIfNotOk(root);
            return root.GetProperty("totalMarcas").GetInt32();
        }

        // 3) GetFilteredVencimientosCount (sí tiene count)
        public async Task<int> GetFilteredVencimientosCountAsync(string value)
        {
            var payload = new { action = "get_filtered_vencimientos_count", value };
            using var doc = await PostAsync(payload);
            var root = doc.RootElement;
            ThrowIfNotOk(root);
            return root.GetProperty("totalMarcas").GetInt32();
        }


        // 4) ObtenerVencimientosPaginados (solo DataTable)
        public async Task<DataTable> ObtenerVencimientosPaginadosAsync(int currentPageIndex, int pageSize)
        {
            var payload = new { action = "obtener_vencimientos_paginados", currentPageIndex, pageSize };
            using var doc = await PostAsync(payload);
            var root = doc.RootElement;

            if (root.TryGetProperty("ok", out var ok) && ok.ValueKind == JsonValueKind.False)
            {
                var msg = root.TryGetProperty("error", out var err) ? err.GetString() : "Operación no OK";
                throw new InvalidOperationException(msg ?? "Operación no OK");
            }

            JsonElement arr;
            if (root.TryGetProperty("rows", out var rows) && rows.ValueKind == JsonValueKind.Array)
                arr = rows;
            else if (root.TryGetProperty("data", out var data) && data.ValueKind == JsonValueKind.Array)
                arr = data;
            else if (root.ValueKind == JsonValueKind.Array)
                arr = root;
            else
                return new DataTable();

            return JsonArrayToDataTable(arr);
        }



        // 5) FiltrarVencimientos (sí tiene count)
        public async Task<DataTable> FiltrarVencimientosAsync(string filtro, int currentPageIndex, int pageSize)
        {
            var payload = new { action = "filtrar_vencimientos", filtro, currentPageIndex, pageSize };
            using var doc = await PostAsync(payload);
            var root = doc.RootElement;
            ThrowIfNotOk(root);

            var arr = root.GetProperty("rows");
            var tabla = JsonArrayToDataTable(arr);
            return (tabla);
        }

        // 7) EjecutarProcedimientoInsertarVencimientos
        public async Task<bool> EjecutarInsertarVencimientosAsync()
        {
            var payload = new { action = "ejecutar_insertar_vencimientos" };
            using var doc = await PostAsync(payload);
            var root = doc.RootElement;
            ThrowIfNotOk(root);
            return root.GetProperty("ok").GetBoolean();
        }

        // 8) ActualizarNotificado
        public async Task<bool> ActualizarNotificadoAsync(int id, string tipo)
        {
            var payload = new { action = "actualizar_notificado", id, tipo };
            using var doc = await PostAsync(payload);
            var root = doc.RootElement;
            ThrowIfNotOk(root);
            return root.GetProperty("ok").GetBoolean();
        }

        // 9) EditarTextoRtf
        public async Task<bool> EditarTextoRtfAsync(string tipo, string mensajeRtfEscapadoJson)
        {
            var payload = new { action = "editar_texto_rtf", tipo, mensaje = mensajeRtfEscapadoJson };
            using var doc = await PostAsync(payload);
            var root = doc.RootElement;
            ThrowIfNotOk(root);
            return root.GetProperty("ok").GetBoolean();
        }

        // 10) ObtenerTextoRtfPorTipo
        public async Task<string?> ObtenerTextoRtfPorTipoAsync(string tipo)
        {
            var payload = new { action = "obtener_texto_rtf_por_tipo", tipo };
            using var doc = await PostAsync(payload);
            var root = doc.RootElement;
            ThrowIfNotOk(root);
            return root.TryGetProperty("mensajeRtf", out var msg) ? msg.GetString() : null;
        }

        // 11) ObtenerTodosLosVencimientosReporte (sí tiene count)
        public async Task<DataTable > ObtenerVencimientosReporteAsync()
        {
            var payload = new { action = "obtener_vencimientos_reporte" };
            using var doc = await PostAsync(payload);
            var root = doc.RootElement;
            ThrowIfNotOk(root);

            var arr = root.TryGetProperty("data", out var d) ? d : root.GetProperty("rows");
            var tabla = JsonArrayToDataTable(arr);
            return (tabla);
        }

        // 12) ObtenerTodosLosVencimientosFiltradosReporte (sí tiene count)
        public async Task<DataTable> ObtenerVencimientosFiltradosReporteAsync(string valor)
        {
            var payload = new { action = "obtener_vencimientos_filtrados_reporte", valor };
            using var doc = await PostAsync(payload);
            var root = doc.RootElement;
            ThrowIfNotOk(root);

            var arr = root.GetProperty("rows");
            var tabla = JsonArrayToDataTable(arr);
            return (tabla);
        }

        // Opcional: obtener un listado simple SIN count
        public async Task<DataTable> GetVencimientoByValueAsync(string value)
        {
            var payload = new { action = "get_vencimiento_by_value", value };
            using var doc = await PostAsync(payload);
            var root = doc.RootElement;
            ThrowIfNotOk(root);

            var arr = root.GetProperty("rows");
            return JsonArrayToDataTable(arr);
        }
    }
}


/*using MySql.Data.MySqlClient;
using System.Data;

namespace AccesoDatos.Entidades
{
    public class VencimientoDao:ConnectionSQL
    {
        
       
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
}*/
