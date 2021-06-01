using ClashRoyaleAplication.Data;
using ClashRoyaleAplication.DBModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ClashRoyaleAplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        readonly string React_Policy = "React_Policy";
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<clashroyaleContext>(options => options.UseInMemoryDatabase("database"));


            services.AddScoped<IJugadorRepository, JugadorRepository>();
            services.AddScoped<IClanRepository, ClanRepository>();
            services.AddScoped<ICartaRepository, CartaRepository>();
            services.AddScoped<IDesafioRepository, DesafioRepository>();
            services.AddScoped<IGuerraDeClanesRepository, GuerraDeClanesRepository>(); 

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddControllers();
            services.AddCors(o => o.AddPolicy(React_Policy, builder =>
            {
                builder.SetIsOriginAllowed(isOriginAllowed: _ => true).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
            }));

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

       
      






        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env , clashroyaleContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                   ForwardedHeaders.XForwardedProto
            });

            app.UseCors(React_Policy);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            PutDataIntoDatabase(context); 
        }



        /// <summary>
        /// Esto es para hacer una prueba con la base de datos en memoria
        /// </summary>
        /// <param name="context"></param>
        private void PutDataIntoDatabase (clashroyaleContext context)
        {
            if (!context.Jugadors.Any())
            {
                context.Jugadors.AddRange(new List<Jugador>
                {
                    new Jugador { Nombre = "Daniel" , CantidadVictorias =  2 , MaximoTrofeos =  2 , Nivel = 4  , CantidadTrofeos = 4 , CantidadCartasEncontradas = 4  },
                    new Jugador { Nombre = "Pedro" , CantidadVictorias =  3 , MaximoTrofeos =  22 , Nivel = 3  , CantidadTrofeos = 1 , CantidadCartasEncontradas = 7  },
                    new Jugador { Nombre = "Alejandro" , CantidadVictorias =  1 , MaximoTrofeos =  5 , Nivel = 7 , CantidadTrofeos = 8 , CantidadCartasEncontradas = 9  },
                    new Jugador { Nombre = "Perico" , CantidadVictorias =  6 , MaximoTrofeos =  8 , Nivel = 3  , CantidadTrofeos = 7, CantidadCartasEncontradas = 10  },
                    new Jugador { Nombre = "David" , CantidadVictorias =  3 , MaximoTrofeos =  9 , Nivel = 4  , CantidadTrofeos = 4 , CantidadCartasEncontradas = 4  },
                    new Jugador { Nombre = "Alexander" , CantidadVictorias =  1 , MaximoTrofeos =  3 , Nivel = 1 , CantidadTrofeos = 2 , CantidadCartasEncontradas = 1  },
                    new Jugador { Nombre = "Alicia" , CantidadVictorias =  2, MaximoTrofeos =  9 , Nivel = 4  , CantidadTrofeos = 4 , CantidadCartasEncontradas = 4  },
                    new Jugador { Nombre = "Neivis" , CantidadVictorias =  4 , MaximoTrofeos =  1 , Nivel = 5  , CantidadTrofeos = 4 , CantidadCartasEncontradas = 4  },
                    new Jugador { Nombre = "Dariel" , CantidadVictorias =  1 , MaximoTrofeos =  4 , Nivel = 6  , CantidadTrofeos = 2 , CantidadCartasEncontradas = 5  },
                    new Jugador { Nombre = "Danilo" , CantidadVictorias =  9 , MaximoTrofeos =  3 , Nivel = 3  , CantidadTrofeos = 2 , CantidadCartasEncontradas = 5  },
                });
                context.SaveChanges();
            }

            if (!context.Carta.Any())
            {
                context.Carta.AddRange(new List<Cartum>() {
                    new Cartum { Nombre="XBow",  CostodeElixir=6 , Calidad="Epic", Descripcion = "It is a single-target, long-ranged offensive and defensive building with both moderate hitpoints and damage output. It appears to be a crossbow with wooden, stone, and metallic features along with a shining steel bolt for ammunition"},
                    new Cartum { Nombre="Minions" ,CostodeElixir=3 , Calidad="Common", Descripcion = "It spawns three single-target, short-ranged, flying Minions with relatively low hitpoints and moderate damage. Their appearance is that of a bluish-purple gargoyle with large horns, stubby wings, and large hands with sharp vicious claws."},
                    new Cartum { Nombre="Fireball",  CostodeElixir=4 , Calidad="Especial", Descripcion = "It is an area damage spell with a medium radius and moderately high damage, and it will push back certain small troops." },
                     new Cartum { Nombre="Musketeer",  CostodeElixir=4 , Calidad="Especial", Descripcion = "She is a single-target, medium-ranged troop with both moderate hitpoints and damage. She has purple combed hair, a blunderbuss-styled musket and a metal helmet with a tiny crater on it" },
                      new Cartum { Nombre="Tesla",  CostodeElixir=4 , Calidad="Common", Descripcion = "It is a single-target, medium-ranged defensive building with both moderate hit points and damage. Tesla retracts when no targets are within range and gains immunity to most negative effects." },
                });
                context.SaveChanges(); 
            }

            if (!context.Clans.Any())
            {
                context.Clans.AddRange(new List<Clan>() {
                    new Clan { Nombre=  "Clan1" ,  Tipo =  "Open" , CantidadDeMiembros =  3 , Region= "America"  , Descripcion = "Esta es la descripcion del Clan1"   , Trofeos = "Trofeo1" , CantidadDeTrofeos =  1 },
                    new Clan { Nombre=  "Clan2" ,  Tipo =  "Invited" , CantidadDeMiembros =  4 , Region= "Asia" , Descripcion = "Esta es la descripcion del Clan2"   , Trofeos = "Trofeo3" , CantidadDeTrofeos =  4 },
                    new Clan { Nombre=  "Clan3" ,  Tipo =  "Open" , CantidadDeMiembros =  1 , Region= "Australia" , Descripcion = "Esta es la descripcion del Clan3"   , Trofeos = "Trofeo9" , CantidadDeTrofeos =  3 },
                    new Clan { Nombre=  "Clan4" ,  Tipo =  "Open" , CantidadDeMiembros =  3 , Region= "Europe" , Descripcion = "Esta es la descripcion del Clan4"   , Trofeos = "Trofeo2" , CantidadDeTrofeos =  1 },
                    new Clan { Nombre=  "Clan5" ,  Tipo =  "Open" , CantidadDeMiembros =  4 , Region= "Europe" , Descripcion = "Esta es la descripcion del Clan5"   , Trofeos = "Trofeo4" , CantidadDeTrofeos =  2 },
                    new Clan { Nombre=  "Clan6" ,  Tipo =  "Open" , CantidadDeMiembros =  5 , Region= "Africa" , Descripcion = "Esta es la descripcion del Clan6"   , Trofeos = "Trofeo5" , CantidadDeTrofeos =  1 },
                    new Clan { Nombre=  "Clan7" ,  Tipo =  "Invited" , CantidadDeMiembros =  6 , Region= "Europe" , Descripcion = "Esta es la descripcion del Clan7"   , Trofeos = "Trofeo6" , CantidadDeTrofeos =  4 },
                    new Clan { Nombre=  "Clan8" ,  Tipo =  "Invited" , CantidadDeMiembros =  7 , Region= "Australia" , Descripcion = "Esta es la descripcion del Clan8"   , Trofeos = "Trofeo7" , CantidadDeTrofeos =  6 },
                    new Clan { Nombre=  "Clan9" ,  Tipo =  "Open" , CantidadDeMiembros =  7 , Region= "Africa" , Descripcion = "Esta es la descripcion del Clan9"   , Trofeos = "Trofeo8" , CantidadDeTrofeos =  7 },
                    new Clan { Nombre=  "Clan10" ,  Tipo =  "Invited" , CantidadDeMiembros =  6 , Region= "America" , Descripcion = "Esta es la descripcion del Clan10"   , Trofeos = "Trofeo9" , CantidadDeTrofeos =  1 },

                });
                context.SaveChanges(); 
            }

            if (!context.GuerradeClanes.Any())
            {
                context.GuerradeClanes.AddRange(new List<GuerradeClane>() 
                {
                    new GuerradeClane { Nombre = "Guerra1" } , 
                    new GuerradeClane { Nombre = "Guerra2"},
                    new GuerradeClane { Nombre = "Guerra3" } ,
                    new GuerradeClane { Nombre = "Guerra4" } ,
                    new GuerradeClane { Nombre = "Guerra5" } ,
                    new GuerradeClane { Nombre = "Guerra6" } ,
                    new GuerradeClane { Nombre = "Guerra7" } ,
                    new GuerradeClane { Nombre = "Guerra8" } ,
                    new GuerradeClane { Nombre = "Guerra9" } ,
                    new GuerradeClane { Nombre = "Guerra10" } ,
                });
                context.SaveChanges(); 
            }

            if (!context.ParticipaEns.Any())
            {
                var guerradeclanes = context.GuerradeClanes.Where(x => x.Nombre== "Guerra1").FirstOrDefault();
                var clan = context.Clans.Where(x => x.Nombre == "Clan1").FirstOrDefault();

                ParticipaEn participaEn = new ParticipaEn();
                participaEn.IdClan = clan.IdClan;
                participaEn.IdGuerraClanes = guerradeclanes.IdGuerraClanes;
                participaEn.FechaComienzo = DateTime.Now;

                context.Add(participaEn);
                context.SaveChanges();
            }

            if (!context.Miembros.Any())
            {
                AsignarMiembro("Clan1", "Daniel" , context);
                AsignarMiembro("Clan1", "Pedro", context); 
            }

            AsignarCartaaJugador("XBow", "Daniel" , context);
            AsignarCartaaJugador("Minions", "Pedro", context);  

        }


        public void AsignarMiembro (string nombreclan , string nombrejugador , clashroyaleContext context)
        {
            var jugador = context.Jugadors.Where(x => x.Nombre == nombrejugador).FirstOrDefault();
            var clan = context.Clans.Where(x => x.Nombre == nombreclan).FirstOrDefault();

            Miembro miembro = new Miembro();

            miembro.IdJugador = jugador.IdJugador;
            miembro.IdClan = clan.IdClan;


            context.Add(miembro);
            context.SaveChanges();
        }

        public void AsignarCartaaJugador (string nombreCarta , string nombreJugador , clashroyaleContext context)
        {
            var Jugador = context.Jugadors.Where(x => x.Nombre == nombreJugador).FirstOrDefault();
            var Carta = context.Carta.Where(x => x.Nombre == nombreCarta).FirstOrDefault();

            if (Jugador != null && Carta != null)
                Jugador.CartaPreferidaActual = Carta.IdCarta;

            context.SaveChanges();
        }
    }
}
