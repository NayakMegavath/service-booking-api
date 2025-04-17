using ServiceApp.DTOs;
using ServiceApp.Model;

namespace ServiceApp.Repositories
{
    public interface IServiceProfessionalRepository
    {
        Task<List<ServiceProfessional>> GetAllAsync();
        Task<ServiceProfessional?> GetByIdAsync(int id);
        Task AddAsync(ServiceProfessional client);
        Task UpdateAsync(ServiceProfessional client);
        Task DeleteAsync(int id);
        Task RegisterServiceProfessionalAsync(ServiceProfessional client);
        Task<ServiceProfessional?> GetByEmailAsync(string email);
        Task<ServiceProfessional?> GetByPhoneAsync(string phoneNumber);
        Task<List<ServiceProfessional?>> GetAllByTypeAsync(string type);
        Task<List<ServiceBooking?>> GetBookingHistoryByIdAsync(int id);
    }
}
