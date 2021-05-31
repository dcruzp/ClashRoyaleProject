using ClashRoyaleAplication.DBModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashRoyaleAplication.Data
{
    public class DonarRepository
    {
        private readonly clashroyaleContext _context;
        private readonly ILogger<DonarRepository> _logger;

        public DonarRepository(clashroyaleContext context, ILogger<DonarRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Task<Cartum[]> GetAllCartasMostGivenByRegion(string region , DateTime fecha)
        {

            var idMiembro = (from clanes in _context.Clans
                        join miembro in _context.Miembros on clanes.IdClan equals miembro.IdClan
                        where clanes.Region == region
                        select miembro.IdMiembro).ToList();


            var idDonacion = from idmiembro in idMiembro
                        join iddonar in _context.Donars on idmiembro equals iddonar.IdMiembro
                        select iddonar.IdMiembro;

            /*         
             var query = from idmiembro in */

            Guid guid = new Guid("");

            //var query = from donacion in _context.Donars
            //            join clan in _context.Clans on donacion.IdClan equals clan.IdClan
            //            where clan.Region == region && donacion.FechaDonacion > fecha
            //            select donacion into todaslasdonaciones

            //            from donaciones1 in todaslasdonaciones
            //            select donaciones1 into 


            //var query = _context.Clans.Join(_context.Miembros,
            //                                clanes => _context.Clans.Where(x => x.Region == region),
            //                                miembros => _context.Miembros,
            //                                (clanes, miembro) => new { clanid = clanes.IdClan, miembro.IdMiembro });

            //var donaciones = _context.Donars.Join()

            throw new Exception(); 
        }
    }
}
