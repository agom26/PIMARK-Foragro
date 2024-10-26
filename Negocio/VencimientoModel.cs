using AccesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos.Entidades;
using System.Data;
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
    }
}
