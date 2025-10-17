using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class PersonaDao
    {
        private readonly string urlApi = "https://foragro.com.es/peticiones/personas.php";
        private static readonly JsonSerializerOptions JsonOpts = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        /* ================= Helpers base ================= */

        private async Task<JsonDocument> PostAsync(object data)
        {
            using var client = new HttpClient();
            string json = JsonSerializer.Serialize(data);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = await client.PostAsync(urlApi, content);
            resp.EnsureSuccessStatusCode();

            string body = await resp.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(body))
                return JsonDocument.Parse("{}");

            return JsonDocument.Parse(body);
        }

        private static DataTable JsonArrayToDataTable(JsonElement root)
        {
            var table = new DataTable();

            if (root.ValueKind != JsonValueKind.Array)
                return table;

            foreach (var el in root.EnumerateArray())
            {
                if (table.Columns.Count == 0)
                {
                    foreach (var prop in el.EnumerateObject())
                    {
                        if (!table.Columns.Contains(prop.Name))
                            table.Columns.Add(prop.Name);
                    }
                }

                var row = table.NewRow();
                foreach (var prop in el.EnumerateObject())
                    row[prop.Name] = prop.Value.ValueKind switch
                    {
                        JsonValueKind.String => prop.Value.GetString(),
                        JsonValueKind.Number => prop.Value.GetRawText(),
                        JsonValueKind.True => true,
                        JsonValueKind.False => false,
                        JsonValueKind.Null => DBNull.Value,
                        _ => prop.Value.ToString()
                    };
                table.Rows.Add(row);
            }

            return table;
        }

        private static (int id, string nombre, string direccion, string nit, string pais, string correo, string telefono, string contacto)? ParsePersona(JsonElement obj)
        {
            if (obj.ValueKind != JsonValueKind.Object) return null;

            int id = obj.TryGetProperty("id", out var idEl) && idEl.TryGetInt32(out var vi) ? vi : 0;
            string nombre = obj.TryGetProperty("nombre", out var p1) ? p1.GetString() : null;
            string direccion = obj.TryGetProperty("direccion", out var p2) ? p2.GetString() : null;
            string nit = obj.TryGetProperty("nit", out var p3) ? p3.GetString() : null;
            string pais = obj.TryGetProperty("pais", out var p4) ? p4.GetString() : null;
            string correo = obj.TryGetProperty("correo", out var p5) ? p5.GetString() : null;
            string telefono = obj.TryGetProperty("telefono", out var p6) ? p6.GetString() : null;
            string contacto = obj.TryGetProperty("nombre_contacto", out var p7) ? p7.GetString()
                               : (obj.TryGetProperty("contacto", out var p8) ? p8.GetString() : null);

            return (id, nombre, direccion, nit, pais, correo, telefono, contacto);
        }

        /* ================= Acciones ================= */

        // AddPersona
        public async Task<bool> AddPersona(string nombre, string direccion, string nit, string pais, string correo, string telefono, string nombreContacto, string tipo)
        {
            var data = new
            {
                action = "add_persona",
                nombre,
                direccion,
                nit,
                pais,
                correo,
                telefono,
                nombreContacto,
                tipo
            };

            using var doc = await PostAsync(data);
            return doc.RootElement.TryGetProperty("success", out var ok) && ok.GetBoolean();
        }

        // UpdatePersona
        public async Task<bool> UpdatePersona(int id, string nombre, string direccion, string nit, string pais, string correo, string telefono, string nombreContacto)
        {
            var data = new
            {
                action = "update_persona",
                id,
                nombre,
                direccion,
                nit,
                pais,
                correo,
                telefono,
                nombreContacto
            };

            using var doc = await PostAsync(data);
            return doc.RootElement.TryGetProperty("success", out var ok) && ok.GetBoolean();
        }

        // GetTitularByValue
        public async Task<DataTable> GetTitularByValue(string value, int currentPageIndex, int pageSize)
        {
            var data = new
            {
                action = "get_titular_by_value",
                searchValue = value,
                currentPageIndex,
                pageSize
            };
            using var doc = await PostAsync(data);

            var root = doc.RootElement;
            return root.ValueKind == JsonValueKind.Array
                ? JsonArrayToDataTable(root)
                : (root.TryGetProperty("registros", out var arr) ? JsonArrayToDataTable(arr) : new DataTable());
        }

        // GetAgenteByValue
        public async Task<DataTable> GetAgenteByValue(string value, int currentPageIndex, int pageSize)
        {
            var data = new
            {
                action = "get_agente_by_value",
                searchValue = value,
                currentPageIndex,
                pageSize
            };
            using var doc = await PostAsync(data);

            var root = doc.RootElement;
            return root.ValueKind == JsonValueKind.Array
                ? JsonArrayToDataTable(root)
                : (root.TryGetProperty("registros", out var arr) ? JsonArrayToDataTable(arr) : new DataTable());
        }

        // GetClienteByValue
        public async Task<DataTable> GetClienteByValue(string value, int currentPageIndex, int pageSize)
        {
            var data = new
            {
                action = "get_cliente_by_value",
                searchValue = value,
                currentPageIndex,
                pageSize
            };
            using var doc = await PostAsync(data);

            var root = doc.RootElement;
            return root.ValueKind == JsonValueKind.Array
                ? JsonArrayToDataTable(root)
                : (root.TryGetProperty("registros", out var arr) ? JsonArrayToDataTable(arr) : new DataTable());
        }

        // GetById
        public async Task<List<(int id, string nombre, string direccion, string nit, string pais, string correo, string telefono, string contacto)>> GetById(int id)
        {
            var data = new { action = "get_persona_by_id", id };
            using var doc = await PostAsync(data);

            var list = new List<(int, string, string, string, string, string, string, string)>();
            var parsed = ParsePersona(doc.RootElement);
            if (parsed.HasValue) list.Add(parsed.Value);
            return list;
        }

        // GetTotalTitulares
        public async Task<int> GetTotalTitulares()
        {
            using var doc = await PostAsync(new { action = "get_total_titulares" });
            return doc.RootElement.TryGetProperty("total", out var t) && t.TryGetInt32(out var vi) ? vi : 0;
        }

        // GetTotalAgentes
        public async Task<int> GetTotalAgentes()
        {
            using var doc = await PostAsync(new { action = "get_total_agentes" });
            return doc.RootElement.TryGetProperty("total", out var t) && t.TryGetInt32(out var vi) ? vi : 0;
        }

        // GetTotalClientes
        public async Task<int> GetTotalClientes()
        {
            using var doc = await PostAsync(new { action = "get_total_clientes" });
            return doc.RootElement.TryGetProperty("total", out var t) && t.TryGetInt32(out var vi) ? vi : 0;
        }

        // GetFilteredTitularesCount
        public async Task<int> GetFilteredTitularesCount(string value)
        {
            using var doc = await PostAsync(new { action = "get_filtered_titulares_count", value });
            return doc.RootElement.TryGetProperty("total", out var t) && t.TryGetInt32(out var vi) ? vi : 0;
        }

        // GetFilteredAgentesCount
        public async Task<int> GetFilteredAgentesCount(string value)
        {
            using var doc = await PostAsync(new { action = "get_filtered_agentes_count", value });
            return doc.RootElement.TryGetProperty("total", out var t) && t.TryGetInt32(out var vi) ? vi : 0;
        }

        // GetFilteredClientesCount
        public async Task<int> GetFilteredClientesCount(string value)
        {
            using var doc = await PostAsync(new { action = "get_filtered_clientes_count", value });
            return doc.RootElement.TryGetProperty("total", out var t) && t.TryGetInt32(out var vi) ? vi : 0;
        }

        // GetAllTitulares
        public async Task<DataTable> GetAllTitulares(int currentPageIndex, int pageSize)
        {
            var data = new
            {
                action = "get_all_titulares",
                currentPageIndex,
                pageSize
            };
            using var doc = await PostAsync(data);

            var root = doc.RootElement;
            return root.ValueKind == JsonValueKind.Array
                ? JsonArrayToDataTable(root)
                : (root.TryGetProperty("registros", out var arr) ? JsonArrayToDataTable(arr) : new DataTable());
        }

        // GetAllAgentes
        public async Task<DataTable> GetAllAgentes(int currentPageIndex, int pageSize)
        {
            var data = new
            {
                action = "get_all_agentes",
                currentPageIndex,
                pageSize
            };
            using var doc = await PostAsync(data);

            var root = doc.RootElement;
            return root.ValueKind == JsonValueKind.Array
                ? JsonArrayToDataTable(root)
                : (root.TryGetProperty("registros", out var arr) ? JsonArrayToDataTable(arr) : new DataTable());
        }

        // GetAllClientes
        public async Task<DataTable> GetAllClientes(int currentPageIndex, int pageSize)
        {
            var data = new
            {
                action = "get_all_clientes",
                currentPageIndex,
                pageSize
            };
            using var doc = await PostAsync(data);

            var root = doc.RootElement;
            return root.ValueKind == JsonValueKind.Array
                ? JsonArrayToDataTable(root)
                : (root.TryGetProperty("registros", out var arr) ? JsonArrayToDataTable(arr) : new DataTable());
        }

        // RemoveTitular
        public async Task<bool> RemoveTitular(int personaId, string deletedUser, string deletedBy)
        {
            var data = new
            {
                action = "remove_persona",
                personaId,
                deletedUser,
                deletedBy,
                tipo = "titular"
            };
            using var doc = await PostAsync(data);

            // success true/false y puede venir error
            if (doc.RootElement.TryGetProperty("success", out var ok))
                return ok.ValueKind == JsonValueKind.True;

            return false;
        }

        // RemoveAgente
        public async Task<bool> RemoveAgente(int personaId, string deletedUser, string deletedBy)
        {
            var data = new
            {
                action = "remove_persona",
                personaId,
                deletedUser,
                deletedBy,
                tipo = "agente"
            };
            using var doc = await PostAsync(data);
            if (doc.RootElement.TryGetProperty("success", out var ok))
                return ok.ValueKind == JsonValueKind.True;

            return false;
        }

        // RemoveCliente
        public async Task<bool> RemoveCliente(int personaId, string deletedUser, string deletedBy)
        {
            var data = new
            {
                action = "remove_persona",
                personaId,
                deletedUser,
                deletedBy,
                tipo = "cliente"
            };
            using var doc = await PostAsync(data);
            if (doc.RootElement.TryGetProperty("success", out var ok))
                return ok.ValueKind == JsonValueKind.True;

            return false;
        }
    }
}

