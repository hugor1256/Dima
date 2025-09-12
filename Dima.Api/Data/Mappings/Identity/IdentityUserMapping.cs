using Dima.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings.Identity;

public class IdentityUserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("IdentityUser");
        builder.HasKey(s => s.Id);
        
        builder.HasIndex(s => s.NormalizedUserName).IsUnique();
        builder.HasIndex(s => s.NormalizedEmail).IsUnique();
        
        builder.Property(s => s.Email).HasMaxLength(180);
        builder.Property(s => s.NormalizedEmail).HasMaxLength(180);
        builder.Property(s => s.UserName).HasMaxLength(180);
        builder.Property(s => s.NormalizedUserName).HasMaxLength(180);
        builder.Property(s => s.PhoneNumber).HasMaxLength(20);
        builder.Property(s => s.ConcurrencyStamp).IsConcurrencyToken();

        builder.HasMany<IdentityUserClaim<long>>().WithOne().HasForeignKey(s => s.UserId).IsRequired();
        builder.HasMany<IdentityUserLogin<long>>().WithOne().HasForeignKey(s => s.UserId).IsRequired();
        builder.HasMany<IdentityUserToken<long>>().WithOne().HasForeignKey(s => s.UserId).IsRequired();
        builder.HasMany<IdentityUserRole<long>>().WithOne().HasForeignKey(s => s.UserId).IsRequired();
    }
}