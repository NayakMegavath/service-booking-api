using ServiceApp.DTOs;
using ServiceApp.Model;

namespace ServiceApp.Services
{
    public interface IClientService
    {
        Task<List<ClientDto>> GetAllAsync();
        Task<ClientDto?> GetByIdAsync(int id);
        Task AddAsync(ClientDto clientDto);
        Task UpdateAsync(ClientDto clientDto);
        Task DeleteAsync(int id);
        Task RegisterClientAsync(ClientRegistrationDto dto);
        Task<ClientDto?> LoginAsync(UserLoginDto user);
        Task<Client?> GetByEmailAsync(string email);
    }
}
