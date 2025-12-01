using Microsoft.EntityFrameworkCore;
using UranusGroup.Data;
using UranusGroup.DTOs;
using AutoMapper;

namespace UranusGroup.Services
{
    public class ServiceService : IServiceService
    {
        private readonly UranusGroupContext _context;
        private readonly IMapper _mapper;

        public ServiceService(UranusGroupContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ServiceDto>> GetAllServicesAsync()
        {
            var services = await _context.Services
                .Include(s => s.Features)
                .Where(s => s.IsActive)
                .OrderBy(s => s.SortOrder)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ServiceDto>>(services);
        }

        public async Task<ServiceDto?> GetServiceByIdAsync(int id)
        {
            var service = await _context.Services
                .Include(s => s.Features)
                .FirstOrDefaultAsync(s => s.Id == id && s.IsActive);

            return service == null ? null : _mapper.Map<ServiceDto>(service);
        }

        public async Task<IEnumerable<ServiceDto>> GetServicesByCategoryAsync(string category)
        {
            var services = await _context.Services
                .Include(s => s.Features)
                .Where(s => s.Category == category && s.IsActive)
                .OrderBy(s => s.SortOrder)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ServiceDto>>(services);
        }

        public async Task<ServiceDto> CreateServiceAsync(ServiceDto serviceDto)
        {
            var service = _mapper.Map<Models.Service>(serviceDto);
            service.CreatedAt = DateTime.UtcNow;
            service.UpdatedAt = DateTime.UtcNow;

            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            return _mapper.Map<ServiceDto>(service);
        }

        public async Task<ServiceDto> UpdateServiceAsync(int id, ServiceDto serviceDto)
        {
            var service = await _context.Services
                .Include(s => s.Features)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (service == null)
                throw new ArgumentException("Service not found");

            _mapper.Map(serviceDto, service);
            service.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return _mapper.Map<ServiceDto>(service);
        }

        public async Task<bool> DeleteServiceAsync(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
                return false;

            service.IsActive = false;
            service.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
