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
        public DataTable ObtenerVencimientos()
        {
            DataTable vencimientos= new DataTable();
            vencimientos= vencimientoDao.ObtenerTodosLosVencimientosReporte();
            return vencimientos;
        }
        public DataTable GetAllVencimientos(int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = vencimientoDao.GetAllVencimientosPaginados(currentPageIndex, pageSize);
            return tabla;
        }
        public DataTable FiltrarVencimientos(string filtro, int currentPageIndex, int pageSize)
        {
            return vencimientoDao.FiltrarVencimientos(filtro, currentPageIndex, pageSize);
        }
        public int GetTotalVencimientos()
        {
            return vencimientoDao.GetTotalVencimientos();
        }
        public int GetFilteredVencimientosCount(string value)
        {
            return vencimientoDao.GetFilteredVencimientosCount(value);
        }
        public DataTable ObtenerTodosLosVencimientosFiltradosReporte(string valor)
        {
            return vencimientoDao.ObtenerTodosLosVencimientosFiltradosReporte(valor);
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

        public void ActualizarNotificado(int id, string tipo)
        {
            vencimientoDao.ActualizarNotificado(id, tipo);

        }

        public void EditarTextoRtf(string tipo, string mensaje)
        {
            vencimientoDao.EditarTextoRtf(tipo, mensaje);
        }
        public string ObtenerTextoRtfPorTipo(string tipo)
        {
            return vencimientoDao.ObtenerTextoRtfPorTipo(tipo);
        }


    }
}
