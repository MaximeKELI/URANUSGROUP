using Microsoft.AspNetCore.Mvc;
using UranusGroup.DTOs;
using UranusGroup.Services;

namespace UranusGroup.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceDto>>> GetServices()
        {
            var services = await _serviceService.GetAllServicesAsync();
            return Ok(services);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceDto>> GetService(int id)
        {
            var service = await _serviceService.GetServiceByIdAsync(id);
            if (service == null)
                return NotFound();

            return Ok(service);
        }

        [HttpGet("category/{category}")]
        public async Task<ActionResult<IEnumerable<ServiceDto>>> GetServicesByCategory(string category)
        {
            var services = await _serviceService.GetServicesByCategoryAsync(category);
            return Ok(services);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceDto>> CreateService([FromBody] ServiceDto serviceDto)
        {
            var service = await _serviceService.CreateServiceAsync(serviceDto);
            return CreatedAtAction(nameof(GetService), new { id = service.Id }, service);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceDto>> UpdateService(int id, [FromBody] ServiceDto serviceDto)
        {
            if (id != serviceDto.Id)
                return BadRequest();

            try
            {
                var service = await _serviceService.UpdateServiceAsync(id, serviceDto);
                return Ok(service);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteService(int id)
        {
            var result = await _serviceService.DeleteServiceAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
