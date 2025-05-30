using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceApp.DTOs;
using ServiceApp.Helpers;
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

        [HttpGet("{id}/{type}/profile")]
        public async Task<IActionResult> GetUserProfile(int id, string type)
        {
            if (type == "client")
            {
                var profile = await _clientService.GetByIdAsync(id);
                return Ok(profile);
            }
            if (type == "service-provider")
            {
                var profile = await _serviceProfessionalService.GetByIdAsync(id);
                return Ok(profile);
            }
            return Ok("No Booking History.");
        }

        [HttpPost("{id}/{userType}/change-password")] // POST /api/account/change-password
        public async Task<IActionResult> ChangePassword(int id, string userType,[FromBody] ChangePasswordRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrEmpty(userType) || id == 0)
            {
                return BadRequest(new { message = "User Id or User Type is required." });
            }

            if (request.NewPassword == request.CurrentPassword)
            {
                return BadRequest(new { message = "New password cannot be the same as the current password." });
            }

            try
            {
                var isPasswordUpdate = false;
                if (userType == "client")
                {
                    var existingClient = await this._clientService.GetClientByIdAsync(id);
                    if (existingClient != null) {
                        existingClient.PasswordHash = PasswordHelper.HashPassword(request.NewPassword);
                        await this._clientService.DirectUpdateAsync(existingClient);
                        isPasswordUpdate = true;
                    }
                }
                if (userType == "service-provider")
                {
                    var existingprofessional = await this._serviceProfessionalService.GetServiceProfessionalByIdAsync(id);
                    if (existingprofessional != null)
                    {
                        existingprofessional.PasswordHash = PasswordHelper.HashPassword(request.NewPassword);
                        await this._serviceProfessionalService.DirectUpdateAsync(existingprofessional);
                        isPasswordUpdate = true;
                    }
                }
                // Pass the entire request object to the service
                if (isPasswordUpdate)
                {
                    return Ok(new { message = "Password updated successfully." });
                }
                else
                {
                    // Return specific error messages from the service
                    return BadRequest(new { message = "Failed to updated password. Please try again later." });
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
