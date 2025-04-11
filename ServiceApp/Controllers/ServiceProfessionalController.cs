using Microsoft.AspNetCore.Mvc;
using ServiceApp.DTOs;
using ServiceApp.Services;

namespace ServiceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceProfessionalController : ControllerBase
    {
        private readonly IServiceProfessionalService _serviceProfessionalService;

        public ServiceProfessionalController(IServiceProfessionalService serviceProfessionalService)
        {
            _serviceProfessionalService = serviceProfessionalService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] ServiceProfessionalRegistrationDto dto)
        {
            await _serviceProfessionalService.RegisterClientAsync(dto);
            return Ok("Service Professional registered successfully");
        }

        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        //{
        //    var token = await _serviceProfessionalService.LoginAsync(dto);
        //    if (token == null)
        //        return Unauthorized("Invalid credentials");

        //    return Ok(new { Token = token });
        //}

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var professionals = await _serviceProfessionalService.GetAllAsync();
            return Ok(professionals);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var professional = await _serviceProfessionalService.GetByIdAsync(id);
            return professional == null ? NotFound() : Ok(professional);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ServiceProfessionalDto dto)
        {
            await _serviceProfessionalService.UpdateAsync(dto);
            return Ok("Service Professional updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceProfessionalService.DeleteAsync(id);
            return Ok("Service Professional deleted successfully");
        }
    }
}
