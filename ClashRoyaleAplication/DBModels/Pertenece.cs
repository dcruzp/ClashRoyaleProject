using System;
using System.Collections.Generic;

#nullable disable

namespace ClashRoyaleAplication.DBModels
{
    public partial class Pertenece
    {
        public Guid IdJugador { get; set; }
        public Guid IdClan { get; set; }

        public virtual Clan IdClanNavigation { get; set; }
        public virtual Jugador IdJugadorNavigation { get; set; }
    }
}
