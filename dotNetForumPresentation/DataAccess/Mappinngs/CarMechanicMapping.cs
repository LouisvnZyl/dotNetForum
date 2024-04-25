using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappinngs
{
    internal class CarMechanicMapping : IEntityTypeConfiguration<CarMechanic>
    {
        public void Configure(EntityTypeBuilder<CarMechanic> builder)
        {
            builder.ToTable(nameof(CarMechanic));

            builder.HasKey(cm => new { cm.CarId, cm.MechanicId });
        }
    }
}
