using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class PlazosDao
    {
        private readonly string urlApi = "https://foragro.com.es/peticiones/plazos.php";

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

        public async Task<(int total, DataTable datos)> ObtenerPlazosAsync(string tipoRegistro, int pageSize, int currentPageIndex)
        {
            var data = new
            {
                action = "obtener_plazos",
                tipoRegistro = tipoRegistro,
                pageSize = pageSize,
                currentPageIndex = currentPageIndex
            };

            try
            {
                JsonDocument jsonDoc = await PostAsync(data);
                var tabla = new DataTable();
                int total = 0;

                if (jsonDoc.RootElement.TryGetProperty("registros", out var registrosProp) &&
                    registrosProp.ValueKind == JsonValueKind.Array)
                {
                    foreach (var item in registrosProp.EnumerateArray())
                    {
                        if (tabla.Columns.Count == 0)
                        {
                            foreach (var prop in item.EnumerateObject())
                            {
                                tabla.Columns.Add(prop.Name);
                            }
                        }

                        var row = tabla.NewRow();
                        foreach (var prop in item.EnumerateObject())
                        {
                            row[prop.Name] = prop.Value.ToString();
                        }
                        tabla.Rows.Add(row);
                    }
                }

                if (jsonDoc.RootElement.TryGetProperty("total", out var totalProp))
                {
                    total = totalProp.GetInt32();
                }

                return (total, tabla);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener plazos: " + ex.Message);
                return (0, null);
            }
        }

        public async Task<(int total, DataTable datos)> ObtenerPlazosFiltradoAsync(
    string tipoRegistro,
    int pageSize,
    int currentPageIndex,
    string filtroBusqueda)
        {
            var data = new
            {
                action = "obtener_plazos_filtrado",
                tipoRegistro = tipoRegistro,
                pageSize = pageSize,
                currentPageIndex = currentPageIndex,
                filtroBusqueda = filtroBusqueda
            };

            try
            {
                JsonDocument jsonDoc = await PostAsync(data);
                var tabla = new DataTable();
                int total = 0;

                if (jsonDoc.RootElement.TryGetProperty("registros", out var registrosProp) &&
                    registrosProp.ValueKind == JsonValueKind.Array)
                {
                    foreach (var item in registrosProp.EnumerateArray())
                    {
                        if (tabla.Columns.Count == 0)
                        {
                            foreach (var prop in item.EnumerateObject())
                            {
                                tabla.Columns.Add(prop.Name);
                            }
                        }

                        var row = tabla.NewRow();
                        foreach (var prop in item.EnumerateObject())
                        {
                            row[prop.Name] = prop.Value.ToString();
                        }
                        tabla.Rows.Add(row);
                    }
                }

                if (jsonDoc.RootElement.TryGetProperty("total", out var totalProp))
                {
                    total = totalProp.GetInt32();
                }

                return (total, tabla);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener plazos filtrados: " + ex.Message);
                return (0, null);
            }
        }

        public async Task<DataTable> FiltrarPlazosAsync(
            string tipoPlazos,
            //int pageSize,
            //int currentPageIndex,
            string filtroExpediente,
            string filtroSigno,
            string filtroEstado,
            string filtroClase,
            string? fechaIngresoInicio,
            string? fechaIngresoFin,
            string? fechaVencimientoInicio,
            string? fechaVencimientoFin,
            string filtroTitular,
            string filtroAgente)
        {
            var data = new
            {
                action = "filtrar_plazos",
                tipoPlazos = tipoPlazos,
                // pageSize = pageSize,
                // currentPageIndex = currentPageIndex,
                filtroExpediente = filtroExpediente ?? "",
                filtroSigno = filtroSigno ?? "",
                filtroEstado = filtroEstado ?? "",
                filtroClase = filtroClase ?? "",
                fechaIngresoInicio = fechaIngresoInicio?.ToString(),
                fechaIngresoFin = fechaIngresoFin?.ToString(),
                fechaVencimientoInicio = fechaVencimientoInicio?.ToString(),
                fechaVencimientoFin = fechaVencimientoFin?.ToString(),
                filtroTitular = filtroTitular ?? "",
                filtroAgente = filtroAgente ?? ""
            };

            try
            {
                JsonDocument jsonDoc = await PostAsync(data);
                var tabla = new DataTable();

                if (jsonDoc.RootElement.TryGetProperty("registros", out var registrosProp) &&
                    registrosProp.ValueKind == JsonValueKind.Array)
                {
                    foreach (var item in registrosProp.EnumerateArray())
                    {
                        if (tabla.Columns.Count == 0)
                        {
                            foreach (var prop in item.EnumerateObject())
                            {
                                tabla.Columns.Add(prop.Name);
                            }
                        }

                        var row = tabla.NewRow();
                        foreach (var prop in item.EnumerateObject())
                        {
                            row[prop.Name] = prop.Value.ToString();
                        }
                        tabla.Rows.Add(row);
                    }
                }

                return tabla;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al filtrar plazos: " + ex.Message);
                return null;
            }
        }



    }
}
