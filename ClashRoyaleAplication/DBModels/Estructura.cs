using System;
using System.Collections.Generic;

#nullable disable

namespace ClashRoyaleAplication.DBModels
{
    public partial class Estructura
    {
        public Guid IdEstructura { get; set; }
        public int PuntosdeVida { get; set; }
        public int VelocidadAtaque { get; set; }
        public int DañoDistancia { get; set; }
    }
}
