using ClashRoyaleAplication.DBModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashRoyaleAplication.Data
{
    public class ClanRepository : IClanRepository
    {
        private readonly clashroyaleContext _context;
        private readonly ILogger<ClanRepository> _logger; 
        public ClanRepository(clashroyaleContext context , ILogger<ClanRepository> logger)
        {
            _context = context;
            _logger = logger; 
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity); 
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity); 
        }

        public async Task<Clan[]> GetAllClansAsync()
        {
            IQueryable<Clan> query = _context.Clans;

            return await query.ToArrayAsync(); 
        }

        public async  Task<Clan> GetClanAsync(string nombre)
        {
            IQueryable<Clan> query = _context.Clans.Where(c => c.Nombre == nombre);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Clan[]> GetAllClanesByRegion (string region )
        {
            IQueryable<Clan> query = _context.Clans
                .Where(c => c.Region == region)
                .OrderBy(c => c.CantidadDeTrofeos);


            return await query.ToArrayAsync(); 
        }

        public async  Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync())>0; 
        }

        public async  Task<Cartum[]> GetAllCartasFavoritas(Clan clan)
        {
            //var cartasfavoritas = clan.Miembros.Select(x => x.IdJugadorNavigation);

            //var cartaspreferidas = cartasfavoritas.Select(x => x.CartaPreferidaActualNavigation).Max();

            /*return  cartaspreferidas;*/

            List<Jugador> jugadoresdelclan = _context.Miembros.Where(x => x.IdClan == clan.IdClan).Select(x=>x.IdJugadorNavigation).ToList();

            var groups = jugadoresdelclan.GroupBy(x => x.CartaPreferidaActual);
            var contador = groups.OrderByDescending(x => x.Count()).First(); 

            var carta = _context.Carta.Where(x => x.IdCarta == contador.Key);



            return await carta.ToArrayAsync();
        }
    }
}
