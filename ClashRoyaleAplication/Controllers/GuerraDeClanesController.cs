using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ClashRoyaleAplication.Data;
using ClashRoyaleAplication.DBModels;
using ClashRoyaleAplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClashRoyaleAplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuerraDeClanesController : ControllerBase
    {
        private readonly IGuerraDeClanesRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkgenerator; 
        public GuerraDeClanesController(IGuerraDeClanesRepository repository , IMapper mapper , LinkGenerator linkGenerator)
        {
            _repository = repository;
            _mapper = mapper;
            _linkgenerator = linkGenerator; 
        }

        // GET: api/<GuerraDeClanesController>
        [HttpGet]
        public async Task<ActionResult<GuerraDeClanesModels[]>> Get()
        {
            try
            {
                var guerradeclanes = await _repository.GetAllGuerraDeClanesAsync();
                

                return _mapper.Map<GuerraDeClanesModels[]>(guerradeclanes); 
                
            }
            catch 
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure"); 
            }
        }

        // GET api/<GuerraDeClanesController>/5
        [HttpGet("{nombre}")]
        public async Task<ActionResult<GuerraDeClanesModels>> Get (string nombre)
        {
            try
            {
                var guerradeclanes = await _repository.GetGuerraDeClanesAsync(nombre);

                var models = _mapper.Map<GuerraDeClanesModels>(guerradeclanes);


                var clanes = new List<Clan>(from clan in guerradeclanes.ParticipaEns
                                               select clan.IdClanNavigation);

                var mejoresjugadores = from bestjugador in clanes
                                          select _repository.GetBestPlayerByTrofeos(bestjugador);

                models.Clanes = _mapper.Map<ClanModels[]>(clanes);

                models.MejoresJugadores = _mapper.Map<JugadorModels[]>(mejoresjugadores);


                return models; 

            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        // POST api/<GuerraDeClanesController>
        [HttpPost]
        public async Task<ActionResult<GuerraDeClanesModels>> Post([FromBody] GuerraDeClanesModels guerradeclanes)
        {
            try
            {
                var existguerradeclanes = _repository.GetGuerraDeClanesAsync(guerradeclanes.Nombre);
                if (existguerradeclanes != null)
                {
                    return BadRequest("Guerra de Clanes in use"); 
                }

                var location = _linkgenerator.GetPathByAction(
                    "Get", 
                    "GuerraDeClanes",
                    new { guerradeclanes.Nombre }); 

                if (string.IsNullOrWhiteSpace(location))
                {
                    return BadRequest("Could not use current Guerra de Clanes"); 
                }

                var models = _mapper.Map<GuerradeClane>(guerradeclanes);
                _repository.Add(models);

                if (await _repository.SaveChangesAsync ())
                {
                    return Created($"api/GuerraDeClanes/{guerradeclanes.Nombre}", _mapper.Map<GuerraDeClanesModels>(models));
                }
            }
            catch 
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure"); 
            }
            return BadRequest(); 
        }

        // PUT api/<GuerraDeClanesController>/5
        [HttpPut("{nombre}")]
        public async Task<ActionResult<GuerraDeClanesModels>> Put(string nombre, [FromBody] GuerraDeClanesModels guerradeclanes)
        {
            try
            {
                var oldguerradeclanes = await _repository.GetGuerraDeClanesAsync(nombre);
                if (oldguerradeclanes == null)
                {
                    return NotFound($"Could not find guerra de clanes with name {nombre}");
                }

                _mapper.Map(guerradeclanes, oldguerradeclanes);

                if (await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<GuerraDeClanesModels>(oldguerradeclanes);
                }
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        // DELETE api/<GuerraDeClanesController>/5
        [HttpDelete("{nombre}")]
        public async Task<ActionResult<GuerraDeClanesModels>> Delete(string nombre)
        {
            try
            {
                var guerradeclanes = await _repository.GetGuerraDeClanesAsync(nombre);
                if (guerradeclanes == null) return NotFound();

                _repository.Delete(guerradeclanes);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return BadRequest(); 
        }
    }
}
