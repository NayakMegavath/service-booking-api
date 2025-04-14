using Microsoft.EntityFrameworkCore;
using ServiceApp.Data;
using ServiceApp.Model;

namespace ServiceApp.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _context;
        public ClientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Client>> GetAllAsync()
        {
            return await _context.Client.ToListAsync();
        }
        public async Task<Client?> GetByIdAsync(int id) => await _context.Client.FindAsync(id);
        public async Task AddAsync(Client client)
        {
            _context.Client.Add(client);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Client client)
        {
            _context.Client.Update(client);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var client = await _context.Client.FindAsync(id);
            if (client != null)
            {
                _context.Client.Remove(client);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RegisterClientAsync(Client client)
        {
            _context.Client.Add(client);
            await _context.SaveChangesAsync();
        }

        public async Task<Client?> GetByEmailAsync(string email)
        {
            return await _context.Client.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<Client?> GetByPhoneAsync(string phoneNumber)
        {
            return await _context.Client.FirstOrDefaultAsync(sp => sp.PhoneNumber == phoneNumber);
        }
    }
}
