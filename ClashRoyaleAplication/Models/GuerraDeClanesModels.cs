using ClashRoyaleAplication.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashRoyaleAplication.Models
{
    public class GuerraDeClanesModels
    {
        public string Nombre { get; set; }

        public ICollection<ClanModels> Clanes { get; set; }

    }
}
