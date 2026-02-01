using Link.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Link.Infrastructure.Persistence.Conffigurations;

public class LinkConfiguration : IEntityTypeConfiguration<LinkObject>
{

    public void Configure(EntityTypeBuilder<LinkObject> builder)
    {
        // 1. Primary Key
        builder.HasKey(l => l.Id);

        // 2. Indexes (CRITICAL for performance)
        // We look up by ShortCode constantly, so it MUST be indexed and unique.
        builder.HasIndex(l => l.ShortCode).IsUnique();


        // 3. Properties
        builder.Property(l => l.ShortCode)
        .HasMaxLength(10)
        .IsRequired();

        builder.Property(l => l.OriginalUrl)
        .IsRequired();

        // 4. Relationships
        builder.HasOne(l => l.User)
        .WithMany(u => u.Links)
        .HasForeignKey(l => l.UserId)
        .OnDelete(DeleteBehavior.Cascade);
    }
}