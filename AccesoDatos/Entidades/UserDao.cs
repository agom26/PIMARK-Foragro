using MySql.Data.MySqlClient;
using System;
using Comun.Cache;
using AccesoDatos.ServiciosEmail;
using AccesoDatos.MySqlServer;
using AccesoDatos.Usuarios;
using System.Data;
using Org.BouncyCastle.Crypto.Generators;

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
                using (var command = new MySqlCommand("UpdateUser", connection))
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
                using (MySqlCommand comando = new MySqlCommand("GetAllUsers", conexion))
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



        /*
        public (bool, bool) Login(string user, string pass)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    connection.Open();

                    using (var command = new MySqlCommand("SELECT isAdmin, id, usuario, nombres, apellidos, correo FROM USERS WHERE usuario=@user AND contrasena=@pass", connection))
                    {
                        command.Parameters.AddWithValue("@user", user);
                        command.Parameters.AddWithValue("@pass", pass);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read()) 
                            {
                                Console.WriteLine("Si hay usuario");
                                UsuarioActivo.isAdmin = reader.GetBoolean(0); 
                                UsuarioActivo.idUser = reader.GetInt32(1);
                                UsuarioActivo.usuario = reader.GetString(2);
                                UsuarioActivo.nombres = reader.GetString(3);
                                UsuarioActivo.apellidos = reader.GetString(4);
                                UsuarioActivo.correo = reader.GetString(5);
                                
                                return (true, UsuarioActivo.isAdmin); 
                            }
                            else
                            {
                                return (false, false); 
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
        }*/

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
}
