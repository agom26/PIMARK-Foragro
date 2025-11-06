using Comun.Cache;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AccesoDatos.Usuarios
{
    public class UserDao
    {
        private readonly string urlApi = "https://foragro.com.es/peticiones/users.php";

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
            }

            return table;
        }

        private static JsonElement GetArrayOrEmpty(JsonElement root, string field = "rows")
        {
            if (root.ValueKind == JsonValueKind.Array) return root; // a veces el endpoint devuelve array directo
            if (root.TryGetProperty(field, out var arr) && arr.ValueKind == JsonValueKind.Array) return arr;
            // fallback: algunos endpoints devuelven {count, rows}, otros directamente array
            return default;
        }

        /* ================= DTOs útiles ================= */

        public class LoginResult
        {
            public bool Ok { get; set; }
            public bool IsAdmin { get; set; }
            public int Id { get; set; }
            public string Usuario { get; set; }
            public string Nombres { get; set; }
            public string Apellidos { get; set; }
            public string Correo { get; set; }
            public bool SoloLectura { get; set; }
        }

        /* ================= Acciones (mirror de users.php) ================= */

        // add_user
        public async Task<bool> AddUser(string usuario, string contrasena, string nombres, string apellidos, bool isAdmin, string correo, bool soloLectura)
        {
            var data = new
            {
                action = "add_user",
                usuario,
                contrasena,
                nombres,
                apellidos,
                isAdmin,
                correo,
                soloLectura
            };

            using var doc = await PostAsync(data);
            return doc.RootElement.TryGetProperty("success", out var ok) && ok.ValueKind == JsonValueKind.True;
        }

        // update_user (contraseña opcional si cambiarContrasena=false)
        public async Task<bool> UpdateUser(int id, string usuario, string contrasena, string nombres, string apellidos, bool isAdmin, string correo, bool cambiarContrasena, bool soloLectura)
        {
            var data = new
            {
                action = "update_user",
                id,
                usuario,
                contrasena,         // se ignora en PHP si cambiarContrasena = false
                nombres,
                apellidos,
                isAdmin,
                correo,
                cambiarContrasena,
                soloLectura
            };

            using var doc = await PostAsync(data);
            return doc.RootElement.TryGetProperty("success", out var ok) && ok.ValueKind == JsonValueKind.True;
        }

        // get_total_usuarios
        public async Task<int> GetTotalUsuarios()
        {
            using var doc = await PostAsync(new { action = "get_total_usuarios" });
            return doc.RootElement.TryGetProperty("totalUsuarios", out var t) && t.TryGetInt32(out var vi) ? vi : 0;
        }

        // get_all_users (devuelve {rows: [...], count: N})
        public async Task<DataTable> GetAllUsers(int currentPageIndex, int pageSize)
        {
            var data = new
            {
                action = "get_all_users",
                currentPageIndex,
                pageSize
            };

            using var doc = await PostAsync(data);
            var arr = GetArrayOrEmpty(doc.RootElement, "rows");
            return arr.ValueKind == JsonValueKind.Array ? JsonArrayToDataTable(arr) : new DataTable();
        }

        // remove_user (log + delete)
        public async Task<bool> RemoveUser(int userId, string deletedUser, string deletedBy)
        {
            var data = new
            {
                action = "remove_user",
                userId,
                deletedUser,
                deletedBy
            };

            using var doc = await PostAsync(data);
            return doc.RootElement.TryGetProperty("success", out var ok) && ok.ValueKind == JsonValueKind.True;
        }

        // get_filtered_user_count
        public async Task<int> GetFilteredUserCount(string value)
        {
            using var doc = await PostAsync(new { action = "get_filtered_user_count", value });
            return doc.RootElement.TryGetProperty("totalUsuarios", out var t) && t.TryGetInt32(out var vi) ? vi : 0;
        }

        // get_user_by_value (devuelve {rows: [...], count: N})
        public async Task<DataTable> GetUserByValue(string value, int currentPageIndex, int pageSize)
        {
            var data = new
            {
                action = "get_user_by_value",
                value,
                currentPageIndex,
                pageSize
            };

            using var doc = await PostAsync(data);
            var arr = GetArrayOrEmpty(doc.RootElement, "rows");
            return arr.ValueKind == JsonValueKind.Array ? JsonArrayToDataTable(arr) : new DataTable();
        }

        // get_user_by_id (devuelve un objeto)
        public async Task<DataTable> GetUserById(int userId)
        {
            var data = new { action = "get_user_by_id", userId };
            using var doc = await PostAsync(data);

            // Normalizamos a DataTable de una fila
            var table = new DataTable();
            var obj = doc.RootElement;
            if (obj.ValueKind != JsonValueKind.Object) return table;

            foreach (var p in obj.EnumerateObject())
                if (!table.Columns.Contains(p.Name)) table.Columns.Add(p.Name);

            var row = table.NewRow();
            foreach (var p in obj.EnumerateObject())
            {
                row[p.Name] = p.Value.ValueKind switch
                {
                    JsonValueKind.String => p.Value.GetString(),
                    JsonValueKind.Number => p.Value.GetRawText(),
                    JsonValueKind.True => true,
                    JsonValueKind.False => false,
                    JsonValueKind.Null => DBNull.Value,
                    _ => p.Value.ToString()
                };
            }
            table.Rows.Add(row);
            return table;
        }

        // login (devuelve {ok, isAdmin, user{...}} o {ok:false})
        public async Task<(bool ,bool )> Login(string user, string pass)
        {
            var data = new { action = "login", user, pass };
            using var doc = await PostAsync(data);
            var root = doc.RootElement;

            var result = new LoginResult
            {
                Ok = root.TryGetProperty("ok", out var ok) && ok.ValueKind == JsonValueKind.True,
                IsAdmin = root.TryGetProperty("isAdmin", out var adm) && adm.ValueKind == JsonValueKind.True
            };

            if (result.Ok && root.TryGetProperty("user", out var u) && u.ValueKind == JsonValueKind.Object)
            {
                result.Id = u.TryGetProperty("id", out var idEl) && idEl.TryGetInt32(out var vi) ? vi : 0;
                result.Usuario = u.TryGetProperty("usuario", out var p1) ? p1.GetString() : null;
                result.Nombres = u.TryGetProperty("nombres", out var p2) ? p2.GetString() : null;
                result.Apellidos = u.TryGetProperty("apellidos", out var p3) ? p3.GetString() : null;
                result.Correo = u.TryGetProperty("correo", out var p4) ? p4.GetString() : null;
                result.SoloLectura = u.TryGetProperty("solo_lectura", out var p5) && p5.ValueKind == JsonValueKind.True;
                result.IsAdmin = u.TryGetProperty("admin", out var p6) && p6.ValueKind == JsonValueKind.True;
                // Si coincide, rellenar usuario activo
                UsuarioActivo.isAdmin = result.IsAdmin;
                UsuarioActivo.idUser = result.Id;
                UsuarioActivo.usuario = result.Usuario;
                UsuarioActivo.nombres = result.Nombres;
                UsuarioActivo.apellidos = result.Apellidos;
                UsuarioActivo.correo = result.Correo;
                UsuarioActivo.soloLectura = result.SoloLectura;
            }

            return (result.Ok, result.IsAdmin);
        }

        // contar_administradores
        public async Task<int> ContarAdministradores()
        {
            using var doc = await PostAsync(new { action = "contar_administradores" });
            return doc.RootElement.TryGetProperty("TotalAdministradores", out var t) && t.TryGetInt32(out var vi) ? vi : 0;
        }

        // probar_conexion
        public async Task<bool> ProbarConexion()
        {
            using var doc = await PostAsync(new { action = "probar_conexion" });
            return doc.RootElement.TryGetProperty("ok", out var ok) && ok.ValueKind == JsonValueKind.True;
        }
    }
}

