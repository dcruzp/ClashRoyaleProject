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
    public class DesafioController : ControllerBase
    {
        private readonly IDesafioRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator; 
        public DesafioController (IDesafioRepository repository , IMapper mapper, LinkGenerator linkGenerator)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator; 
        }

        // GET: api/<DesafioController>
        [HttpGet]
        public async Task<ActionResult<DesafioModels[]>> Get()
        {
            try
            {
                var model = await _repository.GetAllDesafiosAsync();

                return  _mapper.Map<DesafioModels[]>(model); 
            }
            catch 
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        // GET api/<DesafioController>/5
        [HttpGet("nombre")]
        public  async Task<ActionResult<DesafioModels>> Get(string nombre )
        {
            try
            {
                var model = await  _repository.GetDesafioAsync(nombre);

                return _mapper.Map<DesafioModels>(model); 
            }
            catch 
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        // POST api/<DesafioController>
        [HttpPost]
        public  async Task<ActionResult<DesafioModels>> Post([FromBody] DesafioModels desafio)
        {
            try
            {
                var desafioexistente = await _repository.GetDesafioAsync(desafio.Nombre);
                if (desafioexistente != null)
                {
                    return BadRequest("Desafio in use"); 
                }

                var location = _linkGenerator.GetPathByAction(
                    "Get",
                    "Desafio",
                    new { desafio.Nombre }); 

                if (string.IsNullOrWhiteSpace(location))
                {
                    return BadRequest("Could not use current desafio"); 
                }

                var model = _mapper.Map<Desafio>(desafio); 
                _repository.Add(model); 

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/Desafio/{desafio.Nombre}",_mapper.Map<DesafioModels>(model)); 
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest(); 
        }

        // PUT api/<DesafioController>/5
        [HttpPut("{nombre}")]
        public async Task <ActionResult<DesafioModels>> Put(string nombre, [FromBody] DesafioModels desafio)
        {
            try
            {
                var olddesafio = await _repository.GetDesafioAsync(nombre); 
                if (olddesafio == null)
                {
                    return NotFound($"Could not find desafio with name {nombre}"); 
                }

                _mapper.Map(desafio , olddesafio); 

                if (await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<DesafioModels>(olddesafio); 
                }
            }
            catch 
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure"); 
            }

            return BadRequest(); 
        }

        // DELETE api/<DesafioController>/5
        [HttpDelete("{nombre}")]
        public async Task<ActionResult<DesafioModels>> Delete(string nombre)
        {
            try
            {
                var desafio = await _repository.GetDesafioAsync(nombre);
                if (desafio == null) return NotFound();

                _repository.Delete(desafio);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return BadRequest("Failed to delete the desafio"); 
        }
    }
}
