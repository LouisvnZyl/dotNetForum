using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappinngs;
public class CarMechanicMapping
{
    public void Configure(EntityTypeBuilder<CarMechanic> builder)
    {
        builder.ToTable(nameof(CarMechanic));
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedOnAdd();
    }
}
