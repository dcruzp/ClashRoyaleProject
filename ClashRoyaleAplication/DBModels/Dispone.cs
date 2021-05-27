using System;
using System.Collections.Generic;

#nullable disable

namespace ClashRoyaleAplication.DBModels
{
    public partial class Dispone
    {
        public Guid IdJugador { get; set; }
        public Guid IdCarta { get; set; }
        public int Nivel { get; set; }

        public virtual Cartum IdCartaNavigation { get; set; }
        public virtual Jugador IdJugadorNavigation { get; set; }
    }
}
