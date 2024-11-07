using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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



        public DataTable GetTitularByValue(string value)
        {
            DataTable tabla = new DataTable();
            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetTitularByValue", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
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
        public DataTable GetAgenteByValue(string value)
        {
            DataTable tabla = new DataTable();
            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("GetAgenteByValue", conexion))
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
            return tabla;
        }
        //clientes
        public DataTable GetClienteByValue(string value)
        {
            DataTable tabla = new DataTable();
            using (MySqlConnection conexion = GetConnection()) // Asegura que la conexión se cierre al finalizar
            {
                using (MySqlCommand comando = new MySqlCommand("SELECT * FROM Personas WHERE (nombre LIKE @value OR direccion LIKE @value OR nit LIKE @value OR pais LIKE @value OR correo LIKE @value OR telefono LIKE @value OR telefono LIKE @value OR nombre_contacto LIKE @value) AND tipo='cliente';", conexion)) // Inicializa correctamente el comando
                {
                    comando.Parameters.AddWithValue("@value", value);
                    conexion.Open();
                    using (MySqlDataReader leer = comando.ExecuteReader()) // Asegura que el lector se cierre
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


        public DataTable GetAllTitulares()
        {
            DataTable tabla = new DataTable();
            using (MySqlConnection conexion = GetConnection()) 
            {
                using (MySqlCommand comando = new MySqlCommand("SELECT id, nombre as Nombre, direccion as Direccion, nit as Nit, pais as Pais, correo as Correo, telefono as Telefono, nombre_contacto as Contacto FROM Personas WHERE tipo='titular'", conexion)) // Inicializa correctamente el comando
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

        public DataTable GetAllAgentes()
        {
            DataTable tabla = new DataTable();
            using (MySqlConnection conexion = GetConnection()) 
            {
                using (MySqlCommand comando = new MySqlCommand("SELECT id, nombre as Nombre, direccion as Direccion, nit as Nit, pais as Pais, correo as Correo, telefono as Telefono, nombre_contacto as Contacto FROM Personas WHERE tipo='agente'", conexion)) // Inicializa correctamente el comando
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

        public DataTable GetAllClientes()
        {
            DataTable tabla = new DataTable();
            using (MySqlConnection conexion = GetConnection()) 
            {
                using (MySqlCommand comando = new MySqlCommand("SELECT id, nombre as Nombre, direccion as Direccion, nit as Nit, pais as Pais, correo as Correo, telefono as Telefono, nombre_contacto as Contacto FROM Personas WHERE tipo='cliente'", conexion)) // Inicializa correctamente el comando
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

                        using (var deleteCommand = new MySqlCommand("DELETE FROM Personas WHERE id=@personaId", connection, transaction))
                        {
                            deleteCommand.Parameters.AddWithValue("@personaId", personaId);
                            int rowsAffected = deleteCommand.ExecuteNonQuery();
                            transaction.Commit();
                            return rowsAffected > 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Error al eliminar el la persona: " + ex.Message);
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

                        using (var deleteCommand = new MySqlCommand("DELETE FROM Personas WHERE id=@personaId", connection, transaction))
                        {
                            deleteCommand.Parameters.AddWithValue("@personaId", personaId);
                            int rowsAffected = deleteCommand.ExecuteNonQuery();
                            transaction.Commit();
                            return rowsAffected > 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Error al eliminar el la persona: " + ex.Message);
                    }
                }
            }
        }
    }
}
