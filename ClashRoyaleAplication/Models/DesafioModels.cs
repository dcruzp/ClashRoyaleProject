using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashRoyaleAplication.Models
{
    public class DesafioModels
    {
        public string Nombre { get; set; }
        public int CantidadDePremios { get; set; }
        public int NivelMinimo { get; set; }
        public string Descripcion { get; set; }
        public int? Costo { get; set; }
        public int CantidaddeDerrotas { get; set; }
        public DateTime TiempoDeDuracion { get; set; }
    }
}
