using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Apache.Log.Data;
using Apache.Log.Data.Entities;

namespace Apache.Log.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlacklistedResourcesController : ControllerBase
    {
        private readonly ApacheLogContext _context;

        public BlacklistedResourcesController(ApacheLogContext context)
        {
            _context = context;
        }

        // GET: api/BlacklistedResources
        [HttpGet]
        public IEnumerable<BlacklistedResource> GetBlacklistedResources()
        {
            return _context.BlacklistedResources;
        }

        // GET: api/BlacklistedResources/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlacklistedResource([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var blacklistedResource = await _context.BlacklistedResources.FindAsync(id);

            if (blacklistedResource == null)
            {
                return NotFound();
            }

            return Ok(blacklistedResource);
        }

        // PUT: api/BlacklistedResources/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlacklistedResource([FromRoute] int id, [FromBody] BlacklistedResource blacklistedResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != blacklistedResource.Id)
            {
                return BadRequest();
            }

            _context.Entry(blacklistedResource).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlacklistedResourceExists(id))
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

        // POST: api/BlacklistedResources
        [HttpPost]
        public async Task<IActionResult> PostBlacklistedResource([FromBody] BlacklistedResource blacklistedResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.BlacklistedResources.Add(blacklistedResource);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBlacklistedResource", new { id = blacklistedResource.Id }, blacklistedResource);
        }

        // DELETE: api/BlacklistedResources/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlacklistedResource([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var blacklistedResource = await _context.BlacklistedResources.FindAsync(id);
            if (blacklistedResource == null)
            {
                return NotFound();
            }

            _context.BlacklistedResources.Remove(blacklistedResource);
            await _context.SaveChangesAsync();

            return Ok(blacklistedResource);
        }

        private bool BlacklistedResourceExists(int id)
        {
            return _context.BlacklistedResources.Any(e => e.Id == id);
        }
    }
}