/*
using MySql.Data.MySqlClient;
using System.Data;

namespace AccesoDatos.Entidades
{
    public class PersonaDao:ConnectionSQL
    {
        public bool AddPersona(string nombre, string direccion, string nit, string pais, string correo, string telefono, string nombreContacto, string tipo)
        {
            using (var connection = GetConnection()) 
            {
                connection.Open();
                using (var command = new MySqlCommand("INSERT INTO Personas (nombre, direccion, nit, pais, correo, telefono, nombre_contacto, tipo) VALUES (@nombre, @direccion, @nit, @pais, @correo, @telefono, @nombreContacto, @tipo)", connection))
                {
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@direccion", direccion);
                    command.Parameters.AddWithValue("@nit", nit);
                    command.Parameters.AddWithValue("@pais", pais);
                    command.Parameters.AddWithValue("@correo", correo);
                    command.Parameters.AddWithValue("@telefono", telefono);
                    command.Parameters.AddWithValue("@nombreContacto", nombreContacto);
                    command.Parameters.AddWithValue("@tipo", tipo);  

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }

        public bool UpdatePersona(int id, string nombre, string direccion, string nit, string pais, string correo, string telefono, string nombreContacto)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("UPDATE Personas SET nombre=@nombre, direccion=@direccion, nit=@nit, pais=@pais, correo=@correo, telefono=@telefono, nombre_contacto=@nombreContacto WHERE id=@id;", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@direccion", direccion);
                    command.Parameters.AddWithValue("@nit", nit);
                    command.Parameters.AddWithValue("@pais", pais);
                    command.Parameters.AddWithValue("@correo", correo);
                    command.Parameters.AddWithValue("@telefono", telefono);
                    command.Parameters.AddWithValue("@nombreContacto", nombreContacto);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }



        public DataTable GetTitularByValue(string value, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetTitularByValue", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    int registrosOmitidos = (currentPageIndex - 1) * pageSize;
                    
                    comando.Parameters.AddWithValue("pageSize", pageSize);
                    comando.Parameters.AddWithValue("registrosOmitidos", registrosOmitidos);
                    comando.Parameters.AddWithValue("@searchValue", value);

                    conexion.Open();
                    using (MySqlDataReader leer = comando.ExecuteReader())
                    {
                        tabla.Load(leer);
                    }
                }
            }
            return tabla;
        }

        //agentes
        public DataTable GetAgenteByValue(string value, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetAgenteByValue", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    int registrosOmitidos = (currentPageIndex - 1) * pageSize;

                    comando.Parameters.AddWithValue("pageSize", pageSize);
                    comando.Parameters.AddWithValue("registrosOmitidos", registrosOmitidos);
                    comando.Parameters.AddWithValue("@searchValue", value);

                    conexion.Open();
                    using (MySqlDataReader leer = comando.ExecuteReader())
                    {
                        tabla.Load(leer);
                    }
                }
            }
            return tabla;
        }
        //clientes
        public DataTable GetClienteByValue(string value, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetClienteByValue", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    int registrosOmitidos = (currentPageIndex - 1) * pageSize;

                    comando.Parameters.AddWithValue("pageSize", pageSize);
                    comando.Parameters.AddWithValue("registrosOmitidos", registrosOmitidos);
                    comando.Parameters.AddWithValue("@searchValue", value);

                    conexion.Open();
                    using (MySqlDataReader leer = comando.ExecuteReader())
                    {
                        tabla.Load(leer);
                    }
                }
            }
            return tabla;
        }

        public List<(int id, string nombre, string direccion, string nit, string pais, string correo, string telefono, string contacto)> GetById(int id)
        {
            var persona = new List<(int id, string nombre, string direccion, string nit, string pais, string correo, string telefono, string contacto)>();

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("SELECT * FROM Personas WHERE id=@id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read()) 
                        {
                            persona.Add((
                                reader.GetInt32("id"),
                                reader.GetString("nombre"),
                                reader.GetString("direccion"),
                                reader.GetString("nit"),
                                reader.GetString("pais"),
                                reader.GetString("correo"),
                                reader.GetString("telefono"),
                                reader.GetString("nombre_contacto")
                            ));
                        }
                    }
                }
            }
            return persona; 
        }

        public int GetTotalTitulares()
        {
            int totalTitulares = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetTotalTitulares", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    MySqlParameter paramTotalUsuarios = new MySqlParameter("totalTitulares", MySqlDbType.Int32)
                    {
                        Direction = ParameterDirection.Output
                    };
                    comando.Parameters.Add(paramTotalUsuarios);

                    conexion.Open();
                    comando.ExecuteNonQuery();  // Ejecutar el procedimiento almacenado

                    // Obtener el valor de totalUsuarios desde el parámetro de salida
                    totalTitulares = Convert.ToInt32(paramTotalUsuarios.Value);
                }
            }

            return totalTitulares;
        }

        public int GetTotalAgentes()
        {
            int totalAgentes = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetTotalAgentes", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    MySqlParameter paramTotalAgentes= new MySqlParameter("totalAgentes", MySqlDbType.Int32)
                    {
                        Direction = ParameterDirection.Output
                    };
                    comando.Parameters.Add(paramTotalAgentes);

                    conexion.Open();
                    comando.ExecuteNonQuery();  // Ejecutar el procedimiento almacenado

                    // Obtener el valor de totalUsuarios desde el parámetro de salida
                    totalAgentes = Convert.ToInt32(paramTotalAgentes.Value);
                }
            }

            return totalAgentes;
        }
        public int GetTotalClientes()
        {
            int totalClientes = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetTotalClientes", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    MySqlParameter paramTotalClientes= new MySqlParameter("totalClientes", MySqlDbType.Int32)
                    {
                        Direction = ParameterDirection.Output
                    };
                    comando.Parameters.Add(paramTotalClientes);

                    conexion.Open();
                    comando.ExecuteNonQuery();  // Ejecutar el procedimiento almacenado

                    // Obtener el valor de totalUsuarios desde el parámetro de salida
                    totalClientes= Convert.ToInt32(paramTotalClientes.Value);
                }
            }

            return totalClientes;
        }

        public int GetFilteredTitularesCount(string value)
        {
            int totalTitulares = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetFilteredTitularesCount", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    // Parámetro de entrada
                    comando.Parameters.AddWithValue("@value", value);

                    // Parámetro de salida
                    MySqlParameter totalTitularesParam = new MySqlParameter("@totalTitulares", MySqlDbType.Int32);
                    totalTitularesParam.Direction = ParameterDirection.Output;
                    comando.Parameters.Add(totalTitularesParam);

                    conexion.Open();

                    comando.ExecuteNonQuery();

                    totalTitulares = Convert.ToInt32(totalTitularesParam.Value);
                }
            }

            return totalTitulares;
        }

        public int GetFilteredAgentesCount(string value)
        {
            int totalAgentes = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetFilteredAgentesCount", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue("@value", value);

                    MySqlParameter totalAgentesParam = new MySqlParameter("@totalAgentes", MySqlDbType.Int32);
                    totalAgentesParam.Direction = ParameterDirection.Output;
                    comando.Parameters.Add(totalAgentesParam);

                    conexion.Open();

                    comando.ExecuteNonQuery();

                    totalAgentes = Convert.ToInt32(totalAgentesParam.Value);
                }
            }

            return totalAgentes;
        }
        public int GetFilteredClientesCount(string value)
        {
            int totalClientes = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetFilteredClientesCount", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue("@value", value);

                    MySqlParameter totalClientesParam = new MySqlParameter("@totalClientes", MySqlDbType.Int32);
                    totalClientesParam.Direction = ParameterDirection.Output;
                    comando.Parameters.Add(totalClientesParam);

                    conexion.Open();

                    comando.ExecuteNonQuery();

                    totalClientes = Convert.ToInt32(totalClientesParam.Value);
                }
            }

            return totalClientes;
        }
        public DataTable GetAllTitulares(int currentPageIndex, int pageSize)
        {
            int registrosOmitidos = (currentPageIndex - 1) * pageSize;

            DataTable tabla = new DataTable();
            using (MySqlConnection conexion = GetConnection()) 
            {
                using (MySqlCommand comando = new MySqlCommand("SELECT id, nombre as NOMBRE, direccion as DIRECCION, nit as NIT, pais as PAIS, correo as CORREO, telefono as TELEFONO, nombre_contacto as CONTACTO FROM Personas WHERE tipo='titular'" +
                    "ORDER BY id DESC " +
                    $"LIMIT {pageSize} OFFSET {registrosOmitidos};", conexion)) // Inicializa correctamente el comando
                {
                    conexion.Open();
                    using (MySqlDataReader leer = comando.ExecuteReader())
                    {
                        tabla.Load(leer);
                    }
                }
            }
            return tabla;
        }

        public DataTable GetAllAgentes(int currentPageIndex, int pageSize)
        {
            int registrosOmitidos = (currentPageIndex - 1) * pageSize;
            DataTable tabla = new DataTable();
            using (MySqlConnection conexion = GetConnection()) 
            {
                using (MySqlCommand comando = new MySqlCommand($"SELECT id, nombre as NOMBRE, direccion as DIRECCION, nit as NIT, pais as PAIS, correo as CORREO, telefono as TELEFONO, nombre_contacto as CONTACTO FROM Personas WHERE tipo='agente' ORDER BY id DESC LIMIT {pageSize} OFFSET {registrosOmitidos};", conexion)) // Inicializa correctamente el comando
                {
                    conexion.Open();
                    using (MySqlDataReader leer = comando.ExecuteReader()) 
                    {
                        tabla.Load(leer);
                    }
                }
            }
            return tabla;

        }

        public DataTable GetAllClientes(int currentPageIndex, int pageSize)
        {
            int registrosOmitidos = (currentPageIndex - 1) * pageSize;
            DataTable tabla = new DataTable();
            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand($"SELECT id, nombre as NOMBRE, direccion as DIRECCION, nit as NIT, pais as PAIS, correo as CORREO, telefono as TELEFONO, nombre_contacto as CONTACTO FROM Personas WHERE tipo='cliente' ORDER BY id DESC LIMIT {pageSize} OFFSET {registrosOmitidos};", conexion)) // Inicializa correctamente el comando
                {
                    conexion.Open();
                    using (MySqlDataReader leer = comando.ExecuteReader())
                    {
                        tabla.Load(leer);
                    }
                }
            }
            return tabla;

        }

        public bool RemoveTitular(int personaId, string deletedUser, string deletedBy)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var logCommand = new MySqlCommand("INSERT INTO PersonaDeletionLog (persona, tipo, deleted_by) VALUES (@persona, 'titular', @deletedBy)", connection, transaction))
                        {
                            logCommand.Parameters.AddWithValue("@persona", deletedUser);
                            logCommand.Parameters.AddWithValue("@deletedBy", deletedBy);
                            logCommand.ExecuteNonQuery();
                        }

                        using (var deleteCommand = new MySqlCommand("EliminarPersonaSiNoTieneAsociaciones", connection, transaction))
                        {
                            deleteCommand.CommandType = CommandType.StoredProcedure;
                            deleteCommand.Parameters.AddWithValue("@p_persona_id", personaId);

                            deleteCommand.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch (MySqlException ex)
                    {
                        transaction.Rollback();

                        // Detecta error por SIGNAL SQLSTATE
                        if (ex.Number == 1644) // SIGNAL SQLSTATE '45000'
                        {
                            throw new Exception("No se puede eliminar la persona: " + ex.Message);
                        }

                        throw new Exception("Error general al eliminar la persona: " + ex.Message);
                    }
                }
            }
        }


        public bool RemoveAgente(int personaId, string deletedUser, string deletedBy)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var logCommand = new MySqlCommand("INSERT INTO PersonaDeletionLog (persona, tipo, deleted_by) VALUES (@persona, 'agente', @deletedBy)", connection, transaction))
                        {
                            logCommand.Parameters.AddWithValue("@persona", deletedUser);
                            logCommand.Parameters.AddWithValue("@deletedBy", deletedBy);
                            logCommand.ExecuteNonQuery();
                        }

                        using (var deleteCommand = new MySqlCommand("EliminarPersonaSiNoTieneAsociaciones", connection, transaction))
                        {
                            deleteCommand.CommandType = CommandType.StoredProcedure;
                            deleteCommand.Parameters.AddWithValue("@p_persona_id", personaId);

                            deleteCommand.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch (MySqlException ex)
                    {
                        transaction.Rollback();

                        // Detecta error por SIGNAL SQLSTATE
                        if (ex.Number == 1644) // SIGNAL SQLSTATE '45000'
                        {
                            throw new Exception("No se puede eliminar la persona: " + ex.Message);
                        }

                        throw new Exception("Error general al eliminar la persona: " + ex.Message);
                    }
                }
            }
        }

        public bool RemoveCliente(int personaId, string deletedUser, string deletedBy)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var logCommand = new MySqlCommand("INSERT INTO PersonaDeletionLog (persona, tipo, deleted_by) VALUES (@persona, 'cliente', @deletedBy)", connection, transaction))
                        {
                            logCommand.Parameters.AddWithValue("@persona", deletedUser);
                            logCommand.Parameters.AddWithValue("@deletedBy", deletedBy);
                            logCommand.ExecuteNonQuery();
                        }

                        using (var deleteCommand = new MySqlCommand("EliminarPersonaSiNoTieneAsociaciones", connection, transaction))
                        {
                            deleteCommand.CommandType = CommandType.StoredProcedure;
                            deleteCommand.Parameters.AddWithValue("@p_persona_id", personaId);

                            deleteCommand.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch (MySqlException ex)
                    {
                        transaction.Rollback();

                        // Detecta error por SIGNAL SQLSTATE
                        if (ex.Number == 1644) // SIGNAL SQLSTATE '45000'
                        {
                            throw new Exception("No se puede eliminar la persona: " + ex.Message);
                        }

                        throw new Exception("Error general al eliminar la persona: " + ex.Message);
                    }
                }
            }
        }

    }
}*/
