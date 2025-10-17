using AccesoDatos;
using AccesoDatos.Entidades;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace Dominio
{
    public class OposicionModel : ConnectionSQL
    {
        private OposicionesDao oposicionDao;

        public OposicionModel()
        {
            oposicionDao = new OposicionesDao();
        }

        public async Task<(int total, DataTable datos)> ObtenerOposicionesNacionalesInterpuestasCombinado(string situacionActual, int currentPageIndex, int pageSize)
        {
            return await oposicionDao.ObtenerOposicionesNacionalesInterpuestasCombinado(situacionActual, currentPageIndex, pageSize);
        }


        public async Task<(int total, DataTable datos)> ObtenerOposicionesNacionalesRecibidasCombinado(string situacionActual, int currentPageIndex, int pageSize)
        {
            return await oposicionDao.ObtenerOposicionesNacionalesRecibidasCombinado(situacionActual, currentPageIndex, pageSize);
        }

        //internacionales
        public async Task<(int total, DataTable datos)> ObtenerOposicionesInternacionalesInterpuestasCombinado(string situacionActual, int currentPageIndex, int pageSize)
        {
            return await oposicionDao.ObtenerOposicionesNacionalesInterpuestasCombinado(situacionActual, currentPageIndex, pageSize);
        }

        public async Task<(int total, DataTable datos)> ObtenerOposicionesInternacionalesRecibidasCombinado(string situacionActual, int currentPageIndex, int pageSize)
        {
            return await oposicionDao.ObtenerOposicionesInternacionalesRecibidasCombinado(situacionActual, currentPageIndex, pageSize);
        }

        public async Task<string?> ObtenerTipoOposicion(int idOp)
        {
            return await oposicionDao.ObtenerTipoOposicion(idOp);
        }

        public async Task Oposicion_a_abandono(DateTime fecha,
            string anotaciones, string usuario, int idMarca, int idOposicion)
        {
            await oposicionDao.MandarMarcaAbandonoOposicionTerminar(fecha, anotaciones, usuario,
                idMarca, idOposicion);
        }

        public async Task Oposicion_a_desistimiento(DateTime fecha,
            string anotaciones, string usuario, int idMarca, int idOposicion)
        {
            await oposicionDao.MandarMarcaDesistimientoOposicionTerminar(fecha, anotaciones, usuario,
                idMarca, idOposicion);
        }

        public async Task<DataTable> FiltrarOposiciones(
          string tipo_filtro,
          string expediente, string solicitante, string signoPretendido,
          string signoDistintivo, string clase, string opositor, string signoOpositor,
          string estado, string situacionActual, string tipo, string tipoOposicion)
        {
            return await oposicionDao.FiltrarOposiciones(
                tipo_filtro, expediente, solicitante, signoPretendido,
                signoDistintivo, clase, opositor, signoOpositor,
                estado, situacionActual, tipo, tipoOposicion
                );
        }

        //internacionales recibidas
        
        public async Task<int> GetFilteredMarcasInternacionalesRecibidasCount(string value)
        {
            return await oposicionDao.GetFilteredOposicionesInternacionalesRecibidasCount(value);
        }
        public async Task<DataTable> FiltrarOposicionesInternacionalesRecibidas(string filtro, int currentPageIndex, int pageSize)
        {
            return await oposicionDao.FiltrarOposicionesInternacionalesRecibidas(filtro, currentPageIndex, pageSize);
        }
       
        //internacionales internpuestas
        
        public async Task<int> GetFilteredOposicionesInternacionalesInterpuestasCount(string value)
        {
            return await oposicionDao.GetFilteredOposicionesInternacionalesInterpuestasCount(value);
        }

        public async Task<DataTable> FiltrarOposicionesInternacionalesInterpuestas(string filtro, int currentPageIndex, int pageSize)
        {
            return await oposicionDao.FiltrarOposicionesInternacionalesInterpuestas(filtro, currentPageIndex, pageSize);
        }
        
        
        public async Task<int> GetFilteredOposicionesNacionalesRecibidasCount(string value)
        {
            return await oposicionDao.GetFilteredOposicionesNacionalesRecibidasCount(value);
        }

        public async Task<DataTable> FiltrarOposicionesNacionalesRecibidas(string filtro, int currentPageIndex, int pageSize)
        {
            return await oposicionDao.FiltrarOposicionesNacionalesRecibidas(filtro, currentPageIndex, pageSize);
        }
        
       
        public async Task<int> GetFilteredOposicionesNacionalesInterpuestasCount(string value)
        {
            return await oposicionDao.GetFilteredOposicionesNacionalesInterpuestasCount(value);
        }
        public async Task<DataTable> FiltrarOposicionesNacionalesInterpuestas(string filtro, int currentPageIndex, int pageSize)
        {
            return await oposicionDao.FiltrarOposicionesNacionalesInterpuestas(filtro, currentPageIndex, pageSize);
        }
        
        public async Task<int?> CrearOposicion(
            string expediente,
            string signoPretendido,
            string signoDistintivo,
            string clase,
            string solicitanteSignoPretendido,
            int? idSolicitante,
            int? idOpositor,
            string opositor,
            string signoOpositor,
            string situacionActual,
            int? idMarca,
            byte[] logoOpositor,
            byte[] logoSignoPretendido,
            string tipo,
            string tipoOposicion)
        {
            return await oposicionDao.AddOposicion(expediente, signoPretendido, signoDistintivo,
                clase, solicitanteSignoPretendido, idSolicitante, idOpositor, opositor, signoOpositor,
                situacionActual, idMarca, logoOpositor, logoSignoPretendido, tipo, tipoOposicion);
        }


        public async Task<DataTable> GetOposicionPorId(int idOposicion)
        {
            DataTable tabla = new DataTable();
            tabla = await oposicionDao.GetOposicionPorId(idOposicion);
            return tabla;
        }

        public async Task<byte[]> ObtenerLogoOpositorAsync(int idOposicion)
        {
            try
            {
                return await oposicionDao.ObtenerLogoOposicionAsync(idOposicion, "opositor");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener logo opositor: {ex.Message}");
                return null;
            }
        }

        public async Task<byte[]> ObtenerLogoSignoPretendidoAsync(int idOposicion)
        {
            try
            {
                return await oposicionDao.ObtenerLogoOposicionAsync(idOposicion, "pretendido");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener logo signo pretendido: {ex.Message}");
                return null;
            }
        }

        

        public async Task<bool> EditarOposicion(
           int idOposicion,
           string expediente,
           string signoPretendido,
           string signoDistintivo,
           string clase,
           string solicitanteSignoPretendido,
           int? idOpositor,
           string signoOpositor,
           string situacionActual,
           int? idMarca,
           byte[] logoOpositor,
           byte[] logoSignoPretendido,
           string opositor,
           int? idSolicitante)
        {
            return await oposicionDao.EditOposicion(
                idOposicion,
                expediente,
                signoPretendido,
                signoDistintivo,
                clase,
                solicitanteSignoPretendido,
                idOpositor,
                signoOpositor,
                situacionActual,
                idMarca,
                logoOpositor,
                logoSignoPretendido,
                opositor,
                idSolicitante);
        }

        public async Task<bool> CambiarSituacionActualATerminada(int id)
        {
            return await oposicionDao.CambiarSituacionActualATerminada(id);
        }

    }
}


