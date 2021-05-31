using ClashRoyaleAplication.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashRoyaleAplication.Data
{
    public interface IJugadorRepository
    {
        public void Add<T>(T entity) where T : class;

        public void Delete<T>(T entity) where T : class; 

        public Task<bool> SaveChangesAsync();

        public Task<Jugador[]> GetAllJugadoresAsync();

        public Task<Jugador> GetJugadorAsync(string nombre);

        public Task<Clan[]> GetAllClanesToJoin(Jugador jugador, string tipo);

        public Task<Clan> GetClanAsociateAsync(Jugador jugador);

    }
}
