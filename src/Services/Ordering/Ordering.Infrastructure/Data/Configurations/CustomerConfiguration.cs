using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ordering.Infrastructure.Data.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .HasConversion(customerId => customerId.Value, //use customerId.Value when storing the ID to DB
                dbId => CustomerId.Of(dbId)); //convert Guid from DB to CustomerId when retrieving
        
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Email).HasMaxLength(255);
        builder.HasIndex(c => c.Email).IsUnique();
    }
}