using System;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class RenovacionesMarcasDao
    {
        private readonly string urlApi = "https://foragro.com.es/peticiones/renovaciones_marcas.php";
        private static readonly JsonSerializerOptions JsonOpts = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        // ===== infra =====
        private async Task<JsonDocument> PostAsync(object payload)
        {
            using var client = new HttpClient();
            string json = JsonSerializer.Serialize(payload, JsonOpts);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");

            using HttpResponseMessage response = await client.PostAsync(urlApi, content);
            response.EnsureSuccessStatusCode();

            string body = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(body))
                return JsonDocument.Parse("{}");
            return JsonDocument.Parse(body);
        }

        private static DataTable JsonArrayToDataTable(JsonElement arr)
        {
            var table = new DataTable();
            if (arr.ValueKind != JsonValueKind.Array) return table;

            bool colsBuilt = false;
            foreach (var rowEl in arr.EnumerateArray())
            {
                if (rowEl.ValueKind != JsonValueKind.Object) continue;

                if (!colsBuilt)
                {
                    foreach (var p in rowEl.EnumerateObject())
                        if (!table.Columns.Contains(p.Name)) table.Columns.Add(p.Name);
                    colsBuilt = true;
                }

                var row = table.NewRow();
                foreach (var p in rowEl.EnumerateObject())
                    row[p.Name] = p.Value.ValueKind switch
                    {
                        JsonValueKind.String => p.Value.GetString(),
                        JsonValueKind.Number => p.Value.GetRawText(),
                        JsonValueKind.True => true,
                        JsonValueKind.False => false,
                        JsonValueKind.Null => DBNull.Value,
                        _ => p.Value.ToString()
                    };
                table.Rows.Add(row);
            }
            return table;
        }

        private static string IsoDate(DateTime d) => d.ToString("yyyy-MM-dd");

        // ===== API pública (mismos nombres que tu DAO original) =====

        // Insertar
        public async Task<bool> InsertRenovacionMarca(
            string numExpediente,
            int idMarca,
            DateTime fechaVencimientoAntigua,
            DateTime fechaVencimientoNueva)
        {
            var data = new
            {
                action = "insert_renovacion_marca",
                numExpediente,
                idMarca,
                fechaVencimientoAntigua = IsoDate(fechaVencimientoAntigua),
                fechaVencimientoNueva = IsoDate(fechaVencimientoNueva)
            };

            using var doc = await PostAsync(data);
            return doc.RootElement.TryGetProperty("success", out var ok) && ok.ValueKind == JsonValueKind.True;
        }

        // Listar por marca
        public async Task<DataTable> ObtenerRenovacionesDeMarcaPorId(int idMarca)
        {
            var data = new
            {
                action = "obtener_renovaciones_por_marca",
                idMarca
            };

            using var doc = await PostAsync(data);

            // 1) { "renovaciones": [...] }
            if (doc.RootElement.TryGetProperty("renovaciones", out var ren) && ren.ValueKind == JsonValueKind.Array)
                return JsonArrayToDataTable(ren);

            // 2) { "registros": [...] } (fallback, por si cambias el backend)
            if (doc.RootElement.TryGetProperty("registros", out var regs) && regs.ValueKind == JsonValueKind.Array)
                return JsonArrayToDataTable(regs);

            // 3) Array directo
            if (doc.RootElement.ValueKind == JsonValueKind.Array)
                return JsonArrayToDataTable(doc.RootElement);

            // 4) Nada útil
            return new DataTable();
        }


        // Obtener una renovación por id
        public async Task<DataTable> ObtenerRenovacionPorId(int id)
        {
            var data = new { action = "obtener_renovacion_por_id", id };
            using var doc = await PostAsync(data);

            if (doc.RootElement.TryGetProperty("renovacion", out var obj) &&
                obj.ValueKind == JsonValueKind.Object)
            {
                return ConvertSingleObjectToDataTable(obj);  // ← importante
            }

            // Si no vino el objeto, devolvemos tabla vacía para evitar null
            return new DataTable();
        }

        private DataTable ConvertSingleObjectToDataTable(JsonElement jsonObject)
        {
            var table = new DataTable();
            foreach (var prop in jsonObject.EnumerateObject())
                table.Columns.Add(prop.Name, typeof(string));

            var row = table.NewRow();
            foreach (var prop in jsonObject.EnumerateObject())
            {
                row[prop.Name] = prop.Value.ValueKind switch
                {
                    JsonValueKind.String => prop.Value.GetString(),
                    JsonValueKind.Number => prop.Value.GetRawText(),
                    JsonValueKind.True => true,
                    JsonValueKind.False => false,
                    JsonValueKind.Null => DBNull.Value,
                    _ => prop.Value.ToString()
                };
            }
            table.Rows.Add(row);
            return table;
        }

        // Actualizar
        public async Task<bool> ActualizarRenovacionMarca(
            int id,
            string numExpediente,
            int idMarca,
            DateTime fechaVencimientoAntigua,
            DateTime fechaVencimientoNueva)
        {
            var data = new
            {
                action = "actualizar_renovacion_marca",
                id,
                numExpediente,
                idMarca,
                fechaVencimientoAntigua = IsoDate(fechaVencimientoAntigua),
                fechaVencimientoNueva = IsoDate(fechaVencimientoNueva)
            };

            using var doc = await PostAsync(data);
            // Puede venir { ok: true } o { success: true }
            if (doc.RootElement.TryGetProperty("ok", out var ok))
                return ok.ValueKind == JsonValueKind.True;
            return doc.RootElement.TryGetProperty("success", out var s) && s.ValueKind == JsonValueKind.True;
        }
    }
}
