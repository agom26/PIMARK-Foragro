using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class MarcaDao:ConnectionSQL
    {/*
        public bool AddMarcaNacional(string expediente, string nombre, string signoDistintivo, string clase, string folio, string libro, string imagen, string idPersonaTitular)
        {
            using (var connection = GetConnection()) // Asegúrate de que GetConnection esté implementado
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
                    command.Parameters.AddWithValue("@tipo", tipo);  // Debe ser 'titular' o 'agente'

                    // Ejecuta el comando y devuelve el número de filas afectadas
                    int rowsAffected = command.ExecuteNonQuery();

                    // Si se insertó al menos una fila, la operación fue exitosa
                    return rowsAffected > 0;
                }
            }
        }*/
    }
}
