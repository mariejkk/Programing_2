using Microsoft.EntityFrameworkCore;
using Salon_SpaAPI.Models.Entities;

namespace Salon_SpaAPI.Data
{
    public class Salon_SpaDbContext : DbContext
    {
        public Salon_SpaDbContext(DbContextOptions<Salon_SpaDbContext> options)
            : base(options) { }

     
        public DbSet<Service> Services { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Service>()
                .HasMany<Appointment>()
                .WithOne(a => a.Service)
                .HasForeignKey(a => a.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}