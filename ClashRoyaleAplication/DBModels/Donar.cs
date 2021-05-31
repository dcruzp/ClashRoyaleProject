using System;
using System.Collections.Generic;

#nullable disable

namespace ClashRoyaleAplication.DBModels
{
    public partial class Donar
    {
        public Guid IdMiembro { get; set; }
        public Guid IdCarta { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaDonacion { get; set; }
        public Guid IdJugador { get; set; }
        public Guid IdClan { get; set; }

        public virtual Miembro Id { get; set; }
        public virtual Cartum IdCartaNavigation { get; set; }
    }
}
