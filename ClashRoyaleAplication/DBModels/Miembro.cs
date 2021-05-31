using System;
using System.Collections.Generic;

#nullable disable

namespace ClashRoyaleAplication.DBModels
{
    public partial class Miembro
    {
        public Miembro()
        {
            Donars = new HashSet<Donar>();
        }

        public Guid IdJugador { get; set; }
        public Guid IdMiembro { get; set; }
        public Guid IdClan { get; set; }
        public string Cargo { get; set; }

        public virtual Clan IdClanNavigation { get; set; }
        public virtual Jugador IdJugadorNavigation { get; set; }
        public virtual ICollection<Donar> Donars { get; set; }
    }
}
