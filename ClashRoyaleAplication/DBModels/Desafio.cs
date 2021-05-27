using System;
using System.Collections.Generic;

#nullable disable

namespace ClashRoyaleAplication.DBModels
{
    public partial class Desafio
    {
        public Desafio()
        {
            Participas = new HashSet<Participa>();
        }

        public Guid IdDesafio { get; set; }
        public string Nombre { get; set; }
        public int CantidadDePremios { get; set; }
        public int NivelMinimo { get; set; }
        public string Descripcion { get; set; }
        public int? Costo { get; set; }
        public int CantidaddeDerrotas { get; set; }
        public DateTime TiempoDeDuracion { get; set; }

        public virtual ICollection<Participa> Participas { get; set; }
    }
}
