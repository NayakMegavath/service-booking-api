using ServiceApp.Model;

namespace ServiceApp.Repositories
{
    public interface IClientRepository
    {
        Task<List<Client>> GetAllAsync();
        Task<Client?> GetByIdAsync(int id);
        Task AddAsync(Client client);
        Task UpdateAsync(Client client);
        Task DeleteAsync(int id);
        Task RegisterClientAsync(Client client);
        Task<Client?> GetByEmailAsync(string email);
        Task<Client?> GetByPhoneAsync(string phoneNumber);
    }
}
