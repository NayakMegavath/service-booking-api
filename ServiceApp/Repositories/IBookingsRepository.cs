using ServiceApp.Model;

namespace ServiceApp.Repositories
{
    public interface IBookingsRepository
    {
        Task<bool> CreateOrUpdateAsync(ServiceBooking booking);
        Task<List<ServiceBooking>> GetAllAsync();
        Task<ServiceBooking?> GetByIdAsync(int id);

    }
}
