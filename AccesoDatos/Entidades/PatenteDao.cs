using System.Data;
using System.Text;
using System.Text.Json;

namespace AccesoDatos.Entidades
{
    public class PatenteDao
    {
        private readonly string urlApi = "https://foragro.com.es/peticiones/patente.php";
        

        // ========== Infraestructura base ==========
        private async Task<JsonDocument> PostAsync(object data)
        {
            using var client = new HttpClient();
            string json = JsonSerializer.Serialize(data);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            var resp = await client.PostAsync(urlApi, content);
            string body = await resp.Content.ReadAsStringAsync();

            if (!resp.IsSuccessStatusCode)
                throw new Exception($"HTTP {(int)resp.StatusCode}: {body}");

            try
            {
                return JsonDocument.Parse(body);
            }
            catch (Exception ex)
            {
                throw new Exception($"JSON inválido desde {urlApi}: {ex.Message}\nRespuesta: {body}");
            }
        }

        private static int ReadInt(JsonElement root, string propName, int fallback = 0)
        {
            if (root.TryGetProperty(propName, out var p))
            {
                if (p.ValueKind == JsonValueKind.Number)
                {
                    if (p.TryGetInt32(out var i)) return i;
                    if (int.TryParse(p.GetRawText(), out var j)) return j;
                }
                if (p.ValueKind == JsonValueKind.String && int.TryParse(p.GetString(), out var k)) return k;
            }
            return fallback;
        }

        private static DataTable JsonArrayToDataTable(JsonElement arr)
        {
            var table = new DataTable();
            if (arr.ValueKind != JsonValueKind.Array) return table;

            foreach (var item in arr.EnumerateArray())
            {
                if (item.ValueKind != JsonValueKind.Object) continue;

                if (table.Columns.Count == 0)
                {
                    foreach (var p in item.EnumerateObject())
                        if (!table.Columns.Contains(p.Name)) table.Columns.Add(p.Name);
                }

                var row = table.NewRow();
                foreach (var p in item.EnumerateObject())
                {
                    row[p.Name] = p.Value.ValueKind switch
                    {
                        JsonValueKind.String => (object?)p.Value.GetString() ?? DBNull.Value,
                        JsonValueKind.Number => p.Value.GetRawText(), // evita perder precisión/tipo
                        JsonValueKind.True => true,
                        JsonValueKind.False => false,
                        JsonValueKind.Null => DBNull.Value,
                        _ => p.Value.ToString()
                    } ?? DBNull.Value;
                }
                table.Rows.Add(row);
            }
            return table;
        }

        private static DataTable ExtractDataTable(JsonDocument doc)
        {
            var root = doc.RootElement;
            if (root.ValueKind == JsonValueKind.Object && root.TryGetProperty("data", out var dataProp) && dataProp.ValueKind == JsonValueKind.Array)
                return JsonArrayToDataTable(dataProp);
            if (root.ValueKind == JsonValueKind.Array)
                return JsonArrayToDataTable(root);
            return new DataTable();
        }

        // =========================
        // 1) REGISTRADAS
        // =========================

        public async Task<int> GetTotalPatentesRegistradas()
        {
            using var doc = await PostAsync(new { action = "get_total_patentes_registradas" });
            return ReadInt(doc.RootElement, "total", 0);
        }

        public async Task<int> GetFilteredPatentesRegistradasCount(string value)
        {
            using var doc = await PostAsync(new { action = "get_filtered_patentes_registradas_count", value });
            return ReadInt(doc.RootElement, "total", 0);
        }

        public async Task<DataTable> GetAllPatentesRegistradas(int currentPageIndex, int pageSize)
        {
            var data = new
            {
                action = "get_all_patentes_registradas",
                currentPageIndex,
                pageSize
            };
            using var doc = await PostAsync(data);
            return ExtractDataTable(doc);
        }

        public async Task<DataTable> FiltrarPatentesRegistradas(string filtro, int currentPageIndex, int pageSize)
        {
            var data = new
            {
                action = "filtrar_patentes_registradas",
                filtro = string.IsNullOrWhiteSpace(filtro) ? null : filtro,
                currentPageIndex,
                pageSize
            };
            using var doc = await PostAsync(data);
            return ExtractDataTable(doc);
        }

