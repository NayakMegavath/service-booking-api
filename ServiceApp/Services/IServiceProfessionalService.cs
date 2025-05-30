using ServiceApp.DTOs;
using ServiceApp.Model;

namespace ServiceApp.Services
{
    public interface IServiceProfessionalService
    {
        Task<IEnumerable<ServiceProfessionalDto>> GetAllAsync();
        Task<ServiceProfessionalDto?> GetByIdAsync(int id);
        Task AddAsync(ServiceProfessionalDto clientDto);
        Task UpdateAsync(ServiceProfessionalDto clientDto);
        Task DeleteAsync(int id);
        Task RegisterClientAsync(ServiceProfessionalRegistrationDto dto);
        Task<ServiceProfessionalDto?> LoginAsync(UserLoginDto dto);
        Task<ServiceProfessional?> GetByEmailAsync(string email);
        Task<ServiceProfessional?> GetByPhoneAsync(string email);
        Task<List<ServiceProfessionalDto>> GetAllByTypeAsync(string type);
        Task<List<BookingHistoryDto?>> GetBookingHistoryByIdAsync(int id);
        Task DirectUpdateAsync(ServiceProfessional professional);
        Task<ServiceProfessional?> GetServiceProfessionalByIdAsync(int id);
    }
}
