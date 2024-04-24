using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Car> Cars { get; set; }
        //public DbSet<CarMechanic> CarMechanics { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Engine> Engines { get; set; }
        //public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Mechanic> Mechanics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