/*
using MySql.Data.MySqlClient;
using Comun.Cache;
using System.Data;

namespace AccesoDatos.Usuarios
{
    public class UserDao : ConnectionSQL
    {
        
        public bool AddUser(string usuario, string contrasena, string nombres, string apellidos, bool isAdmin, string correo, bool soloLectura)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("INSERT INTO USERS (usuario, contrasena, nombres, apellidos, isAdmin, correo, solo_lectura) VALUES (@usuario, @contrasena, @nombres, @apellidos, @isAdmin, @correo, @lectura)", connection))
                {
                    command.Parameters.AddWithValue("@usuario", usuario);
                    string hash = BCrypt.Net.BCrypt.HashPassword(contrasena);
                    command.Parameters.AddWithValue("@contrasena", hash);
                    command.Parameters.AddWithValue("@nombres", nombres);
                    command.Parameters.AddWithValue("@apellidos", apellidos);
                    command.Parameters.AddWithValue("@isAdmin", isAdmin);
                    command.Parameters.AddWithValue("@lectura", soloLectura);
                    command.Parameters.AddWithValue("@correo", correo);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }

        public bool UpdateUser(int id, string usuario, string contrasena, string nombres, string apellidos, bool isAdmin, string correo, bool cambiarContrasena, bool soloLectura)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("UpdateUser2", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@p_id", id);
                    command.Parameters.AddWithValue("@p_usuario", usuario);

                    string contrasenaFinal = cambiarContrasena ? BCrypt.Net.BCrypt.HashPassword(contrasena) : "";
                    command.Parameters.AddWithValue("@p_contrasena", contrasenaFinal);

                    command.Parameters.AddWithValue("@p_nombres", nombres);
                    command.Parameters.AddWithValue("@p_apellidos", apellidos);
                    command.Parameters.AddWithValue("@p_isAdmin", isAdmin);
                    command.Parameters.AddWithValue("@p_soloLectura", soloLectura);
                    command.Parameters.AddWithValue("@p_correo", correo);

                    command.ExecuteNonQuery();
                    return true;
                }
            }
        }



        public int GetTotalUsuarios()
        {
            int totalUsuarios = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetTotalUsers", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    // Parámetro de salida para el total de usuarios
                    MySqlParameter paramTotalUsuarios = new MySqlParameter("totalUsuarios", MySqlDbType.Int32)
                    {
                        Direction = ParameterDirection.Output
                    };
                    comando.Parameters.Add(paramTotalUsuarios);

                    conexion.Open();
                    comando.ExecuteNonQuery();  // Ejecutar el procedimiento almacenado

                    // Obtener el valor de totalUsuarios desde el parámetro de salida
                    totalUsuarios = Convert.ToInt32(paramTotalUsuarios.Value);
                }
            }

            return totalUsuarios;
        }


        public DataTable GetAllUsers(int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetAllUsers2", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    int registrosOmitidos = (currentPageIndex - 1) * pageSize;
                    // Agregar parámetros de entrada
                    comando.Parameters.AddWithValue("pageNumber", currentPageIndex);
                    comando.Parameters.AddWithValue("pageSize", pageSize);
                    comando.Parameters.AddWithValue("registrosOmitidos", registrosOmitidos);

                    conexion.Open();

                    // Ejecutar la consulta y cargar los datos
                    using (MySqlDataReader leer = comando.ExecuteReader())
                    {
                        tabla.Load(leer);

                        // Procesar la columna "isAdmin"
                        tabla.Columns.Add("ADMINISTRADOR", typeof(string));

                        foreach (DataRow row in tabla.Rows)
                        {
                            var isAdminValue = row["isAdmin"];
                            bool isAdmin = Convert.ToUInt64(isAdminValue) == 1;
                            row["ADMINISTRADOR"] = isAdmin ? "SI" : "NO";
                        }

                        tabla.Columns.Remove("isAdmin");


                        tabla.Columns.Add("SÓLO LECTURA", typeof(string));

                        foreach (DataRow row in tabla.Rows)
                        {
                            var soloLecturaValue = row["solo_lectura"];
                            bool soloLectura = Convert.ToUInt64(soloLecturaValue) == 1;
                            row["SÓLO LECTURA"] = soloLectura ? "SI" : "NO";
                        }

                        tabla.Columns.Remove("solo_lectura");
                    }

                }
            }

            return tabla;
        }



        public bool RemoveUser(int userId,string deletedUser, string deletedBy)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                       
                        using (var logCommand = new MySqlCommand("INSERT INTO UserDeletionLog (user, deleted_by) VALUES (@user, @deletedBy)", connection, transaction))
                        {
                            logCommand.Parameters.AddWithValue("@user", deletedUser);
                            logCommand.Parameters.AddWithValue("@deletedBy", deletedBy);
                            logCommand.ExecuteNonQuery();
                        }

                        using (var deleteCommand = new MySqlCommand("DELETE FROM USERS WHERE id=@userId", connection, transaction))
                        {
                            deleteCommand.Parameters.AddWithValue("@userId", userId);
                            int rowsAffected = deleteCommand.ExecuteNonQuery();

                            transaction.Commit();
                            return rowsAffected > 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Error al eliminar el usuario: " + ex.Message);
                    }
                }
            }
        }

        public int GetFilteredUserCount(string value)
        {
            int totalUsuarios = 0;

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetFilteredUserCount", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    // Parámetro de entrada
                    comando.Parameters.AddWithValue("@value", value);

                    // Parámetro de salida
                    MySqlParameter totalUsuariosParam = new MySqlParameter("@totalUsuarios", MySqlDbType.Int32);
                    totalUsuariosParam.Direction = ParameterDirection.Output;
                    comando.Parameters.Add(totalUsuariosParam);

                    conexion.Open();

                    // Ejecutar el procedimiento almacenado
                    comando.ExecuteNonQuery();

                    // Obtener el valor de totalUsuarios
                    totalUsuarios = Convert.ToInt32(totalUsuariosParam.Value);
                }
            }

            return totalUsuarios;
        }



        public DataTable GetUserByValue(string value, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            using (MySqlConnection conexion = GetConnection()) 
            {
                using (MySqlCommand comando = new MySqlCommand("GetUserByValue", conexion)) 
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    int registrosOmitidos = (currentPageIndex - 1) * pageSize;
                    
                    comando.Parameters.AddWithValue("pageNumber", currentPageIndex);
                    comando.Parameters.AddWithValue("pageSize", pageSize);
                    comando.Parameters.AddWithValue("registrosOmitidos", registrosOmitidos);
                    comando.Parameters.AddWithValue("@value", value); 

                    conexion.Open();
                    using (MySqlDataReader leer = comando.ExecuteReader()) 
                    {
                        tabla.Load(leer); 
                    }
                }
            }

           
            tabla.Columns.Add("ADMINISTRADOR", typeof(string));

           
            foreach (DataRow row in tabla.Rows)
            {
                var isAdminValue = row["isAdmin"];
                bool isAdmin = Convert.ToBoolean(isAdminValue); 
                row["ADMINISTRADOR"] = isAdmin ? "SI" : "NO";
            }

            tabla.Columns.Remove("isAdmin");


            tabla.Columns.Add("SÓLO LECTURA", typeof(string));

            foreach (DataRow row in tabla.Rows)
            {
                var soloLecturaValue = row["solo_lectura"];
                bool soloLectura = Convert.ToUInt64(soloLecturaValue) == 1;
                row["SÓLO LECTURA"] = soloLectura ? "SI" : "NO";
            }

            tabla.Columns.Remove("solo_lectura");

            return tabla;
        }

        public DataTable GetUserById(int id)
        {
            DataTable tabla = new DataTable();
            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetUserById", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@userId", id);
                    
                    conexion.Open();
                    using (MySqlDataReader leer = comando.ExecuteReader())
                    {
                        tabla.Load(leer);
                    }
                }
            }

            return tabla;
        }

        public (bool, bool) Login(string user, string pass)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    connection.Open();

                    using (var command = new MySqlCommand("SELECT contrasena, isAdmin, id, usuario, nombres, apellidos, correo, solo_lectura FROM USERS WHERE usuario=@user", connection))
                    {
                        command.Parameters.AddWithValue("@user", user);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string storedPassword = reader.GetString(0);
                                bool passwordMatches = false;

                                // Verificamos si la contraseña almacenada parece un hash bcrypt (si empieza con $2)
                                if (storedPassword.StartsWith("$2"))
                                {
                                    passwordMatches = BCrypt.Net.BCrypt.Verify(pass, storedPassword);
                                }
                                else
                                {
                                    // Comparar directamente (texto plano)
                                    if (storedPassword == pass)
                                    {
                                        passwordMatches = true;

                                        // Migrar a hash bcrypt
                                        string newHash = BCrypt.Net.BCrypt.HashPassword(pass);

                                        reader.Close(); // cerrar antes de ejecutar otro comando

                                        using (var updateCommand = new MySqlCommand("UPDATE USERS SET contrasena=@newHash WHERE usuario=@user", connection))
                                        {
                                            updateCommand.Parameters.AddWithValue("@newHash", newHash);
                                            updateCommand.Parameters.AddWithValue("@user", user);
                                            updateCommand.ExecuteNonQuery();
                                        }
                                    }
                                }

                                if (!passwordMatches)
                                    return (false, false);

                                // Si coincide, rellenar usuario activo
                                UsuarioActivo.isAdmin = reader.GetBoolean(1);
                                UsuarioActivo.idUser = reader.GetInt32(2);
                                UsuarioActivo.usuario = reader.GetString(3);
                                UsuarioActivo.nombres = reader.GetString(4);
                                UsuarioActivo.apellidos = reader.GetString(5);
                                UsuarioActivo.correo = reader.GetString(6);
                                UsuarioActivo.soloLectura = reader.GetBoolean(7);
                                return (true, UsuarioActivo.isAdmin);
                            }
                            else
                            {
                                return (false, false); // Usuario no existe
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return (false, false);
                }
            }
        }

        public int ContarAdministradores()
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                // Definir el comando para ejecutar el procedimiento almacenado
                using (var command = new MySqlCommand("ContarAdministradores", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Ejecutar el procedimiento almacenado y leer el resultado
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Leer el valor devuelto por el procedimiento
                            return reader.GetInt32("TotalAdministradores");
                        }
                        else
                        {
                            return 0;  // Si no hay registros, devolver 0
                        }
                    }
                }
            }
        }

        public bool ProbarConexion()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open(); // Si esto falla, lanzará excepción
                    return true;       // Conexión exitosa
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al conectar a la base de datos: " + ex.Message);
                return false; // Conexión fallida
            }
        }

    }
}*/