        // ========================================
        // 2) TRÁMITE DE RENOVACIÓN
        // ========================================

        public async Task<int> GetTotalPatentesRegistradasEnTramiteDeRenovacion()
        {
            using var doc = await PostAsync(new { action = "get_total_patentes_registradas_en_tramite_de_renovacion" });
            return ReadInt(doc.RootElement, "total", 0);
        }

        public async Task<int> GetFilteredPatentesRegistradasEnTramiteDeRenovacionCount(string value)
        {
            using var doc = await PostAsync(new
            {
                action = "get_filtered_patentes_registradas_en_tramite_de_renovacion_count",
                filtro=value
            });
            return ReadInt(doc.RootElement, "total", 0);
        }

        public async Task<DataTable> GetAllPatentesRegistradasEnTramiteDeRenovacion(int currentPageIndex, int pageSize)
        {
            var data = new
            {
                action = "get_all_patentes_registradas_en_tramite_de_renovacion",
                currentPageIndex,
                pageSize
            };
            using var doc = await PostAsync(data);
            return ExtractDataTable(doc);
        }

        public async Task<DataTable> FiltrarPatentesRegistradasEnTramiteDeRenovacion(string filtro, int currentPageIndex, int pageSize)
        {
            var data = new
            {
                action = "filtrar_patentes_registradas_en_tramite_de_renovacion",
                filtro = string.IsNullOrWhiteSpace(filtro) ? null : filtro,
                currentPageIndex,
                pageSize
            };
            using var doc = await PostAsync(data);
            return ExtractDataTable(doc);
        }

        // =====================================
        // 3) TRÁMITE DE TRASPASO
        // =====================================

        public async Task<int> GetTotalPatentesRegistradasEnTramiteDeTraspaso()
        {
            using var doc = await PostAsync(new { action = "get_total_patentes_registradas_en_tramite_de_traspaso" });
            return ReadInt(doc.RootElement, "total", 0);
        }

        public async Task<int> GetFilteredPatentesRegistradasEnTramiteDeTraspasoCount(string value)
        {
            using var doc = await PostAsync(new
            {
                action = "get_filtered_patentes_registradas_en_tramite_de_traspaso_count",
                filtro = value
            });
            return ReadInt(doc.RootElement, "total", 0);
        }

        public async Task<DataTable> GetAllPatentesRegistradasEnTramiteDeTraspaso(int currentPageIndex, int pageSize)
        {
            var data = new
            {
                action = "get_all_patentes_registradas_en_tramite_de_traspaso",
                currentPageIndex,
                pageSize
            };
            using var doc = await PostAsync(data);
            return ExtractDataTable(doc);
        }

        public async Task<DataTable> FiltrarPatentesRegistradasEnTramiteDeTraspaso(string filtro, int currentPageIndex, int pageSize)
        {
            var data = new
            {
                action = "filtrar_patentes_registradas_en_tramite_de_traspaso",
                filtro = string.IsNullOrWhiteSpace(filtro) ? null : filtro,
                currentPageIndex,
                pageSize
            };
            using var doc = await PostAsync(data);
            return ExtractDataTable(doc);
        }

        // =================
        // 4) ABANDONO
        // =================

        public async Task<int> GetTotalPatentesEnAbandono()
        {
            using var doc = await PostAsync(new { action = "get_total_patentes_en_abandono" });
            return ReadInt(doc.RootElement, "total", 0);
        }

        public async Task<int> GetFilteredPatentesEnAbandonoCount(string value)
        {
            using var doc = await PostAsync(new
            {
                action = "get_filtered_patentes_en_abandono_count",
                filtro=value
            });
            return ReadInt(doc.RootElement, "total", 0);
        }

        public async Task<DataTable> GetAllPatentesEnAbandono(int currentPageIndex, int pageSize)
        {
            var data = new
            {
                action = "get_all_patentes_en_abandono",
                currentPageIndex,
                pageSize
            };
            using var doc = await PostAsync(data);
            return ExtractDataTable(doc);
        }

