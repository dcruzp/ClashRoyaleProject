using System;
using System.Collections.Generic;

#nullable disable

namespace ClashRoyaleAplication.DBModels
{
    public partial class Batalla
    {
        public Guid IdJugador1 { get; set; }
        public Guid IdJugador2 { get; set; }
        public Guid IdBatalla { get; set; }
        public Guid IdGanador { get; set; }
        public DateTime Duracion { get; set; }

        public virtual Jugador IdJugador1Navigation { get; set; }
        public virtual Jugador IdJugador2Navigation { get; set; }
    }
}
