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
    public class CartaRepository:ICartaRepository
    {
        private readonly clashroyaleContext _context;
        private readonly ILogger<CartaRepository> _logger;

        public CartaRepository(clashroyaleContext context, ILogger<CartaRepository> logger)
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

        public async Task<Cartum[]> GetAllCartasAsync()
        {
            IQueryable<Cartum> query = _context.Carta;

            return await query.ToArrayAsync();
        }

        public async Task<Cartum> GetCartaAsync(string nombre)
        {
            IQueryable<Cartum> query = _context.Carta.Where(c => c.Nombre == nombre);

            return await query.FirstOrDefaultAsync(); 
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
