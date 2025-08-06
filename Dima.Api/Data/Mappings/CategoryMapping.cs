using Dima.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings;

public class CategoryMapping : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Category");

        builder.HasKey(x => x.Id);

        builder.Property(s => s.Title)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(255);
        
        builder.Property(s => s.Description).IsRequired()
            .IsRequired(false)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(255);
        
        builder.Property(s => s.UserId)
            .IsRequired()
            .HasColumnType("VARCHAR")
            .HasMaxLength(255);
    }
}