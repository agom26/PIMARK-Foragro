
using System;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class MarcaDao
    {
        private readonly string urlApi = "https://bpa.com.es/peticiones/marcas.php";

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

        private DataTable ConvertToDataTable(JsonElement root)
        {
            var table = new DataTable();
            if (root.ValueKind == JsonValueKind.Array)
            {
                foreach (var element in root.EnumerateArray())
                {
                    if (table.Columns.Count == 0)
                    {
                        foreach (var prop in element.EnumerateObject())
                        {
                            if (!table.Columns.Contains(prop.Name))
                                table.Columns.Add(prop.Name);
                        }
                    }
                    var row = table.NewRow();
                    foreach (var prop in element.EnumerateObject())
                    {
                        row[prop.Name] = prop.Value.ToString();
                    }
                    table.Rows.Add(row);
                }
            }
            return table;
        }

        public DataTable ConvertSingleObjectToDataTable(JsonElement jsonObject)
        {
            DataTable table = new DataTable();

            foreach (JsonProperty prop in jsonObject.EnumerateObject())
            {
                table.Columns.Add(prop.Name, typeof(string)); // o inferir tipos si querés ser más preciso
            }

            DataRow row = table.NewRow();

            foreach (JsonProperty prop in jsonObject.EnumerateObject())
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


        public async Task<bool> EliminarMarcaConLog(int idMarca, string usuario)
        {
            var data = new
            {
                action = "eliminar_marca_con_log",
                idMarca,
                usuario
            };

            JsonDocument jsonDoc = await PostAsync(data);
            return jsonDoc.RootElement.TryGetProperty("success", out var success) && success.GetBoolean();
        }

        public async Task<DataTable> ObtenerMarcasNacionalesParaLicencia(int pageSize, int currentPageIndex)
        {
            var data = new
            {
                action = "get_all_marcas_nacionales_para_licencia",
                pageSize,
                currentPageIndex
            };

            JsonDocument jsonDoc = await PostAsync(data);
            return ConvertToDataTable(jsonDoc.RootElement);
        }

        public async Task<DataTable> FiltrarMarcasNacionalesParaLicencia(string filtro, int pageSize, int currentPageIndex)
        {
            var data = new
            {
                action = "filtrar_marcas_nacionales_para_licencia",
                filtro,
                pageSize,
                currentPageIndex
            };

            JsonDocument jsonDoc = await PostAsync(data);
            return ConvertToDataTable(jsonDoc.RootElement);
        }

        public async Task<int> ObtenerTotalMarcasNacionalesParaLicencia()
        {
            var data = new
            {
                action = "get_total_marcas_nacionales_para_licencia"
            };

            JsonDocument jsonDoc = await PostAsync(data);
            return jsonDoc.RootElement.TryGetProperty("total", out var total) ? total.GetInt32() : 0;
        }

        public async Task<int> ObtenerTotalFiltradoMarcasNacionalesParaLicencia(string value)
        {
            var data = new
            {
                action = "get_filtered_marcas_nacionales_para_licencia_count",
                value
            };

            JsonDocument jsonDoc = await PostAsync(data);
            return jsonDoc.RootElement.TryGetProperty("total", out var total) ? total.GetInt32() : 0;
        }

        public async Task<bool> TieneEtapaRegistrada(int idMarca)
        {
            var data = new
            {
                action = "tiene_etapa_registrada",
                idMarca
            };

            var jsonDoc = await PostAsync(data);
            return jsonDoc.RootElement.TryGetProperty("tiene", out var tiene) && tiene.GetBoolean();
        }

        public async Task<bool> InsertarTraspasoYHistorial(
            string numExpediente,
            int idMarca,
            int idTitularAnterior,
            int idTitularNuevo,
            string fecha,
            string etapa,
            string anotaciones,
            string usuario,
            string origen)
        {
            var data = new
            {
                action = "insertar_traspaso_y_historial",
                numExpediente,
                idMarca,
                idTitularAnterior,
                idTitularNuevo,
                fecha,
                etapa,
                anotaciones,
                usuario,
                origen
            };

            var jsonDoc = await PostAsync(data);
            return jsonDoc.RootElement.TryGetProperty("success", out var success) && success.GetBoolean();
        }

        public async Task<bool> RenovarMarca(
            string noExpediente,
            int idMarca,
            DateTime fechaVencAnt,
            DateTime fechaVencNueva,
            DateTime fecha,
            string etapa,
            string anotaciones,
            string usuario)
        {
            var data = new
            {
                action = "renovar_marca",
                noExpediente,
                idMarca,
                fechaVencAnt,
                fechaVencNueva,
                fecha,
                etapa,
                anotaciones,
                usuario
            };

            var jsonDoc = await PostAsync(data);
            return jsonDoc.RootElement.TryGetProperty("success", out var success) && success.GetBoolean();
        }
        public async Task<bool> ExisteRegistromarcaIngresada(string registro, int? idMarca)
        {
            var data = new
            {
                action = "existe_registro_marca",
                registro,
                idMarca
            };

            var jsonDoc = await PostAsync(data);
            return jsonDoc.RootElement.TryGetProperty("existe", out var existe) && existe.GetBoolean();
        }


        public async Task<bool> ExisteRegistro(string registro, int? idMarcaActual)
        {
            var data = new
            {
                action = "existe_registro",
                registro,
                idMarcaActual
            };

            var jsonDoc = await PostAsync(data);
            return jsonDoc.RootElement.TryGetProperty("exists", out var exists) && exists.GetBoolean();
        }

        public async Task<bool> InsertarExpedienteMarca(string numExpediente, int idMarca, string tipo)
        {
            var data = new
            {
                action = "insertar_expediente_marca",
                numExpediente,
                idMarca,
                tipo
            };

            var jsonDoc = await PostAsync(data);
            return jsonDoc.RootElement.TryGetProperty("success", out var success) && success.GetBoolean();
        }

        public async Task<DataTable> ObtenerMarcaNacionalPorId(int id)
        {
            var data = new { action = "get_marca_nacional_by_id", id };
            var jsonDoc = await PostAsync(data);

            return ConvertSingleObjectToDataTable(jsonDoc.RootElement);
        }


        public async Task<byte[]> ObtenerLogoMarcaPorId(int id)
        {
            using var client = new HttpClient();

            try
            {
                string url = $"https://bpa.com.es/peticiones/get_logo.php?id={id}";
                byte[] logoBytes = await client.GetByteArrayAsync(url);
                return logoBytes;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error al obtener el logo: {ex.Message}");
                return null;
            }
        }


       






        public async Task<bool> EditMarcaInternacionalRegistrada(
           int id, string expediente,
           string nombre,
           string signoDistintivo, 
           string tipoSigno, 
           string clase, 
           byte[] logo, 
           int idPersonaTitular, 
           int idPersonaAgente, 
           DateTime fecha_solicitud, 
           string paisRegistro, 
           string tiene_poder, 
           int? idCliente, 
           string registro, 
           string folio, 
           string libro, 
           DateTime fechaRegistro, 
           DateTime fechaVencimiento, 
           string erenov, 
           string etrasp)
        {
            using var client = new HttpClient();
            using var form = new MultipartFormDataContent();

            form.Add(new StringContent("edit_marca_internacional_registrada"), "action");
            form.Add(new StringContent(id.ToString()), "id");
            form.Add(new StringContent(expediente), "expediente");
            form.Add(new StringContent(nombre), "nombre");
            form.Add(new StringContent(signoDistintivo), "signoDistintivo");
            form.Add(new StringContent(tipoSigno), "tipoSigno");
            form.Add(new StringContent(clase), "clase");
            form.Add(new StringContent(folio), "folio");
            form.Add(new StringContent(libro), "libro");
            form.Add(new StringContent(idPersonaTitular.ToString()), "idPersonaTitular");
            form.Add(new StringContent(idPersonaAgente.ToString()), "idPersonaAgente");
            form.Add(new StringContent(fecha_solicitud.ToString("yyyy-MM-dd")), "fecha_solicitud");
            form.Add(new StringContent(paisRegistro), "pais_registro");
            form.Add(new StringContent(tiene_poder), "tiene_poder");
            form.Add(new StringContent(registro), "registro");
            form.Add(new StringContent(fechaRegistro.ToString("yyyy-MM-dd")), "fecha_registro");
            form.Add(new StringContent(fechaVencimiento.ToString("yyyy-MM-dd")), "fecha_vencimiento");
            form.Add(new StringContent(erenov ), "erenov");
            form.Add(new StringContent(etrasp ), "etrasp");
            form.Add(new StringContent(idCliente?.ToString() ?? ""), "idCliente");

            if (logo != null && logo.Length > 0)
            {
                var logoContent = new ByteArrayContent(logo);
                logoContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                form.Add(logoContent, "logo", "logo.png");
            }

            var response = await client.PostAsync("https://bpa.com.es/peticiones/acciones_marca.php", form);
            var responseContent = await response.Content.ReadAsStringAsync();
            return response.IsSuccessStatusCode && responseContent.Contains("true");
        }

        public async Task<DataTable> ObtenerMarcaInternacionalPorId(int id)
        {
            var data = new { action = "get_marca_internacional_by_id", id };
            var jsonDoc = await PostAsync(data);

            return ConvertSingleObjectToDataTable(jsonDoc.RootElement);
        }
       

        public async Task<int> GetTotalMarcasSinRegistro()
        {
            var data = new { action = "get_total_marcas_sin_registro" };
            var jsonDoc = await PostAsync(data);
            if (jsonDoc.RootElement.TryGetProperty("total", out var totalProp))
                return totalProp.GetInt32();
            return 0;
        }

        public async Task<int> GetFilteredMarcasSinRegistroCount(string value)
        {
            var data = new { action = "get_filtered_marcas_sin_registro_count", value };
            var jsonDoc = await PostAsync(data);
            if (jsonDoc.RootElement.TryGetProperty("total", out var totalProp))
                return totalProp.GetInt32();
            return 0;
        }

        public async Task<bool> ActualizarExpedienteMarca(
            int id,
            string expediente,
            string fecha,
            string estado,
            string anotaciones,
            string usuario)
        {
            var data = new
            {
                action = "actualizar_expediente_marca",
                p_id = id,
                p_expediente = expediente,
                p_fecha = fecha,
                estado,
                anotaciones,
                usuario
            };

            var jsonDoc = await PostAsync(data);
            return jsonDoc.RootElement.TryGetProperty("success", out var success) && success.GetBoolean();
        }

        public async Task<DataTable> FiltrarMarcas(
        string tipo_filtro,
        string estado ,
        string nombre ,
        string pais,
        string folio,
        string libro,
        string registro,
        string clase,
        string fechaSolicitudInicio,
        string fechaSolicitudFin,
        string fechaRegistroInicio,
        string fechaRegistroFin,
        string fechaVencimientoInicio,
        string fechaVencimientoFin,
        string titular,
        string agente,
        string cliente)
        {
            var data = new
            {
                action = "filtrar_marcas",
                tipo_filtro,
                estado,
                nombre,
                pais,
                folio,
                libro,
                registro,
                clase,
                fechaSolicitudInicio,
                fechaSolicitudFin,
                fechaRegistroInicio,
                fechaRegistroFin,
                fechaVencimientoInicio,
                fechaVencimientoFin,
                titular,
                agente,
                cliente
            };

            var jsonDoc = await PostAsync(data);

            if (jsonDoc.RootElement.TryGetProperty("data", out var dataArray))
            {
                return ConvertToDataTable(dataArray);
            }
            return new DataTable();
        }

        public async Task<DataTable> FiltrarMarcasNacionalesEnTramite(int pageSize, int currentPageIndex, string filtro)
        {
            var data = new
            {
                action = "filtrar_marcas_nacionales_en_tramite",
                pageSize,
                currentPageIndex,
                filtro
            };

            var jsonDoc = await PostAsync(data);
            return ConvertToDataTable(jsonDoc.RootElement);
        }

        public async Task<DataTable> FiltrarMarcasInternacionalesEnTramite(int pageSize, int currentPageIndex, string filtro)
        {
            var data = new
            {
                action = "filtrar_marcas_internacionales_en_tramite",
                pageSize,
                currentPageIndex,
                filtro
            };

            var jsonDoc = await PostAsync(data);
            return ConvertToDataTable(jsonDoc.RootElement);
        }

        public async Task<DataTable> FiltrarMarcasNacionalesEnOposicion(string filtro)
        {
            var data = new
            {
                action = "filtrar_marcas_nacionales_en_oposicion",
                filtro
            };

            var jsonDoc = await PostAsync(data);
            return ConvertToDataTable(jsonDoc.RootElement);
        }

        public async Task<DataTable> FiltrarMarcasNacionalesEnOposicionInterpuestas(string filtro)
        {
            var data = new
            {
                action = "filtrar_marcas_nacionales_en_oposicion_interpuestas",
                filtro
            };

            var jsonDoc = await PostAsync(data);
            return ConvertToDataTable(jsonDoc.RootElement);
        }

        public async Task<DataTable> filtrarMarcasInternacionalesRecibidasEnOposicion(int pageSize, int currentPageIndex, string filtro)
        {
            var data = new
            {
                action = "filtrar_marcas_internacionales_en_oposicion",
                pageSize,
                currentPageIndex,
                filtro
            };

            var jsonDoc = await PostAsync(data);
            return ConvertToDataTable(jsonDoc.RootElement);
        }

        public async Task<DataTable> FiltrarMarcasInternacionalesEnOposicionInterpuestas(string filtro)
        {
            var data = new
            {
                action = "filtrar_marcas_internacionales_en_oposicion_interpuestas",
                filtro
            };

            var jsonDoc = await PostAsync(data);
            return ConvertToDataTable(jsonDoc.RootElement);
        }

        public async Task<DataTable> FiltrarMarcasNacionalesRegistradas(int pageSize, int currentPageIndex, string filtro)
        {
            var data = new
            {
                action = "filtrar_marcas_nacionales_registradas",
                pageSize,
                currentPageIndex,
                filtro
            };

            var jsonDoc = await PostAsync(data);
            return ConvertToDataTable(jsonDoc.RootElement);
        }

        public async Task<DataTable> FiltrarMarcasInternacionalesRegistradas(int pageSize, int currentPageIndex, string filtro)
        {
            var data = new
            {
                action = "filtrar_marcas_internacionales_registradas",
                pageSize,
                currentPageIndex,
                filtro
            };

            var jsonDoc = await PostAsync(data);
            return ConvertToDataTable(jsonDoc.RootElement);
        }

        public async Task<DataTable> FiltrarMarcasNacionalesEnTramiteDeRenovacion(int pageSize, int currentPageIndex, string filtro)
        {
            var data = new
            {
                action = "filtrar_marcas_nacionales_en_tramite_de_renovacion",
                pageSize,
                currentPageIndex,
                filtro
            };

            var jsonDoc = await PostAsync(data);
            return ConvertToDataTable(jsonDoc.RootElement);
        }

        public async Task<DataTable> FiltrarMarcasNacionalesEnTramiteDeTraspaso(int pageSize, int currentPageIndex, string filtro)
        {
            var data = new
            {
                action = "filtrar_marcas_nacionales_en_tramite_de_traspaso",
                pageSize,
                currentPageIndex,
                filtro
            };

            var jsonDoc = await PostAsync(data);
            return ConvertToDataTable(jsonDoc.RootElement);
        }

        public async Task<DataTable> ObtenerTodasMarcasInternacionales(int pageSize, int currentPageIndex)
        {
            var data = new
            {
                action = "get_all_marcas_internacionales",
                pageSize,
                currentPageIndex
            };

            var jsonDoc = await PostAsync(data);
            return ConvertToDataTable(jsonDoc.RootElement);
        }

        public async Task<DataTable> FiltrarMarcasNacionales(string filtro, int pageSize, int currentPageIndex)
        {
            var data = new
            {
                action = "filtrar_marcas_nacionales",
                filtro,
                pageSize,
                currentPageIndex
                
            };

            var jsonDoc = await PostAsync(data);
            return ConvertToDataTable(jsonDoc.RootElement);
        }

        public async Task<DataTable> FiltrarMarcasInternacionales(int pageSize, int currentPageIndex, string filtro)
        {
            var data = new
            {
                action = "filtrar_marcas_internacionales",
                pageSize,
                currentPageIndex,
                filtro
            };

            var jsonDoc = await PostAsync(data);
            return ConvertToDataTable(jsonDoc.RootElement);
        }

        public async Task<int> GetTotalMarcasNacionales()
        {
            var data = new { action = "get_total_marcas_nacionales" };
            var jsonDoc = await PostAsync(data);

            if (jsonDoc.RootElement.TryGetProperty("total", out var totalProp))
                return totalProp.GetInt32();

            return 0;
        }

        public async Task<int> GetTotalMarcasInternacionales()
        {
            var data = new { action = "get_total_marcas_internacionales" };
            var jsonDoc = await PostAsync(data);

            if (jsonDoc.RootElement.TryGetProperty("total", out var totalProp))
                return totalProp.GetInt32();

            return 0;
        }

        public async Task<int> GetFilteredMarcasNacionalesCount(string value)
        {
            var data = new
            {
                action = "get_filtered_marcas_nacionales_count",
                value
            };

            var jsonDoc = await PostAsync(data);
            if (jsonDoc.RootElement.TryGetProperty("total", out var totalProp))
                return totalProp.GetInt32();

            return 0;
        }

        public async Task<int> GetFilteredMarcasInternacionalesCount(string value)
        {
            var data = new
            {
                action = "get_filtered_marcas_internacionales_count",
                value
            };

            var jsonDoc = await PostAsync(data);
            if (jsonDoc.RootElement.TryGetProperty("total", out var totalProp))
                return totalProp.GetInt32();

            return 0;
        }

        public async Task<DataTable> ObtenerTodasMarcasNacionales(int pageSize, int currentPageIndex)
        {
            var data = new
            {
                action = "get_all_marcas_nacionales",
                pageSize,
                currentPageIndex
            };

            var jsonDoc = await PostAsync(data);
            return ConvertToDataTable(jsonDoc.RootElement);
        }

        public async Task<DataTable> ObtenerMarcasNacionalesEnTramite(int pageSize, int currentPageIndex)
        {
            var data = new
            {
                action = "get_all_marcas_nacionales_en_tramite",
                pageSize,
                currentPageIndex
            };

            var jsonDoc = await PostAsync(data);
            return ConvertToDataTable(jsonDoc.RootElement);
        }

        public async Task<int> GetTotalMarcasInternacionalesSinRegistro()
        {
            var data = new { action = "get_total_marcas_internacionales_sin_registro" };

            var jsonDoc = await PostAsync(data);
            if (jsonDoc.RootElement.TryGetProperty("total", out var totalProp))
                return totalProp.GetInt32();

            return 0;
        }

        public async Task<int> GetFilteredMarcasInternacionalesSinRegistroCount(string value)
        {
            var data = new
            {
                action = "get_filtered_marcas_internacionales_sin_registro_count",
                value
            };

            var jsonDoc = await PostAsync(data);
            return jsonDoc.RootElement.TryGetProperty("total", out var total) ? total.GetInt32() : 0;
        }

        public async Task<DataTable> ObtenerMarcasInternacionalesSinRegistro(int pageSize, int currentPageIndex)
        {
            var data = new
            {
                action = "get_all_marcas_internacionales_ingresadas",
                pageSize,
                currentPageIndex
            };

            var jsonDoc = await PostAsync(data);
            return ConvertToDataTable(jsonDoc.RootElement);
        }

        public async Task<DataTable> ObtenerMarcasNacionalesEnOposicion()
        {
            var data = new { action = "get_all_marcas_nacionales_en_oposicion" };

            var jsonDoc = await PostAsync(data);
            return ConvertToDataTable(jsonDoc.RootElement);
        }

        public async Task<int> GetTotalMarcasInternacionalesEnOposicionRecibidas(string situacionActual, int pageSize)
        {
            var data = new
            {
                action = "get_total_marcas_internacionales_en_oposicion_recibidas",
                situacionActual,
                pageSize
            };

            var jsonDoc = await PostAsync(data);
            return jsonDoc.RootElement.TryGetProperty("total", out var total) ? total.GetInt32() : 0;
        }

        public async Task<int> GetFilteredMarcasInternacionalesEnOposicionRecibidasCount(string value)
        {
            var data = new
            {
                action = "get_filtered_marcas_internacionales_en_oposicion_recibidas_count",
                value
            };

            var jsonDoc = await PostAsync(data);
            return jsonDoc.RootElement.TryGetProperty("total", out var total) ? total.GetInt32() : 0;
        }

        public async Task<int> GetFilteredMarcasInternacionalesRecibidasCount(string value)
        {
            var data = new
            {
                action = "get_filtered_marcas_internacionales_recibidas_count",
                value
            };

            var jsonDoc = await PostAsync(data);
            return jsonDoc.RootElement.TryGetProperty("total", out var total) ? total.GetInt32() : 0;
        }

        public async Task<DataTable> ObtenerMarcasInternacionalesEnOposicionRecibidas(string situacionActual, int pageSize, int currentPageIndex)
        {
            var data = new
            {
                action = "get_all_marcas_internacionales_en_oposicion",
                situacionActual,
                pageSize,
                currentPageIndex
            };

            var jsonDoc = await PostAsync(data);
            return ConvertToDataTable(jsonDoc.RootElement);
        }

        public async Task<DataTable> ObtenerMarcasInternacionalesEnOposicionInterpuestas()
        {
            var data = new
            {
                action = "get_all_marcas_internacionales_en_oposicion_interpuestas"
            };

            var jsonDoc = await PostAsync(data);
            return ConvertToDataTable(jsonDoc.RootElement);
        }

        public async Task<int> GetTotalMarcasRegistradas()
        {
            var data = new { action = "get_total_marcas_registradas" };

            var jsonDoc = await PostAsync(data);
            return jsonDoc.RootElement.TryGetProperty("total", out var total) ? total.GetInt32() : 0;
        }

        public async Task<int> GetFilteredMarcasRegistradasCount(string value)
        {
            var data = new
            {
                action = "get_filtered_marcas_registradas_count",
                value
            };

            var jsonDoc = await PostAsync(data);
            return jsonDoc.RootElement.TryGetProperty("total", out var total) ? total.GetInt32() : 0;
        }

        public async Task<DataTable> ObtenerMarcasNacionalesRegistradas(int pageSize, int currentPageIndex)
        {
            var data = new
            {
                action = "get_all_marcas_nacionales_registradas",
                pageSize,
                currentPageIndex
            };

            var jsonDoc = await PostAsync(data);
            return ConvertToDataTable(jsonDoc.RootElement);
        }

        public async Task<DataTable> ObtenerMarcasInternacionalesRegistradas(int pageSize, int currentPageIndex)
        {
            var data = new
            {
                action = "get_all_marcas_internacionales_registradas",
                pageSize,
                currentPageIndex
            };

            var jsonDoc = await PostAsync(data);
            return ConvertToDataTable(jsonDoc.RootElement);
        }

        public async Task<int> GetTotalMarcasEnTramiteDeRenovacion()
        {
            var data = new { action = "get_total_marcas_en_tramite_de_renovacion" };

            var jsonDoc = await PostAsync(data);
            return jsonDoc.RootElement.TryGetProperty("total", out var total) ? total.GetInt32() : 0;
        }

        public async Task<int> GetFilteredMarcasEnTramiteDeRenovacionCount(string value)
        {
            var data = new
            {
                action = "get_filtered_marcas_en_tramite_de_renovacion_count",
                value
            };

            var jsonDoc = await PostAsync(data);
            return jsonDoc.RootElement.TryGetProperty("total", out var total) ? total.GetInt32() : 0;
        }

        public async Task<int> GetTotalMarcasEnTramiteDeTraspaso()
        {
            var data = new { action = "get_total_marcas_en_tramite_de_traspaso" };

            var jsonDoc = await PostAsync(data);
            return jsonDoc.RootElement.TryGetProperty("total", out var total) ? total.GetInt32() : 0;
        }

        public async Task<int> GetFilteredMarcasEnTramiteDeTraspasoCount(string value)
        {
            var data = new
            {
                action = "get_filtered_marcas_en_tramite_de_traspaso_count",
                value
            };

            var jsonDoc = await PostAsync(data);
            return jsonDoc.RootElement.TryGetProperty("total", out var total) ? total.GetInt32() : 0;
        }
        public async Task<int> GetTotalMarcasInternacionalesEnTramiteDeRenovacion()
        {
            var data = new { action = "get_total_marcas_internacionales_en_tramite_de_renovacion" };

            var jsonDoc = await PostAsync(data);
            return jsonDoc.RootElement.TryGetProperty("total", out var total) ? total.GetInt32() : 0;
        }

        public async Task<int> GetFilteredMarcasInternacionalesEnTramiteDeRenovacionCount(string value)
        {
            var data = new
            {
                action = "get_filtered_marcas_internacionales_en_tramite_de_renovacion_count",
                value
            };

            var jsonDoc = await PostAsync(data);
            return jsonDoc.RootElement.TryGetProperty("total", out var total) ? total.GetInt32() : 0;
        }

        public async Task<int> GetTotalMarcasInternacionalesEnTramiteDeTraspaso()
        {
            var data = new { action = "get_total_marcas_internacionales_en_tramite_de_traspaso" };

            var jsonDoc = await PostAsync(data);
            return jsonDoc.RootElement.TryGetProperty("total", out var total) ? total.GetInt32() : 0;
        }

        public async Task<int> GetFilteredMarcasInternacionalesEnTramiteDeTraspasoCount(string value)
        {
            var data = new
            {
                action = "get_filtered_marcas_internacionales_en_tramite_de_traspaso_count",
                value
            };

            var jsonDoc = await PostAsync(data);
            return jsonDoc.RootElement.TryGetProperty("total", out var total) ? total.GetInt32() : 0;
        }

        public async Task<int> GetTotalMarcasInternacionalesRegistradas()
        {
            var data = new { action = "get_total_marcas_internacionales_registradas" };

            var jsonDoc = await PostAsync(data);
            return jsonDoc.RootElement.TryGetProperty("total", out var total) ? total.GetInt32() : 0;
        }

        public async Task<int> GetFilteredMarcasInternacionalesRegistradasCount(string value)
        {
            var data = new
            {
                action = "get_filtered_marcas_internacionales_registradas_count",
                value
            };

            var jsonDoc = await PostAsync(data);
            return jsonDoc.RootElement.TryGetProperty("total", out var total) ? total.GetInt32() : 0;
        }

        public async Task<int> GetTotalMarcasEnAbandono()
        {
            var data = new { action = "get_total_marcas_en_abandono" };

            var jsonDoc = await PostAsync(data);
            return jsonDoc.RootElement.TryGetProperty("total", out var total) ? total.GetInt32() : 0;
        }

        public async Task<int> GetFilteredMarcasEnAbandonoCount(string value)
        {
            var data = new
            {
                action = "get_filtered_marcas_en_abandono_count",
                value
            };

            var jsonDoc = await PostAsync(data);
            return jsonDoc.RootElement.TryGetProperty("total", out var total) ? total.GetInt32() : 0;
        }

        public async Task<DataTable> ObtenerMarcasEnAbandono(int pageSize, int currentPageIndex)
        {
            var data = new
            {
                action = "obtener_marcas_en_abandono",
                pageSize,
                currentPageIndex
            };

            var jsonDoc = await PostAsync(data);
            return ConvertToDataTable(jsonDoc.RootElement);
        }

        public async Task<DataTable> FiltrarMarcasEnAbandono(string filtro, int pageSize, int currentPageIndex)
        {
            var data = new
            {
                action = "filtrar_marcas_en_abandono",
                filtro,
                pageSize,
                currentPageIndex
            };

            var jsonDoc = await PostAsync(data);
            return ConvertToDataTable(jsonDoc.RootElement);
        }

        public async Task<int> GetTotalMarcasInternacionalesEnAbandono()
        {
            var data = new { action = "get_total_marcas_internacionales_en_abandono" };

            var jsonDoc = await PostAsync(data);
            return jsonDoc.RootElement.TryGetProperty("total", out var total) ? total.GetInt32() : 0;
        }

        public async Task<int> GetFilteredMarcasInternacionalesEnAbandonoCount(string value)
        {
            var data = new
            {
                action = "get_filtered_marcas_internacionales_en_abandono_count",
                value
            };

            var jsonDoc = await PostAsync(data);
            return jsonDoc.RootElement.TryGetProperty("total", out var total) ? total.GetInt32() : 0;
        }

        public async Task<DataTable> ObtenerMarcasInternacionalesEnAbandono(int pageSize, int currentPageIndex)
        {
            var data = new
            {
                action = "obtener_marcas_internacionales_en_abandono",
                pageSize,
                currentPageIndex
            };

            var jsonDoc = await PostAsync(data);
            return ConvertToDataTable(jsonDoc.RootElement);
        }

        public async Task<DataTable> FiltrarMarcasInternacionalesEnAbandono(string filtro, int pageSize, int currentPageIndex)
        {
            var data = new
            {
                action = "filtrar_marcas_internacionales_en_abandono",
                filtro,
                pageSize,
                currentPageIndex
            };

            var jsonDoc = await PostAsync(data);
            return ConvertToDataTable(jsonDoc.RootElement);
        }

        public async Task<DataTable> ObtenerMarcasRegistradasRenovaciones(int pageSize, int currentPageIndex)
        {
            var data = new
            {
                action = "obtener_marcas_registradas_renovaciones",
                pageSize,
                currentPageIndex
            };

            var jsonDoc = await PostAsync(data);
            return ConvertToDataTable(jsonDoc.RootElement);
        }

        public async Task<DataTable> FiltrarMarcasInternacionalesEnRenovacion(string filtro, int pageSize, int currentPageIndex)
        {
            var data = new
            {
                action = "filtrar_marcas_internacionales_en_renovacion",
                filtro,
                pageSize,
                currentPageIndex
            };

            var jsonDoc = await PostAsync(data);
            return ConvertToDataTable(jsonDoc.RootElement);
        }

        public async Task<DataTable> ObtenerMarcasRegistradasEnTramiteDeTraspaso(int pageSize, int currentPageIndex)
        {
            var data = new
            {
                action = "obtener_marcas_registradas_en_traspaso",
                pageSize,
                currentPageIndex
            };

            var jsonDoc = await PostAsync(data);
            return ConvertToDataTable(jsonDoc.RootElement);
        }

        public async Task<DataTable> FiltrarMarcaInternacionalEnTramiteDeTraspaso(string filtro, int pageSize, int currentPageIndex)
        {
            var data = new
            {
                action = "filtrar_marca_internacional_en_traspaso",
                filtro,
                pageSize,
                currentPageIndex
            };

            var jsonDoc = await PostAsync(data);
            return ConvertToDataTable(jsonDoc.RootElement);
        }

        public async Task<DataTable> ObtenerMarcasInternacionalesRegistradasRenovaciones(int pageSize, int currentPageIndex)
        {
            var data = new
            {
                action = "obtener_marcas_internacionales_renovaciones",
                pageSize,
                currentPageIndex
            };

            var jsonDoc = await PostAsync(data);
            return ConvertToDataTable(jsonDoc.RootElement);
        }

        public async Task<DataTable> ObtenerMarcasInternacionalesRegistradasEnTramiteDeTraspaso(int pageSize, int currentPageIndex)
        {
            var data = new
            {
                action = "obtener_marcas_internacionales_traspaso",
                pageSize,
                currentPageIndex
            };

            var jsonDoc = await PostAsync(data);
            return ConvertToDataTable(jsonDoc.RootElement);
        }

        public async Task<DataTable> ObtenerTipoMarca(int id)
        {
            var data = new
            {
                action = "obtener_tipo_marca",
                id
            };

            var jsonDoc = await PostAsync(data);
            return ConvertToDataTable(jsonDoc.RootElement);
        }

        public async Task<int> AddMarcaNacional(
    string expediente,
    string nombre,
    string signoDistintivo,
    string tipoSigno,
    string clase,
    byte[] logo,
    int idPersonaTitular,
    int idPersonaAgente,
    DateTime fechaSolicitud,
    int? idCliente)
        {
            using var client = new HttpClient();
            using var form = new MultipartFormDataContent();

            form.Add(new StringContent("add_marca_nacional"), "action");
            form.Add(new StringContent(expediente), "expediente");
            form.Add(new StringContent(nombre), "nombre");
            form.Add(new StringContent(signoDistintivo), "signoDistintivo");
            form.Add(new StringContent(tipoSigno), "tipoSigno");
            form.Add(new StringContent(clase), "clase");
            form.Add(new StringContent(idPersonaTitular.ToString()), "idPersonaTitular");
            form.Add(new StringContent(idPersonaAgente.ToString()), "idPersonaAgente");
            form.Add(new StringContent(fechaSolicitud.ToString("yyyy-MM-dd")), "fecha_solicitud");
            form.Add(new StringContent(idCliente?.ToString() ?? ""), "idCliente");

            if (logo != null && logo.Length > 0)
            {
                var logoContent = new ByteArrayContent(logo);
                logoContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                form.Add(logoContent, "logo", "logo.png");
            }

            var response = await client.PostAsync("https://bpa.com.es/peticiones/acciones_marca.php", form);
            var responseContent = await response.Content.ReadAsStringAsync();

            using var json = JsonDocument.Parse(responseContent);
            return json.RootElement.TryGetProperty("idMarca", out var id) ? id.GetInt32() : -1;
        }

        public async Task<int> AddMarcaInternacional(
    string expediente,
    string nombre,
    string signoDistintivo,
    string tipoSigno,
    string clase,
    byte[] logo,
    int idPersonaTitular,
    int idPersonaAgente,
    DateTime fechaSolicitud,
    string paisDeRegistro,
    string tienePoder,
    int? idCliente)
        {
            using var client = new HttpClient();
            using var form = new MultipartFormDataContent();

            form.Add(new StringContent("add_marca_internacional"), "action");
            form.Add(new StringContent(expediente), "expediente");
            form.Add(new StringContent(nombre), "nombre");
            form.Add(new StringContent(signoDistintivo), "signoDistintivo");
            form.Add(new StringContent(tipoSigno), "tipoSigno");
            form.Add(new StringContent(clase), "clase");
            form.Add(new StringContent(idPersonaTitular.ToString()), "idPersonaTitular");
            form.Add(new StringContent(idPersonaAgente.ToString()), "idPersonaAgente");
            form.Add(new StringContent(fechaSolicitud.ToString("yyyy-MM-dd")), "fecha_solicitud");
            form.Add(new StringContent(paisDeRegistro), "pais_de_registro");
            form.Add(new StringContent(tienePoder), "tiene_poder");
            form.Add(new StringContent(idCliente?.ToString() ?? ""), "idCliente");

            if (logo != null && logo.Length > 0)
            {
                var logoContent = new ByteArrayContent(logo);
                logoContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                form.Add(logoContent, "logo", "logo.png");
            }

            var response = await client.PostAsync("https://bpa.com.es/peticiones/acciones_marca.php", form);
            var responseContent = await response.Content.ReadAsStringAsync();

            using var json = JsonDocument.Parse(responseContent);
            return json.RootElement.TryGetProperty("idMarca", out var id) ? id.GetInt32() : -1;
        }

        public async Task<int> AddMarcaNacionalRegistrada(
    string expediente,
    string nombre,
    string signoDistintivo,
    string tipoSigno,
    string clase,
    string folio,
    string libro,
    byte[] logo,
    int idPersonaTitular,
    int idPersonaAgente,
    DateTime fechaSolicitud,
    string registro,
    DateTime fechaRegistro,
    DateTime fechaVencimiento,
    int? idCliente)
        {
            using var client = new HttpClient();
            using var form = new MultipartFormDataContent();

            form.Add(new StringContent("add_marca_nacional_registrada"), "action");
            form.Add(new StringContent(expediente), "expediente");
            form.Add(new StringContent(nombre), "nombre");
            form.Add(new StringContent(signoDistintivo), "signoDistintivo");
            form.Add(new StringContent(tipoSigno), "tipoSigno");
            form.Add(new StringContent(clase), "clase");
            form.Add(new StringContent(folio), "folio");
            form.Add(new StringContent(libro), "libro");
            form.Add(new StringContent(idPersonaTitular.ToString()), "idPersonaTitular");
            form.Add(new StringContent(idPersonaAgente.ToString()), "idPersonaAgente");
            form.Add(new StringContent(fechaSolicitud.ToString("yyyy-MM-dd")), "fecha_solicitud");
            form.Add(new StringContent(registro), "registro");
            form.Add(new StringContent(fechaRegistro.ToString("yyyy-MM-dd")), "fechaRegistro");
            form.Add(new StringContent(fechaVencimiento.ToString("yyyy-MM-dd")), "fechaVencimiento");
            form.Add(new StringContent(idCliente?.ToString() ?? ""), "idCliente");

            if (logo != null && logo.Length > 0)
            {
                var logoContent = new ByteArrayContent(logo);
                logoContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                form.Add(logoContent, "logo", "logo.png");
            }

            var response = await client.PostAsync("https://bpa.com.es/peticiones/acciones_marca.php", form);
            var responseContent = await response.Content.ReadAsStringAsync();

            using var json = JsonDocument.Parse(responseContent);
            return json.RootElement.TryGetProperty("idMarca", out var id) ? id.GetInt32() : -1;
        }

        public async Task<int> AddMarcaInternacionalRegistrada(
            string expediente,
            string nombre,
            string signoDistintivo,
            string tipoSigno,
            string clase,
            byte[] logo,
            int idPersonaTitular,
            int idPersonaAgente,
            DateTime fechaSolicitud,
            string paisRegistro,
            string tienePoder,
            int? idCliente,
            string registro,
            string folio,
            string libro,
            DateTime fechaRegistro,
            DateTime fechaVencimiento)
        {
            using var client = new HttpClient();
            using var form = new MultipartFormDataContent();

            form.Add(new StringContent("add_marca_internacional_registrada"), "action");
            form.Add(new StringContent(expediente), "expediente");
            form.Add(new StringContent(nombre), "nombre");
            form.Add(new StringContent(signoDistintivo), "signoDistintivo");
            form.Add(new StringContent(tipoSigno), "tipoSigno");
            form.Add(new StringContent(clase), "clase");
            form.Add(new StringContent(idPersonaTitular.ToString()), "idPersonaTitular");
            form.Add(new StringContent(idPersonaAgente.ToString()), "idPersonaAgente");
            form.Add(new StringContent(fechaSolicitud.ToString("yyyy-MM-dd")), "fecha_solicitud");
            form.Add(new StringContent(paisRegistro), "pais_de_registro");
            form.Add(new StringContent(tienePoder), "tiene_poder");
            form.Add(new StringContent(idCliente?.ToString() ?? ""), "idCliente");
            form.Add(new StringContent(registro), "registro");
            form.Add(new StringContent(folio), "folio");
            form.Add(new StringContent(libro), "libro");
            form.Add(new StringContent(fechaRegistro.ToString("yyyy-MM-dd")), "fechaRegistro");
            form.Add(new StringContent(fechaVencimiento.ToString("yyyy-MM-dd")), "fechaVencimiento");

            if (logo != null && logo.Length > 0)
            {
                var logoContent = new ByteArrayContent(logo);
                logoContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                form.Add(logoContent, "logo", "logo.png");
            }

            var response = await client.PostAsync("https://bpa.com.es/peticiones/acciones_marca.php", form);
            var responseContent = await response.Content.ReadAsStringAsync();

            using var json = JsonDocument.Parse(responseContent);
            return json.RootElement.TryGetProperty("idMarca", out var id) ? id.GetInt32() : -1;
        }

        public async Task<bool> EditMarcaNacional(
            int id,
            string expediente,
            string nombre,
            string signoDistintivo,
            string tipoSigno,
            string clase,
            byte[] logo,
            int idPersonaTitular,
            int idPersonaAgente,
            DateTime fechaSolicitud,
            int? idCliente)
        {
            using var client = new HttpClient();
            using var form = new MultipartFormDataContent();

            form.Add(new StringContent("edit_marca_nacional"), "action");
            form.Add(new StringContent(id.ToString()), "id");
            form.Add(new StringContent(expediente), "expediente");
            form.Add(new StringContent(nombre), "nombre");
            form.Add(new StringContent(signoDistintivo), "signoDistintivo");
            form.Add(new StringContent(tipoSigno), "tipoSigno");
            form.Add(new StringContent(clase), "clase");
            form.Add(new StringContent(idPersonaTitular.ToString()), "idPersonaTitular");
            form.Add(new StringContent(idPersonaAgente.ToString()), "idPersonaAgente");
            form.Add(new StringContent(fechaSolicitud.ToString("yyyy-MM-dd")), "fecha_solicitud");
            form.Add(new StringContent(idCliente?.ToString() ?? ""), "idCliente");

            if (logo != null && logo.Length > 0)
            {
                var logoContent = new ByteArrayContent(logo);
                logoContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                form.Add(logoContent, "logo", "logo.png");
            }

            var response = await client.PostAsync("https://bpa.com.es/peticiones/acciones_marca.php", form);
            var responseContent = await response.Content.ReadAsStringAsync();
            return response.IsSuccessStatusCode && responseContent.Contains("true");
        }

        public async Task<bool> EditMarcaNacionalRegistrada(
           int id,
           string expediente,
           string nombre,
           string signoDistintivo,
           string tipoSigno,
           string clase,
           string folio,
           string libro,
           byte[] logo,
           int idPersonaTitular,
           int idPersonaAgente,
           DateTime fechaSolicitud,
           string registro,
           DateTime fechaRegistro,
           DateTime fechaVencimiento,
           string erenov,
           string etrasp,
           int? idCliente)
        {
            using var client = new HttpClient();
            using var form = new MultipartFormDataContent();

            form.Add(new StringContent("edit_marca_nacional_registrada"), "action");
            form.Add(new StringContent(id.ToString()), "id");
            form.Add(new StringContent(expediente), "expediente");
            form.Add(new StringContent(nombre), "nombre");
            form.Add(new StringContent(signoDistintivo), "signoDistintivo");
            form.Add(new StringContent(tipoSigno), "tipoSigno");
            form.Add(new StringContent(clase), "clase");
            form.Add(new StringContent(folio), "folio");
            form.Add(new StringContent(libro), "libro");
            form.Add(new StringContent(idPersonaTitular.ToString()), "idPersonaTitular");
            form.Add(new StringContent(idPersonaAgente.ToString()), "idPersonaAgente");
            form.Add(new StringContent(fechaSolicitud.ToString("yyyy-MM-dd")), "fecha_solicitud");
            form.Add(new StringContent(registro), "registro");
            form.Add(new StringContent(fechaRegistro.ToString("yyyy-MM-dd")), "fecha_registro");
            form.Add(new StringContent(fechaVencimiento.ToString("yyyy-MM-dd")), "fecha_vencimiento");
            form.Add(new StringContent(erenov ?? ""), "erenov");
            form.Add(new StringContent(etrasp ?? ""), "etrasp");
            form.Add(new StringContent(idCliente?.ToString() ?? ""), "idCliente");

            if (logo != null && logo.Length > 0)
            {
                var logoContent = new ByteArrayContent(logo);
                logoContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                form.Add(logoContent, "logo", "logo.png");
            }

            var response = await client.PostAsync("https://bpa.com.es/peticiones/acciones_marca.php", form);
            var responseContent = await response.Content.ReadAsStringAsync();
            return response.IsSuccessStatusCode && responseContent.Contains("true");
        }


        public async Task<bool> EditMarcaInternacional(
            int id,
            string expediente,
            string nombre,
            string signoDistintivo,
            string tipoSigno,
            string clase,
            byte[] logo,
            int idPersonaTitular,
            int idPersonaAgente,
            DateTime fechaSolicitud,
            string pais, 
            string tiene_poder,
            int? idCliente)
        {
            using var client = new HttpClient();
            using var form = new MultipartFormDataContent();

            form.Add(new StringContent("edit_marca_internacional"), "action");
            form.Add(new StringContent(id.ToString()), "id");
            form.Add(new StringContent(expediente), "expediente");
            form.Add(new StringContent(nombre), "nombre");
            form.Add(new StringContent(signoDistintivo), "signoDistintivo");
            form.Add(new StringContent(tipoSigno), "tipoSigno");
            form.Add(new StringContent(clase), "clase");
            form.Add(new StringContent(idPersonaTitular.ToString()), "idPersonaTitular");
            form.Add(new StringContent(idPersonaAgente.ToString()), "idPersonaAgente");
            form.Add(new StringContent(fechaSolicitud.ToString("yyyy-MM-dd")), "fecha_solicitud");
            form.Add(new StringContent(pais), "pais_registro");
            form.Add(new StringContent(tiene_poder), "tiene_poder");
            form.Add(new StringContent(idCliente?.ToString() ?? ""), "idCliente");

            if (logo != null && logo.Length > 0)
            {
                var logoContent = new ByteArrayContent(logo);
                logoContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                form.Add(logoContent, "logo", "logo.png");
            }

            var response = await client.PostAsync("https://bpa.com.es/peticiones/acciones_marca.php", form);
            var responseContent = await response.Content.ReadAsStringAsync();
            return response.IsSuccessStatusCode && responseContent.Contains("true");
        }




    }
}


