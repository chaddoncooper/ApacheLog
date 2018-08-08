using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Apache.Log.Data;
using Apache.Log.Data.Entities;

namespace Apache.Log.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WhitelistedResourcesController : ControllerBase
    {
        private readonly ApacheLogContext _context;

        public WhitelistedResourcesController(ApacheLogContext context)
        {
            _context = context;
        }

        // GET: api/WhitelistedResources
        [HttpGet]
        public IEnumerable<WhitelistedResource> GetWhitelistedResources()
        {
            return _context.WhitelistedResources;
        }

        // GET: api/WhitelistedResources/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWhitelistedResource([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var whitelistedResource = await _context.WhitelistedResources.FindAsync(id);

            if (whitelistedResource == null)
            {
                return NotFound();
            }

            return Ok(whitelistedResource);
        }

        // PUT: api/WhitelistedResources/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWhitelistedResource([FromRoute] int id, [FromBody] WhitelistedResource whitelistedResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != whitelistedResource.Id)
            {
                return BadRequest();
            }

            _context.Entry(whitelistedResource).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WhitelistedResourceExists(id))
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

        // POST: api/WhitelistedResources
        [HttpPost]
        public async Task<IActionResult> PostWhitelistedResource([FromBody] WhitelistedResource whitelistedResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.WhitelistedResources.Add(whitelistedResource);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWhitelistedResource", new { id = whitelistedResource.Id }, whitelistedResource);
        }

        // DELETE: api/WhitelistedResources/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWhitelistedResource([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var whitelistedResource = await _context.WhitelistedResources.FindAsync(id);
            if (whitelistedResource == null)
            {
                return NotFound();
            }

            _context.WhitelistedResources.Remove(whitelistedResource);
            await _context.SaveChangesAsync();

            return Ok(whitelistedResource);
        }

        private bool WhitelistedResourceExists(int id)
        {
            return _context.WhitelistedResources.Any(e => e.Id == id);
        }
    }
}