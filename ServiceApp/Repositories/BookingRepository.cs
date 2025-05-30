using Microsoft.EntityFrameworkCore;
using ServiceApp.Data;
using ServiceApp.DTOs;
using ServiceApp.Model;

namespace ServiceApp.Repositories
{
    public class BookingRepository : IBookingsRepository
    {
        private readonly AppDbContext _context;
        public BookingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateOrUpdateAsync(ServiceBooking booking)
        {
            if (booking.Id > 0)
            {
                var bookingDetail = await GetByIdAsync(booking.Id);
                if (bookingDetail != null) {
                    var updatedBooking = new ServiceBooking
                    {
                        AppointmentDate = bookingDetail.AppointmentDate,
                        ServiceProfessionalId = bookingDetail.ServiceProfessionalId,
                        ServiceType = bookingDetail.ServiceType,
                        AmountPaid = bookingDetail.AmountPaid,
                    };
                    _context.ServiceBooking.Update(updatedBooking);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            else
            {
                _context.ServiceBooking.Add(booking);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<ServiceBooking>> GetAllAsync()
        {
            return await _context.ServiceBooking.ToListAsync();
        }

        public async Task<ServiceBooking?> GetByIdAsync(int id) => await _context.ServiceBooking.FindAsync(id);
    }
}
