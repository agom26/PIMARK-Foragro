using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class TraspasoPatenteDao:ConnectionSQL
    {
        public void InsertarTraspasoPatente(
       string numExpediente,
       int idPatente,
       int idTitularAnterior,
       int idTitularNuevo,
       string antiguoNombre,
       string nuevoNombre)
        {
            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("InsertarTraspasoPatente", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue("@p_NumExpediente", numExpediente);
                    comando.Parameters.AddWithValue("@p_IdMarca", idPatente);
                    comando.Parameters.AddWithValue("@p_IdTitularAnterior", idTitularAnterior);
                    comando.Parameters.AddWithValue("@p_IdTitularNuevo", idTitularNuevo);
                    comando.Parameters.AddWithValue("@p_AntiguoNombre", antiguoNombre);
                    comando.Parameters.AddWithValue("@p_NuevoNombre", nuevoNombre);

                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }

        public DataTable ObtenerTraspasosDePatentePorId(int idPatente)
        {
            DataTable resultados = new DataTable();

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("ObtenerTraspasosDePatentePorId", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@p_IdPatente", idPatente);

                    conexion.Open();
                    using (MySqlDataAdapter adaptador = new MySqlDataAdapter(comando))
                    {
                        adaptador.Fill(resultados);
                    }
                }
            }

            return resultados;
        }
        public DataTable ObtenerTraspasoPatentePorId(int id)
        {
            DataTable resultado = new DataTable();

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("ObtenerTraspasoPatentePorId", conexion))
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

        public bool ActualizarTraspasoPatente(int id, string numExpediente, int idPatente, int idTitularAnterior, int idTitularNuevo, string antiguoNombre, string nuevoNombre)
        {
            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("ActualizarTraspasoPatente", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@p_Id", id);
                    comando.Parameters.AddWithValue("@p_NumExpediente", numExpediente);
                    comando.Parameters.AddWithValue("@p_IdPatente", idPatente);
                    comando.Parameters.AddWithValue("@p_IdTitularAnterior", idTitularAnterior);
                    comando.Parameters.AddWithValue("@p_IdTitularNuevo", idTitularNuevo);
                    comando.Parameters.AddWithValue("@p_AntiguoNombre", antiguoNombre);
                    comando.Parameters.AddWithValue("@p_NuevoNombre", nuevoNombre);

                    conexion.Open();
                    int filasAfectadas = comando.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }
    }
}
