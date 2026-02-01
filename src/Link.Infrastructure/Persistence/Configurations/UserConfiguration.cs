using Link.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Link.Infrastructure.Persistence.Conffigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{

    public void Configure(EntityTypeBuilder<User> builder)
    {
        // 1. Primary Key
        builder.HasKey(u => u.Id);

        // 2. Indexes
        builder.HasIndex(u => u.Email).IsUnique();

        // 3. Properties
        builder.Property(u => u.Email)
        .HasMaxLength(255)
        .IsRequired();

        builder.Property(u => u.PasswordHash)
        .IsRequired();

    }
}