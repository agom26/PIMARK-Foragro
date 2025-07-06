using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class LicenciaUso
    {
        private readonly string urlApi = "https://bpa.com.es/peticiones/licencias_uso.php";

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


        public async Task<bool> RenovarLicenciaUso(int idLicencia, string numExpediente, DateTime fechaAntigua, DateTime fechaNueva)
        {
            var data = new
            {
                action = "renovar_licencia_uso",
                idLicencia = idLicencia,
                numExpediente = numExpediente,
                fechaAntigua = fechaAntigua.ToString("yyyy-MM-dd"), // formato esperado como string
                fechaNueva = fechaNueva.ToString("yyyy-MM-dd")
            };

            try
            {
                JsonDocument response = await PostAsync(data);

                // Asumiendo que PHP responde con { "success": true }
                if (response.RootElement.TryGetProperty("success", out var success))
                {
                    return success.GetBoolean();
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al renovar licencia de uso: " + ex.Message);
                return false;
            }
        }


        public async Task<DataTable> ObtenerDatosRenovacionLicencia(int idLicencia)
        {
            var data = new
            {
                action = "obtener_datos_renovacion_licencia",
                idLicencia = idLicencia
            };

            JsonDocument jsonDoc = await PostAsync(data);

            var tabla = new DataTable();

            if (jsonDoc.RootElement.ValueKind == JsonValueKind.Array)
            {
                foreach (var elem in jsonDoc.RootElement.EnumerateArray())
                {
                    // Crear columnas si no existen
                    if (tabla.Columns.Count == 0)
                    {
                        foreach (var prop in elem.EnumerateObject())
                        {
                            tabla.Columns.Add(prop.Name);
                        }
                    }

                    // Crear fila y llenarla con los valores
                    var row = tabla.NewRow();
                    foreach (var prop in elem.EnumerateObject())
                    {
                        row[prop.Name] = prop.Value.ToString();
                    }
                    tabla.Rows.Add(row);
                }
            }

            return tabla.Rows.Count > 0 ? tabla : null;
        }

        public async Task<DataTable> ObtenerLicenciasPorMarca(int idMarca)
        {
            var data = new
            {
                action = "obtener_licencias_por_marca",
                idMarca = idMarca
            };

            JsonDocument jsonDoc = await PostAsync(data);

            var tabla = new DataTable();

            if (jsonDoc.RootElement.ValueKind == JsonValueKind.Array)
            {
                foreach (var elem in jsonDoc.RootElement.EnumerateArray())
                {
                    if (tabla.Columns.Count == 0)
                    {
                        foreach (var prop in elem.EnumerateObject())
                        {
                            tabla.Columns.Add(prop.Name);
                        }
                    }

                    var row = tabla.NewRow();
                    foreach (var prop in elem.EnumerateObject())
                    {
                        row[prop.Name] = prop.Value.ToString();
                    }
                    tabla.Rows.Add(row);
                }
            }

            return tabla.Rows.Count > 0 ? tabla : null;
        }

        public async Task<bool> EliminarLicenciaUsoConLog(int idLicencia, string usuario)
        {
            var data = new
            {
                action = "eliminar_licencia_uso_con_log",
                idLicencia = idLicencia,
                usuario = usuario
            };

            JsonDocument jsonDoc = await PostAsync(data);

            // Suponiendo que la respuesta JSON es algo como { "success": true } o { "success": false }
            if (jsonDoc.RootElement.TryGetProperty("success", out var successProp))
            {
                return successProp.GetBoolean();
            }

            return false; // En caso de no recibir el campo "success"
        }

        public async Task<DataTable> FiltrarLicenciasUso(
            string tipoLicencia,
            string expediente,
            string titulo,
            string signo,
            string signoDistintivo,
            string estado,
            string clase,
            string origen,
            string nombreRazonSocial,
            string titular)
        {
            var data = new
            {
                action = "filtrar_licencias_uso",
                tipoLicencia = string.IsNullOrWhiteSpace(tipoLicencia) ? null : tipoLicencia,
                expediente = string.IsNullOrWhiteSpace(expediente) ? null : expediente,
                titulo = string.IsNullOrWhiteSpace(titulo) ? null : titulo,
                signo = string.IsNullOrWhiteSpace(signo) ? null : signo,
                signoDistintivo = string.IsNullOrWhiteSpace(signoDistintivo) ? null : signoDistintivo,
                estado = string.IsNullOrWhiteSpace(estado) ? null : estado,
                clase = string.IsNullOrWhiteSpace(clase) ? null : clase,
                origen = string.IsNullOrWhiteSpace(origen) ? null : origen,
                nombreRazonSocial = string.IsNullOrWhiteSpace(nombreRazonSocial) ? null : nombreRazonSocial,
                titular = string.IsNullOrWhiteSpace(titular) ? null : titular
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


        public async Task<bool> InsertarLicenciaUso(
            int idMarca1,
            int idTitular1,
            string titulo,
            string tipo,
            DateTime? fechaInicio,
            DateTime? fechaFin,
            string territorio,
            string nombreRazonSocial,
            string direccion,
            string domicilio,
            string nacionalidad,
            string apoderadoRepresentanteLegal,
            string estado,
            string origen,
            int anios1,
            int meses1,
            int dias1)
        {
            var data = new
            {
                action = "insertar_licencia_uso",
                idMarca = idMarca1,
                idTitular = idTitular1,
                titulo = titulo ?? "",
                tipo = tipo ?? "",
                fechaInicio = fechaInicio?.ToString("yyyy-MM-dd"),
                fechaFin = fechaFin?.ToString("yyyy-MM-dd"),
                territorio = territorio ?? "",
                nombreRazonSocial = nombreRazonSocial ?? "",
                direccion = direccion ?? "",
                domicilio = domicilio ?? "",
                nacionalidad = nacionalidad ?? "",
                apoderadoRepresentanteLegal = apoderadoRepresentanteLegal ?? "",
                estado = estado ?? "",
                origen = origen ?? "",
                anios = anios1,
                meses = meses1,
                dias = dias1
            };

            var responseJson = await PostAsync(data);

            // Suponiendo que el JSON es { "success": true/false }
            if (responseJson.RootElement.TryGetProperty("success", out var successProp))
            {
                return successProp.GetBoolean();
            }

            return false;
        }

        public async Task<bool> EditarLicenciaUso(
            int id,
            int idMarca,
            int idTitular,
            string titulo,
            string tipo,
            DateTime? fechaInicio,
            DateTime? fechaFin,
            string territorio,
            string nombreRazonSocial,
            string direccion,
            string domicilio,
            string nacionalidad,
            string apoderadoRepresentanteLegal,
            string estado,
            string origen,
            int anios,
            int meses,
            int dias)
        {
            var data = new
            {
                action = "editar_licencia_uso",
                id = id,
                idMarca = idMarca,
                idTitular = idTitular,
                titulo = titulo ?? "",
                tipo = tipo ?? "",
                fechaInicio = fechaInicio?.ToString("yyyy-MM-dd"),
                fechaFin = fechaFin?.ToString("yyyy-MM-dd"),
                territorio = territorio ?? "",
                nombreRazonSocial = nombreRazonSocial ?? "",
                direccion = direccion ?? "",
                domicilio = domicilio ?? "",
                nacionalidad = nacionalidad ?? "",
                apoderadoRepresentanteLegal = apoderadoRepresentanteLegal ?? "",
                estado = estado ?? "",
                origen = origen ?? "",
                anios = anios,
                meses = meses,
                dias = dias
            };

            var responseJson = await PostAsync(data);

            if (responseJson.RootElement.TryGetProperty("success", out var successProp))
            {
                return successProp.GetBoolean();
            }

            return false;
        }

        public async Task<bool> ExisteOtraLicenciaUsoNoExclusiva(int idMarca, int idLicenciaExcluir, string origen)
        {
            var data = new
            {
                action = "existe_otra_licencia_uso_no_exclusiva",
                idMarca = idMarca,
                idLicenciaExcluir = idLicenciaExcluir,
                origen = origen ?? ""
            };

            var responseJson = await PostAsync(data);

            if (responseJson.RootElement.TryGetProperty("success", out var successProp) && successProp.GetBoolean())
            {
                if (responseJson.RootElement.TryGetProperty("existe", out var existeProp))
                {
                    return existeProp.GetBoolean();
                }
            }

            return false; // si hay error o no se encontró el campo
        }


        public async Task<DataTable> ObtenerLicenciasUsoNacionalesExclusivas(
            string estadoFiltro = null,
            int currentPageIndex = 1,
            int pageSize = 10)
        {
            var data = new
            {
                action = "obtener_licencias_uso_nacionales_exclusivas",
                estadoFiltro,
                currentPageIndex,
                pageSize
            };

            var dt = new DataTable();

            try
            {
                JsonDocument responseJson = await PostAsync(data);

                if (responseJson.RootElement.TryGetProperty("success", out var successProp) && successProp.GetBoolean())
                {
                    if (responseJson.RootElement.TryGetProperty("data", out var dataProp) && dataProp.ValueKind == JsonValueKind.Array)
                    {
                        bool columnsCreated = false;

                        foreach (var item in dataProp.EnumerateArray())
                        {
                            if (!columnsCreated)
                            {
                                // Crear columnas en DataTable según propiedades del primer objeto
                                foreach (var prop in item.EnumerateObject())
                                {
                                    dt.Columns.Add(prop.Name, typeof(object));
                                }
                                columnsCreated = true;
                            }

                            var row = dt.NewRow();

                            foreach (var prop in item.EnumerateObject())
                            {
                                object value = prop.Value.ValueKind switch
                                {
                                    JsonValueKind.String => prop.Value.GetString(),
                                    JsonValueKind.Number => prop.Value.TryGetInt32(out int i) ? (object)i : prop.Value.GetDecimal(),
                                    JsonValueKind.True => true,
                                    JsonValueKind.False => false,
                                    JsonValueKind.Null => DBNull.Value,
                                    _ => DBNull.Value
                                };
                                row[prop.Name] = value ?? DBNull.Value;
                            }

                            dt.Rows.Add(row);
                        }
                    }
                }

                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener licencias uso nacionales exclusivas (DataTable): " + ex.Message);
                return dt; // Retorna DataTable vacío si falla
            }
        }


        public async Task<int> GetTotalLicenciasUsoNacionalesExclusivas(string situacion)
        {
            var data = new
            {
                action = "get_total_licencias_uso_nacionales_exclusivas",
                situacion = situacion
            };

            try
            {
                JsonDocument response = await PostAsync(data);

                if (response.RootElement.TryGetProperty("success", out var success) && success.GetBoolean())
                {
                    if (response.RootElement.TryGetProperty("total", out var total))
                    {
                        return total.GetInt32();
                    }
                }

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el total de licencias uso exclusivas: " + ex.Message);
                return 0;
            }
        }

        public async Task<DataTable> FiltrarLicenciasUsoNacionalesExclusivas(string valor, int currentPageIndex, int pageSize)
        {
            var data = new
            {
                action = "filtrar_licencias_uso_nacionales_exclusivas",
                valor = valor ?? "",
                currentPageIndex = currentPageIndex,
                pageSize = pageSize
            };

            var dt = new DataTable();

            try
            {
                JsonDocument response = await PostAsync(data);

                if (response.RootElement.TryGetProperty("success", out var success) && success.GetBoolean())
                {
                    if (response.RootElement.TryGetProperty("data", out var dataElement) && dataElement.ValueKind == JsonValueKind.Array)
                    {
                        bool columnsCreated = false;

                        foreach (JsonElement item in dataElement.EnumerateArray())
                        {
                            if (!columnsCreated)
                            {
                                // Crear columnas en el DataTable según propiedades del primer objeto
                                foreach (JsonProperty prop in item.EnumerateObject())
                                {
                                    dt.Columns.Add(prop.Name, typeof(object));
                                }
                                columnsCreated = true;
                            }

                            var row = dt.NewRow();

                            foreach (JsonProperty prop in item.EnumerateObject())
                            {
                                object value = prop.Value.ValueKind switch
                                {
                                    JsonValueKind.String => prop.Value.GetString(),
                                    JsonValueKind.Number => prop.Value.TryGetInt32(out int i) ? (object)i : prop.Value.GetDecimal(),
                                    JsonValueKind.True => true,
                                    JsonValueKind.False => false,
                                    JsonValueKind.Null => DBNull.Value,
                                    _ => DBNull.Value
                                };

                                row[prop.Name] = value ?? DBNull.Value;
                            }

                            dt.Rows.Add(row);
                        }
                    }
                }

                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al filtrar licencias uso exclusivas (DataTable): " + ex.Message);
                return dt; // Retorna DataTable vacío en caso de error
            }
        }


        public async Task<int> ObtenerCantidadFiltradaLicenciasUsoNacionalesExclusivas(string valor)
        {
            var data = new
            {
                action = "get_filtered_licencias_uso_nacionales_exclusivas_count",
                valor = valor
            };

            try
            {
                JsonDocument response = await PostAsync(data); // Tu método para enviar la petición

                if (response.RootElement.TryGetProperty("success", out var success) && success.GetBoolean())
                {
                    if (response.RootElement.TryGetProperty("total", out var totalElement))
                    {
                        return totalElement.GetInt32();
                    }
                }

                return 0; // Retorna 0 si no hay éxito o no se encuentra el total
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la cantidad filtrada: " + ex.Message);
                return 0;
            }
        }

        public async Task<DataTable> ObtenerLicenciasUsoNacionalesNoExclusivas(
            string estadoFiltro,
            int currentPageIndex,
            int pageSize)
        {
            var data = new
            {
                action = "obtener_licencias_uso_nacionales_no_exclusivas",
                estadoFiltro = estadoFiltro ?? "",
                currentPageIndex = currentPageIndex,
                pageSize = pageSize
            };

            try
            {
                JsonDocument response = await PostAsync(data);

                if (response.RootElement.TryGetProperty("success", out var success) && success.GetBoolean())
                {
                    if (response.RootElement.TryGetProperty("data", out var dataElement) &&
                        dataElement.ValueKind == JsonValueKind.Array)
                    {
                        var dt = new DataTable();

                        bool columnsAdded = false;

                        foreach (JsonElement item in dataElement.EnumerateArray())
                        {
                            if (!columnsAdded)
                            {
                                foreach (JsonProperty prop in item.EnumerateObject())
                                {
                                    dt.Columns.Add(prop.Name, typeof(string));
                                }
                                columnsAdded = true;
                            }

                            var row = dt.NewRow();

                            foreach (JsonProperty prop in item.EnumerateObject())
                            {
                                // Guardar todo como string para evitar problemas de tipos
                                row[prop.Name] = prop.Value.ToString();
                            }

                            dt.Rows.Add(row);
                        }

                        return dt;
                    }
                }

                return new DataTable(); // Retornar vacío si no hubo éxito
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener licencias uso nacionales no exclusivas: " + ex.Message);
                return new DataTable();
            }
        }

        public async Task<int> ObtenerTotalLicenciasUsoNacionalesNoExclusivas(string situacion)
        {
            var data = new
            {
                action = "get_total_licencias_uso_nacionales_no_exclusivas",
                situacion = situacion ?? ""
            };

            try
            {
                JsonDocument response = await PostAsync(data);

                if (response.RootElement.TryGetProperty("success", out var success) && success.GetBoolean())
                {
                    if (response.RootElement.TryGetProperty("total", out var totalElement))
                    {
                        return totalElement.GetInt32();
                    }
                }

                return 0; // Retorna 0 si no hay éxito o no encuentra total
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener total licencias no exclusivas: " + ex.Message);
                return 0;
            }
        }

        public async Task<DataTable> FiltrarLicenciasUsoNacionalesNoExclusivas(string valor, int currentPageIndex, int pageSize)
        {
            var data = new
            {
                action = "filtrar_licencias_uso_nacionales_no_exclusivas",
                valor = valor ?? "",
                currentPageIndex = currentPageIndex,
                pageSize = pageSize
            };

            var dt = new DataTable();

            try
            {
                JsonDocument response = await PostAsync(data);

                if (response.RootElement.TryGetProperty("success", out var success) && success.GetBoolean())
                {
                    if (response.RootElement.TryGetProperty("data", out var dataElement) && dataElement.ValueKind == JsonValueKind.Array)
                    {
                        bool columnsCreated = false;

                        foreach (JsonElement item in dataElement.EnumerateArray())
                        {
                            if (!columnsCreated)
                            {
                                // Crear columnas en el DataTable basadas en las propiedades del primer elemento
                                foreach (JsonProperty prop in item.EnumerateObject())
                                {
                                    dt.Columns.Add(prop.Name, typeof(object));
                                }
                                columnsCreated = true;
                            }

                            var row = dt.NewRow();

                            foreach (JsonProperty prop in item.EnumerateObject())
                            {
                                object value = prop.Value.ValueKind switch
                                {
                                    JsonValueKind.String => prop.Value.GetString(),
                                    JsonValueKind.Number => prop.Value.TryGetInt32(out int i) ? (object)i : prop.Value.GetDecimal(),
                                    JsonValueKind.True => true,
                                    JsonValueKind.False => false,
                                    JsonValueKind.Null => DBNull.Value,
                                    _ => DBNull.Value
                                };

                                row[prop.Name] = value ?? DBNull.Value;
                            }

                            dt.Rows.Add(row);
                        }
                    }
                }

                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al filtrar licencias no exclusivas (DataTable): " + ex.Message);
                return dt; // retornamos DataTable vacío en caso de error
            }
        }

        public async Task<int> ObtenerCantidadFiltradaLicenciasUsoNacionalesNoExclusivas(string valor)
        {
            var data = new
            {
                action = "get_filtered_licencias_uso_nacionales_no_exclusivas_count",
                valor = valor ?? ""
            };

            try
            {
                JsonDocument response = await PostAsync(data);

                if (response.RootElement.TryGetProperty("success", out var success) && success.GetBoolean())
                {
                    if (response.RootElement.TryGetProperty("total", out var totalElement))
                    {
                        return totalElement.GetInt32();
                    }
                }

                return 0; // Retorna 0 si no hay éxito o no encuentra total
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la cantidad filtrada (no exclusivas): " + ex.Message);
                return 0;
            }
        }

        public async Task<bool> VerificarCompatibilidadLicenciaUso(int idMarca, string tipoLicencia, string origen)
        {
            var data = new
            {
                action = "verificar_compatibilidad_licencia_uso",
                idMarca = idMarca,
                tipoLicencia = tipoLicencia ?? "",
                origen = origen ?? ""
            };

            try
            {
                JsonDocument response = await PostAsync(data);

                if (response.RootElement.TryGetProperty("success", out var success) && success.GetBoolean())
                {
                    if (response.RootElement.TryGetProperty("conflicto", out var conflictoElement))
                    {
                        int conflicto = conflictoElement.GetInt32();
                        return conflicto != 0; // true si hay conflicto, false si no
                    }
                }

                return false; // No hay éxito o no encontró campo, asume no conflicto
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al verificar compatibilidad de licencia de uso: " + ex.Message);
                return false;
            }
        }


        public async Task<DataTable> ObtenerLicenciaUsoPorId(int idLicencia)
        {
            var data = new
            {
                action = "obtener_licencia_uso_por_id",
                idLicencia = idLicencia
            };

            var dt = new DataTable();

            try
            {
                JsonDocument response = await PostAsync(data);

                if (response.RootElement.TryGetProperty("success", out var success) && success.GetBoolean())
                {
                    if (response.RootElement.TryGetProperty("data", out var dataElement) && dataElement.ValueKind == JsonValueKind.Array)
                    {
                        bool columnsCreated = false;

                        foreach (JsonElement item in dataElement.EnumerateArray())
                        {
                            if (!columnsCreated)
                            {
                                // Crear columnas basadas en las propiedades del primer elemento
                                foreach (JsonProperty prop in item.EnumerateObject())
                                {
                                    dt.Columns.Add(prop.Name, typeof(object));
                                }
                                columnsCreated = true;
                            }

                            var row = dt.NewRow();

                            foreach (JsonProperty prop in item.EnumerateObject())
                            {
                                object value = prop.Value.ValueKind switch
                                {
                                    JsonValueKind.String => prop.Value.GetString(),
                                    JsonValueKind.Number => prop.Value.TryGetInt32(out int i) ? (object)i : prop.Value.GetDecimal(),
                                    JsonValueKind.True => true,
                                    JsonValueKind.False => false,
                                    JsonValueKind.Null => DBNull.Value,
                                    _ => DBNull.Value
                                };

                                row[prop.Name] = value ?? DBNull.Value;
                            }

                            dt.Rows.Add(row);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No se encontró ninguna licencia con id: " + idLicencia);
                }

                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener licencia por ID (DataTable): " + ex.Message);
                return dt; // retorna DataTable vacío si falla
            }
        }

        public async Task<(int total, DataTable data)> GetLicenciasUsoNacionalesExclusivasCombinado(
    string estadoFiltro, int currentPageIndex, int pageSize)
        {
            var requestData = new
            {
                action = "get_licencias_uso_nacionales_exclusivas_combinado",
                estadoFiltro = estadoFiltro ?? string.Empty,
                currentPageIndex,
                pageSize
            };

            var dataTable = new DataTable();
            int total = 0;

            try
            {
                JsonDocument response = await PostAsync(requestData).ConfigureAwait(false);

                if (!response.RootElement.TryGetProperty("success", out var successProp) || !successProp.GetBoolean())
                {
                    Console.WriteLine("La respuesta no fue exitosa.");
                    return (0, dataTable);
                }

                if (response.RootElement.TryGetProperty("total", out var totalProp) && totalProp.TryGetInt32(out int parsedTotal))
                {
                    total = parsedTotal;
                }
                else
                {
                    Console.WriteLine("No se encontró el campo total o no se pudo convertir.");
                }

                if (!response.RootElement.TryGetProperty("data", out var dataArray) || dataArray.ValueKind != JsonValueKind.Array)
                {
                    Console.WriteLine("El campo 'data' no es un array válido.");
                    return (total, dataTable);
                }

                bool columnsInitialized = false;

                foreach (JsonElement item in dataArray.EnumerateArray())
                {
                    if (!columnsInitialized)
                    {
                        foreach (JsonProperty prop in item.EnumerateObject())
                        {
                            if (!dataTable.Columns.Contains(prop.Name))
                                dataTable.Columns.Add(prop.Name, typeof(object));
                        }
                        columnsInitialized = true;
                    }

                    var row = dataTable.NewRow();

                    foreach (JsonProperty prop in item.EnumerateObject())
                    {
                        object value = prop.Value.ValueKind switch
                        {
                            JsonValueKind.String => prop.Value.GetString(),
                            JsonValueKind.Number => prop.Value.TryGetInt32(out int intVal) ? intVal : prop.Value.GetDecimal(),
                            JsonValueKind.True => true,
                            JsonValueKind.False => false,
                            JsonValueKind.Null => DBNull.Value,
                            _ => DBNull.Value
                        };

                        row[prop.Name] = value ?? DBNull.Value;
                    }

                    dataTable.Rows.Add(row);
                }

                Console.WriteLine($"DataTable cargado: filas={dataTable.Rows.Count}, columnas={dataTable.Columns.Count}");

                return (total, dataTable);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] GetLicenciasUsoNacionalesExclusivasCombinado: {ex.Message}");
                return (0, dataTable);
            }
        }



        public async Task<(int total, DataTable data)> GetLicenciasUsoNacionalesNoExclusivasCombinado(
    string estadoFiltro, int currentPageIndex, int pageSize)
        {
            var data = new
            {
                action = "get_licencias_uso_nacionales_no_exclusivas_combinado",
                estadoFiltro = estadoFiltro ?? "",
                currentPageIndex = currentPageIndex,
                pageSize = pageSize
            };

            int total = 0;
            var dt = new DataTable();

            try
            {
                JsonDocument response = await PostAsync(data);

                if (response.RootElement.TryGetProperty("success", out var success) && success.GetBoolean())
                {
                    if (response.RootElement.TryGetProperty("total", out var totalElement))
                    {
                        total = totalElement.GetInt32();
                    }

                    if (response.RootElement.TryGetProperty("data", out var dataElement) && dataElement.ValueKind == JsonValueKind.Array)
                    {
                        bool columnsCreated = false;

                        foreach (JsonElement item in dataElement.EnumerateArray())
                        {
                            if (!columnsCreated)
                            {
                                foreach (JsonProperty prop in item.EnumerateObject())
                                {
                                    dt.Columns.Add(prop.Name, typeof(object));
                                }
                                columnsCreated = true;
                            }

                            var row = dt.NewRow();

                            foreach (JsonProperty prop in item.EnumerateObject())
                            {
                                object value = prop.Value.ValueKind switch
                                {
                                    JsonValueKind.String => prop.Value.GetString(),
                                    JsonValueKind.Number => prop.Value.TryGetInt32(out int i) ? (object)i : prop.Value.GetDecimal(),
                                    JsonValueKind.True => true,
                                    JsonValueKind.False => false,
                                    JsonValueKind.Null => DBNull.Value,
                                    _ => DBNull.Value
                                };

                                row[prop.Name] = value ?? DBNull.Value;
                            }

                            dt.Rows.Add(row);
                        }
                    }
                }

                return (total, dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en GetLicenciasUsoNacionalesNoExclusivasCombinado_DataTable: " + ex.Message);
                return (0, dt);
            }
        }

        public async Task<string> CambiarEstadoLicencia(int idLicencia)
        {
            var data = new
            {
                action = "cambiar_estado_licencia",
                idLicencia = idLicencia
            };

            try
            {
                JsonDocument response = await PostAsync(data);

                if (response.RootElement.TryGetProperty("success", out var successProp) && successProp.GetBoolean())
                {
                    if (response.RootElement.TryGetProperty("mensaje", out var mensajeProp))
                    {
                        return mensajeProp.GetString() ?? "Operación realizada correctamente";
                    }
                    else
                    {
                        return "Operación realizada correctamente";
                    }
                }

                return "No se pudo cambiar el estado de la licencia";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cambiar estado de licencia: " + ex.Message);
                return ex.Message;
            }
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
    public class LicenciaUso : ConnectionSQL
    {
        public bool RenovarLicenciaUso(int idLicencia, string numExpediente, DateTime fechaAntigua, DateTime fechaNueva)
        {
            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("RenovarLicenciaUso", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("p_IdLicencia", idLicencia);
                        cmd.Parameters.AddWithValue("p_NumExpediente", numExpediente);
                        cmd.Parameters.AddWithValue("p_FechaVencimientoAntigua", fechaAntigua);
                        cmd.Parameters.AddWithValue("p_FechaVencimientoNueva", fechaNueva);

                        cmd.ExecuteNonQuery();
                        return true; 
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al renovar licencia de uso: " + ex.Message);
                    return false;
                }
            }
        }

        public DataTable ObtenerDatosRenovacionLicencia(int idLicencia)
        {
            DataTable dt = new DataTable();

            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("ObtenerDatosRenovacionLicencia", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_idLicencia", idLicencia);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener datos para renovación de licencia: " + ex.Message);
                }
            }

            return dt.Rows.Count > 0 ? dt : null;
        }


        public DataTable ObtenerLicenciasPorMarca(int idMarca)
        {
            DataTable dt = new DataTable();

            try
            {
                using (var connection = GetConnection())
                {
                    using (var command = new MySqlCommand("ObtenerLicenciasPorMarcaId", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("p_IdMarca", idMarca);

                        connection.Open();

                        using (var adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                // Manejo de errores, log, etc.
                Console.WriteLine($"Error SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
            }

            return dt;
        }


        public bool EliminarLicenciaUsoConLog(int idLicencia, string usuario)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    using (var command = new MySqlCommand("EliminarLicenciaUso", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@p_id", idLicencia);
                        command.Parameters.AddWithValue("@p_usuario", usuario);

                        connection.Open();
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"[Error SQL] {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error general] {ex.Message}");
                return false;
            }
        }


        public DataTable FiltrarLicenciasUso(
            string tipoLicencia,
            string expediente,
            string titulo,
            string signo,
            string signoDistintivo,
            string estado,
            string clase,
            string origen,
            string nombreRazonSocial,
            string titular)
        {
            DataTable dataTable = new DataTable();

            using (MySqlConnection conexion = GetConnection())
            {
                conexion.Open();
                using (MySqlCommand cmd = new MySqlCommand("FiltrarLicenciasUso", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("p_tipo_licencia", tipoLicencia ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_expediente", expediente ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_titulo", titulo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_signo", signo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_signo_distintivo", signoDistintivo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_estado", estado ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_clase", clase ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_origen", origen ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_nombre_razon_social", nombreRazonSocial ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_titular", titular ?? (object)DBNull.Value);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }



        public bool InsertarLicenciaUso
        (
            int idMarca,
            int idTitular,
            string titulo,
            string tipo,
            DateTime fechaInicio,
            DateTime fechaFin,
            string territorio,
            string nombreRazonSocial,
            string direccion,
            string domicilio,
            string nacionalidad,
            string apoderadoRepresentanteLegal,
            string estado,
            string origen,
            int anios, 
            int meses, 
            int dias
        )
        {
            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("InsertarLicenciaUso", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros
                        cmd.Parameters.AddWithValue("p_IdMarca", idMarca);
                        cmd.Parameters.AddWithValue("p_IdTitular", idTitular);
                        cmd.Parameters.AddWithValue("p_titulo", titulo);
                        cmd.Parameters.AddWithValue("p_tipo", tipo);
                        cmd.Parameters.AddWithValue("p_fecha_inicio", fechaInicio);
                        cmd.Parameters.AddWithValue("p_fecha_fin", fechaFin);
                        cmd.Parameters.AddWithValue("p_territorio", territorio);
                        cmd.Parameters.AddWithValue("p_nombre_razon_social", nombreRazonSocial);
                        cmd.Parameters.AddWithValue("p_direccion", direccion);
                        cmd.Parameters.AddWithValue("p_domicilio", domicilio);
                        cmd.Parameters.AddWithValue("p_nacionalidad", nacionalidad);
                        cmd.Parameters.AddWithValue("p_apoderado_representante_legal", apoderadoRepresentanteLegal);
                        cmd.Parameters.AddWithValue("p_estado", estado);
                        cmd.Parameters.AddWithValue("p_origen", origen);
                        cmd.Parameters.AddWithValue("p_anios", anios);
                        cmd.Parameters.AddWithValue("p_meses", meses);
                        cmd.Parameters.AddWithValue("p_dias", dias);

                        cmd.ExecuteNonQuery();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al insertar licencia de uso: " + ex.Message);
                    return false;
                }
            }
        }

        public bool EditarLicenciaUso(
            int id,
            int idMarca,
            int idTitular,
            string titulo,
            string tipo,
            DateTime fechaInicio,
            DateTime fechaFin,
            string territorio,
            string nombreRazonSocial,
            string direccion,
            string domicilio,
            string nacionalidad,
            string apoderadoRepresentanteLegal,
            string estado,
            string origen,
            int anios,
            int meses,
            int dias
        )
        {
            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("EditarLicenciaUso", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros
                        cmd.Parameters.AddWithValue("p_id", id);
                        cmd.Parameters.AddWithValue("p_IdMarca", idMarca);
                        cmd.Parameters.AddWithValue("p_IdTitular", idTitular);
                        cmd.Parameters.AddWithValue("p_titulo", titulo);
                        cmd.Parameters.AddWithValue("p_tipo", tipo);
                        cmd.Parameters.AddWithValue("p_fecha_inicio", fechaInicio);
                        cmd.Parameters.AddWithValue("p_fecha_fin", fechaFin);
                        cmd.Parameters.AddWithValue("p_territorio", territorio);
                        cmd.Parameters.AddWithValue("p_nombre_razon_social", nombreRazonSocial);
                        cmd.Parameters.AddWithValue("p_direccion", direccion);
                        cmd.Parameters.AddWithValue("p_domicilio", domicilio);
                        cmd.Parameters.AddWithValue("p_nacionalidad", nacionalidad);
                        cmd.Parameters.AddWithValue("p_apoderado_representante_legal", apoderadoRepresentanteLegal);
                        cmd.Parameters.AddWithValue("p_estado", estado);
                        cmd.Parameters.AddWithValue("p_origen", origen);
                        cmd.Parameters.AddWithValue("p_anios", anios);
                        cmd.Parameters.AddWithValue("p_meses", meses);
                        cmd.Parameters.AddWithValue("p_dias", dias);

                        cmd.ExecuteNonQuery();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al editar licencia de uso: " + ex.Message);
                    return false;
                }
            }
        }

        public bool ExisteOtraLicenciaUsoNoExclusiva(int idMarca, int idLicenciaExcluir, string origen)
{
            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("ExisteOtraLicenciaUsoNoExclusiva", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("p_IdMarca", idMarca);
                        cmd.Parameters.AddWithValue("p_IdLicenciaExcluir", idLicenciaExcluir);
                        cmd.Parameters.AddWithValue("p_origen", origen);

                        // Parámetro de salida
                        var existeLicenciaParam = new MySqlParameter("p_ExisteLicencia", MySqlDbType.Int32);
                        existeLicenciaParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(existeLicenciaParam);

                        cmd.ExecuteNonQuery();

                        int existeLicencia = Convert.ToInt32(existeLicenciaParam.Value);
                        return existeLicencia == 1;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error verificando licencia no exclusiva: " + ex.Message);
                    return false;
                }
    }
}


        public DataTable ObtenerLicenciasUsoNacionalesExclusivas(string estadoFiltro, int currentPageIndex, int pageSize)
        {
            DataTable dt = new DataTable();

            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("ObtenerLicenciasUsoNacionalesExclusivas", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        int registrosOmitidos = (currentPageIndex - 1) * pageSize;
                        cmd.Parameters.AddWithValue("estadoFiltro", estadoFiltro);
                        cmd.Parameters.AddWithValue("pageSize", pageSize);
                        cmd.Parameters.AddWithValue("registrosOmitidos", registrosOmitidos);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener licencias nacionales exclusivas: " + ex.Message);
                }
            }

            return dt;
        }

        public int GetTotalLicenciasUsoNacionalesExclusivas(string situacion)
        {
            int total = 0;

            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("GetTotalLicenciasUsoNacionalesExclusivas", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("situacion", situacion);

                        MySqlParameter outputParam = new MySqlParameter("totalMarcas", MySqlDbType.Int32);
                        outputParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(outputParam);

                        cmd.ExecuteNonQuery();

                        total = Convert.ToInt32(outputParam.Value);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener total de licencias nacionales exclusivas: " + ex.Message);
                }
            }

            return total;
        }

        public DataTable FiltrarLicenciasUsoNacionalesExclusivas(string valor, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();

            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("filtrarLicenciasUsoNacionalesExclusivas", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        int registrosOmitidos = (currentPageIndex - 1) * pageSize;
                        cmd.Parameters.AddWithValue("p_valor", valor);
                        cmd.Parameters.AddWithValue("pageSize", pageSize);
                        cmd.Parameters.AddWithValue("registrosOmitidos", registrosOmitidos);

                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            da.Fill(tabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al filtrar licencias nacionales exclusivas: " + ex.Message);
                }
            }

            return tabla;
        }

        public int GetFilteredLicenciasUsoNacionalesExclusivasCount(string valor)
        {
            int total = 0;

            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("GetFilteredLicenciasUsoNacionalesExclusivasCount", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("p_valor", valor);

                        // Parámetro de salida
                        MySqlParameter outputParam = new MySqlParameter("totalMarcas", MySqlDbType.Int32);
                        outputParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(outputParam);

                        cmd.ExecuteNonQuery();

                        total = Convert.ToInt32(outputParam.Value);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener el total de licencias filtradas: " + ex.Message);
                }
            }

            return total;
        }

        // no exclusivas
        public DataTable ObtenerLicenciasUsoNacionalesNoExclusivas(string estadoFiltro, int currentPageIndex, int pageSize)
        {
            DataTable dt = new DataTable();

            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("ObtenerLicenciasUsoNacionalesNoExclusivas", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        int registrosOmitidos = (currentPageIndex - 1) * pageSize;
                        cmd.Parameters.AddWithValue("estadoFiltro", estadoFiltro);
                        cmd.Parameters.AddWithValue("pageSize", pageSize);
                        cmd.Parameters.AddWithValue("registrosOmitidos", registrosOmitidos);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener licencias nacionales exclusivas: " + ex.Message);
                }
            }

            return dt;
        }

        public int GetTotalLicenciasUsoNacionalesNoExclusivas(string situacion)
        {
            int total = 0;

            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("GetTotalLicenciasUsoNacionalesNoExclusivas", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("situacion", situacion);

                        MySqlParameter outputParam = new MySqlParameter("totalMarcas", MySqlDbType.Int32);
                        outputParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(outputParam);

                        cmd.ExecuteNonQuery();

                        total = Convert.ToInt32(outputParam.Value);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener total de licencias nacionales exclusivas: " + ex.Message);
                }
            }

            return total;
        }

        public DataTable FiltrarLicenciasUsoNacionalesNoExclusivas(string valor, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();

            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("filtrarLicenciasUsoNacionalesNoExclusivas", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        int registrosOmitidos = (currentPageIndex - 1) * pageSize;
                        cmd.Parameters.AddWithValue("p_valor", valor);
                        cmd.Parameters.AddWithValue("pageSize", pageSize);
                        cmd.Parameters.AddWithValue("registrosOmitidos", registrosOmitidos);

                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            da.Fill(tabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al filtrar licencias nacionales exclusivas: " + ex.Message);
                }
            }

            return tabla;
        }

        public int GetFilteredLicenciasUsoNacionalesNoExclusivasCount(string valor)
        {
            int total = 0;

            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("GetFilteredLicenciasUsoNacionalesNoExclusivasCount", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("p_valor", valor);

                        // Parámetro de salida
                        MySqlParameter outputParam = new MySqlParameter("totalMarcas", MySqlDbType.Int32);
                        outputParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(outputParam);

                        cmd.ExecuteNonQuery();

                        total = Convert.ToInt32(outputParam.Value);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener el total de licencias filtradas: " + ex.Message);
                }
            }

            return total;
        }

        public bool VerificarCompatibilidadLicenciaUso(int idMarca, string tipoLicencia, string origen)
        {
            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("VerificarCompatibilidadLicenciaUso", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("p_IdMarca", idMarca);
                        cmd.Parameters.AddWithValue("p_TipoLicencia", tipoLicencia);
                        cmd.Parameters.AddWithValue("p_origen", origen);

                        var conflictoParam = new MySqlParameter("p_ExisteConflicto", MySqlDbType.Int32);
                        conflictoParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(conflictoParam);

                        cmd.ExecuteNonQuery();

                        int conflicto = Convert.ToInt32(conflictoParam.Value);
                        return conflicto == 1; 
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error verificando compatibilidad de licencia: " + ex.Message);
                    return true;
                }
            }
        }



        public DataTable ObtenerLicenciaUsoPorId(int idLicencia)
        {
            DataTable dt = new DataTable();

            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("ObtenerLicenciaPorId", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_id", idLicencia);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener la licencia de uso por ID: " + ex.Message);
                }
            }

            if (dt.Rows.Count > 0)
                return dt; // Devuelve la primera (y única) fila encontrada
            else
                return null; // No se encontró ninguna licencia
        }


        public (int total, DataTable datos) ObtenerLicenciasUsoNacionalesExclusivasCombinado(string estadoFiltro, int currentPageIndex, int pageSize)
        {
            int total = 0;
            DataTable datos = new DataTable();

            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("GetLicenciasUsoNacionalesExclusivasCombinado", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        int registrosOmitidos = (currentPageIndex - 1) * pageSize;
                        cmd.Parameters.AddWithValue("estadoFiltro", estadoFiltro);
                        cmd.Parameters.AddWithValue("pageSize", pageSize);
                        cmd.Parameters.AddWithValue("registrosOmitidos", registrosOmitidos);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            // 1. Primer resultado: total de registros
                            if (reader.Read())
                            {
                                total = reader.GetInt32("totalMarcas");
                            }

                            // 2. Avanzar al segundo resultado
                            if (reader.NextResult())
                            {
                                datos.Load(reader);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener licencias nacionales exclusivas combinadas: " + ex.Message);
                }
            }

            return (total, datos);
        }

        public (int total, DataTable datos) ObtenerLicenciasUsoNacionalesNoExclusivasCombinado(string estadoFiltro, int currentPageIndex, int pageSize)
        {
            int total = 0;
            DataTable datos = new DataTable();

            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("GetLicenciasUsoNacionalesNoExclusivasCombinado", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        int registrosOmitidos = (currentPageIndex - 1) * pageSize;
                        cmd.Parameters.AddWithValue("estadoFiltro", estadoFiltro);
                        cmd.Parameters.AddWithValue("pageSize", pageSize);
                        cmd.Parameters.AddWithValue("registrosOmitidos", registrosOmitidos);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            // 1. Primer resultado: total de registros
                            if (reader.Read())
                            {
                                total = reader.GetInt32("totalMarcas");
                            }

                            // 2. Avanzar al segundo resultado
                            if (reader.NextResult())
                            {
                                datos.Load(reader);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener licencias nacionales no exclusivas combinadas: " + ex.Message);
                }
            }

            return (total, datos);
        }

       

    public string CambiarEstadoLicencia(int idLicencia)
    {
        string resultado = "";

        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();

            using (var cmd = new MySqlCommand("CambiarEstadoLicencia", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("p_id", idLicencia);

                var output = new MySqlParameter("p_mensaje", MySqlDbType.VarChar, 100);
                output.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(output);

                cmd.ExecuteNonQuery();

                resultado = output.Value.ToString();
            }
        }

        return resultado;
    }


}
}
*/