        public async Task<DataTable> FiltrarPatentesEnAbandono(string filtro, int currentPageIndex, int pageSize)
        {
            var data = new
            {
                action = "filtrar_patentes_en_abandono",
                filtro = string.IsNullOrWhiteSpace(filtro) ? null : filtro,
                currentPageIndex,
                pageSize
            };
            using var doc = await PostAsync(data);
            return ExtractDataTable(doc);
        }

        // =====================
        // 5) DESISTIMIENTO
        // =====================

        public async Task<int> GetTotalPatentesEnDesistimiento()
        {
            using var doc = await PostAsync(new { action = "get_total_patentes_en_desistimiento" });
            return ReadInt(doc.RootElement, "total", 0);
        }

        public async Task<int> GetFilteredPatentesEnDesistimientoCount(string value)
        {
            using var doc = await PostAsync(new
            {
                action = "get_filtered_patentes_en_desistimiento_count",
                filtro=value
            });
            return ReadInt(doc.RootElement, "total", 0);
        }

        public async Task<DataTable> GetAllPatentesEnDesistimiento(int currentPageIndex, int pageSize)
        {
            var data = new
            {
                action = "get_all_patentes_en_desistimiento",
                currentPageIndex,
                pageSize
            };
            using var doc = await PostAsync(data);
            return ExtractDataTable(doc);
        }

        public async Task<DataTable> FiltrarPatentesEnDesistimiento(string filtro, int currentPageIndex, int pageSize)
        {
            var data = new
            {
                action = "filtrar_patentes_en_desistimiento",
                filtro = string.IsNullOrWhiteSpace(filtro) ? null : filtro,
                currentPageIndex,
                pageSize
            };
            using var doc = await PostAsync(data);
            return ExtractDataTable(doc);
        }

        public async Task<bool> EditarPatente(
        int id,
        string caso,
        string expediente,
        string nombre,
        string estado,
        string tipo,
        int idTitular,
        int idAgente,
        DateTime fechaSolicitud,
        string registro,
        string folio,
        string libro,
        DateTime? fechaRegistro,
        DateTime? fechaVencimiento,
        string erenov,
        string etrasp,
        int anualidades,
        string pct,
        string comprobantePagos,
        string descripcion,
        string reivindicaciones,
        string dibujos,
        string resumen,
        string documentoCesion,
        string poderNombramiento)
        {
            // Helper local para serializar fechas como string o null
            static string? DateToStr(DateTime? d) =>
                d.HasValue ? d.Value.ToString("yyyy-MM-dd") : null;

            var payload = new
            {
                action = "editar_patente",
                // mismos nombres que tu PHP espera en param('...'):
                id,
                caso,
                expediente,
                nombre,
                estado,
                tipo,
                idTitular,
                idAgente,
                fechaSolicitud = DateToStr(fechaSolicitud),   // PHP lo recibe como string
                registro,
                folio,
                libro,
                fechaRegistro = DateToStr(fechaRegistro),    // puede ir null
                fechaVencimiento = DateToStr(fechaVencimiento), // puede ir null
                erenov,
                etrasp,
                anualidades,
                pct,
                comprobantePagos,
                descripcion,
                reivindicaciones,
                dibujos,
                resumen,
                documentoCesion,
                poderNombramiento
            };

            using var doc = await PostAsync(payload);

            // Se considera éxito si "ok" es true o 1
            if (doc.RootElement.TryGetProperty("ok", out var ok))
            {
                if (ok.ValueKind == System.Text.Json.JsonValueKind.True) return true;
                if (ok.ValueKind == System.Text.Json.JsonValueKind.Number && ok.GetInt32() == 1) return true;
            }

            // Si vino un error, lánzalo para depurar rápido
            if (doc.RootElement.TryGetProperty("error", out var err) && err.ValueKind == System.Text.Json.JsonValueKind.String)
                throw new Exception($"EditarPatente: {err.GetString()}");

            return false;
        }

