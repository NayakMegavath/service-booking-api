using Microsoft.AspNetCore.Mvc;
using ServiceApp.DTOs;
using ServiceApp.Model;
using ServiceApp.Services;

namespace ServiceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceBookingController : ControllerBase
    {
        #region Constructor
        private readonly IBookingService _bookingService;

        public ServiceBookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        #endregion

        #region Public Methods
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateBookingsAsync([FromBody] BookingHistoryDto booking)
        {
            var isSaved = false;
            if (booking != null)
            {
                isSaved = await _bookingService.CreateOrUpdateAsync(booking);
            }
            return Ok(isSaved);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bookings = await _bookingService.GetAllAsync();
            return Ok(bookings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var booking = await _bookingService.GetByIdAsync(id);
            return booking == null ? NotFound() : Ok(booking);
        }
        #endregion
    }
}
