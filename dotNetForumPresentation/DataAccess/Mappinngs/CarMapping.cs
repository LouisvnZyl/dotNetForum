using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappinngs
{
    internal class CarMapping : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.ToTable(nameof(Car));

            builder.HasMany(car => car.CarMechanics)
                   .WithOne(carMechanic => carMechanic.Car)
                   .HasForeignKey(carMechanic => carMechanic.CarId);

            //builder.HasOne(car => car.Engine)
            //       .WithOne(engine => engine.Car)
            //       .HasPrincipalKey<Engine>(x => x.Id)
            //       .HasForeignKey<Car>(x => x.EngineId);

            //builder.HasKey(x => x.EngineId);

            //builder.HasOne(car => car.Engine)
            //       .WithOne(engine => engine.Car)
            //       .HasForeignKey<Car>(x => x.EngineId)
            //       .HasPrincipalKey<Engine>(x => x.Id);
        }
    }
}
