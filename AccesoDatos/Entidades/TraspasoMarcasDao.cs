using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class TraspasoMarcasDao:ConnectionSQL
    {
        public void InsertarTraspasoMarca(
        string numExpediente,
        int idMarca,
        int idTitularAnterior,
        int idTitularNuevo,
        string antiguoNombre,
        string nuevoNombre)
        {
            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("InsertarTraspasoMarca", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue("@p_NumExpediente", numExpediente);
                    comando.Parameters.AddWithValue("@p_IdMarca", idMarca);
                    comando.Parameters.AddWithValue("@p_IdTitularAnterior", idTitularAnterior);
                    comando.Parameters.AddWithValue("@p_IdTitularNuevo", idTitularNuevo);
                    comando.Parameters.AddWithValue("@p_AntiguoNombre", antiguoNombre);
                    comando.Parameters.AddWithValue("@p_NuevoNombre", nuevoNombre);

                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }

        public DataTable ObtenerTraspasosDeMarcaPorId(int idMarca)
        {
            DataTable resultados = new DataTable();

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("ObtenerTraspasosDeMarcaPorId", conexion))
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
        public DataTable ObtenerTraspasoPorId(int id)
        {
            DataTable resultado = new DataTable();

            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("ObtenerTraspasoPorId", conexion))
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

        public bool ActualizarTraspasoMarca(int id, string numExpediente, int idMarca, int idTitularAnterior, int idTitularNuevo, string antiguoNombre, string nuevoNombre)
        {
            using (MySqlConnection conexion = GetConnection())
            {
                using (MySqlCommand comando = new MySqlCommand("ActualizarTraspasoMarca", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@p_Id", id);
                    comando.Parameters.AddWithValue("@p_NumExpediente", numExpediente);
                    comando.Parameters.AddWithValue("@p_IdMarca", idMarca);
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
