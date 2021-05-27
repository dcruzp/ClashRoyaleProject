using System;
using System.Collections.Generic;

#nullable disable

namespace ClashRoyaleAplication.DBModels
{
    public partial class Cartum
    {
        public Cartum()
        {
            Dispones = new HashSet<Dispone>();
            Jugadors = new HashSet<Jugador>();
        }

        public Guid IdCarta { get; set; }
        public string Nombre { get; set; }
        public int CostodeElixir { get; set; }
        public string Descripcion { get; set; }
        public string Calidad { get; set; }

        public virtual ICollection<Dispone> Dispones { get; set; }
        public virtual ICollection<Jugador> Jugadors { get; set; }
    }
}
