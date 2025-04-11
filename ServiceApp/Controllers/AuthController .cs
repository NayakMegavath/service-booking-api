using Microsoft.AspNetCore.Mvc;
using ServiceApp.DTOs;
using ServiceApp.Helpers;
using ServiceApp.Services;

namespace ServiceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IServiceProfessionalService _serviceProfessionalService;
        private readonly ITokenService _tokenService;

        public AuthController(IClientService clientService,
                              IServiceProfessionalService serviceProfessionalService,
                              ITokenService tokenService)
        {
            _clientService = clientService;
            _serviceProfessionalService = serviceProfessionalService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
            // First, try Client login
            var client = await _clientService.GetByEmailAsync(dto.Email);
            if (client != null && client.PasswordHash == PasswordHelper.HashPassword(dto.Password))
            {
                var token = await _tokenService.GenerateTokenAsync(client.Id.ToString(), client.Email, "Client");
                return Ok(new { Token = token });
            }

            // Then try Service Professional login
            var pro = await _serviceProfessionalService.GetByEmailAsync(dto.Email);
            if (pro != null && pro.PasswordHash == PasswordHelper.HashPassword(dto.Password))
            {
                var token = await _tokenService.GenerateTokenAsync(pro.Id.ToString(), pro.Email, "ServiceProfessional");
                return Ok(new { Token = token });
            }

            return Unauthorized("Invalid credentials.");
        }
    }
}
