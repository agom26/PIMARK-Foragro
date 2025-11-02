using System.Data;
using System.Text;
using System.Text.Json;

namespace AccesoDatos.Entidades
{
    public class TraspasoPatenteDao
    {
        private readonly string urlApi = "https://foragro.com.es/peticiones/traspasos_patentes.php";

        // ======== Infraestructura ========
        private async Task<JsonDocument> PostAsync(object data)
        {
            using var client = new HttpClient();
            string json = JsonSerializer.Serialize(data);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            var resp = await client.PostAsync(urlApi, content);
            resp.EnsureSuccessStatusCode();
            string body = await resp.Content.ReadAsStringAsync();
            return JsonDocument.Parse(body);
        }

        private static DataTable JsonArrayToDataTable(JsonElement arr)
        {
            var table = new DataTable();
            if (arr.ValueKind != JsonValueKind.Array) return table;

            foreach (var item in arr.EnumerateArray())
            {
                if (item.ValueKind != JsonValueKind.Object) continue;

                if (table.Columns.Count == 0)
                    foreach (var p in item.EnumerateObject())
                        if (!table.Columns.Contains(p.Name)) table.Columns.Add(p.Name);

                var row = table.NewRow();
                foreach (var p in item.EnumerateObject())
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

        private static DataTable JsonObjectToDataTable(JsonElement obj)
        {
            var table = new DataTable();
            if (obj.ValueKind != JsonValueKind.Object) return table;

            foreach (var p in obj.EnumerateObject())
                if (!table.Columns.Contains(p.Name)) table.Columns.Add(p.Name);

            var row = table.NewRow();
            foreach (var p in obj.EnumerateObject())
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
            return table;
        }

        // ======== Métodos HTTP ========
        public async Task<bool> InsertarTraspasoPatente(string numExpediente, int idPatente, int idTitularAnterior, int idTitularNuevo)
        {
            var data = new
            {
                action = "insertar_traspaso_patente",
                numExpediente,
                idPatente,
                idTitularAnterior,
                idTitularNuevo
            };

            using var doc = await PostAsync(data);
            return doc.RootElement.TryGetProperty("ok", out var ok)
                && (ok.ValueKind == JsonValueKind.True ||
                    (ok.ValueKind == JsonValueKind.Number && ok.GetInt32() == 1));
        }

        public async Task<DataTable> ObtenerTraspasosDePatentePorId(int idPatente)
        {
            var data = new { action = "obtener_traspasos_por_patente", idPatente };
            using var doc = await PostAsync(data);

            if (doc.RootElement.TryGetProperty("traspasos", out var arr) && arr.ValueKind == JsonValueKind.Array)
                return JsonArrayToDataTable(arr);
            if (doc.RootElement.ValueKind == JsonValueKind.Array)
                return JsonArrayToDataTable(doc.RootElement);

            return new DataTable();
        }

        public async Task<DataTable> ObtenerTraspasoPatentePorId(int id)
        {
            var data = new { action = "obtener_traspaso_patente_por_id", id };
            using var doc = await PostAsync(data);

            if (doc.RootElement.TryGetProperty("traspaso", out var obj) && obj.ValueKind == JsonValueKind.Object)
                return JsonObjectToDataTable(obj);

            return new DataTable();
        }

        public async Task<bool> ActualizarTraspasoPatente(int id, string numExpediente, int idPatente, int idTitularAnterior, int idTitularNuevo)
        {
            var data = new
            {
                action = "actualizar_traspaso_patente",
                id,
                numExpediente,
                idPatente,
                idTitularAnterior,
                idTitularNuevo
            };

            using var doc = await PostAsync(data);
            return doc.RootElement.TryGetProperty("ok", out var ok)
                && (ok.ValueKind == JsonValueKind.True ||
                    (ok.ValueKind == JsonValueKind.Number && ok.GetInt32() == 1));
        }
    }
}
