using Domain.Models;
using Microsoft.EntityFrameworkCore;
using DataAccess.Mappinngs;
using Domain.Models.Cars;

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


            //*Modeling inheritance: https://learn.microsoft.com/en-us/ef/core/modeling/inheritance*//
            /*Table Per Hierarchy
             * Adding the two derived types' mappings (they're empty), causes EF to apply Table Per Hierarchy mapping by convention
             TPH: nullable columns in the Car table*/
            modelBuilder.ApplyConfiguration(new BakkieCarsMapping());
            modelBuilder.ApplyConfiguration(new SchoolBusMapping());

            /*TPType
             You will get 3 tables: one for the base type, and one each for the derived types
            modelBuilder.Entity<Car>().UseTptMappingStrategy();
  */
            /*TPDiscreet
             You will get two tables: one per derived type, with "duplicate" columns for the base class
            This is "new" as in EFCore 7
            modelBuilder.Entity<Car>().UseTpcMappingStrategy();
            */
            
            //modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