        public async Task<DataTable> ObtenerPatentePorId(int idPatente)
        {
            if (idPatente <= 0)
                throw new ArgumentException("El idPatente debe ser mayor que cero.", nameof(idPatente));

            var data = new
            {
                action = "obtener_patente_por_id",
                idPatente
            };

            using var doc = await PostAsync(data);

            // Intenta leer el array bajo "data"
            if (doc.RootElement.TryGetProperty("data", out var arr) && arr.ValueKind == JsonValueKind.Array)
                return JsonArrayToDataTable(arr);

            // Si por alguna razón la API responde un array de raíz
            if (doc.RootElement.ValueKind == JsonValueKind.Array)
                return JsonArrayToDataTable(doc.RootElement);

            // Si vino vacío o error
            if (doc.RootElement.TryGetProperty("error", out var err) && err.ValueKind == JsonValueKind.String)
                throw new Exception($"Error al obtener patente: {err.GetString()}");

            return new DataTable();
        }

        public async Task<DataTable> FiltrarPatentesEnTramite(string? filtro, int currentPageIndex, int pageSize)
        {
            // El backend espera: action = "filtrar_patentes_sin_registro"
            // y envía filtro como null cuando no aplica (para activar p_valor IS NULL en el SP)
            var payload = new
            {
                action = "filtrar_patentes_sin_registro",
                filtro = string.IsNullOrWhiteSpace(filtro) ? null : filtro,
                currentPageIndex,
                pageSize
            };

            using var doc = await PostAsync(payload);

            // Respuesta esperada: { ok: true, data: [ ... ], count: N }
            if (doc.RootElement.TryGetProperty("data", out var arr) && arr.ValueKind == JsonValueKind.Array)
                return JsonArrayToDataTable(arr);

            // fallback si el backend responde array en la raíz
            if (doc.RootElement.ValueKind == JsonValueKind.Array)
                return JsonArrayToDataTable(doc.RootElement);

            // Manejo de error explícito
            if (doc.RootElement.TryGetProperty("error", out var err) && err.ValueKind == JsonValueKind.String)
                throw new Exception($"FiltrarPatentesEnTramite: {err.GetString()}");

            return new DataTable();
        }

        public async Task<DataTable> GetAllPatentesEnTramite(int currentPageIndex, int pageSize)
        {
            var payload = new
            {
                action = "obtener_patentes_sin_registro",
                currentPageIndex,
                pageSize
            };

            using var doc = await PostAsync(payload);

            // Lo normal: { ok: true, data: [ ... ], count: N }
            if (doc.RootElement.TryGetProperty("data", out var arr) && arr.ValueKind == JsonValueKind.Array)
                return JsonArrayToDataTable(arr);

            // Fallback si el backend devuelve un array en la raíz
            if (doc.RootElement.ValueKind == JsonValueKind.Array)
                return JsonArrayToDataTable(doc.RootElement);

            // Manejo de error explícito
            if (doc.RootElement.TryGetProperty("error", out var err) && err.ValueKind == JsonValueKind.String)
                throw new Exception($"GetAllPatentesEnTramite: {err.GetString()}");

            return new DataTable();
        }

        // Helper local (puedes moverlo a la clase si no lo tienes aún)
        private static string? DateToStr(DateTime? d) =>
            d.HasValue ? d.Value.ToString("yyyy-MM-dd") : null;


        public async Task<bool> TieneEtapaRegistradaPatente(int idPatente)
        {
            if (idPatente <= 0) throw new ArgumentException("idPatente inválido.", nameof(idPatente));

            var payload = new
            {
                action = "verificar_etapa_registrada_patente",
                idPatente
            };

            using var doc = await PostAsync(payload);

            // Esperado: { ok:true, tieneEtapaRegistrada: true/false }
            if (doc.RootElement.TryGetProperty("tieneEtapaRegistrada", out var v))
            {
                if (v.ValueKind == JsonValueKind.True) return true;
                if (v.ValueKind == JsonValueKind.False) return false;
                if (v.ValueKind == JsonValueKind.Number && v.GetInt32() == 1) return true;
            }

            if (doc.RootElement.TryGetProperty("error", out var err) && err.ValueKind == JsonValueKind.String)
                throw new Exception($"TieneEtapaRegistradaPatente: {err.GetString()}");

            return false;
        }


