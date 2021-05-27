using System;
using System.Collections.Generic;

#nullable disable

namespace ClashRoyaleAplication.DBModels
{
    public partial class Jugador
    {
        public Jugador()
        {
            BatallaIdJugador1Navigations = new HashSet<Batalla>();
            BatallaIdJugador2Navigations = new HashSet<Batalla>();
            Dispones = new HashSet<Dispone>();
            LuchaIdJugador1Navigations = new HashSet<Lucha>();
            LuchaIdJugador2Navigations = new HashSet<Lucha>();
            Miembros = new HashSet<Miembro>();
            Participas = new HashSet<Participa>();
            Perteneces = new HashSet<Pertenece>();
        }

        public Guid IdJugador { get; set; }
        public string Nombre { get; set; }
        public int CantidadVictorias { get; set; }
        public int MaximoTrofeos { get; set; }
        public int Nivel { get; set; }
        public int CantidadTrofeos { get; set; }
        public int CantidadCartasEncontradas { get; set; }
        public Guid? CartaPreferidaActual { get; set; }

        public virtual Cartum CartaPreferidaActualNavigation { get; set; }
        public virtual ICollection<Batalla> BatallaIdJugador1Navigations { get; set; }
        public virtual ICollection<Batalla> BatallaIdJugador2Navigations { get; set; }
        public virtual ICollection<Dispone> Dispones { get; set; }
        public virtual ICollection<Lucha> LuchaIdJugador1Navigations { get; set; }
        public virtual ICollection<Lucha> LuchaIdJugador2Navigations { get; set; }
        public virtual ICollection<Miembro> Miembros { get; set; }
        public virtual ICollection<Participa> Participas { get; set; }
        public virtual ICollection<Pertenece> Perteneces { get; set; }
    }
}
