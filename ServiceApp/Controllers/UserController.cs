using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceApp.DTOs;
using ServiceApp.Services;
using System.Security.Claims;

namespace ServiceApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        // Inject your user data service or repository
        private readonly IUserService _userService;
        private readonly IClientService _clientService;
        private readonly IServiceProfessionalService _serviceProfessionalService;

        public UserController(IUserService userService, IClientService clientService)
        {
            _userService = userService;
            _clientService = clientService;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetUserProfile()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var userTypeClaim = User.FindFirst("userType"); // Assuming you added userType to the token

            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId) || userTypeClaim == null)
            {
                return Unauthorized();
            }

            var userProfile = await _userService.GetUserProfileAsync(userId, userTypeClaim.Value);

            if (userProfile == null)
            {
                return NotFound();
            }

            return Ok(userProfile);
        }

        [HttpGet("{id}/{type}/history")]
        public async Task<IActionResult> GetUserBookingHistory(int id, string type)
        {
            if (type == "client") {
                var bookingHistory = await _clientService.GetBookingHistoryByIdAsync(id);
                return Ok(bookingHistory);
            }
            if (type == "service-provider")
            {
                var bookingHistory = await _serviceProfessionalService.GetBookingHistoryByIdAsync(id);
                return Ok(bookingHistory);
            }
            return Ok("No Booking History.");
        }
    }
}
