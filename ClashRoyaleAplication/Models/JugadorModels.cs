using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashRoyaleAplication.Models
{
    public class JugadorModels
    {
        public string Nombre { get; set; }
        public int CantidadVictorias { get; set; }
        public int MaximoTrofeos { get; set; }
        public int Nivel { get; set; }
        public int CantidadTrofeos { get; set; }
        public int CantidadCartasEncontradas { get; set; }
        public Guid? CartaPreferidaActual { get; set; }
    }
}
