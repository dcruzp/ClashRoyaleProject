using AutoMapper;
using ClashRoyaleAplication.DBModels;
using ClashRoyaleAplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashRoyaleAplication.Data
{
    public class ClashRoyaleProfile: Profile
    {
        public ClashRoyaleProfile()
        {
            this.CreateMap<Jugador, JugadorModels>();
            this.CreateMap<Jugador, JugadorModels>().ReverseMap();

            this.CreateMap<Cartum, CartaModels>();
            this.CreateMap<Cartum, CartaModels>().ReverseMap();

            this.CreateMap<Clan, ClanModels>();
            this.CreateMap<Clan, ClanModels>().ReverseMap();

            this.CreateMap<Desafio, DesafioModels>();
            this.CreateMap<DesafioModels, Desafio>().ReverseMap(); 
        }
    }
}
