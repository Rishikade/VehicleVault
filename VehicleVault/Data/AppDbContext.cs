using Microsoft.EntityFrameworkCore;
using VehicleVault.Models;

namespace VehicleVault.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<User> Users { get; set; }   // ✅ ADD THIS
    }
}