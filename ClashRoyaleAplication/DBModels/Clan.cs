using System;
using System.Collections.Generic;

#nullable disable

namespace ClashRoyaleAplication.DBModels
{
    public partial class Clan
    {
        public Clan()
        {
            Miembros = new HashSet<Miembro>();
            ParticipaEns = new HashSet<ParticipaEn>();
            Perteneces = new HashSet<Pertenece>();
        }

        public Guid IdClan { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public int CantidadDeMiembros { get; set; }
        public string Region { get; set; }
        public string Descripcion { get; set; }
        public string Trofeos { get; set; }
        public int CantidadDeTrofeos { get; set; }

        public virtual ICollection<Miembro> Miembros { get; set; }
        public virtual ICollection<ParticipaEn> ParticipaEns { get; set; }
        public virtual ICollection<Pertenece> Perteneces { get; set; }
    }
}
