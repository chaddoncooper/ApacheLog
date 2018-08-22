using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Apache.Log.Data.Entities;
using Apache.Log.Repository;

namespace Apache.Log.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlacklistedResourcesController : ControllerBase
    {
        private readonly IBlacklistedResourceRepository _blacklistedResourceRepository;

        public BlacklistedResourcesController(IBlacklistedResourceRepository blacklistedResourceRepository)
        {
            _blacklistedResourceRepository = blacklistedResourceRepository;
        }

        // GET: api/BlacklistedResources
        [HttpGet]
        public IEnumerable<BlacklistedResource> GetBlacklistedResources()
        {
            return _blacklistedResourceRepository.GetAll();
        }

        // GET: api/BlacklistedResources/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlacklistedResource([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var blacklistedResource = await _blacklistedResourceRepository.GetSingleAsync(id);

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

            _blacklistedResourceRepository.Edit(blacklistedResource);

            try
            {
                await _blacklistedResourceRepository.SaveChangesAsync();
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

            await _blacklistedResourceRepository.AddAsync(blacklistedResource);
            await _blacklistedResourceRepository.SaveChangesAsync();

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

            var blacklistedResource = await _blacklistedResourceRepository.GetSingleAsync(id);
            if (blacklistedResource == null)
            {
                return NotFound();
            }

            _blacklistedResourceRepository.Delete(blacklistedResource);
            await _blacklistedResourceRepository.SaveChangesAsync();

            return Ok(blacklistedResource);
        }

        private bool BlacklistedResourceExists(int id)
        {
            return _blacklistedResourceRepository.FindBy(x => x.Id == id).Any();
        }
    }
}