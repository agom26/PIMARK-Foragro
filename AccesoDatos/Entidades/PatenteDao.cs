using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class PatenteDao:ConnectionSQL
    {
        public int InsertarPatente(
        string caso,
        string expediente,
        string nombre,
        string estado,
        string tipo,
        int idTitular,
        int idAgente,
        DateTime fechaSolicitud,
        string registro,
        string folio,
        string libro,
        DateTime? fechaRegistro,
        DateTime? fechaVencimiento,
        string erenov,
        string etrasp,
        int anualidades,
        string pct,
        string comprobantePagos,
        string descripcion,
        string reivindicaciones,
        string dibujos,
        string resumen,
        string documentoCesion,
        string poderNombramiento)
        {
            using (MySqlConnection connection = GetConnection()) 
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("InsertarPatente", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@p_caso", caso);
                    command.Parameters.AddWithValue("@p_expediente", expediente);
                    command.Parameters.AddWithValue("@p_nombre", nombre);
                    command.Parameters.AddWithValue("@p_estado", estado);
                    command.Parameters.AddWithValue("@p_tipo", tipo);
                    command.Parameters.AddWithValue("@p_IdTitular", idTitular);
                    command.Parameters.AddWithValue("@p_IdAgente", idAgente);
                    command.Parameters.AddWithValue("@p_fecha_solicitud", fechaSolicitud);
                    command.Parameters.AddWithValue("@p_registro", registro);
                    command.Parameters.AddWithValue("@p_folio", folio);
                    command.Parameters.AddWithValue("@p_libro", libro);
                    command.Parameters.AddWithValue("@p_fecha_registro", fechaRegistro);
                    command.Parameters.AddWithValue("@p_fecha_vencimiento", fechaVencimiento);
                    command.Parameters.AddWithValue("@p_Erenov", erenov);
                    command.Parameters.AddWithValue("@p_Etrasp", etrasp);
                    command.Parameters.AddWithValue("@p_anualidades", anualidades);
                    command.Parameters.AddWithValue("@p_pct", pct);
                    command.Parameters.AddWithValue("@p_comprobante_pagos", comprobantePagos);
                    command.Parameters.AddWithValue("@p_descripcion", descripcion);
                    command.Parameters.AddWithValue("@p_reivindicaciones", reivindicaciones);
                    command.Parameters.AddWithValue("@p_dibujos", dibujos);
                    command.Parameters.AddWithValue("@p_resumen", resumen);
                    command.Parameters.AddWithValue("@p_documento_cesion", documentoCesion);
                    command.Parameters.AddWithValue("@p_poder_nombramiento", poderNombramiento);

                    MySqlParameter outputParam = new MySqlParameter("p_id_patente", MySqlDbType.Int32)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParam);

                    command.ExecuteNonQuery();
                    int idPatente = Convert.ToInt32(outputParam.Value);
                    return idPatente;
                }
            }
        }

        public DataTable GetAllPatentesEnTramite()
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = GetConnection())
                {
                    using (MySqlCommand comando = new MySqlCommand("ObtenerPatentesSinRegistro", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                        conexion.Open();
                        using (MySqlDataReader leer = comando.ExecuteReader())
                        {
                            tabla.Load(leer);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener las patentes sin registro: {ex.Message}");

            }
            return tabla;
        }

    }
}