        public async Task<bool> InsertarTraspasoYHistorial(
        string numExpediente,
        int idPatente,
        int idTitularAnterior,
        int idTitularNuevo,
        DateTime fecha,
        string etapa,
        string anotaciones,
        string usuario,
        string usuarioEdicion)
        {
            if (string.IsNullOrWhiteSpace(numExpediente) || idPatente <= 0 || idTitularAnterior <= 0 ||
                idTitularNuevo <= 0 || fecha == default)
                throw new ArgumentException("Parámetros requeridos inválidos en InsertarTraspasoYHistorial.");

            var payload = new
            {
                action = "insertar_traspaso_y_historial_patente",
                numExpediente,
                idPatente,
                idTitularAnterior,
                idTitularNuevo,
                fecha = DateToStr(fecha),   // 'YYYY-MM-DD'
                etapa = etapa ?? string.Empty,
                anotaciones = anotaciones ?? string.Empty,
                usuario = usuario ?? string.Empty,
                usuarioEdicion = usuarioEdicion ?? string.Empty
            };

            using var doc = await PostAsync(payload);

            // Esperado éxito: { ok:true }
            if (doc.RootElement.TryGetProperty("ok", out var ok) &&
                (ok.ValueKind == JsonValueKind.True ||
                 (ok.ValueKind == JsonValueKind.Number && ok.GetInt32() == 1)))
                return true;

            if (doc.RootElement.TryGetProperty("error", out var err) && err.ValueKind == JsonValueKind.String)
                throw new Exception($"InsertarTraspasoYHistorial: {err.GetString()}");

            return false;
        }


        public async Task<bool> RenovarPatente(
        string numExpediente,
        int idPatente,
        DateTime fechaVencAnt,
        DateTime fechaVencNueva,
        DateTime fecha,
        string etapa,
        string anotaciones,
        string usuario)
        {
            if (string.IsNullOrWhiteSpace(numExpediente) || idPatente <= 0 ||
                fechaVencAnt == default || fechaVencNueva == default || fecha == default)
                throw new ArgumentException("Parámetros requeridos inválidos en RenovarPatente.");

            var payload = new
            {
                action = "renovar_patente",
                numExpediente,
                idPatente,
                fechaVencAnt = DateToStr(fechaVencAnt),
                fechaVencNueva = DateToStr(fechaVencNueva),
                fecha = DateToStr(fecha),
                etapa = etapa ?? string.Empty,
                anotaciones = anotaciones ?? string.Empty,
                usuario = usuario ?? string.Empty
            };

            using var doc = await PostAsync(payload);

            // Esperado: { ok:true/false, message:"..." }
            if (doc.RootElement.TryGetProperty("ok", out var ok))
            {
                if (ok.ValueKind == JsonValueKind.True) return true;
                if (ok.ValueKind == JsonValueKind.False) return false;
                if (ok.ValueKind == JsonValueKind.Number) return ok.GetInt32() == 1;
            }

            if (doc.RootElement.TryGetProperty("error", out var err) && err.ValueKind == JsonValueKind.String)
                throw new Exception($"RenovarPatente: {err.GetString()}");

            return false;
        }

        public async Task<bool> InsertarExpedientePatente(string numExpediente, int idMarca, string tipo)
        {
            if (string.IsNullOrWhiteSpace(numExpediente) || idMarca <= 0 || string.IsNullOrWhiteSpace(tipo))
                throw new ArgumentException("Parámetros requeridos inválidos en InsertarExpedientePatente.");

            var payload = new
            {
                action = "insertar_expediente_patente",
                numExpediente,
                idMarca,
                tipo
            };

            using var doc = await PostAsync(payload);

            // Esperado éxito: { ok:true }
            if (doc.RootElement.TryGetProperty("ok", out var ok) &&
                (ok.ValueKind == JsonValueKind.True ||
                 (ok.ValueKind == JsonValueKind.Number && ok.GetInt32() == 1)))
                return true;

            if (doc.RootElement.TryGetProperty("error", out var err) && err.ValueKind == JsonValueKind.String)
                throw new Exception($"InsertarExpedientePatente: {err.GetString()}");

            return false;
        }


