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
    public class CartaController : ControllerBase
    {
        private readonly ICartaRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator; 

        public  CartaController (ICartaRepository repository , IMapper mapper , LinkGenerator linkGenerator)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator; 
        }


        // GET: api/<CartaController>
        [HttpGet]
        public async Task<ActionResult<CartaModels[]>>  Get()
        {
            try
            {
                var model = await _repository.GetAllCartasAsync();

                return _mapper.Map<CartaModels[]>(model);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        // GET api/<CartaController>/5
        [HttpGet("{nombre}")]
        public async Task<ActionResult<CartaModels>> Get(string nombre)
        {
            try
            {
                var model = await _repository.GetCartaAsync(nombre);

                return _mapper.Map<CartaModels>(model); 

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        // POST api/<CartaController>
        [HttpPost]
        public async Task<ActionResult<CartaModels>> Post([FromBody] CartaModels carta )
        {
            try
            {
                var existing = await _repository.GetCartaAsync(carta.Nombre);
                if (existing != null)
                {
                    return BadRequest("Cart in use");
                }

                var location = _linkGenerator.GetPathByAction(
                    "Get",
                    "Carta",
                    new { nombre = carta.Nombre });

                if (string.IsNullOrWhiteSpace(location))
                {
                    return BadRequest("Could not use current cart");
                }

                var model = _mapper.Map<Cartum>(carta);

                _repository.Add(model);

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/carta/{carta.Nombre}", _mapper.Map<CartaModels>(model));
                }

            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        // PUT api/<CartaController>/5
        [HttpPut("{nombre}")]
        public async Task<ActionResult<CartaModels>> Put(int nombre, [FromBody] CartaModels carta)
        {
            try
            {
                var oldcart = await _repository.GetCartaAsync(carta.Nombre);
                if (oldcart == null)
                {
                    return NotFound($"Could not find cart with name {nombre}");
                }

                _mapper.Map(carta, oldcart);

                if (await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<CartaModels>(oldcart);
                }
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return BadRequest();
        }

        // DELETE api/<CartaController>/5
        [HttpDelete("{nombre}")]
        public async Task<ActionResult<CartaModels>>Delete(string nombre)
        {
            try
            {
                var carta = await _repository.GetCartaAsync(nombre);
                if (carta == null) return NotFound();

                _repository.Delete(carta);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok(); ; 
                }

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return BadRequest("Failed to delete the cart");
        }
    }
}
