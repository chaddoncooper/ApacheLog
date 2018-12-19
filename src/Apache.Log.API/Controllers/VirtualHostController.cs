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


    }
}
