using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ClashRoyaleAplication.Data;
using ClashRoyaleAplication.DBModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddControllers();
            services.AddCors(o => o.AddPolicy(React_Policy, builder =>
            {
                builder.SetIsOriginAllowed(isOriginAllowed: _ => true).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
            }));
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
                    new Jugador { Nombre = "Daniel" , CantidadVictorias =  3 , MaximoTrofeos =  9 , Nivel = 4  , CantidadTrofeos = 4 , CantidadCartasEncontradas = 4  },
                });
                context.SaveChanges();
            }

            if (!context.Carta.Any())
            {
                context.Carta.AddRange(new List<Cartum>() {
                    new Cartum { Nombre = "Carta1" , CostodeElixir = 2 , Descripcion = "descricion de la Carta 1" , Calidad = "Alta" },
                    new Cartum { Nombre = "Carta2" , CostodeElixir = 6 , Descripcion = "descricion de la Carta 8" , Calidad = "Baja" },
                    new Cartum { Nombre = "Carta3" , CostodeElixir = 4 , Descripcion = "descricion de la Carta 6" , Calidad = "Alta" },
                });
                context.SaveChanges(); 
            }

            if (!context.Clans.Any())
            {
                context.Clans.AddRange(new List<Clan>() {
                    new Clan { Nombre=  "Clan1" ,  Tipo =  "Tipodeclan" , CantidadDeMiembros =  3 , Region= "Center"  , Descripcion = "Esta es la descripcion del Clan1"   , Trofeos = "Trofeo1" , CantidadDeTrofeos =  1 },
                    new Clan { Nombre=  "Clan2" ,  Tipo =  "Tipodeclan" , CantidadDeMiembros =  4 , Region= "Center" , Descripcion = "Esta es la descripcion del Clan2"   , Trofeos = "Trofeo3" , CantidadDeTrofeos =  4 },
                });
                context.SaveChanges(); 
            }
        }
    }
}
