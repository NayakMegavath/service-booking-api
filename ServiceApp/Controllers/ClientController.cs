using Microsoft.AspNetCore.Mvc;
using ServiceApp.DTOs;
using ServiceApp.Services;

namespace ServiceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] ClientRegistrationDto dto)
        {
            await _clientService.RegisterClientAsync(dto);
            return Ok(true);
        }

        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        //{
        //    var token = await _clientService.LoginAsync(dto);
        //    if (token == null)
        //        return Unauthorized("Invalid credentials");

        //    return Ok(new { Token = token });
        //}

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clients = await _clientService.GetAllAsync();
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var client = await _clientService.GetByIdAsync(id);
            return client == null ? NotFound() : Ok(client);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ClientDto dto)
        {
            await _clientService.UpdateAsync(dto);
            return Ok("Client updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _clientService.DeleteAsync(id);
            return Ok("Client deleted successfully");
        }
    }
}
