using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashRoyaleAplication.Models
{
    public class ClanModels
    {
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public int CantidadDeMiembros { get; set; }
        public string Region { get; set; }
        public string Descripcion { get; set; }
        public string Trofeos { get; set; }
        public int CantidadDeTrofeos { get; set; }

        public CartaModels[] CartaFavorita { get; set;  }

    }
}
