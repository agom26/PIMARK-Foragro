using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class OposicionesDao
    {
        private readonly string urlApi = "https://foragro.com.es/peticiones/oposiciones.php";
        private static readonly JsonSerializerOptions jsonOpts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        /* =======================
           Infraestructura HTTP
           ======================= */
        private async Task<JsonDocument> PostJsonAsync(object data)
        {
            using var client = new HttpClient();
            string json = JsonSerializer.Serialize(data);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            var resp = await client.PostAsync(urlApi, content);
            resp.EnsureSuccessStatusCode();
            var body = await resp.Content.ReadAsStringAsync();
            return JsonDocument.Parse(body);
        }

        private async Task<JsonDocument> PostFormAsync(
            Dictionary<string, string> fields,
            IEnumerable<(string FieldName, byte[] Content, string FileName, string ContentType)>? files = null)
        {
            using var client = new HttpClient();
            using var form = new MultipartFormDataContent();

            foreach (var kv in fields)
                form.Add(new StringContent(kv.Value ?? string.Empty, Encoding.UTF8), kv.Key);

            if (files != null)
            {
                foreach (var f in files)
                {
                    var content = new ByteArrayContent(f.Content ?? Array.Empty<byte>());
                    content.Headers.ContentType = new MediaTypeHeaderValue(string.IsNullOrWhiteSpace(f.ContentType) ? "application/octet-stream" : f.ContentType);
                    form.Add(content, f.FieldName, string.IsNullOrWhiteSpace(f.FileName) ? "file.bin" : f.FileName);
                }
            }

            var resp = await client.PostAsync(urlApi, form);
            resp.EnsureSuccessStatusCode();
            var body = await resp.Content.ReadAsStringAsync();
            return JsonDocument.Parse(body);
        }

        /* =======================
           Utilidades de parsing
           ======================= */
        private static DataTable JsonArrayToDataTable(JsonElement array)
        {
            var table = new DataTable();
            bool cols = false;
            foreach (var item in array.EnumerateArray())
            {
                if (!cols)
                {
                    foreach (var p in item.EnumerateObject())
                        if (!table.Columns.Contains(p.Name)) table.Columns.Add(p.Name);
                    cols = true;
                }
                var row = table.NewRow();
                foreach (var p in item.EnumerateObject())
                    row[p.Name] = p.Value.ValueKind == JsonValueKind.Null ? DBNull.Value : p.Value.ToString();
                table.Rows.Add(row);
            }
            return table;
        }

        private static (int total, DataTable datos) ParseTotalYRegistros(JsonDocument doc)
        {
            int total = 0;
            if (doc.RootElement.TryGetProperty("total", out var t) && t.ValueKind == JsonValueKind.Number)
                total = t.GetInt32();

            DataTable dt = new DataTable();
            if (doc.RootElement.TryGetProperty("registros", out var regs) && regs.ValueKind == JsonValueKind.Array)
                dt = JsonArrayToDataTable(regs);

            return (total, dt);
        }

        /* =======================================
           1) COMBINADOS (total + registros)
           ======================================= */

        public async Task<(int total, DataTable datos)> ObtenerOposicionesNacionalesInterpuestasCombinado(string situacionActual, int currentPageIndex, int pageSize)
        {
            var data = new
            {
                action = "obtener_oposiciones_nac_interpuestas_combinado",
                situacionActual,
                pageSize,
                currentPageIndex
            };
            using var doc = await PostJsonAsync(data);
            return ParseTotalYRegistros(doc);
        }

        public async Task<(int total, DataTable datos)> ObtenerOposicionesNacionalesRecibidasCombinado(string situacionActual, int currentPageIndex, int pageSize)
        {
            var data = new
            {
                action = "obtener_oposiciones_nac_recibidas_combinado",
                situacionActual,
                pageSize,
                currentPageIndex
            };
            using var doc = await PostJsonAsync(data);
            return ParseTotalYRegistros(doc);
        }

        public async Task<(int total, DataTable datos)> ObtenerOposicionesInternacionalesInterpuestasCombinado(string situacionActual, int currentPageIndex, int pageSize)
        {
            var data = new
            {
                action = "obtener_oposiciones_int_interpuestas_combinado",
                situacionActual,
                pageSize,
                currentPageIndex
            };
            using var doc = await PostJsonAsync(data);
            return ParseTotalYRegistros(doc);
        }

        public async Task<(int total, DataTable datos)> ObtenerOposicionesInternacionalesRecibidasCombinado(string situacionActual, int currentPageIndex, int pageSize)
        {
            var data = new
            {
                action = "obtener_oposiciones_int_recibidas_combinado",
                situacionActual,
                pageSize,
                currentPageIndex
            };
            using var doc = await PostJsonAsync(data);
            return ParseTotalYRegistros(doc);
        }

        /* ================================
           2) OBTENER TIPO (param OUT)
           ================================ */
        public async Task<string> ObtenerTipoOposicion(int idOposicion)
        {
            var data = new { action = "obtener_tipo_oposicion", idOposicion };
            using var doc = await PostJsonAsync(data);
            if (doc.RootElement.TryGetProperty("tipo", out var tipo) && tipo.ValueKind == JsonValueKind.String)
                return tipo.GetString() ?? "";
            return "";
        }

        /* ==========================
           3) Filtro general (SP)
           ========================== */
        public async Task<DataTable> FiltrarOposiciones(
            string tipo_filtro,
            string expediente, string solicitante, string signoPretendido,
            string signoDistintivo, string clase, string opositor, string signoOpositor,
            string estado, string situacionActual, string tipo, string tipoOposicion)
        {
            var data = new
            {
                action = "filtrar_oposiciones",
                tipo_filtro,
                expediente = expediente ?? "",
                solicitante = solicitante ?? "",
                signoPretendido = signoPretendido ?? "",
                signoDistintivo = signoDistintivo ?? "",
                clase = clase ?? "",
                opositor = opositor ?? "",
                signoOpositor = signoOpositor ?? "",
                estado = estado ?? "",
                situacionActual = situacionActual ?? "",
                tipo = tipo ?? "",
                tipoOposicion = tipoOposicion ?? ""
            };
            using var doc = await PostJsonAsync(data);
            if (doc.RootElement.TryGetProperty("registros", out var regs) && regs.ValueKind == JsonValueKind.Array)
                return JsonArrayToDataTable(regs);
            return new DataTable();
        }

        /* ==========================================================
           4) Listas “plain” y contadores (nacionales/internacionales)
           (puedes ajustar los action strings a tu PHP)
           ========================================================== */

        // INTERNACIONALES INTERPUESTAS
        public async Task<DataTable> FiltrarOposicionesInternacionalesInterpuestas(string filtro, int currentPageIndex, int pageSize)
        {
            var data = new
            {
                action = "filtrar_oposiciones_int_interpuestas",
                pageSize,
                currentPageIndex,
                valor = filtro
            };
            using var doc = await PostJsonAsync(data);
            return doc.RootElement.ValueKind == JsonValueKind.Array ? JsonArrayToDataTable(doc.RootElement) :
                   (doc.RootElement.TryGetProperty("registros", out var regs) ? JsonArrayToDataTable(regs) : new DataTable());
        }

        public async Task<int> GetTotalOposicionesInternacionalesInterpuestas(string situacion)
        {
            var data = new { action = "get_total_op_int_interpuestas", situacion };
            using var doc = await PostJsonAsync(data);
            return doc.RootElement.TryGetProperty("total", out var t) ? t.GetInt32() : 0;
        }

        public async Task<int> GetFilteredOposicionesInternacionalesInterpuestasCount(string value)
        {
            var data = new { action = "contar_oposiciones_int_interpuestas_filtrado", value };
            using var doc = await PostJsonAsync(data);
            return doc.RootElement.TryGetProperty("total", out var t) ? t.GetInt32() : 0;
        }

        public async Task<DataTable> GetAllOposicionesInternacionalesInterpuestas(string situacionActual, int currentPageIndex, int pageSize)
        {
            var data = new
            {
                action = "listar_oposiciones_int_interpuestas",
                situacionActual,
                pageSize,
                currentPageIndex
            };
            using var doc = await PostJsonAsync(data);
            if (doc.RootElement.TryGetProperty("registros", out var regs) && regs.ValueKind == JsonValueKind.Array)
                return JsonArrayToDataTable(regs);
            return new DataTable();
        }

        // INTERNACIONALES RECIBIDAS
        public async Task<DataTable> FiltrarOposicionesInternacionalesRecibidas(string filtro, int currentPageIndex, int pageSize)
        {
            var data = new
            {
                action = "filtrar_oposiciones_int_recibidas",
                pageSize,
                currentPageIndex,
                valor = filtro
            };
            using var doc = await PostJsonAsync(data);
            return doc.RootElement.ValueKind == JsonValueKind.Array ? JsonArrayToDataTable(doc.RootElement) :
                   (doc.RootElement.TryGetProperty("registros", out var regs) ? JsonArrayToDataTable(regs) : new DataTable());
        }

        public async Task<int> GetTotalOposicionesInternacionalesRecibidas(string situacion)
        {
            var data = new { action = "get_total_op_int_recibidas", situacion };
            using var doc = await PostJsonAsync(data);
            return doc.RootElement.TryGetProperty("total", out var t) ? t.GetInt32() : 0;
        }

        public async Task<int> GetFilteredOposicionesInternacionalesRecibidasCount(string value)
        {
            var data = new { action = "contar_oposiciones_int_recibidas_filtrado", value };
            using var doc = await PostJsonAsync(data);
            return doc.RootElement.TryGetProperty("total", out var t) ? t.GetInt32() : 0;
        }

        public async Task<DataTable> GetAllOposicionesInternacionalesRecibidas(string situacionActual, int currentPageIndex, int pageSize)
        {
            var data = new
            {
                action = "listar_oposiciones_int_recibidas",
                situacionActual,
                pageSize,
                currentPageIndex
            };
            using var doc = await PostJsonAsync(data);
            if (doc.RootElement.TryGetProperty("registros", out var regs) && regs.ValueKind == JsonValueKind.Array)
                return JsonArrayToDataTable(regs);
            return new DataTable();
        }

        // NACIONALES RECIBIDAS
        public async Task<DataTable> FiltrarOposicionesNacionalesRecibidas(string filtro, int currentPageIndex, int pageSize)
        {
            var data = new
            {
                action = "filtrar_oposiciones_nac_recibidas",
                pageSize,
                currentPageIndex,
                valor = filtro
            };
            using var doc = await PostJsonAsync(data);
            return doc.RootElement.ValueKind == JsonValueKind.Array ? JsonArrayToDataTable(doc.RootElement) :
                   (doc.RootElement.TryGetProperty("registros", out var regs) ? JsonArrayToDataTable(regs) : new DataTable());
        }

        public async Task<int> GetTotalOposicionesNacionalesRecibidas(string situacion)
        {
            var data = new { action = "get_total_op_nac_recibidas", situacion };
            using var doc = await PostJsonAsync(data);
            return doc.RootElement.TryGetProperty("total", out var t) ? t.GetInt32() : 0;
        }

        public async Task<int> GetFilteredOposicionesNacionalesRecibidasCount(string value)
        {
            var data = new { action = "contar_oposiciones_nac_recibidas_filtrado", value };
            using var doc = await PostJsonAsync(data);
            return doc.RootElement.TryGetProperty("total", out var t) ? t.GetInt32() : 0;
        }

        public async Task<DataTable> GetAllOposicionesNacionales(string situacionActual, int currentPageIndex, int pageSize)
        {
            var data = new
            {
                action = "listar_oposiciones_nac_recibidas",
                situacionActual,
                pageSize,
                currentPageIndex
            };
            using var doc = await PostJsonAsync(data);
            if (doc.RootElement.TryGetProperty("registros", out var regs) && regs.ValueKind == JsonValueKind.Array)
                return JsonArrayToDataTable(regs);
            return new DataTable();
        }

        // NACIONALES INTERPUESTAS
        public async Task<DataTable> FiltrarOposicionesNacionalesInterpuestas(string filtro, int currentPageIndex, int pageSize)
        {
            var data = new
            {
                action = "filtrar_oposiciones_nac_interpuestas",
                pageSize,
                currentPageIndex,
                valor = filtro
            };
            using var doc = await PostJsonAsync(data);
            return doc.RootElement.ValueKind == JsonValueKind.Array ? JsonArrayToDataTable(doc.RootElement) :
                   (doc.RootElement.TryGetProperty("registros", out var regs) ? JsonArrayToDataTable(regs) : new DataTable());
        }

        public async Task<int> GetTotalOposicionesNacionalesInterpuestas(string situacion)
        {
            var data = new { action = "get_total_op_nac_interpuestas", situacion };
            using var doc = await PostJsonAsync(data);
            return doc.RootElement.TryGetProperty("total", out var t) ? t.GetInt32() : 0;
        }

        public async Task<int> GetFilteredOposicionesNacionalesInterpuestasCount(string value)
        {
            var data = new { action = "contar_oposiciones_nac_interpuestas_filtrado", value };
            using var doc = await PostJsonAsync(data);
            return doc.RootElement.TryGetProperty("total", out var t) ? t.GetInt32() : 0;
        }

        public async Task<DataTable> GetAllOposicionesNacionalesInterpuestas(string situacionActual, int currentPageIndex, int pageSize)
        {
            var data = new
            {
                action = "listar_oposiciones_nac_interpuestas",
                situacionActual,
                pageSize,
                currentPageIndex
            };
            using var doc = await PostJsonAsync(data);
            if (doc.RootElement.TryGetProperty("registros", out var regs) && regs.ValueKind == JsonValueKind.Array)
                return JsonArrayToDataTable(regs);
            return new DataTable();
        }

        /* ==========================================
           5) INSERTAR (multipart) + OBTENER POR ID
           ========================================== */

        public async Task<int?> AddOposicion(
            string expediente,
            string signoPretendido,
            string signoDistintivo,
            string clase,
            string solicitanteSignoPretendido,
            int? idSolicitante,
            int? idOpositor,
            string opositor,
            string signoOpositor,
            string situacionActual,
            int? idMarca,
            byte[]? logoOpositor,
            byte[]? logoSignoPretendido,
            string tipo,
            string tipoOposicion,
            string logoOpositorFileName = "logo_opositor.png",
            string logoSignoPretendidoFileName = "logo_signo_pretendido.png",
            string logoOpositorContentType = "image/png",
            string logoSignoPretendidoContentType = "image/png")
        {
            var fields = new Dictionary<string, string>
            {
                ["action"] = "insertar_oposicion",
                ["expediente"] = expediente ?? "",
                ["signoPretendido"] = signoPretendido ?? "",
                ["signoDistintivo"] = signoDistintivo ?? "",
                ["clase"] = clase ?? "",
                ["solicitanteSignoPretendido"] = solicitanteSignoPretendido ?? "",
                ["idSolicitante"] = idSolicitante?.ToString() ?? "",
                ["idOpositor"] = idOpositor?.ToString() ?? "",
                ["opositor"] = opositor ?? "",
                ["signoOpositor"] = signoOpositor ?? "",
                ["situacionActual"] = situacionActual ?? "",
                ["idMarca"] = idMarca?.ToString() ?? "",
                ["tipo"] = tipo ?? "",
                ["tipoOposicion"] = tipoOposicion ?? ""
            };

            var files = new List<(string, byte[], string, string)>();
            if (logoOpositor is { Length: > 0 })
                files.Add(("logoOpositor", logoOpositor, logoOpositorFileName, logoOpositorContentType));
            if (logoSignoPretendido is { Length: > 0 })
                files.Add(("logoSignoPretendido", logoSignoPretendido, logoSignoPretendidoFileName, logoSignoPretendidoContentType));

            using var doc = await PostFormAsync(fields, files);

            if (doc.RootElement.TryGetProperty("idOposicion", out var idP) && idP.ValueKind == JsonValueKind.Number)
                return idP.GetInt32();
            return null;
        }

        public async Task<DataTable> GetOposicionPorId(int idOposicion)
        {
            var data = new { action = "obtener_oposicion_por_id", idOposicion };
            using var doc = await PostJsonAsync(data);
            if (doc.RootElement.TryGetProperty("registros", out var regs) && regs.ValueKind == JsonValueKind.Array)
                return JsonArrayToDataTable(regs);
            return new DataTable();
        }

        //obtener logos de oposicion
        public async Task<byte[]> ObtenerLogoOposicionAsync(int id, string which = "opositor")
        {
            using var client = new HttpClient();

            try
            {
                // Construye la URL según el parámetro
                string url = $"https://foragro.com.es/peticiones/get_logo_oposiciones.php?id={id}&which={which}";

                // Realiza la solicitud HTTP GET
                byte[] logoBytes = await client.GetByteArrayAsync(url);
                return logoBytes;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error al obtener el logo de oposición: {ex.Message}");
                return null;
            }
        }

        /* ==========================
           6) EDITAR (multipart)
           ========================== */

        public async Task<bool> EditOposicion(
            int idOposicion,
            string expediente,
            string signoPretendido,
            string signoDistintivo,
            string clase,
            string solicitanteSignoPretendido,
            int? idOpositor,
            string signoOpositor,
            string situacionActual,
            int? idMarca,
            byte[]? logoOpositor,
            byte[]? logoSignoPretendido,
            string opositor,
            int? idSolicitante,
            string logoOpositorFileName = "logo_opositor.png",
            string logoSignoPretendidoFileName = "logo_signo_pretendido.png",
            string logoOpositorContentType = "image/png",
            string logoSignoPretendidoContentType = "image/png")
        {
            var fields = new Dictionary<string, string>
            {
                ["action"] = "editar_oposicion",
                ["idOposicion"] = idOposicion.ToString(),
                ["expediente"] = expediente ?? "",
                ["signoPretendido"] = signoPretendido ?? "",
                ["signoDistintivo"] = signoDistintivo ?? "",
                ["clase"] = clase ?? "",
                ["solicitanteSignoPretendido"] = solicitanteSignoPretendido ?? "",
                ["idOpositor"] = idOpositor?.ToString() ?? "",
                ["signoOpositor"] = signoOpositor ?? "",
                ["situacionActual"] = situacionActual ?? "",
                ["idMarca"] = idMarca?.ToString() ?? "",
                ["opositor"] = opositor ?? "",
                ["idSolicitante"] = idSolicitante?.ToString() ?? ""
            };

            var files = new List<(string, byte[], string, string)>();
            if (logoOpositor is { Length: > 0 })
                files.Add(("logoOpositor", logoOpositor, logoOpositorFileName, logoOpositorContentType));
            if (logoSignoPretendido is { Length: > 0 })
                files.Add(("logoSignoPretendido", logoSignoPretendido, logoSignoPretendidoFileName, logoSignoPretendidoContentType));

            using var doc = await PostFormAsync(fields, files);
            return doc.RootElement.TryGetProperty("ok", out var ok) && ok.ValueKind == JsonValueKind.True;
        }

        /* =======================================
           7) Cambios de estado / terminar
           ======================================= */

        public async Task MandarMarcaAbandonoOposicionTerminar(DateTime fecha, string anotaciones, string usuario, int idMarca, int idOposicion)
        {
            var data = new
            {
                action = "mandar_abandono_y_terminar",
                fecha = fecha.ToString("yyyy-MM-dd HH:mm:ss"),
                anotaciones = anotaciones ?? "",
                usuario = usuario ?? "",
                idMarca,
                idOposicion
            };
            using var _ = await PostJsonAsync(data);
        }

        public async Task MandarMarcaDesistimientoOposicionTerminar(DateTime fecha, string anotaciones, string usuario, int idMarca, int idOposicion)
        {
            var data = new
            {
                action = "mandar_desistimiento_y_terminar",
                fecha = fecha.ToString("yyyy-MM-dd HH:mm:ss"),
                anotaciones = anotaciones ?? "",
                usuario = usuario ?? "",
                idMarca,
                idOposicion
            };
            using var _ = await PostJsonAsync(data);
        }

        public async Task<bool> CambiarSituacionActualATerminada(int id)
        {
            var data = new { action = "terminar_oposicion", id };
            using var doc = await PostJsonAsync(data);
            return doc.RootElement.TryGetProperty("ok", out var ok) && ok.ValueKind == JsonValueKind.True;
        }
    }
}

