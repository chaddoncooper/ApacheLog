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
    public class VirtualHostController : ControllerBase
    {
        private readonly IVirtualHostRepository _virtualHostRepository;

        public VirtualHostController(IVirtualHostRepository virtualHostRepository)
        {
            _virtualHostRepository = virtualHostRepository;
        }

        // GET: api/VirtualHost
        [HttpGet]
        public IEnumerable<VirtualHost> GetVirtualHosts()
        {
            return _virtualHostRepository.GetAll();
        }

        // GET: api/VirtualHost/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVirtualHost([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var virtualHost = await _virtualHostRepository.GetSingleAsync(id);

            if (virtualHost == null)
            {
                return NotFound();
            }

            return Ok(virtualHost);
        }

        // PUT: api/VirtualHost/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVirtualHost([FromRoute] int id, [FromBody] VirtualHost virtualHost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != virtualHost.Id)
            {
                return BadRequest();
            }

            _virtualHostRepository.Edit(virtualHost);

            try
            {
                await _virtualHostRepository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VirtualHostExists(id))
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

        // POST: api/VirtualHost
        [HttpPost]
        public async Task<IActionResult> PostBlacklistedResource([FromBody] VirtualHost virtualHost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _virtualHostRepository.AddAsync(virtualHost);
            await _virtualHostRepository.SaveChangesAsync();

            return CreatedAtAction("GetVirtualHost", new { id = virtualHost.Id }, virtualHost);
        }

        private bool VirtualHostExists(int id)
        {
            return _virtualHostRepository.FindBy(x => x.Id == id).Any();
        }

        // GET: api/VirtualHost/totalcount
        [HttpGet]
        [Route("totalcount")]
        public async Task<IActionResult> GetTotalCount()
        {
            return Ok(await _virtualHostRepository.TotalCountAsync());
        }
    }
}
