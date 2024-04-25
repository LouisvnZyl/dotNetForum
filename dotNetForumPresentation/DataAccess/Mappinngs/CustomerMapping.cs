using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappinngs
{
    internal class CustomerMapping : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable(nameof(Customer));

        //    builder.HasKey(customer => customer.Id);

        //    builder.HasMany(customer => customer.Cars)
        //           .WithOne(car => car.Customer);
        }
    }
}
