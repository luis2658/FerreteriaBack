using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using CardexFerreteriaBackEnd.DB;
using CardexFerreteriaBackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CardexFerreteriaBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly FerreteriaContext _context;

        public ProductoController(FerreteriaContext context)
        {
            _context = context;
        }

        // GET: api/<ProductoController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            /*  creacion de la base de datos
                _context.Database.EnsureCreated();
                await _context.SaveChangesAsync();             
            */

            var result = await _context.productos.ToListAsync();
            return Ok(result);
        }

        // GET api/<ProductoController>/5
        [HttpGet("{id}/id")]
        public async Task<IActionResult> Get(int id)            
        {            
            var producto = await _context.productos.SingleOrDefaultAsync(x => x.id == id);
            if(producto == null)
            {
                return NotFound();
            }
            return Ok(producto);
        }

        // GET api/<ProductoController>/nomre
        [HttpGet("{nombre}/nombre")]
        public async Task<IActionResult> Get(string nombre)
        {   
            var producto = await _context.productos.Where(x => x.nombre == nombre).ToListAsync();
            if (producto == null)
            {
                return NotFound();
            }
            return Ok(producto);
        }

        // POST api/<ProductoController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] object producto)
        {
            string data = producto.ToString();            

            JObject jObject = JObject.Parse(data);

            Producto _producto = new Producto
            {
                id = (int)jObject["id"],
                nombre = (string)jObject["nombre"],
                descripcion = (string)jObject["descripcion"],
                categoria = (string)jObject["categoria"],
                precio = (double)jObject["precio"],
                stock = (int)jObject["stock"],
            };

            _context.productos.Add(_producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = _producto.id }, _producto);
        }

        // PUT api/<ProductoController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Producto>> Put(int id, [FromBody] object producto)
        {
            string data = producto.ToString();

            JObject jObject = JObject.Parse(data);

            Producto _producto = new Producto
            {
                id = (int)jObject["id"],
                nombre = (string)jObject["nombre"],
                descripcion = (string)jObject["descripcion"],
                categoria = (string)jObject["categoria"],
                precio = (double)jObject["precio"],
                stock = (int)jObject["stock"],
            };

            if (id != _producto.id)
            {
                return BadRequest();
            }

            _context.Entry(_producto).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!productoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();

        }

        // DELETE api/<ProductoController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Producto>> Delete(int id)
        {
            var producto = await _context.productos.FindAsync(id);
            if(producto== null)
            {
                return NotFound();
            }
            _context.productos.Remove(producto);
            await _context.SaveChangesAsync();

            return producto;
        }

        private bool productoExists(int id)
        {
            return _context.productos.Any(e => e.id == id);
        }
    }
}
