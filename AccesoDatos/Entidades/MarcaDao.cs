using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class MarcaDao:ConnectionSQL
    {
        public bool AddMarcaNacional(string expediente, string nombre, string signoDistintivo, string clase, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud, string estado)
        {
            using (var connection = GetConnection()) // Asegúrate de que GetConnection esté implementado
            {
                connection.Open();
                using (var command = new MySqlCommand("INSERT INTO Marcas (expediente, nombre, signo_distintivo, clase, logo, idTitular, idAgente, fecha_solicitud, estado,tipo) VALUES (@expediente, @nombre, @signoDistintivo, @clase, @logo, @idPersonaTitular, @idPersonaAgente, @fecha_solicitud, @estado,'nacional')", connection))
                {
                    command.Parameters.AddWithValue("@expediente", expediente);
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@signoDistintivo", signoDistintivo);
                    command.Parameters.AddWithValue("@clase", clase);
                    command.Parameters.AddWithValue("@logo", logo); // Asignar el logo
                    command.Parameters.AddWithValue("@idPersonaTitular", idPersonaTitular);
                    command.Parameters.AddWithValue("@idPersonaAgente", idPersonaAgente);
                    command.Parameters.AddWithValue("@fecha_solicitud", fecha_solicitud);
                    command.Parameters.AddWithValue("@estado", estado);

                    // Ejecuta el comando y devuelve el número de filas afectadas
                    int rowsAffected = command.ExecuteNonQuery();

                    // Si se insertó al menos una fila, la operación fue exitosa
                    return rowsAffected > 0;
                }
            }
        }

        public bool AddMarcaNacionalRegistrada(string expediente, string nombre, string signoDistintivo, string clase, string folio, string libro, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud, string estado, string registro, DateTime fechaRegistro, DateTime fechaVencimiento)
        {
            using (var connection = GetConnection()) // Asegúrate de que GetConnection esté implementado
            {
                connection.Open();
                using (var command = new MySqlCommand("INSERT INTO Marcas (expediente, nombre, signo_distintivo, clase, folio, libro, logo, idTitular, idAgente, fecha_solicitud, estado,tipo, registro, fecha_registro, fecha_vencimiento) VALUES (@expediente, @nombre, @signoDistintivo, @clase, @folio, @libro, @logo, @idPersonaTitular, @idPersonaAgente, @fecha_solicitud, @estado,'nacional',@registro, @fecha_registro, @fecha_vencimiento)", connection))
                {
                    command.Parameters.AddWithValue("@expediente", expediente);
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@signoDistintivo", signoDistintivo);
                    command.Parameters.AddWithValue("@clase", clase);
                    command.Parameters.AddWithValue("@folio", folio);
                    command.Parameters.AddWithValue("@libro", libro);
                    command.Parameters.AddWithValue("@logo", logo); // Asignar el logo
                    command.Parameters.AddWithValue("@idPersonaTitular", idPersonaTitular);
                    command.Parameters.AddWithValue("@idPersonaAgente", idPersonaAgente);
                    command.Parameters.AddWithValue("@fecha_solicitud", fecha_solicitud);
                    command.Parameters.AddWithValue("@estado", estado);
                    command.Parameters.AddWithValue("@registro", registro);
                    command.Parameters.AddWithValue("@fecha_registro", fechaRegistro);
                    command.Parameters.AddWithValue("@fecha_vencimiento", fechaVencimiento);

                    // Ejecuta el comando y devuelve el número de filas afectadas
                    int rowsAffected = command.ExecuteNonQuery();

                    // Si se insertó al menos una fila, la operación fue exitosa
                    return rowsAffected > 0;
                }
            }
        }
    }
}
