using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class BusquedaRetrospectivaDao
    {
        private readonly string urlApi = "https://foragro.com.es/peticiones/busqueda_retrospectiva.php";

        private async Task<JsonDocument> PostAsync(object data)
        {
            using var client = new HttpClient();
            string json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(urlApi, content);
            string responseBody = await response.Content.ReadAsStringAsync();

            // Puedes guardar el código por si quieres hacer algo diferente según el status
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error HTTP: {response.StatusCode} - {responseBody}");
                // Aún así devolvemos el JSON para que lo lea quien lo llama
            }

            return JsonDocument.Parse(responseBody);
        }


        public async Task<(int? idBusqueda, string mensajeError)> CrearBusquedaAsync(string signo, string clase, string signoDistintivo, string tipo, bool multiclase)
        {
            var data = new
            {
                action = "crear_busqueda",
                signo,
                clase,
                signoDistintivo,
                tipo,
                multiclase

            };

            try
            {
                JsonDocument jsonDoc = await PostAsync(data);

                if (jsonDoc.RootElement.TryGetProperty("idBusqueda", out var idElem))
                    return (idElem.GetInt32(), null);

                if (jsonDoc.RootElement.TryGetProperty("error", out var errorElem))
                    return (null, errorElem.GetString());

                return (null, "Error desconocido al crear la búsqueda.");
            }
            catch (Exception ex)
            {
                return (null, $"Error de red o servidor: {ex.Message}");
            }
        }


        public async Task<bool> AgregarPaisAsync(int idBusqueda, string pais, string nivel, string observaciones)
        {
            var data = new
            {
                action = "agregar_pais_retrospectiva",
                idBusqueda,
                pais,
                nivel,
                observaciones
            };

            try
            {
                JsonDocument jsonDoc = await PostAsync(data);
                return jsonDoc.RootElement.TryGetProperty("success", out var successProp) && successProp.GetBoolean();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al agregar país: " + ex.Message);
                return false;
            }
        }

        public async Task<(DataRow busqueda, DataTable paises)> ObtenerBusquedaConPaisesAsync(int idBusqueda)
        {
            var data = new
            {
                action = "obtener_busqueda_con_paises",
                idBusqueda
            };

            try
            {
                JsonDocument jsonDoc = await PostAsync(data);

                // Obtener datos de la búsqueda
                var tablaBusqueda = new DataTable();
                DataRow busquedaRow = null;

                if (jsonDoc.RootElement.TryGetProperty("busqueda", out var busq) &&
                    busq.ValueKind == JsonValueKind.Object)
                {
                    foreach (var prop in busq.EnumerateObject())
                        tablaBusqueda.Columns.Add(prop.Name);
                    var row = tablaBusqueda.NewRow();
                    foreach (var prop in busq.EnumerateObject())
                        row[prop.Name] = prop.Value.ToString();
                    tablaBusqueda.Rows.Add(row);
                    busquedaRow = row;
                }

                // Obtener países asociados
                var tablaPaises = new DataTable();
                if (jsonDoc.RootElement.TryGetProperty("paises", out var paises) &&
                    paises.ValueKind == JsonValueKind.Array)
                {
                    foreach (var item in paises.EnumerateArray())
                    {
                        if (tablaPaises.Columns.Count == 0)
                        {
                            foreach (var prop in item.EnumerateObject())
                                tablaPaises.Columns.Add(prop.Name);
                        }
                        var row = tablaPaises.NewRow();
                        foreach (var prop in item.EnumerateObject())
                            row[prop.Name] = prop.Value.ToString();
                        tablaPaises.Rows.Add(row);
                    }
                }

                return (busquedaRow, tablaPaises);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener búsqueda con países: " + ex.Message);
                return (null, null);
            }
        }

        public async Task<(int total, DataTable datos)> ObtenerBusquedasAsync(int pageSize, int currentPageIndex)
        {
            var data = new
            {
                action = "obtener_busquedas",
                pageSize,
                currentPageIndex
            };

            try
            {
                JsonDocument jsonDoc = await PostAsync(data);

                var tabla = new DataTable();
                if (jsonDoc.RootElement.TryGetProperty("busquedas", out var busquedas) &&
                    busquedas.ValueKind == JsonValueKind.Array)
                {
                    foreach (var item in busquedas.EnumerateArray())
                    {
                        if (tabla.Columns.Count == 0)
                        {
                            foreach (var prop in item.EnumerateObject())
                                tabla.Columns.Add(prop.Name);
                        }
                        var row = tabla.NewRow();
                        foreach (var prop in item.EnumerateObject())
                            row[prop.Name] = prop.Value.ToString();
                        tabla.Rows.Add(row);
                    }
                }

                int total = jsonDoc.RootElement.TryGetProperty("total", out var totalProp) ? totalProp.GetInt32() : 0;
                return (total, tabla);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener búsquedas: " + ex.Message);
                return (0, null);
            }
        }

        public async Task<(int total, DataTable datos)> ObtenerBusquedasFiltradoAsync(string filtro, int pageSize, int currentPageIndex)
        {
            var data = new
            {
                action = "obtener_busquedas_filtrado",
                filtro,
                pageSize,
                currentPageIndex
            };

            try
            {
                JsonDocument jsonDoc = await PostAsync(data);

                var tabla = new DataTable();
                if (jsonDoc.RootElement.TryGetProperty("busquedas", out var busquedas) &&
                    busquedas.ValueKind == JsonValueKind.Array)
                {
                    foreach (var item in busquedas.EnumerateArray())
                    {
                        if (tabla.Columns.Count == 0)
                        {
                            foreach (var prop in item.EnumerateObject())
                                tabla.Columns.Add(prop.Name);
                        }
                        var row = tabla.NewRow();
                        foreach (var prop in item.EnumerateObject())
                            row[prop.Name] = prop.Value.ToString();
                        tabla.Rows.Add(row);
                    }
                }

                int total = jsonDoc.RootElement.TryGetProperty("total", out var totalProp) ? totalProp.GetInt32() : 0;
                return (total, tabla);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener búsquedas filtradas: " + ex.Message);
                return (0, null);
            }
        }

        public async Task<string> EditarBusquedaAsync(int id, string signo, string clase, string signoDistintivo, string tipo, bool multiclase)
        {
            var data = new
            {
                action = "editar_busqueda",
                id,
                signo,
                clase,
                signoDistintivo,
                tipo,
                multiclase
            };

            try
            {
                JsonDocument jsonDoc = await PostAsync(data);
                if (jsonDoc.RootElement.TryGetProperty("success", out var successProp) && successProp.GetBoolean())
                    return null;
                if (jsonDoc.RootElement.TryGetProperty("error", out var errorProp))
                    return errorProp.GetString();
                return "Error desconocido al editar.";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al editar búsqueda: " + ex.Message);
                return "Error de conexión o formato de respuesta.";
            }
        }


        public async Task<bool> EliminarPaisAsync(int idPais)
        {
            var data = new
            {
                action = "eliminar_pais_retrospectiva",
                idPais
            };

            try
            {
                JsonDocument jsonDoc = await PostAsync(data);
                return jsonDoc.RootElement.TryGetProperty("success", out var successProp) && successProp.GetBoolean();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar país: " + ex.Message);
                return false;
            }
        }

        public async Task<string> EditarPaisAsync(int idPais, int idBusqueda, string pais, string nivel, string observaciones)
        {
            var data = new
            {
                action = "editar_pais_retrospectiva",
                idPais,
                pId = idBusqueda, // Esto debe coincidir con lo que espera tu PHP ('pId')
                pais,
                nivel,
                observaciones
            };

            try
            {
                JsonDocument jsonDoc = await PostAsync(data);
                if (jsonDoc.RootElement.TryGetProperty("success", out var successProp) && successProp.GetBoolean())
                    return null;
                if (jsonDoc.RootElement.TryGetProperty("error", out var errorProp))
                    return errorProp.GetString();
                return "Error desconocido al editar el país.";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al editar país: " + ex.Message);
                return "Error de conexión o formato de respuesta.";
            }
        }

        public async Task<bool> EliminarPaisesPorBusquedaAsync(int idBusqueda)
        {
            var data = new
            {
                action = "eliminar_paises_por_busqueda",
                idBusqueda
            };

            try
            {
                JsonDocument jsonDoc = await PostAsync(data);
                return jsonDoc.RootElement.TryGetProperty("success", out var successProp) && successProp.GetBoolean();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar países por búsqueda: " + ex.Message);
                return false;
            }
        }




    }
}
