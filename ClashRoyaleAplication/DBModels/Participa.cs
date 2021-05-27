using System;
using System.Collections.Generic;

#nullable disable

namespace ClashRoyaleAplication.DBModels
{
    public partial class Participa
    {
        public Guid IdJugador { get; set; }
        public Guid IdDesafio { get; set; }
        public int CantidadDePremios { get; set; }
        public DateTime FechadeComienzo { get; set; }

        public virtual Desafio IdDesafioNavigation { get; set; }
        public virtual Jugador IdJugadorNavigation { get; set; }
    }
}
