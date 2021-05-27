using System;
using System.Collections.Generic;

#nullable disable

namespace ClashRoyaleAplication.DBModels
{
    public partial class ParticipaEn
    {
        public Guid IdClan { get; set; }
        public Guid IdGuerraClanes { get; set; }
        public int TrofeosGanados { get; set; }
        public DateTime FechaComienzo { get; set; }

        public virtual Clan IdClanNavigation { get; set; }
        public virtual GuerradeClane IdGuerraClanesNavigation { get; set; }
    }
}
