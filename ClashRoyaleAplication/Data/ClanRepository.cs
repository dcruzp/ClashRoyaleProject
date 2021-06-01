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

        public async Task<Cartum[]> GetAllCartasFavoritas(Clan clan)
        {

            var jugadoresdelclan = _context.Miembros
                .Where(x => x.IdClan == clan.IdClan)
                .Select(x=>x.IdJugadorNavigation).ToList();

            if (jugadoresdelclan.Count == 0) return null;

            var result = from jugadores in jugadoresdelclan
                         where jugadores.CartaPreferidaActual != null
                         group jugadores by jugadores.CartaPreferidaActual into g
                         orderby g.Count() descending
                         select _context.Carta.Where(x => x.IdCarta == g.Key).ToArrayAsync();

            return await result.FirstOrDefault(); 
        }

        public async  Task<Jugador[]> GetAllJugadoresMember(Clan clan)
        {
            IQueryable<Jugador> query = _context.Miembros
                 .Where(x => x.IdClan == clan.IdClan)
                 .Select(x => x.IdJugadorNavigation);

            return await query.ToArrayAsync();
        }
    }
}
