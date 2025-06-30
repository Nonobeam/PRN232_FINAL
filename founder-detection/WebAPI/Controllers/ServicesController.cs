using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Service;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServicesService _iservicesService;

        public ServicesController(IServicesService servicesService)
        {
            _iservicesService = servicesService;
        }

        // GET: api/Services
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Services>>> GetServices()
        {
            return await _iservicesService.GetAllAsync();
        }

        // GET: api/Services/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Services>> GetServices(int id)
        {
            var blogPost = await _iservicesService.GetByIdAsync(id);

            return blogPost;
        }

        // PUT: api/Services/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServices(int id, Services services)
        {
            if (id != services.ServiceId) return BadRequest();

            await _iservicesService.UpdateAsync(services);

            return NoContent();
        }

        // POST: api/Services
        [HttpPost]
        public async Task<ActionResult<Services>> PostServices(Services services)
        {
            await _iservicesService.CreateAsync(services);

            return CreatedAtAction("GetServices", new { id = services.ServiceId }, services);
        }

        // DELETE: api/Services/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServices(Services services)
        {
            await _iservicesService.DeleteAsync(services);

            return NoContent();
        }
    }
}
