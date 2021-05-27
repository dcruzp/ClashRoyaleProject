using System;
using System.Collections.Generic;

#nullable disable

namespace ClashRoyaleAplication.DBModels
{
    public partial class Hechizo
    {
        public Guid IdHechizo { get; set; }
        public int Radio { get; set; }
        public DateTime Duracion { get; set; }
        public int AreadeHechizo { get; set; }
        public int DañoTorres { get; set; }
    }
}
