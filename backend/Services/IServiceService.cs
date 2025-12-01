using UranusGroup.DTOs;

namespace UranusGroup.Services
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceDto>> GetAllServicesAsync();
        Task<ServiceDto?> GetServiceByIdAsync(int id);
        Task<IEnumerable<ServiceDto>> GetServicesByCategoryAsync(string category);
        Task<ServiceDto> CreateServiceAsync(ServiceDto serviceDto);
        Task<ServiceDto> UpdateServiceAsync(int id, ServiceDto serviceDto);
        Task<bool> DeleteServiceAsync(int id);
    }
}
