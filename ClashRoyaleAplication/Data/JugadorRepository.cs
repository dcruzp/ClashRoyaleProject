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
    public class JugadorRepository : IJugadorRepository
    {

        private readonly clashroyaleContext _context;
        private readonly ILogger<JugadorRepository> _logger; 

        public JugadorRepository(clashroyaleContext context , ILogger<JugadorRepository> logger )
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

        public async Task<Clan[]> GetAllClanesToJoin(Jugador jugador , string tipo)
        {

            IQueryable<Clan> query = _context.Clans.Where(x => x.Tipo == tipo && x.CantidadDeTrofeos <= jugador.CantidadTrofeos);

            return await query.ToArrayAsync(); 
        }

        public async Task<Jugador[]> GetAllJugadoresAsync()
        {
            IQueryable<Jugador> query = _context.Jugadors;

            return await query.ToArrayAsync();

        }

        public async Task<Clan> GetClanAsociateAsync(Jugador jugador)
        {
            IQueryable<Clan> query = _context.Miembros
                .Where(x => x.IdJugador == jugador.IdJugador)
                .Select(x => x.IdClanNavigation);

            return await query.FirstOrDefaultAsync(); 
        }

        public async Task<Jugador> GetJugadorAsync(string nombre)
        {
            IQueryable<Jugador> query = _context.Jugadors.Where(x => x.Nombre == nombre) ;
            
            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
