using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.Clases
{
    public class FtpFileInfo
    {
        public string Nombre { get; set; }
        public string RutaCompleta { get; set; }
        public DateTime FechaModificacion { get; set; }
        public long Tamaño { get; set; }
    }
}
