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
    public class DesafioRepository : IDesafioRepository
    {
        private readonly clashroyaleContext _context;
        private readonly ILogger<DesafioRepository> _logger; 

        public DesafioRepository (clashroyaleContext context , ILogger<DesafioRepository> logger)
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

        public async Task<Desafio[]> GetAllDesafiosAsync()
        {
            IQueryable<Desafio> query = _context.Desafios;

            return await query.ToArrayAsync(); 
        }

        public async Task<Desafio> GetDesafioAsync(string nombre)
        {
            IQueryable<Desafio> query = _context.Desafios.Where(d => d.Nombre == nombre);

            return await query.FirstOrDefaultAsync();
        }

        public async  Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
