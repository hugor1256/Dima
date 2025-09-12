using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings.Identity;

public class IdentityRoleMapping : IEntityTypeConfiguration<IdentityRole<long>>
{
    public void Configure(EntityTypeBuilder<IdentityRole<long>> builder)
    {
        builder.ToTable("IdentityRole");
        builder.HasKey(s => s.Id);
        
        builder.HasIndex(s => s.NormalizedName).IsUnique();
        
        builder.Property(s => s.ConcurrencyStamp).IsConcurrencyToken();
        builder.Property(s => s.Name).HasMaxLength(256);
        builder.Property(s => s.NormalizedName).HasMaxLength(256);
    }
}