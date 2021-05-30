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
    public class GuerraDeClanesRepository : IGuerraDeClanesRepository
    {
        private readonly clashroyaleContext _context;
        private readonly ILogger<GuerraDeClanesRepository> _logger; 
        public GuerraDeClanesRepository(clashroyaleContext context , ILogger<GuerraDeClanesRepository> logger)
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

        public async Task<GuerradeClane[]> GetAllGuerraDeClanesAsync()
        {
            IQueryable<GuerradeClane> query = _context.GuerradeClanes/*.Include(x => x.ParticipaEns).ThenInclude(x => x.IdClanNavigation)*/;
            
            return await query.ToArrayAsync(); 
        }

        public async Task<GuerradeClane> GetGuerraDeClanesAsync(string nombre)
        {
            IQueryable<GuerradeClane> query = _context.GuerradeClanes
                .Where(x => x.Nombre == nombre)
                .Include(x => x.ParticipaEns)
                .ThenInclude(x => x.IdClanNavigation);

            return await query.FirstOrDefaultAsync(); 
        }

        public async  Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public Jugador GetBestPlayerByTrofeos (Clan clan)
        {
            List<Miembro> miembros = clan.Miembros.ToList();

            List<Jugador> jugadores = (from maxjug in miembros.Select(x => x.IdJugadorNavigation)
                           orderby maxjug.CantidadTrofeos descending
                           select maxjug).ToList();

            return jugadores.FirstOrDefault();  
        }
       
    }
}
