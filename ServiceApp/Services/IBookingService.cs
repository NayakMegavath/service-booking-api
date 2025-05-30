using ServiceApp.DTOs;

namespace ServiceApp.Services
{
    public interface IBookingService
    {
        Task<bool> CreateOrUpdateAsync(BookingHistoryDto bookingsDto);
        Task<List<BookingHistoryDto>> GetAllAsync();
        Task<BookingHistoryDto?> GetByIdAsync(int id);
    }
}
