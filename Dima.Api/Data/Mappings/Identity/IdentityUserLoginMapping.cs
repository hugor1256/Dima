using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings.Identity;

public class IdentityUserLoginMapping : IEntityTypeConfiguration<IdentityUserLogin<long>>
{
    public void Configure(EntityTypeBuilder<IdentityUserLogin<long>> builder)
    {
        builder.ToTable("IdentityUserLogin");
        builder.HasKey(s => new { s.LoginProvider, s.ProviderKey });
        
        builder.Property(s => s.LoginProvider).HasMaxLength(128);
        builder.Property(s => s.ProviderKey).HasMaxLength(128);
        builder.Property(s => s.ProviderDisplayName).HasMaxLength(255);
    }
}