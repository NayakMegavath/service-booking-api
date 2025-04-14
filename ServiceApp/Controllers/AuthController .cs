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
        #region Constructor
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
        #endregion

        #region Public Methods
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
            var token = string.Empty;
            if (dto.UserType == "client")
            {
                token = await ClientLoginProcessAsync(dto);
                return Ok(new { Token = token });
            }
            if (dto.UserType == "service-provider")
            {
                token = await ServiceLoginProcessAsync(dto);
                return Ok(new { Token = token });
            }
            return Unauthorized("Invalid credentials.");
        }
        #endregion

        private async Task<string> ServiceLoginProcessAsync(UserLoginDto dto)
        {
            string identifier = dto.UserName;
            var token = string.Empty;
            // Try Service Professional login by Email
            if (UtilityHelper.IsValidEmail(identifier))
            {
                var proByEmail = await _serviceProfessionalService.GetByEmailAsync(identifier);
                if (proByEmail != null && proByEmail.PasswordHash == PasswordHelper.HashPassword(dto.Password))
                {
                    token = await _tokenService.GenerateTokenAsync(proByEmail.Id.ToString(), proByEmail.Email, "ServiceProfessional");
                    //return Ok(new { Token = token });
                }
            }

            // Try Service Professional login by Phone
            if (UtilityHelper.IsValidPhone(identifier))
            {
                var proByPhone = await _serviceProfessionalService.GetByPhoneAsync(identifier); // Assuming you have this method
                if (proByPhone != null && proByPhone.PasswordHash == PasswordHelper.HashPassword(dto.Password))
                {
                    token = await _tokenService.GenerateTokenAsync(proByPhone.Id.ToString(), proByPhone.Email, "ServiceProfessional"); // Assuming Professional has an Email
                    //return Ok(new { Token = token });
                }
            }
            return token;
        }

        #region Private Methods

        private async Task<string> ClientLoginProcessAsync(UserLoginDto dto)
        {
            string identifier = dto.UserName;
            var token = string.Empty;
            // Try Client login by Email
            if (UtilityHelper.IsValidEmail(identifier))
            {
                var clientByEmail = await _clientService.GetByEmailAsync(identifier);
                if (clientByEmail != null && clientByEmail.PasswordHash == PasswordHelper.HashPassword(dto.Password))
                {
                    token = await _tokenService.GenerateTokenAsync(clientByEmail.Id.ToString(), clientByEmail.Email, "Client");
                }
            }

            // Try Client login by Phone
            if (UtilityHelper.IsValidPhone(identifier))
            {
                var clientByPhone = await _clientService.GetByPhoneAsync(identifier); // Assuming you have this method
                if (clientByPhone != null && clientByPhone.PasswordHash == PasswordHelper.HashPassword(dto.Password))
                {
                    token = await _tokenService.GenerateTokenAsync(clientByPhone.Id.ToString(), clientByPhone.Email, "Client"); // Assuming Client has an Email
                }
            }
            return token;
        }
        #endregion
    }
}
