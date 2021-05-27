using System;
using System.Collections.Generic;

#nullable disable

namespace ClashRoyaleAplication.DBModels
{
    public partial class Lucha
    {
        public Guid IdJugador1 { get; set; }
        public Guid IdJugador2 { get; set; }
        public DateTime DateTime { get; set; }

        public virtual Jugador IdJugador1Navigation { get; set; }
        public virtual Jugador IdJugador2Navigation { get; set; }
    }
}