/*
using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Google.Protobuf.WellKnownTypes;

namespace AccesoDatos.Entidades
{
    public class MarcaDao:ConnectionSQL
    {
        public bool EliminarMarcaConLog(int idMarca, string usuario)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    using (var command = new MySqlCommand("EliminarMarcaConLog", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@p_Id", idMarca);
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

        //todas las marcas nacionales para licencias de uso
        public DataTable GetAllMarcasNacionalesParaLicencia(int currentPageIndex, int pageSize)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("ObtenerMarcasNacionalesParaLicencia", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    int registrosOmitidos = (currentPageIndex - 1) * pageSize;
                    // Agregar parámetros de entrada
                    command.Parameters.AddWithValue("pageSize", pageSize);
                    command.Parameters.AddWithValue("registrosOmitidos", registrosOmitidos);
                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        DataTable resultado = new DataTable();
                        adapter.Fill(resultado);
                        return resultado;
                    }
                }
            }
        }
        public DataTable filtrarMarcasNacionalesParaLicencia(string filtro, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("filtrarMarcasNacionalesParaLicencia", conexion))
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
                Console.WriteLine($"Error al obtener las marcas para licencia de uso: {ex.Message}");
            }
            return tabla;
        }
       
        public int GetTotalMarcasNacionalesParaLicencia()
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetTotalMarcasNacionalesParaLicencia", conexion))
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
        
        public int GetFilteredMarcasNacionalesParaLicenciaCount(string value)
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetTotalMarcasNacionalesParaLicencia", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue("@value", value);

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
        //
        public bool TieneEtapaRegistrada(int idMarca)
        {
            using (MySqlConnection conn = GetConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand("VerificarEtapaRegistrada", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@p_IdMarca", idMarca);

                    MySqlParameter tieneEtapa = new MySqlParameter("@p_TieneEtapaRegistrada", MySqlDbType.Bit);
                    tieneEtapa.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(tieneEtapa);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    return Convert.ToBoolean(tieneEtapa.Value);
                }
            }
        }

        public void InsertarTraspasoYHistorialMarca(
        string numExpediente,
        int idMarca,
        int idTitularAnterior,
        int idTitularNuevo,
        DateTime fecha,
        string etapa,
        string anotaciones,
        string usuario,
        string origen)
        {
            using (MySqlConnection conn = GetConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand("InsertarTraspasoYHistorialMarca", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@p_NumExpediente", numExpediente);
                    cmd.Parameters.AddWithValue("@p_IdMarca", idMarca);
                    cmd.Parameters.AddWithValue("@p_IdTitularAnterior", idTitularAnterior);
                    cmd.Parameters.AddWithValue("@p_IdTitularNuevo", idTitularNuevo);
                    cmd.Parameters.AddWithValue("@p_Fecha", fecha);
                    cmd.Parameters.AddWithValue("@p_Etapa", etapa);
                    cmd.Parameters.AddWithValue("@p_Anotaciones", anotaciones);
                    cmd.Parameters.AddWithValue("@p_Usuario", usuario);
                    cmd.Parameters.AddWithValue("@p_Origen", origen);

                    MySqlParameter exito = new MySqlParameter("@p_Exito", MySqlDbType.Bit);
                    exito.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(exito);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    bool resultado = Convert.ToBoolean(exito.Value);
                    if (!resultado)
                    {
                        throw new Exception("Error al realizar la transacción de traspaso e historial.");
                    }
                }
            }
        }


        public bool RenovarMarca(string noExpediente, int idMarca, DateTime fechaVencAnt, DateTime fechaVencNueva,
                         DateTime fecha, string etapa, string anotaciones, string usuario)
        {
            using (MySqlConnection conn = GetConnection())
            using (MySqlCommand cmd = new MySqlCommand("CALL RenovarMarcaConTransaccion(@NumExpediente, @IdMarca, @FechaVencAnt, @FechaVencNueva, " +
                                                       "@Fecha, @Etapa, @Anotaciones, @Usuario, @Origen, @Exito)", conn))
            {
                // Parámetros de entrada
                cmd.Parameters.AddWithValue("@NumExpediente", noExpediente);
                cmd.Parameters.AddWithValue("@IdMarca", idMarca);
                cmd.Parameters.AddWithValue("@FechaVencAnt", fechaVencAnt);
                cmd.Parameters.AddWithValue("@FechaVencNueva", fechaVencNueva);
                cmd.Parameters.AddWithValue("@Fecha", fecha);
                cmd.Parameters.AddWithValue("@Etapa", etapa);
                cmd.Parameters.AddWithValue("@Anotaciones", anotaciones);
                cmd.Parameters.AddWithValue("@Usuario", usuario);
                cmd.Parameters.AddWithValue("@Origen", "TRÁMITE");

                // Parámetro de salida (indica si la operación fue exitosa)
                MySqlParameter outputParam = new MySqlParameter("@Exito", MySqlDbType.Bit);
                outputParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outputParam);

                conn.Open();
                cmd.ExecuteNonQuery();

                return Convert.ToBoolean(outputParam.Value);
            }
        }

        public bool ExisteRegistro(string registro, int? idMarcaActual)
        {
            using (MySqlConnection conn = GetConnection())
            using (MySqlCommand cmd = new MySqlCommand("CALL ExisteRegistroMarca(@Registro, @IdMarca, @Existe)", conn))
            {
                cmd.Parameters.AddWithValue("@Registro", registro);
                cmd.Parameters.AddWithValue("@IdMarca", idMarcaActual ?? 0); // Si es NULL, pasamos 0

                MySqlParameter outputParam = new MySqlParameter("@Existe", MySqlDbType.Bit);
                outputParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outputParam);

                conn.Open();
                cmd.ExecuteNonQuery();

                return Convert.ToBoolean(outputParam.Value);
            }
        }



        public void InsertarExpedienteMarca(string numExpediente, int idMarca, string tipo)
        {
            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("InsertarExpedienteMarca", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue("@p_NumExpediente",numExpediente);
                    comando.Parameters.AddWithValue("@p_IdMarca", idMarca);
                    comando.Parameters.AddWithValue("@p_tipo", tipo);

                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }

        public int GetTotalMarcasSinRegistro()
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetTotalMarcasSinRegistro", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

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
        public int GetFilteredMarcasSinRegistroCount(string value)
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetFilteredMarcasSinRegistroCount", conexion))
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
        public void ActualizarExpedienteMarca(int p_id, string p_expediente, DateTime fecha, string estado, 
            string anotaciones, string usuario)
        {
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("expedienteMarca", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                        comando.Parameters.AddWithValue("@p_id", p_id);
                        comando.Parameters.AddWithValue("@p_expediente", string.IsNullOrEmpty(p_expediente) ? DBNull.Value : (object)p_expediente);
                        comando.Parameters.AddWithValue("@p_fecha", string.IsNullOrEmpty(fecha.ToString()) ? DBNull.Value : (object)fecha);
                        comando.Parameters.AddWithValue("@p_estado", string.IsNullOrEmpty(estado) ? DBNull.Value : (object)estado);
                        comando.Parameters.AddWithValue("@p_anotaciones", string.IsNullOrEmpty(anotaciones) ? DBNull.Value : (object)anotaciones);
                        comando.Parameters.AddWithValue("@p_usuario", string.IsNullOrEmpty(usuario) ? DBNull.Value : (object)usuario);

                        conexion.Open();
                        comando.ExecuteNonQuery();  
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar expediente de la marca: {ex.Message}");
            }
        }

            public DataTable Filtrar(string tipo_filtro,
            string? estado, string? nombre, string? pais, string? folio, string? libro,
            string? registro, string? clase, 
            string? fechaSolicitudInicio, string? fechaSolicitudFin,
            string? fechaRegistroInicio, string? fechaRegistroFin,
            string? fechaVencimientoInicio, string? fechaVencimientoFin,
            string? titular, string? agente,string? cliente)
            {
                DataTable dataTable = new DataTable();

                using (MySqlConnection conexion = GetConnection())
                {
                    conexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Filtrar", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("tipo_filtro", tipo_filtro);
                        cmd.Parameters.AddWithValue("estado_filtro", estado ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("p_nombre", nombre ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("p_pais", pais ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("p_folio", folio ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("p_tomo", libro ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("p_registro", registro ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("p_clase", clase ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("P_solicitud_inicio", fechaSolicitudInicio ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("p_solicitud_fin", fechaSolicitudFin ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("p_registro_inicio", fechaRegistroInicio?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("p_registro_fin", fechaRegistroFin?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("p_vencimiento_inicio", fechaVencimientoInicio ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("p_vencimiento_fin", fechaVencimientoFin ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("p_titular", titular ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("p_agente", agente ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("p_cliente", cliente ?? (object)DBNull.Value);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(dataTable);  
                        }
                    }
                }

                return dataTable; 
            }
        



        public DataTable FiltrarMarcasNacionalesEnTramite(string filtro, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("filtrarMarcasSinRegistro", conexion))
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
                Console.WriteLine($"Error al obtener las marcas sin registro: {ex.Message}");
            }
            return tabla;
        }
        public DataTable FiltrarMarcasInternacionalesEnTramite(string filtro, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("filtrarMarcasInternacionalesSinRegistro", conexion))
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
                Console.WriteLine($"Error al obtener las marcas sin registro: {ex.Message}");
            }
            return tabla;
        }

        public DataTable FiltrarMarcasNacionalesEnOposicion(string filtro)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("filtrarMarcasRecibidasEnOposicion", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

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
                Console.WriteLine($"Error al obtener las marcas : {ex.Message}");
            }
            return tabla;
        }

        public DataTable FiltrarMarcasNacionalesEnOposicionInterpuestas(string filtro)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("filtrarMarcasInterpuestasEnOposicion", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

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
                Console.WriteLine($"Error al obtener las marcas : {ex.Message}");
            }
            return tabla;
        }

        public DataTable FiltrarMarcasInternacionalesEnOposicion(string filtro, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("filtrarMarcasInternacionalesRecibidasEnOposicion ", conexion))
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
                Console.WriteLine($"Error al obtener las marcas : {ex.Message}");
            }
            return tabla;
        }

        public DataTable FiltrarMarcasInternacionalesEnOposicionInterpuestas(string filtro)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("filtrarMarcasInternacionalesInterpuestasEnOposicion", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

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
                Console.WriteLine($"Error al obtener las marcas : {ex.Message}");
            }
            return tabla;
        }


        public DataTable FiltrarMarcasNacionalesRegistradas(string filtro, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("filtrarMarcasRegistradas", conexion))
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
                Console.WriteLine($"Error al obtener las marcas sin registro: {ex.Message}");
            }
            return tabla;
        }
        public DataTable FiltrarMarcasInternacionalesRegistradas(string filtro, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("filtrarMarcasInternacionalesRegistradas", conexion))
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
                Console.WriteLine($"Error al obtener las marcas sin registro: {ex.Message}");
            }
            return tabla;
        }


        public DataTable FiltrarMarcasNacionalesEnTramiteDeRenovacion(string filtro, int currentPageIndex, int pageSize)
            {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("filtrarMarcasEnRenovacion", conexion))
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
                Console.WriteLine($"Error al obtener las marcas sin registro: {ex.Message}");
            }
            return tabla;
        }
        public DataTable FiltrarMarcasNacionalesEnTramiteDeTraspaso(string filtro, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("filtrarMarcaEnTramiteDeTraspaso", conexion))
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
                Console.WriteLine($"Error al obtener las marcas sin registro: {ex.Message}");
            }
            return tabla;
        }

        public DataTable GetAllMarcasInternacionales(int currentPageIndex, int pageSize)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("ObtenerMarcasInternacionales", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    int registrosOmitidos = (currentPageIndex - 1) * pageSize;
                    // Agregar parámetros de entrada
                    command.Parameters.AddWithValue("pageSize", pageSize);
                    command.Parameters.AddWithValue("registrosOmitidos", registrosOmitidos);
                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        DataTable resultado = new DataTable();
                        adapter.Fill(resultado);
                        return resultado;
                    }
                }
            }
        }
        //todas las marcas nacionales 
        public DataTable filtrarMarcasNacionales(string filtro, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("filtrarMarcasNacionales", conexion))
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
                Console.WriteLine($"Error al obtener las marcas sin registro: {ex.Message}");
            }
            return tabla;
        }
        public DataTable filtrarMarcasInternacionales(string filtro, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("filtrarMarcasInternacionales", conexion))
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
                Console.WriteLine($"Error al obtener las marcas sin registro: {ex.Message}");
            }
            return tabla;
        }

        public int GetTotalMarcasNacionales()
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetTotalMarcasNacionales", conexion))
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
        public int GetTotalMarcasInternacionales()
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetTotalMarcasInternacionales", conexion))
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
        public int GetFilteredMarcasNacionalesCount(string value)
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetFilteredMarcasNacionalesCount", conexion))
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
        public int GetFilteredMarcasInternacionalesCount(string value)
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetFilteredMarcasInternacionalesCount", conexion))
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

        public DataTable GetAllMarcasNacionales(int currentPageIndex, int pageSize)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("ObtenerMarcasNacionales", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    int registrosOmitidos = (currentPageIndex - 1) * pageSize;
                    // Agregar parámetros de entrada
                    command.Parameters.AddWithValue("pageSize", pageSize);
                    command.Parameters.AddWithValue("registrosOmitidos", registrosOmitidos);
                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        DataTable resultado = new DataTable();
                        adapter.Fill(resultado);
                        return resultado;
                    }
                }
            }
        }
       
        public DataTable GetAllMarcasNacionalesEnTramite(int currentPageIndex, int pageSize)
        {
           
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection()) 
                {
                    using (MySqlCommand comando = new MySqlCommand("ObtenerMarcasSinRegistro", conexion))
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
                Console.WriteLine($"Error al obtener las marcas sin registro: {ex.Message}");
               
            }
            return tabla; 
        }

        public int GetTotalMarcasInternacionalesSinRegistro()
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetTotalMarcasInternacionalesSinRegistro", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

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
        public int GetFilteredMarcasInternacionalesSinRegistroCount(string value)
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetFilteredMarcasInternacionalesSinRegistroCount", conexion))
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
        
        public DataTable GetAllMarcasInternacionalesIngresadas(int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection()) 
                {
                    using (MySqlCommand comando = new MySqlCommand("ObtenerMarcasInternacionalesSinRegistro", conexion))
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
                Console.WriteLine($"Error al obtener las marcas internacionales sin registro: {ex.Message}");

            }
            return tabla;
        }

        public DataTable GetAllMarcasNacionalesEnOposicion()
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection()) 
                {
                    using (MySqlCommand comando = new MySqlCommand(@"
                    SELECT  
                        M.id,
                        M.nombre AS Nombre, 
                        M.estado AS Estado,
                        M.expediente As Expediente,
                        M.clase AS Clase,  
                        P1.nombre AS Titular, 
                        P2.nombre AS Agente
                    FROM 
                        `Marcas` M
                    JOIN 
                        Personas AS P1 ON M.IdTitular = P1.id 
                    JOIN 
                        Personas AS P2 ON M.IdAgente = P2.id 
                    WHERE 
                        M.tipo = 'nacional' AND 
                        (estado='Oposición')
                    ORDER BY 
                        M.id DESC;", conexion))
                    {
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
                Console.WriteLine($"Error al obtener las marcas nacionales: {ex.Message}");
                
            }
            return tabla;
        }
        public int GetTotalMarcasInternacionalesEnOposicionRecibidas(string situacionActual, int currentPage, int pageSize)
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetTotalOposicionesInternacionalesRecibidas", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("p_situacion_actual", situacionActual);
                    comando.Parameters.AddWithValue("pageSize", pageSize);
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
        public int GetFilteredMarcasInternacionalesEnOposicionRecibidasCount(string value)
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetFilteredMarcasInternacionalesSinRegistroCount", conexion))
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
        
        public int GetFilteredMarcasInternacionalesRecibidasCount(string value)
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetFilteredMarcasInternacionalesRecibidasCount", conexion))
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
        public DataTable GetAllMarcasInternacionalesEnOposicion(string situacionActual, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection()) 
                {
                    using (MySqlCommand comando = new MySqlCommand("ObtenerOposicionesInternacionalesRecibidas", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("p_situacion_actual", situacionActual);
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
                Console.WriteLine($"Error al obtener las marcas sin registro: {ex.Message}");

            }
            return tabla;
        }

        public DataTable GetAllMarcasInternacionalesEnOposicionInterpuestas()
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("ObtenerOposicionesInternacionalesInterpuestas", conexion))
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
                Console.WriteLine($"Error al obtener las marcas sin registro: {ex.Message}");

            }
            return tabla;
        }
        public int GetTotalMarcasRegistradas()
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetTotalMarcasRegistradas", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

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
        public int GetFilteredMarcasRegistradasCount(string value)
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetFilteredMarcasRegistradasCount", conexion))
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
        public DataTable GetAllMarcasNacionalesRegistradas(int currentPageIndex, int pageSize)
        {
            string estadoFiltro = "Registrada";
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection()) 
                {
                    using (MySqlCommand comando = new MySqlCommand("ObtenerMarcasRegistradas", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        int registrosOmitidos = (currentPageIndex - 1) * pageSize;
                        // Agregar parámetros de entrada
                        comando.Parameters.AddWithValue("pageSize", pageSize);
                        comando.Parameters.AddWithValue("registrosOmitidos", registrosOmitidos);
                        comando.Parameters.AddWithValue("@estadoFiltro", estadoFiltro);

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
                Console.WriteLine($"Error al obtener las marcas nacionales: {ex.Message}");
                
            }
            return tabla; 
        }
        //internacionales registradas

        public int GetTotalMarcasInternacionalesRegistradas()
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetTotalMarcasInternacionalesRegistradas", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

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
        public int GetFilteredMarcasInternacionalesRegistradasCount(string value)
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetFilteredMarcasInternacionalesRegistradasCount", conexion))
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
        public DataTable GetAllMarcasInternacionalesRegistradas(int currentPageIndex, int pageSize)
        {
            string estadoFiltro = "Registrada";
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("ObtenerMarcasInternacionalesRegistradas", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        int registrosOmitidos = (currentPageIndex - 1) * pageSize;
                        // Agregar parámetros de entrada
                        comando.Parameters.AddWithValue("pageSize", pageSize);
                        comando.Parameters.AddWithValue("registrosOmitidos", registrosOmitidos);
                        comando.Parameters.AddWithValue("@estadoFiltro", estadoFiltro);

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
                Console.WriteLine($"Error al obtener las marcas nacionales: {ex.Message}");

            }
            return tabla;
        }

        public int GetTotalMarcasEnAbandono()
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetTotalMarcasAbandonadas", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

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
        public int GetFilteredMarcasEnAbandonoCount(string value)
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetFilteredMarcasAbandonadasCount", conexion))
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
        public DataTable GetAllMarcasNacionalesEnAbandono(int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("ObtenerMarcasEnAbandono", conexion))
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
                Console.WriteLine($"Error al obtener las marcas nacionales en abandono: {ex.Message}");
            }
            return tabla;
        }
        public DataTable FiltrarMarcasNacionalesEnAbandono(string filtro, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("filtrarMarcasEnAbandono", conexion))
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
                Console.WriteLine($"Error al obtener las marcas sin registro: {ex.Message}");
            }
            return tabla;
        }


        //internacionales en abandono
        public int GetTotalMarcasInternacionalesEnAbandono()
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetTotalMarcasInternacionalesAbandonadas", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

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
        public int GetFilteredMarcasInternacionalesEnAbandonoCount(string value)
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetFilteredMarcasInternacionalesAbandonadasCount", conexion))
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
        public DataTable GetAllMarcasInternacionalesEnAbandono(int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("ObtenerMarcasInternacionalesEnAbandono", conexion))
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
                Console.WriteLine($"Error al obtener las marcas internacionales en abandono: {ex.Message}");
            }
            return tabla;
        }
        public DataTable FiltrarMarcasInternacionalesEnAbandono(string filtro, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("filtrarMarcasInternacionalesEnAbandono", conexion))
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
                Console.WriteLine($"Error al obtener las marcas sin registro: {ex.Message}");
            }
            return tabla;
        }



                          
        public int AddMarcaNacional(string expediente, string nombre, string signoDistintivo,string tipoSigno, string clase, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud, int? idCliente)
        {
            using (var connection = GetConnection()) 
            {
                connection.Open();

                using (var command = new MySqlCommand(@"INSERT INTO Marcas (expediente, nombre, signo_distintivo,tipoSigno, clase, logo, idTitular, idAgente, fecha_solicitud, tipo, idCliente) 
                                                  VALUES (@expediente, @nombre, @signoDistintivo,@tipoSigno, @clase, @logo, @idPersonaTitular, @idPersonaAgente, @fecha_solicitud, 'nacional',@idCliente); 
                                                  SELECT LAST_INSERT_ID();", connection))
                {
                    command.Parameters.AddWithValue("@expediente", expediente);
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@signoDistintivo", signoDistintivo);
                    command.Parameters.AddWithValue("@tipoSigno", tipoSigno);
                    command.Parameters.AddWithValue("@clase", clase);
                    command.Parameters.AddWithValue("@logo", logo); 
                    command.Parameters.AddWithValue("@idPersonaTitular", idPersonaTitular);
                    command.Parameters.AddWithValue("@idPersonaAgente", idPersonaAgente);
                    command.Parameters.AddWithValue("@fecha_solicitud", fecha_solicitud);
                    command.Parameters.AddWithValue("@idCliente", idCliente);

                    int idMarca = Convert.ToInt32(command.ExecuteScalar());
                    return idMarca; 
                }
            }
        }


        public int AddMarcaInternacional(string expediente, string nombre, string signoDistintivo, string tipoSigno, string clase, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud, string pais_de_registro, string tiene_poder, int? idCliente)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                
                using (var command = new MySqlCommand(@"INSERT INTO Marcas (expediente, nombre, signo_distintivo, tipoSigno, clase, logo, idTitular, idAgente, fecha_solicitud, pais_de_registro, tiene_poder, idCliente, tipo) 
                                                  VALUES (@expediente, @nombre, @signoDistintivo, @tipoSigno, @clase, @logo, @idPersonaTitular, @idPersonaAgente, @fecha_solicitud, @pais_de_registro, @tiene_poder, @idCliente, 'internacional'); 
                                                  SELECT LAST_INSERT_ID();", connection))
                {
                    command.Parameters.AddWithValue("@expediente", expediente);
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@signoDistintivo", signoDistintivo);
                    command.Parameters.AddWithValue("@tipoSigno", tipoSigno);
                    command.Parameters.AddWithValue("@clase", clase);
                    command.Parameters.AddWithValue("@logo", logo);
                    command.Parameters.AddWithValue("@idPersonaTitular", idPersonaTitular);
                    command.Parameters.AddWithValue("@idPersonaAgente", idPersonaAgente);
                    command.Parameters.AddWithValue("@fecha_solicitud", fecha_solicitud);
                    command.Parameters.AddWithValue("@pais_de_registro", pais_de_registro);
                    command.Parameters.AddWithValue("@tiene_poder", tiene_poder);
                    command.Parameters.AddWithValue("@idCliente", idCliente);

                    int idMarca = Convert.ToInt32(command.ExecuteScalar());
                    return idMarca;
                }
            }
        }


        
        public int AddMarcaNacionalRegistrada(string expediente, string nombre, string signoDistintivo, string tipoSigno, string clase, string folio,
                                      string libro, byte[] logo, int idPersonaTitular, int idPersonaAgente,
                                      DateTime fecha_solicitud, string registro,
                                      DateTime fechaRegistro, DateTime fechaVencimiento, int? idCliente)
        {
            int idMarca = 0;
            using (var connection = GetConnection()) 
            {
                connection.Open();
                using (var command = new MySqlCommand(@"
                    INSERT INTO Marcas (expediente, nombre, signo_distintivo, tipoSigno, clase, folio, libro, logo, idTitular, idAgente, 
                                        fecha_solicitud, tipo, registro, fecha_registro, fecha_vencimiento, idCliente) 
                    VALUES (@expediente, @nombre, @signoDistintivo, @tipoSigno, @clase, @folio, @libro, @logo, @idPersonaTitular, 
                            @idPersonaAgente, @fecha_solicitud, 'nacional', @registro, @fecha_registro, @fecha_vencimiento, @idCliente);
                    SELECT LAST_INSERT_ID();", connection))
                {
                    command.Parameters.AddWithValue("@expediente", expediente);
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@signoDistintivo", signoDistintivo);
                    command.Parameters.AddWithValue("@tipoSigno", tipoSigno);
                    command.Parameters.AddWithValue("@clase", clase);
                    command.Parameters.AddWithValue("@folio", folio);
                    command.Parameters.AddWithValue("@libro", libro);
                    command.Parameters.AddWithValue("@logo", logo); 
                    command.Parameters.AddWithValue("@idPersonaTitular", idPersonaTitular);
                    command.Parameters.AddWithValue("@idPersonaAgente", idPersonaAgente);
                    command.Parameters.AddWithValue("@fecha_solicitud", fecha_solicitud);
                    command.Parameters.AddWithValue("@registro", registro);
                    command.Parameters.AddWithValue("@fecha_registro", fechaRegistro);
                    command.Parameters.AddWithValue("@fecha_vencimiento", fechaVencimiento);
                    command.Parameters.AddWithValue("@idCliente", idCliente);

                    idMarca = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            return idMarca; 
        }


        public int AddMarcaInternacionalRegistrada(string expediente, string nombre, string signoDistintivo, string tipoSigno, string clase, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud, string pais_de_registro, string tiene_poder, int? idCliente, string registro, string folio, string libro, DateTime fechaRegistro, DateTime fechaVencimiento)
        {
            int idMarca = 0;
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(@"
                    INSERT INTO Marcas (expediente, nombre, signo_distintivo, tipoSigno, clase, logo, idTitular, idAgente, 
                                        fecha_solicitud, pais_de_registro, tiene_poder, idCliente, tipo, 
                                        registro, folio, libro, fecha_registro, fecha_vencimiento) 
                    VALUES (@expediente, @nombre, @signoDistintivo, @tipoSigno, @clase, @logo, @idPersonaTitular, 
                            @idPersonaAgente, @fecha_solicitud, @pais_de_registro, @tiene_poder, @idCliente, 
                            'internacional', @registro, @folio, @libro, @fecha_registro, @fecha_vencimiento);
                    SELECT LAST_INSERT_ID();", connection))
                {
                    command.Parameters.AddWithValue("@expediente", expediente);
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@signoDistintivo", signoDistintivo);
                    command.Parameters.AddWithValue("@tipoSigno", tipoSigno);
                    command.Parameters.AddWithValue("@clase", clase);
                    command.Parameters.AddWithValue("@logo", logo);
                    command.Parameters.AddWithValue("@idPersonaTitular", idPersonaTitular);
                    command.Parameters.AddWithValue("@idPersonaAgente", idPersonaAgente);
                    command.Parameters.AddWithValue("@fecha_solicitud", fecha_solicitud);
                    command.Parameters.AddWithValue("@pais_de_registro", pais_de_registro);
                    command.Parameters.AddWithValue("@tiene_poder", tiene_poder);
                    command.Parameters.AddWithValue("@idCliente", idCliente);
                    command.Parameters.AddWithValue("@registro", registro);
                    command.Parameters.AddWithValue("@folio", folio);
                    command.Parameters.AddWithValue("@libro", libro);
                    command.Parameters.AddWithValue("@fecha_registro", fechaRegistro);
                    command.Parameters.AddWithValue("@fecha_vencimiento", fechaVencimiento);

                    idMarca = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            return idMarca; 
        }


        public DataTable GetMarcaNacionalById(int id)
        {
            DataTable marcaTable = new DataTable();

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("ObtenerMarcaNacionalPorId", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@marcaId", id);

                    conexion.Open();

                    using (MySqlDataReader reader = comando.ExecuteReader())
                    {
                        marcaTable.Load(reader);
                    }
                }
            }

            return marcaTable;
        }



        public bool EditMarcaNacional(int id, string expediente, string nombre, string signoDistintivo, string tipoSigno, string clase, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud, int? idCliente)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("EditMarcaNacional", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("p_id", id);
                    command.Parameters.AddWithValue("p_expediente", expediente);
                    command.Parameters.AddWithValue("p_nombre", nombre);
                    command.Parameters.AddWithValue("p_signoDistintivo", signoDistintivo);
                    command.Parameters.AddWithValue("p_tipoSigno", tipoSigno);
                    command.Parameters.AddWithValue("p_clase", clase);
                    command.Parameters.AddWithValue("p_logo", logo);
                    command.Parameters.AddWithValue("p_idPersonaTitular", idPersonaTitular);
                    command.Parameters.AddWithValue("p_idPersonaAgente", idPersonaAgente);
                    command.Parameters.AddWithValue("p_fecha_solicitud", fecha_solicitud);
                    command.Parameters.AddWithValue("p_idCliente", (object)idCliente ?? DBNull.Value);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }


        public bool EditMarcaNacionalRegistrada(int id, string expediente, string nombre, string signoDistintivo, string tipoSigno, string clase, string folio, string libro, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud, string registro, DateTime fechaRegistro, DateTime fechaVencimiento, string erenov, string etrasp, int? idCliente)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("EditMarcaNacionalRegistrada", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@p_id", id);
                    command.Parameters.AddWithValue("@p_expediente", expediente);
                    command.Parameters.AddWithValue("@p_nombre", nombre);
                    command.Parameters.AddWithValue("@p_signoDistintivo", signoDistintivo);
                    command.Parameters.AddWithValue("@p_tipoSigno", tipoSigno);
                    command.Parameters.AddWithValue("@p_clase", clase);
                    command.Parameters.AddWithValue("@p_folio", folio);
                    command.Parameters.AddWithValue("@p_libro", libro);

                    // Validar si el logo viene nulo
                    if (logo != null)
                        command.Parameters.AddWithValue("@p_logo", logo);
                    else
                        command.Parameters.AddWithValue("@p_logo", DBNull.Value);

                    command.Parameters.AddWithValue("@p_idPersonaTitular", idPersonaTitular);
                    command.Parameters.AddWithValue("@p_idPersonaAgente", idPersonaAgente);
                    command.Parameters.AddWithValue("@p_fechaSolicitud", fecha_solicitud);
                    command.Parameters.AddWithValue("@p_registro", registro);
                    command.Parameters.AddWithValue("@p_fechaRegistro", fechaRegistro);
                    command.Parameters.AddWithValue("@p_fechaVencimiento", fechaVencimiento);
                    command.Parameters.AddWithValue("@p_erenov", erenov);
                    command.Parameters.AddWithValue("@p_etrasp", etrasp);

                    // Validar nulo en idCliente
                    if (idCliente.HasValue)
                        command.Parameters.AddWithValue("@p_idCliente", idCliente.Value);
                    else
                        command.Parameters.AddWithValue("@p_idCliente", DBNull.Value);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }



        public bool EditarMarcaInternacional(int id, string expediente, string nombre, string signoDistintivo,string tipoSigno, string clase, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud, string paisRegistro, string tiene_poder, int? idCliente)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("EditarMarcaInternacional", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("p_id", id);
                    command.Parameters.AddWithValue("p_expediente", expediente);
                    command.Parameters.AddWithValue("p_nombre", nombre);
                    command.Parameters.AddWithValue("p_signo_distintivo", signoDistintivo);
                    command.Parameters.AddWithValue("p_tipoSigno", tipoSigno);
                    command.Parameters.AddWithValue("p_clase", clase);
                    command.Parameters.AddWithValue("p_logo", logo);
                    command.Parameters.AddWithValue("p_idPersonaTitular", idPersonaTitular);
                    command.Parameters.AddWithValue("p_idPersonaAgente", idPersonaAgente);
                    command.Parameters.AddWithValue("p_fecha_solicitud", fecha_solicitud);
                    command.Parameters.AddWithValue("p_pais_de_registro", paisRegistro);
                    command.Parameters.AddWithValue("p_tiene_poder", tiene_poder);
                    command.Parameters.AddWithValue("p_idCliente", idCliente);

                   
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public bool EditarMarcaInternacionalRegistrada(int id, string expediente, string nombre, string signoDistintivo,
            string tipoSigno,
        string clase, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fechaSolicitud,
        string paisRegistro, string tienePoder, int? idCliente, string registro, string folio,
        string libro, DateTime fechaRegistro, DateTime fechaVencimiento, string erenov, string etrasp)
        {
            int resultado;

            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new MySqlCommand("EditarMarcaInternacionalRegistrada", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("p_id", id);
                    command.Parameters.AddWithValue("p_expediente", expediente);
                    command.Parameters.AddWithValue("p_nombre", nombre);
                    command.Parameters.AddWithValue("p_signo_distintivo", signoDistintivo);
                    command.Parameters.AddWithValue("p_tipoSigno", tipoSigno);
                    command.Parameters.AddWithValue("p_clase", clase);
                    command.Parameters.AddWithValue("p_logo", logo);
                    command.Parameters.AddWithValue("p_idPersonaTitular", idPersonaTitular);
                    command.Parameters.AddWithValue("p_idPersonaAgente", idPersonaAgente);
                    command.Parameters.AddWithValue("p_fecha_solicitud", fechaSolicitud);
                    command.Parameters.AddWithValue("p_pais_de_registro", paisRegistro);
                    command.Parameters.AddWithValue("p_tiene_poder", tienePoder);
                    command.Parameters.AddWithValue("p_idCliente", idCliente);
                    command.Parameters.AddWithValue("p_registro", registro);
                    command.Parameters.AddWithValue("p_folio", folio);
                    command.Parameters.AddWithValue("p_libro", libro);
                    command.Parameters.AddWithValue("p_fecha_registro", fechaRegistro);
                    command.Parameters.AddWithValue("p_fecha_vencimiento", fechaVencimiento);
                    command.Parameters.AddWithValue("@p_erenov", erenov);
                    command.Parameters.AddWithValue("@p_etrasp", etrasp);

                    MySqlParameter resultadoParam = new MySqlParameter("p_resultado", MySqlDbType.Int32)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    };
                    command.Parameters.Add(resultadoParam);

                    command.ExecuteNonQuery();

                    resultado = Convert.ToInt32(resultadoParam.Value);
                }
            }

            return resultado == 0;
        }

        public DataTable GetMarcaInternacionalById(int id)
        {
            var dataTable = new DataTable();

            using (var conexion = GetConnection())
            {
                using (var comando = new MySqlCommand("GetMarcaInternacionalById", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("p_id", id);

                    conexion.Open();

                    using (var adapter = new MySqlDataAdapter(comando))
                    {
                       
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }
        //nacionales en renovacion
        public int GetTotalMarcasEnTramiteDeRenovacion()
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetTotalMarcasEnTramiteDeRenovacion", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

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
        public int GetFilteredMarcasEnTramiteDeRenovacionCount(string value)
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetFilteredMarcasEnTramiteDeRenovacionCount", conexion))
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
        
        public DataTable ObtenerMarcasRegistradasEnTramiteDeRenovacion(int currentPageIndex, int pageSize)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("ObtenerMarcasRegistradasRenovaciones", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    int registrosOmitidos = (currentPageIndex - 1) * pageSize;
                    // Agregar parámetros de entrada
                    command.Parameters.AddWithValue("pageSize", pageSize);
                    command.Parameters.AddWithValue("registrosOmitidos", registrosOmitidos);
                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        DataTable resultado = new DataTable();
                        adapter.Fill(resultado);
                        return resultado;
                    }
                }
            }
        }
        //internacionales en renvacion
        public DataTable FiltrarMarcasInternacionalesEnTramiteDeRenovacion(string filtro, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("filtrarMarcasInternacionalesEnRenovacion", conexion))
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
                Console.WriteLine($"Error al obtener las marcas sin registro: {ex.Message}");
            }
            return tabla;
        }
        public int GetTotalMarcasInternacionalesEnTramiteDeRenovacion()
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetTotalMarcasInternacionalesEnTramiteDeRenovacion", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

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
        public int GetFilteredMarcasInternacionalesEnTramiteDeRenovacionCount(string value)
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetFilteredMarcasinternacionalesEnTramiteDeRenovacionCount", conexion))
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

        public DataTable ObtenerMarcasInternacionalesRegistradasEnTramiteDeRenovacion(int currentPageIndex, int pageSize)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("ObtenerMarcasInternacionalesRegistradasRenovaciones", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    int registrosOmitidos = (currentPageIndex - 1) * pageSize;
                    // Agregar parámetros de entrada
                    command.Parameters.AddWithValue("pageSize", pageSize);
                    command.Parameters.AddWithValue("registrosOmitidos", registrosOmitidos);
                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        DataTable resultado = new DataTable();
                        adapter.Fill(resultado);
                        return resultado;
                    }
                }
            }
        }
        //nacionales en traspaso
        public int GetTotalMarcasEnTramiteDeTraspaso()
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetTotalMarcasEnTramiteDeTraspaso", conexion))
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
        public int GetFilteredMarcasEnTramiteDeTraspasoCount(string value)
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetFilteredMarcasEnTramiteDeTraspasoCount", conexion))
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
        public DataTable ObtenerMarcasRegistradasEnTramiteDeTraspaso(int currentPageIndex, int pageSize)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("ObtenerMarcasRegistradasEnTramiteDeTraspaso", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    int registrosOmitidos = (currentPageIndex - 1) * pageSize;
                    // Agregar parámetros de entrada
                    command.Parameters.AddWithValue("pageSize", pageSize);
                    command.Parameters.AddWithValue("registrosOmitidos", registrosOmitidos);
                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        DataTable resultado = new DataTable();
                        adapter.Fill(resultado);
                        return resultado;
                    }
                }
            }
        }

        //internacionales en traspaso
        public DataTable FiltrarMarcasInternacionalesEnTramiteDeTraspaso(string filtro, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("filtrarMarcaInternacionalEnTramiteDeTraspaso", conexion))
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
                Console.WriteLine($"Error al obtener las marcas sin registro: {ex.Message}");
            }
            return tabla;
        }
        public int GetTotalMarcasInternacionalesEnTramiteDeTraspaso()
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetTotalMarcasInternacionalesEnTramiteDeTraspaso", conexion))
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
        public int GetFilteredMarcasInternacionalesEnTramiteDeTraspasoCount(string value)
        {
            int totalMarcas = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetFilteredMarcasInternacionalesEnTramiteDeTraspasoCount", conexion))
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
        public DataTable ObtenerMarcasInternacionalesRegistradasEnTramiteDeTraspaso(int currentPageIndex, int pageSize)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("ObtenerMarcasInternacionalesRegistradasEnTramiteDeTraspaso", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    int registrosOmitidos = (currentPageIndex - 1) * pageSize;
                    // Agregar parámetros de entrada
                    command.Parameters.AddWithValue("pageSize", pageSize);
                    command.Parameters.AddWithValue("registrosOmitidos", registrosOmitidos);
                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        DataTable resultado = new DataTable();
                        adapter.Fill(resultado);
                        return resultado;
                    }
                }
            }
        }
        

        public DataTable ObtenerTipoMarca(int id)
        {
            var dataTable = new DataTable();

            using (var conexion = GetConnection())
            {
                using (var comando = new MySqlCommand("ObtenerTipoMarca", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("p_id", id);

                    conexion.Open();

                    using (var adapter = new MySqlDataAdapter(comando))
                    {

                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }

    }
}
*/