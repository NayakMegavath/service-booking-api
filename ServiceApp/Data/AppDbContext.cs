using Microsoft.EntityFrameworkCore;
using ServiceApp.Model;

namespace ServiceApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Client> Client { get; set; }
        public DbSet<ServiceProfessional> ServiceProfessional { get; set; }
        public DbSet<ServiceBooking> ServiceBooking { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
