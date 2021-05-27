using System;
using System.Collections.Generic;

#nullable disable

namespace ClashRoyaleAplication.DBModels
{
    public partial class GuerradeClane
    {
        public GuerradeClane()
        {
            ParticipaEns = new HashSet<ParticipaEn>();
        }

        public Guid IdGuerraClanes { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<ParticipaEn> ParticipaEns { get; set; }
    }
}