        public async Task<bool> ActualizarExpedientePatente(
            int id,
            string? expediente,
            DateTime? fecha,
            string? estado,
            string? anotaciones,
            string? usuario)
        {
            if (id <= 0) throw new ArgumentException("id inválido.", nameof(id));

            static string? DateToStr(DateTime? d) => d.HasValue ? d.Value.ToString("yyyy-MM-dd") : null;

            var payload = new
            {
                action = "actualizar_expediente_patente",
                id,
                expediente = string.IsNullOrWhiteSpace(expediente) ? null : expediente,
                fecha = DateToStr(fecha), // PHP acepta null
                estado = string.IsNullOrWhiteSpace(estado) ? null : estado,
                anotaciones = string.IsNullOrWhiteSpace(anotaciones) ? null : anotaciones,
                usuario = string.IsNullOrWhiteSpace(usuario) ? null : usuario
            };

            using var doc = await PostAsync(payload);

            // ok: true/1 => éxito
            if (doc.RootElement.TryGetProperty("ok", out var ok) &&
                (ok.ValueKind == JsonValueKind.True ||
                 (ok.ValueKind == JsonValueKind.Number && ok.GetInt32() == 1)))
                return true;

            if (doc.RootElement.TryGetProperty("error", out var err) && err.ValueKind == JsonValueKind.String)
                throw new Exception($"ActualizarExpedientePatente: {err.GetString()}");

            return false;
        }


        public async Task<int> InsertarPatente(
            string caso, string expediente, string nombre, string estado, string tipo,
            int idTitular, int idAgente, DateTime fechaSolicitud,
            string registro, string folio, string libro,
            DateTime? fechaRegistro, DateTime? fechaVencimiento,
            string erenov, string etrasp, int anualidades,
            string pct, string comprobantePagos, string descripcion,
            string reivindicaciones, string dibujos, string resumen,
            string documentoCesion, string poderNombramiento)
        {
            static string? DateToStr(DateTime? d) => d.HasValue ? d.Value.ToString("yyyy-MM-dd") : null;

            var payload = new
            {
                action = "insertar_patente",
                caso,
                expediente,
                nombre,
                estado,
                tipo,
                idTitular,
                idAgente,
                fechaSolicitud = fechaSolicitud.ToString("yyyy-MM-dd"),
                registro,
                folio,
                libro,
                fechaRegistro = DateToStr(fechaRegistro),     // puede ir null
                fechaVencimiento = DateToStr(fechaVencimiento),  // puede ir null
                erenov,
                etrasp,
                anualidades,
                pct,
                comprobantePagos,
                descripcion,
                reivindicaciones,
                dibujos,
                resumen,
                documentoCesion,
                poderNombramiento
            };

            using var doc = await PostAsync(payload);

            // Espera: { ok:true, idPatente: <int> }
            if (doc.RootElement.TryGetProperty("idPatente", out var idProp))
            {
                if (idProp.ValueKind == JsonValueKind.Number && idProp.TryGetInt32(out var idVal) && idVal > 0)
                    return idVal;

                if (idProp.ValueKind == JsonValueKind.String && int.TryParse(idProp.GetString(), out var idStr) && idStr > 0)
                    return idStr;
            }

            if (doc.RootElement.TryGetProperty("error", out var err) && err.ValueKind == JsonValueKind.String)
                throw new Exception($"InsertarPatente: {err.GetString()}");

            throw new Exception("InsertarPatente: respuesta sin idPatente válido.");
        }


        public async Task<int> GetTotalPatentesSinRegistro()
        {
            using var doc = await PostAsync(new { action = "get_total_patentes_sin_registro" });

            // Espera: { ok:true, total:<int> }
            if (doc.RootElement.TryGetProperty("total", out var t))
            {
                if (t.ValueKind == JsonValueKind.Number && t.TryGetInt32(out var n)) return n;
                if (t.ValueKind == JsonValueKind.String && int.TryParse(t.GetString(), out var m)) return m;
            }
            return 0;
        }

        public async Task<int> GetFilteredPatentesSinRegistroCount(string value)
        {
            var payload = new
            {
                action = "get_filtered_patentes_sin_registro_count",
                value = value ?? string.Empty
            };

            using var doc = await PostAsync(payload);

            // Espera: { ok:true, total:<int> }
            if (doc.RootElement.TryGetProperty("total", out var t))
            {
                if (t.ValueKind == JsonValueKind.Number && t.TryGetInt32(out var n)) return n;
                if (t.ValueKind == JsonValueKind.String && int.TryParse(t.GetString(), out var m)) return m;
            }
            return 0;
        }



    }
}
