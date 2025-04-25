using AutoMapper;
using ServiceApp.DTOs;
using ServiceApp.Model;
using ServiceApp.Repositories;

namespace ServiceApp.Services
{
    public class BookingsService : IBookingService
    {
        private readonly IBookingsRepository _bookingRepository;
        private readonly IMapper _mapper;
        public BookingsService(IBookingsRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateOrUpdateAsync(BookingHistoryDto bookingsDto)
        {
            var booking = _mapper.Map<ServiceBooking>(bookingsDto);
            return await _bookingRepository.CreateOrUpdateAsync(booking);
        }

        public async Task<List<BookingHistoryDto>> GetAllAsync()
        {
            var clients = await _bookingRepository.GetAllAsync();
            return _mapper.Map<List<BookingHistoryDto>>(clients);
        }

        public async Task<BookingHistoryDto?> GetByIdAsync(int id)
        {
            var client = await _bookingRepository.GetByIdAsync(id);
            return _mapper.Map<BookingHistoryDto>(client);
        }
    }
}
