using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class RenovacionesMarcasDao:ConnectionSQL
    {
        public void InsertRenovacionMarca(
            string numExpediente,
            int idMarca,
            DateTime fechaRegistroAntigua,
            DateTime fechaVencimientoAntigua,
            DateTime fechaRegistroNueva,
            DateTime fechaVencimientoNueva)
        {
            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("InsertarRenovacionMarca", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue("@p_NumExpediente", numExpediente);
                    comando.Parameters.AddWithValue("@p_IdMarca", idMarca);
                    comando.Parameters.AddWithValue("@p_FechaRegistroAntigua", fechaRegistroAntigua);
                    comando.Parameters.AddWithValue("@p_FechaVencimientoAntigua", fechaVencimientoAntigua);
                    comando.Parameters.AddWithValue("@p_FechaRegistroNueva", fechaRegistroNueva);
                    comando.Parameters.AddWithValue("@p_FechaVencimientoNueva", fechaVencimientoNueva);

                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }

        public DataTable ObtenerRenovacionesDeMarcaPorId(int idMarca)
        {
            DataTable resultados = new DataTable();

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("ObtenerRenovacionesMarcaPorMarca", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@p_IdMarca", idMarca);

                    conexion.Open();
                    using (MySqlDataAdapter adaptador = new MySqlDataAdapter(comando))
                    {
                        adaptador.Fill(resultados);
                    }
                }
            }

            return resultados;
        }

        public DataTable ObtenerRenovacionPorId(int id)
        {
            DataTable resultado = new DataTable();

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("ObtenerRenovacionPorId", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@p_Id", id);

                    conexion.Open();
                    using (MySqlDataAdapter adaptador = new MySqlDataAdapter(comando))
                    {
                        adaptador.Fill(resultado);
                    }
                }
            }

            return resultado;
        }
        public bool ActualizarRenovacionMarca(int id, string numExpediente, int idMarca, DateTime fechaRegistroAntigua, DateTime fechaVencimientoAntigua, DateTime fechaRegistroNueva, DateTime fechaVencimientoNueva)
        {
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("ActualizarRenovacionMarca", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("p_Id", id);
                        comando.Parameters.AddWithValue("p_NumExpediente", numExpediente);
                        comando.Parameters.AddWithValue("p_IdMarca", idMarca);
                        comando.Parameters.AddWithValue("p_FechaRegistroAntigua", fechaRegistroAntigua);
                        comando.Parameters.AddWithValue("p_FechaVencimientoAntigua", fechaVencimientoAntigua);
                        comando.Parameters.AddWithValue("p_FechaRegistroNueva", fechaRegistroNueva);
                        comando.Parameters.AddWithValue("p_FechaVencimientoNueva", fechaVencimientoNueva);

                        conexion.Open();
                        int filasAfectadas = comando.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine("Error al actualizar la renovación: " + ex.Message);
                return false;
            }
        }





    }
}
