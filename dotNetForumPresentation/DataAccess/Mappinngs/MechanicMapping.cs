using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappinngs
{
    public class MechanicMapping : IEntityTypeConfiguration<Mechanic>
    {
        public void Configure(EntityTypeBuilder<Mechanic> builder)
        {
            builder.ToTable("Mechanic");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.DateOfBirth)
                   .HasConversion(toDb => toDb.UtcTicks, fromDb =>  DateTimeOffset.MinValue.AddTicks(fromDb));
        }
    }
}
