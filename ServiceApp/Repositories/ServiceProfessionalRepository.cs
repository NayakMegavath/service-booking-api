using Microsoft.EntityFrameworkCore;
using ServiceApp.Data;
using ServiceApp.DTOs;
using ServiceApp.Model;
using ServiceApp.Services;

namespace ServiceApp.Repositories
{
    public class ServiceProfessionalRepository : IServiceProfessionalRepository
    {
        private readonly AppDbContext _context;
        private readonly PasswordService _passwordService;

        public ServiceProfessionalRepository(AppDbContext context, PasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }

        public async Task<List<ServiceProfessional>> GetAllAsync() => await _context.ServiceProfessional.ToListAsync();

        public async Task<ServiceProfessional?> GetByIdAsync(int id) => await _context.ServiceProfessional.FindAsync(id);

        public async Task AddAsync(ServiceProfessional client)
        {
            _context.ServiceProfessional.Add(client);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ServiceProfessional client)
        {
            _context.ServiceProfessional.Update(client);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var client = await _context.ServiceProfessional.FindAsync(id);
            if (client != null)
            {
                _context.ServiceProfessional.Remove(client);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RegisterServiceProfessionalAsync(ServiceProfessional service)
        {
            _context.ServiceProfessional.Add(service);
            await _context.SaveChangesAsync();
        }

        public async Task<ServiceProfessional?> GetByEmailAsync(string email)
        {
            return await _context.ServiceProfessional.FirstOrDefaultAsync(sp => sp.Email == email);
        }

        public async Task<ServiceProfessional?> GetByPhoneAsync(string phoneNumber)
        {
            return await _context.ServiceProfessional.FirstOrDefaultAsync(sp => sp.PhoneNumber == phoneNumber);
        }

        public async Task<List<ServiceProfessional?>> GetAllByTypeAsync(string type)
        {
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return await _context.ServiceProfessional
                .Where(sp => sp.Skills != null &&
                             EF.Functions.Like("," + sp.Skills + ",", "%," + type + ",%"))
                .ToListAsync();
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }

        public async Task<List<ServiceBooking?>> GetBookingHistoryByIdAsync(int id)
        {
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return await _context.ServiceBooking.Where(sp => sp.ServiceProfessionalId == id).ToListAsync();
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }
    }
}
