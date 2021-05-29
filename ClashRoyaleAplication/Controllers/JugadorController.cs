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
    public class JugadorController : ControllerBase
    {
        private readonly IJugadorRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator; 
        public JugadorController (IJugadorRepository repository, IMapper mapper , LinkGenerator linkGenerator)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator; 
        }

        // GET: api/<JugadorController>
        [HttpGet]
        public async  Task<ActionResult<JugadorModels[]>> Get()
        {
            try
            {
                var model = await _repository.GetAllJugadoresAsync();

                return  _mapper.Map<JugadorModels[]>(model); 
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

        }

        // GET api/<JugadorController>/5
        [HttpGet("{nombre}")]
        public async Task<ActionResult<JugadorModels>> Get(string nombre)
        {
            try
            {
                var model = await _repository.GetJugadorAsync(nombre);

                return _mapper.Map<JugadorModels>(model);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        // POST api/<JugadorController>
        [HttpPost]
        public async Task<ActionResult<JugadorModels>> Post([FromBody] JugadorModels jugador)
        {
            try
            {
                var existing =  await _repository.GetJugadorAsync(jugador.Nombre); 
                if (existing!=null)
                {
                    return BadRequest("Jugador in use"); 
                }

                var location = _linkGenerator.GetPathByAction(
                    "Get", 
                    "Jugador", 
                    new { nombre = jugador.Nombre });

                if (string.IsNullOrWhiteSpace(location))
                {
                    return BadRequest("Could not use current jugador"); 
                }

                var model = _mapper.Map<Jugador>(jugador);
                _repository.Add(model);

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/jugador/{jugador.Nombre}", _mapper.Map<JugadorModels>(model));
                }
                
            }
            catch 
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest(); 
        }

        // PUT api/<JugadorController>/5
        [HttpPut("{nombre}")]
        public async Task<ActionResult<JugadorModels>> Put(string nombre, [FromBody] JugadorModels jugador)
        {
            try
            {
                var jugadorantiguo =  await _repository.GetJugadorAsync(jugador.Nombre); 
                if (jugadorantiguo == null)
                {
                    return NotFound($"Could not find player with name {nombre}"); 
                }

                _mapper.Map(jugador,jugadorantiguo);

                if (await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<JugadorModels>(jugadorantiguo); 
                }
            }
            catch 
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return BadRequest(); 
        }

        // DELETE api/<JugadorController>/5
        [HttpDelete("{nombre}")]
        public async Task<ActionResult<JugadorModels>> Delete(string nombre)
        {
            try
            {
                var jugador = await _repository.GetJugadorAsync(nombre);
                if (jugador == null) return NotFound();

                _repository.Delete(jugador); 

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
