using ClashRoyaleAplication.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashRoyaleAplication.Data
{
    public interface IClanRepository
    {
        public void Add<T>(T entity) where T : class;

        public void Delete<T>(T entity) where T : class;

        public Task<bool> SaveChangesAsync();

        public Task<Clan[]> GetAllClansAsync();

        public Task<Clan> GetClanAsync(string nombre);

        public Task<Clan[]> GetAllClanesByRegion(string region);
    }
}
