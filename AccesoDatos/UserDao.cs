using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;

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

                    using (var command = new MySqlCommand("SELECT isAdmin FROM USERS WHERE usuario=@user AND contrasena=@pass", connection))
                    {
                        command.Parameters.AddWithValue("@user", user);
                        command.Parameters.AddWithValue("@pass", pass);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read()) // Si el usuario existe
                            {
                                Console.WriteLine("Si hay usuario");
                                bool isAdmin = reader.GetBoolean(0); // Obtener el valor de isAdmin
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