/*
using AccesoDatos;
using AccesoDatos.Entidades;
using System.Data;

namespace Dominio
{
    public class OposicionModel:ConnectionSQL
    {
        private OposicionesDao oposicionDao;

        public OposicionModel()
        {
            oposicionDao = new OposicionesDao();
        }

        public (int total, DataTable datos) ObtenerOposicionesNacionalesInterpuestasCombinado(string situacionActual, int currentPageIndex, int pageSize)
        {
            return oposicionDao.ObtenerOposicionesNacionalesInterpuestasCombinado(situacionActual, currentPageIndex, pageSize);
        }


        public (int total, DataTable datos) ObtenerOposicionesNacionalesRecibidasCombinado(string situacionActual, int currentPageIndex, int pageSize)
        {
            return oposicionDao.ObtenerOposicionesNacionalesRecibidasCombinado(situacionActual, currentPageIndex, pageSize);
        }

        //internacionales
        public (int total, DataTable datos) ObtenerOposicionesInternacionalesInterpuestasCombinado(string situacionActual, int currentPageIndex, int pageSize)
        {
            return oposicionDao.ObtenerOposicionesInternacionalesInterpuestasCombinado(situacionActual, currentPageIndex, pageSize);
        }


        public (int total, DataTable datos) ObtenerOposicionesInternacionalesRecibidasCombinado(string situacionActual, int currentPageIndex, int pageSize)
        {
            return oposicionDao.ObtenerOposicionesInternacionalesRecibidasCombinado(situacionActual, currentPageIndex, pageSize);
        }
        public string ObtenerTipoOposicion(int idOp)
        {
            return oposicionDao.ObtenerTipoOposicion(idOp);
        }
        public void Oposicion_a_abandono(DateTime fecha, 
            string anotaciones, string usuario, int idMarca, int idOposicion)
        {
            oposicionDao.MandarMarcaAbandonoOposicionTerminar(fecha, anotaciones, usuario,
                idMarca, idOposicion);
        }

        public void Oposicion_a_desistimiento(DateTime fecha,
            string anotaciones, string usuario, int idMarca, int idOposicion)
        {
            oposicionDao.MandarMarcaDesistimientoOposicionTerminar(fecha, anotaciones, usuario,
                idMarca, idOposicion);
        }
        public DataTable FiltrarOposiciones(
          string tipo_filtro,
          string expediente, string solicitante, string signoPretendido,
          string signoDistintivo, string clase, string opositor, string signoOpositor,
          string estado, string situacionActual, string tipo, string tipoOposicion)
        {
            return oposicionDao.FiltrarOposiciones(
                tipo_filtro, expediente, solicitante, signoPretendido,
                signoDistintivo, clase, opositor, signoOpositor,
                estado, situacionActual, tipo, tipoOposicion
                );
        }
        //internacionales recibidas
        public int GetTotalOposicionesInternacionalesRecibidas(string situacion)
        {
            return oposicionDao.GetTotalOposicionesInternacionalesRecibidas(situacion);
        }
        public int GetFilteredMarcasInternacionalesRecibidasCount(string value)
        {
            return oposicionDao.GetFilteredOposicionesInternacionalesRecibidasCount(value);
        }
        public DataTable FiltrarOposicionesInternacionalesRecibidas(string filtro, int currentPageIndex, int pageSize)
        {
            return oposicionDao.FiltrarOposicionesInternacionalesRecibidas(filtro, currentPageIndex, pageSize);
        }
        public DataTable GetAllOposicionesInternacionalesRecibidas(string situacionActual, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = oposicionDao.GetAllOposicionesInternacionalesRecibidas(situacionActual, currentPageIndex, pageSize);
            return tabla;
        }
        //internacionales internpuestas
        public int GetTotalOposicionesInteracionalesInterpuestas(string situacion)
        {
            return oposicionDao.GetTotalOposicionesInternacionalesInterpuestas(situacion);
        }
        public int GetFilteredOposicionesInternacionalesInterpuestasCount(string value)
        {
            return oposicionDao.GetFilteredOposicionesInternacionalesInterpuestasCount(value);
        }
        public DataTable FiltrarOposicionesInternacionalesInterpuestas(string filtro, int currentPageIndex, int pageSize)
        {
            return oposicionDao.FiltrarOposicionesInternacionalesInterpuestas(filtro, currentPageIndex, pageSize);
        }
        public DataTable GetAllOposicionesInternacionalesInterpuestas(string situacionActual, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = oposicionDao.GetAllOposicionesInternacionalesInterpuestas(situacionActual, currentPageIndex, pageSize);
            return tabla;
        }
        public int GetTotalOposicionesNacionalesRecibidas(string situacion)
        {
            return oposicionDao.GetTotalOposicionesNacionalesRecibidas(situacion);
        }
        public int GetFilteredOposicionesNacionalesRecibidasCount(string value)
        {
            return oposicionDao.GetFilteredOposicionesNacionalesRecibidasCount(value);
        }
        public DataTable FiltrarOposicionesNacionalesRecibidas(string filtro, int currentPageIndex, int pageSize)
        {
            return oposicionDao.FiltrarOposicionesNacionalesRecibidas(filtro, currentPageIndex, pageSize);
        }
        public DataTable GetAllOposicionesNacionales(string situacionActual, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = oposicionDao.GetAllOposicionesNacionales(situacionActual, currentPageIndex, pageSize);
            return tabla;
        }
        public int GetTotalOposicionesNacionalesInterpuestas(string situacion)
        {
            return oposicionDao.GetTotalOposicionesNacionalesInterpuestas(situacion);
        }
        public int GetFilteredOposicionesNacionalesInterpuestasCount(string value)
        {
            return oposicionDao.GetFilteredOposicionesNacionalesInterpuestasCount(value);
        }
        public DataTable FiltrarOposicionesNacionalesInterpuestas(string filtro, int currentPageIndex, int pageSize)
        {
            return oposicionDao.FiltrarOposicionesNacionalesInterpuestas(filtro, currentPageIndex, pageSize);
        }
        public DataTable GetAllOposicionesNacionalesInterpuestas(string situacionActual, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = oposicionDao.GetAllOposicionesNacionalesInterpuestas(situacionActual, currentPageIndex, pageSize);
            return tabla;
        }

        public int CrearOposicion(
            string expediente,
            string signoPretendido,
            string signoDistintivo,
            string clase,
            string solicitanteSignoPretendido,
            int? idSolicitante,
            int? idOpositor,
            string opositor,
            string signoOpositor,
            string situacionActual,
            int? idMarca,
            byte[] logoOpositor,
            byte[] logoSignoPretendido,
            string tipo, 
            string tipoOposicion)
        {
            return oposicionDao.AddOposicion(expediente, signoPretendido, signoDistintivo, 
                clase, solicitanteSignoPretendido,idSolicitante, idOpositor,opositor, signoOpositor, 
                situacionActual, idMarca, logoOpositor, logoSignoPretendido, tipo, tipoOposicion);
        }

        
        public DataTable GetOposicionPorId(int idOposicion)
        {
            DataTable tabla = new DataTable();
            tabla = oposicionDao.GetOposicionPorId(idOposicion);
            return tabla;
        }

        public bool EditarOposicion(
           int idOposicion,
           string expediente,
           string signoPretendido,
           string signoDistintivo,
           string clase,
           string solicitanteSignoPretendido,
           int? idOpositor,
           string signoOpositor,
           string situacionActual,
           int? idMarca,
           byte[] logoOpositor,
           byte[] logoSignoPretendido,
           string opositor,
           int? idSolicitante)
        {
            return oposicionDao.EditOposicion(
                idOposicion,
                expediente,
                signoPretendido,
                signoDistintivo,
                clase,
                solicitanteSignoPretendido,
                idOpositor,
                signoOpositor,
                situacionActual,
                idMarca,
                logoOpositor,
                logoSignoPretendido,
                opositor,
                idSolicitante);
        }

        public bool CambiarSituacionActualATerminada(int id)
        {
            return oposicionDao.CambiarSituacionActualATerminada(id);
        }

    }
}*/
