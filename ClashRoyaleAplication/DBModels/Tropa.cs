using System;
using System.Collections.Generic;

#nullable disable

namespace ClashRoyaleAplication.DBModels
{
    public partial class Tropa
    {
        public Guid IdTropa { get; set; }
        public int Puntos { get; set; }
        public int Cantidadunidades { get; set; }
        public int DanoenArea { get; set; }
    }
}
