using Domain.Models;
using Microsoft.EntityFrameworkCore;
using DataAccess.Mappinngs;

namespace DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        //public DbSet<Customer> Customers { get; set; }
        //public DbSet<Car> Cars { get; set; }
        ////public DbSet<CarMechanic> CarMechanics { get; set; }
        //public DbSet<Engine> Engines { get; set; }
        ////public DbSet<Manufacturer> Manufacturers { get; set; }
        //public DbSet<Mechanic> Mechanics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new EmptyCustomerMapping());
            modelBuilder.ApplyConfiguration(new CarCustomerOnlyMapping(mapMechanic: true));
            modelBuilder.ApplyConfiguration(new MechanicValueConversionMapping());
            //modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
