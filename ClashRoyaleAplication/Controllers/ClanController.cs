using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ClashRoyaleAplication.Data;
using ClashRoyaleAplication.DBModels;
using ClashRoyaleAplication.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClashRoyaleAplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("React_Policy")]
    public class ClanController : ControllerBase
    {
        private readonly IClanRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public ClanController (IClanRepository repository , IMapper mapper , LinkGenerator linkGenerator)
        {
            _mapper = mapper;
            _repository = repository;
            _linkGenerator = linkGenerator; 
        }

        // GET: api/<ClanController>
        [HttpGet]
        public async Task<ActionResult<ClanModels[]>> Get()
        {
            try
            {
                var model = await _repository.GetAllClansAsync();

                return _mapper.Map<ClanModels[]>(model);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{nombre}/region/{region}")]
        public async Task<ActionResult<ClanModels[]>> GetByRegion(string region )
        {
            try
            {
                var model = await _repository.GetAllClanesByRegion(region);

                return _mapper.Map<ClanModels[]>(model);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        // GET api/<ClanController>/5
        [HttpGet("{nombre}")]
        public async Task<ActionResult<ClanModels>> Get(string nombre)
        {
            try
            {
                var model = await _repository.GetClanAsync(nombre);
                if (model == null) return NotFound(); 

                var modelreturn = _mapper.Map<ClanModels>(model);

                var cartafavorita = await _repository.GetAllCartasFavoritas(model);

                if (cartafavorita != null)
                    modelreturn.CartaFavorita = _mapper.Map<CartaModels[]>(cartafavorita);

                Jugador[] jugadoresMiembros = await _repository.GetAllJugadoresMember(model);

                if (jugadoresMiembros != null)
                    modelreturn.AllJugadores = _mapper.Map<JugadorModels[]>(jugadoresMiembros); 
                
                return modelreturn; 
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        // POST api/<ClanController>
        [HttpPost]
        public async Task<ActionResult<ClanModels>> Post([FromBody] ClanModels clan)
        {
            try
            {
                var existing = await _repository.GetClanAsync(clan.Nombre);
                if (existing != null)
                {
                    return BadRequest("Clan in use");
                }

                var location = _linkGenerator.GetPathByAction(
                    "Get",
                    "Clan",
                    new { nombre = clan.Nombre });

                if (string.IsNullOrWhiteSpace(location))
                {
                    return BadRequest("Could not use current clan");
                }

                var model = _mapper.Map<Clan>(clan);
                _repository.Add(model);

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/clan/{clan.Nombre}", _mapper.Map<ClanModels>(model));
                }
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        // PUT api/<ClanController>/5
        [HttpPut("{nombre}")]
        public async Task<ActionResult<ClanModels>> Put(string nombre , [FromBody] ClanModels clan)
        {
            try
            {
                var clanantiguo = await _repository.GetClanAsync(clan.Nombre);
                if (clanantiguo == null)
                {
                    return NotFound($"Could not find clan with name {nombre}");
                }

                _mapper.Map(clan, clanantiguo);

                if (await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<ClanModels>(clanantiguo);
                }
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return BadRequest();

        }

        // DELETE api/<ClanController>/5
        [HttpDelete("{nombre}")]
        public async Task<ActionResult<ClanModels>> Delete(string nombre)
        {
            try
            {
                var clan = await _repository.GetClanAsync(nombre);
                if (clan == null) return NotFound();

                _repository.Delete(clan);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the player");
        }
    }
}
