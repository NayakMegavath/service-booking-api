using Azure;
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
                var response = await ClientLoginProcessAsync(dto);
                return Ok(new { response });
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

        private async Task<ClientLoginResponseDto> ClientLoginProcessAsync(UserLoginDto dto)
        {
            string identifier = dto.UserName;
            ClientLoginResponseDto? response = null;
            var token = string.Empty;
            // Try Client login by Email
            if (UtilityHelper.IsValidEmail(identifier))
            {
                var clientByEmail = await _clientService.GetByEmailAsync(identifier);
                if (clientByEmail != null && clientByEmail.PasswordHash == PasswordHelper.HashPassword(dto.Password))
                {
                    //var clientData = await _clientService.GetClientByIdAsync(clientByEmail.Id);
                    token = await _tokenService.GenerateTokenAsync(clientByEmail.Id.ToString(), clientByEmail.Email, "Client");
                    response = new ClientLoginResponseDto
                    {
                        Token = token,
                        UserId = clientByEmail.Id.ToString(),
                        Email = clientByEmail.Email,
                        UserType = "Client",
                        // Map the RegisteredAddress from your client entity to AddressDto
                        RegisteredAddress = new AddressDto // You'll need to fetch and map this from clientByEmail
                        {
                            Address1 = clientByEmail?.Address1 ?? string.Empty, // Assuming Client has RegisteredAddress property
                            Address2 = clientByEmail?.Address2 ?? string.Empty,
                            City = clientByEmail?.City ?? string.Empty,
                            State = clientByEmail?.State ?? string.Empty,
                            ZipCode = clientByEmail?.Zip ?? string.Empty,
                        }
                    };
                }
            }

            // Try Client login by Phone
            if (UtilityHelper.IsValidPhone(identifier))
            {
                var clientByPhone = await _clientService.GetByPhoneAsync(identifier); // Assuming you have this method
                if (clientByPhone != null && clientByPhone.PasswordHash == PasswordHelper.HashPassword(dto.Password))
                {
                    token = await _tokenService.GenerateTokenAsync(clientByPhone.Id.ToString(), clientByPhone.Email, "Client"); // Assuming Client has an Email
                    response = new ClientLoginResponseDto
                    {
                        Token = token,
                        UserId = clientByPhone.Id.ToString(),
                        Email = clientByPhone.Email,
                        UserType = "Client",
                        // Map the RegisteredAddress from your client entity to AddressDto
                        RegisteredAddress = new AddressDto // You'll need to fetch and map this from clientByEmail
                        {
                            Address1 = clientByPhone?.Address1 ?? string.Empty, // Assuming Client has RegisteredAddress property
                            Address2 = clientByPhone?.Address2 ?? string.Empty,
                            City = clientByPhone?.City ?? string.Empty,
                            State = clientByPhone?.State ?? string.Empty,
                            ZipCode = clientByPhone?.Zip ?? string.Empty,
                        }
                    };
                }
            }
            return response;
        }
        #endregion
    }
}
