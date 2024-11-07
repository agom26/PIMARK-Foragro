using MySql.Data.MySqlClient;
using System;
using Comun.Cache;
using AccesoDatos.ServiciosEmail;
using AccesoDatos.MySqlServer;
using AccesoDatos.Usuarios;
using System.Data;

namespace AccesoDatos.Usuarios
{
    public class UserDao : ConnectionSQL
    {
        
       
        public bool AddUser(string usuario, string contrasena, string nombres, string apellidos, bool isAdmin, string correo)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("INSERT INTO USERS (usuario, contrasena, nombres, apellidos, isAdmin, correo) VALUES (@usuario, @contrasena, @nombres, @apellidos, @isAdmin, @correo)", connection))
                {
                    command.Parameters.AddWithValue("@usuario", usuario);
                    command.Parameters.AddWithValue("@contrasena", contrasena);
                    command.Parameters.AddWithValue("@nombres", nombres);
                    command.Parameters.AddWithValue("@apellidos", apellidos);
                    command.Parameters.AddWithValue("@isAdmin", isAdmin);
                    command.Parameters.AddWithValue("@correo", correo);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }

        public bool UpdateUser(int id, string usuario, string contrasena, string nombres, string apellidos, bool isAdmin, string correo)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("UPDATE USERS SET usuario=@usuario, contrasena=@contrasena, nombres=@nombres, apellidos=@apellidos, isAdmin=@isAdmin, correo=@correo WHERE id=@id AND (SELECT COUNT(*) FROM USERS WHERE usuario=@usuario AND id!=@id) = 0;", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@usuario", usuario);
                    command.Parameters.AddWithValue("@contrasena", contrasena);
                    command.Parameters.AddWithValue("@nombres", nombres);
                    command.Parameters.AddWithValue("@apellidos", apellidos);
                    command.Parameters.AddWithValue("@isAdmin", isAdmin);
                    command.Parameters.AddWithValue("@correo", correo);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }

        

        public DataTable GetAllUsers()
        {
            DataTable tabla = new DataTable();
            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("SELECT id, usuario as Usuario, nombres as Nombre, apellidos as Apellido, correo as Correo, isAdmin as Administrador FROM USERS", conexion))
                {
                    conexion.Open();
                    using (MySqlDataReader leer = comando.ExecuteReader())
                    {
                       
                        tabla.Load(leer);
                        tabla.Columns.Add("Es Administrador", typeof(string));

                        foreach (DataRow row in tabla.Rows)
                        {
                            var isAdminValue = row["Administrador"];
                            bool isAdmin = Convert.ToUInt64(isAdminValue) == 1; 
                            row["Es Administrador"] = isAdmin ? "sí" : "no";
                        }

                        tabla.Columns.Remove("Administrador");
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
       
        
        public DataTable GetUserByValue(string value)
        {
            DataTable tabla = new DataTable();
            using (MySqlConnection conexion = GetConnection()) 
            {
                using (MySqlCommand comando = new MySqlCommand("GetUserByValue", conexion)) 
                {
                    comando.CommandType = CommandType.StoredProcedure; 
                    comando.Parameters.AddWithValue("@value", value); 

                    conexion.Open();
                    using (MySqlDataReader leer = comando.ExecuteReader()) 
                    {
                        tabla.Load(leer); 
                    }
                }
            }

           
            tabla.Columns.Add("Es Administrador", typeof(string));

           
            foreach (DataRow row in tabla.Rows)
            {
                var isAdminValue = row["EsAdministrador"];
                bool isAdmin = Convert.ToBoolean(isAdminValue); 
                row["Es Administrador"] = isAdmin ? "sí" : "no";
            }

            tabla.Columns.Remove("EsAdministrador");

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

                    // Primera consulta: verificar si el usuario existe y obtener isAdmin
                    using (var command = new MySqlCommand("SELECT isAdmin, id, usuario, nombres, apellidos, correo FROM USERS WHERE usuario=@user AND contrasena=@pass", connection))
                    {
                        command.Parameters.AddWithValue("@user", user);
                        command.Parameters.AddWithValue("@pass", pass);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read()) // Si el usuario existe
                            {
                                Console.WriteLine("Si hay usuario");
                                bool isAdmin = reader.GetBoolean(0); // Obtener el valor de isAdmin
                                UsuarioActivo.idUser = reader.GetInt32(1);
                                UsuarioActivo.usuario = reader.GetString(2);
                                UsuarioActivo.nombres = reader.GetString(3);
                                UsuarioActivo.apellidos = reader.GetString(4);
                                UsuarioActivo.correo = reader.GetString(5);

                                return (true, isAdmin); // Retornar si el login es válido y si es admin
                            }
                            else
                            {
                                return (false, false); // Si no existe el usuario
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return (false, false); // Maneja el error de manera adecuada
                }
            }
        }


    }
}
