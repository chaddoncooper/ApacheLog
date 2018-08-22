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
    public class WhitelistedResourcesController : ControllerBase
    {
        private readonly IWhitelistedResourceRepository _whitelistedResourceRepository;

        public WhitelistedResourcesController(IWhitelistedResourceRepository whitelistedResourceRepository)
        {
            _whitelistedResourceRepository = whitelistedResourceRepository;
        }

        // GET: api/WhitelistedResources
        [HttpGet]
        public IEnumerable<WhitelistedResource> GetWhitelistedResources()
        {
            return _whitelistedResourceRepository.GetAll();
        }

        // GET: api/WhitelistedResources/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWhitelistedResource([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var whitelistedResource = await _whitelistedResourceRepository.GetSingleAsync(id);

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

            _whitelistedResourceRepository.Edit(whitelistedResource);

            try
            {
                await _whitelistedResourceRepository.SaveChangesAsync();
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

            await _whitelistedResourceRepository.AddAsync(whitelistedResource);
            await _whitelistedResourceRepository.SaveChangesAsync();

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

            var whitelistedResource = await _whitelistedResourceRepository.GetSingleAsync(id);
            if (whitelistedResource == null)
            {
                return NotFound();
            }

            _whitelistedResourceRepository.Delete(whitelistedResource);
            await _whitelistedResourceRepository.SaveChangesAsync();

            return Ok(whitelistedResource);
        }

        private bool WhitelistedResourceExists(int id)
        {
            return _whitelistedResourceRepository.FindBy(x => x.Id == id).Any();
        }
    }
}