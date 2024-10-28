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

                    // Ejecuta el comando y devuelve el número de filas afectadas
                    int rowsAffected = command.ExecuteNonQuery();

                    // Si se insertó al menos una fila, la operación fue exitosa
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

                    // Ejecuta el comando y devuelve el número de filas afectadas
                    int rowsAffected = command.ExecuteNonQuery();

                    // Si se actualizó al menos una fila, la operación fue exitosa
                    return rowsAffected > 0;
                }
            }
        }

        public List<(int id,string usuario, string nombres, string apellidos, string correo, string contrasena, bool isAdmin)> GetByValue(string value)
        {
            var users = new List<(int id,string usuario, string nombres, string apellidos, string correo, string contrasena, bool isAdmin)>();

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("SELECT * FROM USERS WHERE usuario LIKE @value OR nombres LIKE @value OR apellidos LIKE @value OR correo LIKE @value", connection))
                {
                    command.Parameters.AddWithValue("@value", "%" + value + "%");

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("id");
                            string usuario = reader.GetString("usuario");
                            string contrasena = reader.GetString("contrasena");
                            string nombres = reader.GetString("nombres");
                            string apellidos = reader.GetString("apellidos");
                            string correo = reader.GetString("correo");
                            bool isAdmin = reader.GetBoolean("isAdmin");

                            // Agrega la tupla a la lista
                            users.Add((id,usuario, nombres, apellidos, correo, contrasena, isAdmin));
                        }
                    }
                }
            }
            return users; // Retorna la lista de usuarios encontrados
        }

        public DataTable GetAllUsers()
        {
            DataTable tabla = new DataTable();
            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("SELECT usuario as Usuario, nombres as Nombre, apellidos as Apellido, correo as Correo, isAdmin as Administrador FROM USERS", conexion))
                {
                    conexion.Open();
                    using (MySqlDataReader leer = comando.ExecuteReader())
                    {
                        // Cargar los datos en la tabla
                        tabla.Load(leer);

                        // Agregar una nueva columna para mostrar "sí" o "no"
                        tabla.Columns.Add("Es Administrador", typeof(string));

                        // Llenar la nueva columna según el valor de isAdmin
                        foreach (DataRow row in tabla.Rows)
                        {
                            // Cambiar la forma de obtener el valor de isAdmin
                            var isAdminValue = row["Administrador"];

                            // Asumimos que el valor puede ser un UInt64
                            bool isAdmin = Convert.ToUInt64(isAdminValue) == 1; // O 0 para false

                            // Asignar "sí" o "no" a la nueva columna
                            row["Es Administrador"] = isAdmin ? "sí" : "no";
                        }

                        // Opcional: eliminar la columna original de isAdmin si no la necesitas
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
                        // Inserta en la tabla de log antes de eliminar al usuario
                        using (var logCommand = new MySqlCommand("INSERT INTO UserDeletionLog (user, deleted_by) VALUES (@user, @deletedBy)", connection, transaction))
                        {
                            logCommand.Parameters.AddWithValue("@user", deletedUser);
                            logCommand.Parameters.AddWithValue("@deletedBy", deletedBy);
                            logCommand.ExecuteNonQuery();
                        }

                        // Elimina el usuario de la tabla USERS
                        using (var deleteCommand = new MySqlCommand("DELETE FROM USERS WHERE id=@userId", connection, transaction))
                        {
                            deleteCommand.Parameters.AddWithValue("@userId", userId);
                            int rowsAffected = deleteCommand.ExecuteNonQuery();

                            // Si se eliminó el usuario, confirma la transacción
                            transaction.Commit();
                            return rowsAffected > 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        // En caso de error, revertir la transacción
                        transaction.Rollback();
                        throw new Exception("Error al eliminar el usuario: " + ex.Message);
                    }
                }
            }
        }
        /*
        public string solicitarContrasenaUsuario(string usuarioSolicitante)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand("SELECT * FROM USERS WHERE usuario=@user OR correo=@email", connection))
                {
                    command.Parameters.AddWithValue("@user", usuarioSolicitante);
                    command.Parameters.AddWithValue("@email", usuarioSolicitante);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        { // Si el usuario existe
                            string nombreUsuario = reader.GetString(3) + " " + reader.GetString(4);
                            string correo = reader.GetString(6);
                            string cont = reader.GetString(2);

                            CorreoSoporteSistema correoSoporte = new CorreoSoporteSistema();
                            correoSoporte.enviarCorreo(
                                asunto: "SISTEMA: Solicitud de recuperación de contraseña",
                                cuerpo: $"Hola {nombreUsuario}{Environment.NewLine}Has solicitado la recuperación de tu contraseña.{Environment.NewLine}" +
                                      $"Tu contraseña actual es: {cont}{Environment.NewLine}",
                                correoReceptor: new List<string> { nombreUsuario }
                            );
                            return "Hola " + nombreUsuario + "\nHas solicitado la recuperación de tu contraseña.\nPor favor revisa tu correo: " + correo;
                        }
                        else
                        {
                            return "Lo sentimos, no tiene una cuenta con este usuario o correo electrónico";
                        }
                    }
                }
            }
        }*/

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
