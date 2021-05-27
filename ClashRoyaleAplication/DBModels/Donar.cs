using System;
using System.Collections.Generic;

#nullable disable

namespace ClashRoyaleAplication.DBModels
{
    public partial class Donar
    {
        public Guid IdJugador { get; set; }
        public Guid IdMiembro { get; set; }
        public Guid IdCarta { get; set; }
        public int Cantidad { get; set; }
    }
}