/*
using MySql.Data.MySqlClient;
using System.Data;

namespace AccesoDatos.Entidades
{
    public class OposicionesDao: ConnectionSQL
    {
        public (int total, DataTable datos) ObtenerOposicionesNacionalesInterpuestasCombinado(string situacionActual, int currentPageIndex, int pageSize)
        {
            int total = 0;
            DataTable datos = new DataTable();

            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("ObtenerOposicionesNacionalesInterpuestasCombinado", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        int registrosOmitidos = (currentPageIndex - 1) * pageSize;
                        cmd.Parameters.AddWithValue("p_situacion_actual", situacionActual);
                        cmd.Parameters.AddWithValue("pageSize", pageSize);
                        cmd.Parameters.AddWithValue("registrosOmitidos", registrosOmitidos);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                total = reader.GetInt32("totalMarcas");
                            }

                            if (reader.NextResult())
                            {
                                datos.Load(reader);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener oposiciones interpuestas combinadas: " + ex.Message);
                }
            }

            return (total, datos);
        }


        public (int total, DataTable datos) ObtenerOposicionesNacionalesRecibidasCombinado(string situacionActual, int currentPageIndex, int pageSize)
        {
            int total = 0;
            DataTable datos = new DataTable();

            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("ObtenerOposicionesNacionalesRecibidasCombinado", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        int registrosOmitidos = (currentPageIndex - 1) * pageSize;

                        cmd.Parameters.AddWithValue("p_situacion_actual", situacionActual);
                        cmd.Parameters.AddWithValue("pageSize", pageSize);
                        cmd.Parameters.AddWithValue("registrosOmitidos", registrosOmitidos);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            // 1. Primer resultado: total
                            if (reader.Read())
                            {
                                total = reader.GetInt32("totalMarcas");
                            }

                            // 2. Segundo resultado: los registros paginados
                            if (reader.NextResult())
                            {
                                datos.Load(reader);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener oposiciones nacionales combinadas: " + ex.Message);
                }
            }

            return (total, datos);
        }
        //internacionales
        public (int total, DataTable datos) ObtenerOposicionesInternacionalesInterpuestasCombinado(string situacionActual, int currentPageIndex, int pageSize)
        {
            int total = 0;
            DataTable datos = new DataTable();

            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("ObtenerOposicionesInternacionalesInterpuestasCombinado", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        int registrosOmitidos = (currentPageIndex - 1) * pageSize;
                        cmd.Parameters.AddWithValue("p_situacion_actual", situacionActual);
                        cmd.Parameters.AddWithValue("pageSize", pageSize);
                        cmd.Parameters.AddWithValue("registrosOmitidos", registrosOmitidos);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                total = reader.GetInt32("totalMarcas");
                            }

                            if (reader.NextResult())
                            {
                                datos.Load(reader);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener oposiciones interpuestas combinadas: " + ex.Message);
                }
            }

            return (total, datos);
        }


        public (int total, DataTable datos) ObtenerOposicionesInternacionalesRecibidasCombinado(string situacionActual, int currentPageIndex, int pageSize)
        {
            int total = 0;
            DataTable datos = new DataTable();

            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("ObtenerOposicionesInternacionalesRecibidasCombinado", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        int registrosOmitidos = (currentPageIndex - 1) * pageSize;

                        cmd.Parameters.AddWithValue("p_situacion_actual", situacionActual);
                        cmd.Parameters.AddWithValue("pageSize", pageSize);
                        cmd.Parameters.AddWithValue("registrosOmitidos", registrosOmitidos);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            // 1. Primer resultado: total
                            if (reader.Read())
                            {
                                total = reader.GetInt32("totalMarcas");
                            }

                            // 2. Segundo resultado: los registros paginados
                            if (reader.NextResult())
                            {
                                datos.Load(reader);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener oposiciones internacionales combinadas: " + ex.Message);
                }
            }

            return (total, datos);
        }


        public string ObtenerTipoOposicion(int idOposicion)
        {
            string resultado = "";
            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand("ObtenerTipoOposicion", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@p_id_oposicion", idOposicion); // Cambia el ID según lo que necesites

                        var outputParam = new MySqlParameter("@p_tipo_oposicion", MySqlDbType.VarChar, 255);
                        outputParam.Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.Add(outputParam);
                        cmd.ExecuteNonQuery();
                        
                        string tipoOposicion = outputParam.Value?.ToString();
                        resultado = tipoOposicion;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return resultado;
        }
        public DataTable FiltrarOposiciones(string tipo_filtro,
            string expediente, string solicitante, string signoPretendido, 
            string signoDistintivo, string clase, string opositor, string signoOpositor, 
            string estado, string situacionActual, string tipo, string tipoOposicion
        )
        {
            DataTable dataTable = new DataTable();

            using (MySqlConnection conexion = GetConnection())
            {
                conexion.Open();
                using (MySqlCommand cmd = new MySqlCommand("FiltrarOposiciones", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("tipo_filtro", tipo_filtro);
                    cmd.Parameters.AddWithValue("p_expediente", expediente ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_solicitante_signoP", solicitante ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_signo_pretendido", signoPretendido ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_signo_distintivo", signoDistintivo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_clase", clase ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_opositor", opositor ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_signoO", signoOpositor ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_estadoA", estado ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_situacion_actual", situacionActual ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_tipo", tipo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_tipo_op", tipoOposicion ?? (object)DBNull.Value);
                    

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }

        //internacionales interpuestas
        public DataTable FiltrarOposicionesInternacionalesInterpuestas(string filtro, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("filtrarOposicionesInternacionalesInterpuestas", conexion))
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
                Console.WriteLine($"Error al obtener las oposiciones interpuestas: {ex.Message}");
            }
            return tabla;
        }
        public int GetTotalOposicionesInternacionalesInterpuestas(string situacion)
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetTotalOposicionesInternacionalesInterpuestas", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("situacion", situacion);
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
        public int GetFilteredOposicionesInternacionalesInterpuestasCount(string value)
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetFilteredOposicionesInternacionalesInterpuestasCount", conexion))
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
        public DataTable GetAllOposicionesInternacionalesInterpuestas(string situacionActual, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("ObtenerOposicionesInternacionalesInterpuestas ", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        int registrosOmitidos = (currentPageIndex - 1) * pageSize;
                        // Agregar parámetros de entrada
                        comando.Parameters.AddWithValue("pageSize", pageSize);
                        comando.Parameters.AddWithValue("registrosOmitidos", registrosOmitidos);
                        comando.Parameters.AddWithValue("p_situacion_actual", situacionActual);

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
                Console.WriteLine($"Error al obtener las oposiciones internacionales: {ex.Message}");
            }
            return tabla;
        }
        


        //internacionales recibidas
        public DataTable FiltrarOposicionesInternacionalesRecibidas(string filtro, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("filtrarOposicionesInternacionalesRecibidas", conexion))
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
                Console.WriteLine($"Error al obtener las oposiciones recibidas: {ex.Message}");
            }
            return tabla;
        }
        public int GetTotalOposicionesInternacionalesRecibidas(string situacion)
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetTotalOposicionesInternacionalesRecibidas", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("situacion", situacion);
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
        public int GetFilteredOposicionesInternacionalesRecibidasCount(string value)
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetFilteredOposicionesInternacionalesRecibidasCount", conexion))
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
        public DataTable GetAllOposicionesInternacionalesRecibidas(string situacionActual, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("ObtenerOposicionesInternacionalesRecibidas ", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        int registrosOmitidos = (currentPageIndex - 1) * pageSize;
                        // Agregar parámetros de entrada
                        comando.Parameters.AddWithValue("pageSize", pageSize);
                        comando.Parameters.AddWithValue("registrosOmitidos", registrosOmitidos);
                        comando.Parameters.AddWithValue("p_situacion_actual", situacionActual);

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
                Console.WriteLine($"Error al obtener las oposiciones internacionales: {ex.Message}");
            }
            return tabla;
        }
        public DataTable FiltrarOposicionesNacionalesRecibidas(string filtro, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("filtrarOposicionesNacionalesRecibidas", conexion))
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
                Console.WriteLine($"Error al obtener las oposiciones recibidas: {ex.Message}");
            }
            return tabla;
        }
        public int GetTotalOposicionesNacionalesRecibidas(string situacion)
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetTotalOposicionesNacionalesRecibidas", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("situacion", situacion);
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
        public int GetFilteredOposicionesNacionalesRecibidasCount(string value)
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetFilteredOposicionesNacionalesRecibidasCount", conexion))
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
        public DataTable GetAllOposicionesNacionales(string situacionActual, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("ObtenerOposicionesNacionalesRecibidas", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        int registrosOmitidos = (currentPageIndex - 1) * pageSize;
                        // Agregar parámetros de entrada
                        comando.Parameters.AddWithValue("pageSize", pageSize);
                        comando.Parameters.AddWithValue("registrosOmitidos", registrosOmitidos);
                        comando.Parameters.AddWithValue("p_situacion_actual", situacionActual);

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
                Console.WriteLine($"Error al obtener las oposiciones nacionales: {ex.Message}");
            }
            return tabla;
        }
        public DataTable FiltrarOposicionesNacionalesInterpuestas(string filtro, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("filtrarOposicionesNacionalesInterpuestas", conexion))
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
                Console.WriteLine($"Error al obtener las oposiciones interpuestas: {ex.Message}");
            }
            return tabla;
        }
        public int GetTotalOposicionesNacionalesInterpuestas(string situacion)
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetTotalOposicionesNacionalesInterpuestas", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("situacion", situacion);
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
        public int GetFilteredOposicionesNacionalesInterpuestasCount(string value)
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetFilteredOposicionesNacionalesInterpuestasCount", conexion))
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
        public DataTable GetAllOposicionesNacionalesInterpuestas(string situacionActual, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("ObtenerOposicionesNacionalesInterpuestas", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        int registrosOmitidos = (currentPageIndex - 1) * pageSize;
                        comando.Parameters.AddWithValue("pageSize", pageSize);
                        comando.Parameters.AddWithValue("registrosOmitidos", registrosOmitidos);
                        comando.Parameters.AddWithValue("p_situacion_actual", situacionActual);

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
                Console.WriteLine($"Error al obtener las oposiciones nacionales: {ex.Message}");
            }
            return tabla;
        }

        public int AddOposicion(
            string expediente,
            string signoPretendido,
            string signoDistintivo,
            string clase,
            string solicitanteSignoPretendido,
            int? idSolicitante,
            int? idOpositor,
            string opositor,
            string signoOpositor,
            string situacionActual,
            int? idMarca,
            byte[] logoOpositor, 
            byte[] logoSignoPretendido,
            string tipo,
            string tipoOposicion)
        {
            int lastInsertedId = 0;
            using (var connection = GetConnection())
            {
                try
                {
                    connection.Open();

                    using (MySqlCommand cmd = new MySqlCommand("InsertarOposicion", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@p_expediente", expediente);
                        cmd.Parameters.AddWithValue("@p_signo_pretendido", signoPretendido);
                        cmd.Parameters.AddWithValue("@p_signo_distintivo", signoDistintivo);
                        cmd.Parameters.AddWithValue("@p_clase", clase);
                        cmd.Parameters.AddWithValue("@p_solicitante_signo_pretendido", solicitanteSignoPretendido);
                        cmd.Parameters.AddWithValue("@p_idSolicitante", idSolicitante ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@p_idopositor", idOpositor ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@p_opositor", opositor);
                        cmd.Parameters.AddWithValue("@p_signo_opositor", signoOpositor);
                        cmd.Parameters.AddWithValue("@p_situacion_actual", situacionActual);
                        cmd.Parameters.AddWithValue("@p_idMarca", idMarca ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@p_logo_opositor", logoOpositor ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@p_logo_signo_pretendido", logoSignoPretendido ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@p_tipo", tipo);
                        cmd.Parameters.AddWithValue("@p_tipo_oposicion", tipoOposicion);
                        // Leer el ID del último registro insertado
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                lastInsertedId = Convert.ToInt32(reader["idOposicion"]);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }
            }

            return lastInsertedId;
        }

        public DataTable GetOposicionPorId(int idOposicion)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    
                    using (MySqlCommand comando = new MySqlCommand("ObtenerOposicionPorId", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                       
                        comando.Parameters.AddWithValue("@oposicion_id", idOposicion);

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
                Console.WriteLine($"Error al obtener la oposición por ID: {ex.Message}");
            }
            return tabla;
        }

        public bool EditOposicion(
        int idOposicion,
        string expediente,
        string signoPretendido,
        string signoDistintivo,
        string clase,
        string solicitanteSignoPretendido,
        int? idOpositor,
        string signoOpositor,
        string situacionActual,
        int? idMarca,
        byte[] logoOpositor,
        byte[] logoSignoPretendido,
        string opositor,
        int? idSolicitante)
        {
            try
            {
                using (MySqlConnection connection = GetConnection())
                {
                    connection.Open();

                    using (MySqlCommand cmd = new MySqlCommand("EditarOposicion", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@p_idOposicion", idOposicion);
                        cmd.Parameters.AddWithValue("@p_expediente", expediente);
                        cmd.Parameters.AddWithValue("@p_signo_pretendido", signoPretendido);
                        cmd.Parameters.AddWithValue("@p_signo_distintivo", signoDistintivo);
                        cmd.Parameters.AddWithValue("@p_clase", clase);
                        cmd.Parameters.AddWithValue("@p_solicitante_signo_pretendido", solicitanteSignoPretendido);
                        cmd.Parameters.AddWithValue("@p_idopositor", idOpositor);
                        cmd.Parameters.AddWithValue("@p_signo_opositor", signoOpositor);
                        cmd.Parameters.AddWithValue("@p_situacion_actual", situacionActual);
                        cmd.Parameters.AddWithValue("@p_idMarca", idMarca);
                        cmd.Parameters.AddWithValue("@p_logo_opositor", logoOpositor ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@p_logo_signo_pretendido", logoSignoPretendido ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@p_opositor", opositor);
                        cmd.Parameters.AddWithValue("@p_idSolicitante", idSolicitante);

                        var resultParam = new MySqlParameter("@p_result", MySqlDbType.Bit)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(resultParam);

                        cmd.ExecuteNonQuery();

                        return Convert.ToBoolean(resultParam.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al editar la oposición: {ex.Message}");
                throw;
            }
        }

        public void MandarMarcaAbandonoOposicionTerminar(DateTime p_fecha,
            string p_anotaciones, string p_usuario, int idMarca, int idOposicion)
        {
            try
            {
                using(MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("MandarOposicionMarcaAbandonoYTerminar", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("p_fecha", p_fecha);
                        comando.Parameters.AddWithValue("p_anotaciones", p_anotaciones);
                        comando.Parameters.AddWithValue("p_usuario", p_usuario);
                        comando.Parameters.AddWithValue("p_id_marca", idMarca);
                        comando.Parameters.AddWithValue("p_id_oposicion", idOposicion);
                        conexion.Open();
                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cambiar la marca y oposicion de estado: {ex.Message}");
            }

            
        }

        //desistir y terminar
        public void MandarMarcaDesistimientoOposicionTerminar(DateTime p_fecha,
            string p_anotaciones, string p_usuario, int idMarca, int idOposicion)
        {
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("MandarOposicionMarcaDesistimientoYTerminar", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("p_fecha", p_fecha);
                        comando.Parameters.AddWithValue("p_anotaciones", p_anotaciones);
                        comando.Parameters.AddWithValue("p_usuario", p_usuario);
                        comando.Parameters.AddWithValue("p_id_marca", idMarca);
                        comando.Parameters.AddWithValue("p_id_oposicion", idOposicion);
                        conexion.Open();
                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cambiar la marca y oposicion de estado: {ex.Message}");
            }


        }

        public bool CambiarSituacionActualATerminada(int id)
        {
            bool resultado = false;

            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("CambiarSituacionActualATerminada", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                        comando.Parameters.AddWithValue("p_id", id);

                        var resultadoParam = new MySqlParameter("p_resultado", MySqlDbType.Bit)
                        {
                            Direction = ParameterDirection.Output
                        };
                        comando.Parameters.Add(resultadoParam);

                      
                        conexion.Open();
                        comando.ExecuteNonQuery();

                       
                        resultado = Convert.ToBoolean(resultadoParam.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cambiar la situación actual: {ex.Message}");
            }

            return resultado;
        }
    }
}*/
