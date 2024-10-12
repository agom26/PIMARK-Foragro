using MySql.Data.MySqlClient;
using System;
using Comun.Cache;

namespace AccesoDatos
{
    public class UserDao : ConnectionSQL
    {
       

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
