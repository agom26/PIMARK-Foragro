using AccesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos.Entidades;
using System.Data;
using AccesoDatos.ServiciosEmail;
namespace Dominio
{
    public class VencimientoModel:ConnectionSQL
    {
        private VencimientoDao vencimientoDao;

        public VencimientoModel()
        {
            vencimientoDao = new VencimientoDao();
        }

        public DataTable GetAllVencimientos()
        {
            DataTable tabla = new DataTable();
            tabla = vencimientoDao.GetAllVencimientos();
            return tabla;
        }
        public DataTable GetVencimientoByValue(string value)
        {
            // Llama al método correspondiente en personaDao para obtener la persona y devolver el resultado
            return vencimientoDao.GetVencimientoByValue(value);
        }

        public void EjecutarProcedimiento()
        {
            vencimientoDao.EjecutarProcedimientoInsertarVencimientos();
        }

        public (string CorreoTitular, string CorreoAgente) GetCorreosPorMarcaId(int id)
        {
            // Llama al método en vencimientoDao y guarda la tupla de correos
            var correos = vencimientoDao.GetCorreosPorMarcaId(id);

            // Devuelve la tupla de correos
            return correos;
        }

        public void EnviarCorreo(string correo)
        {
            var mailService = new ServicioEmail();
            mailService.Send(
                recipient: correo,
                subject: "Vencimiento de marca o patente",
                body: "Hola, le comentamos que ya va a vencer su documento legal"
                );

        }

        public void ActualizarNotificado(int marcaId)
        {
            vencimientoDao.ActualizarNotificado(marcaId);

        }
    }
}
