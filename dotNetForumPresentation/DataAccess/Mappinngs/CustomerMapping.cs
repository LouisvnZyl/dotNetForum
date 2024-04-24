using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappinngs;

internal class CustomerMappingWithRelationships : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable(nameof(Customer));

        builder.HasKey(customer => customer.Id);

        builder.HasMany(customer => customer.Cars)
               .WithOne(car => car.Customer);
    }
}


/// <summary>
/// Register the Entity on the DbContext without specifying any specific mappings. 
/// This will assume your classes follow ALL the expected conventions
/// </summary>
internal class EmptyCustomerMapping : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        //builder.ToTable(nameof(Customer));

        //builder.HasKey(customer => customer.Id);

        //builder.Ignore(x=>x.Cars);
    }
}

/// <summary>
/// Register the Entity on the DbContext with specific mappings. 
/// Do this when you are not following the conventions
/// </summary>
internal class CustomerMappingFloutingConvention : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customer2", "bob");
        builder.HasKey(customer => customer.Id);
        builder.Property(customer => customer.Id).HasColumnName("Id2");

        builder.Ignore(x => x.Cars); //A property that should not be mapped to the db at all
    }
}