using Logic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logic.EntityConfigurations;

public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
{
    public void Configure(EntityTypeBuilder<Subject> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Department).HasMaxLength(3).IsRequired();
        builder.Property(s => s.Code).HasMaxLength(3).IsRequired();

        builder.HasIndex(s => s.Code).IsUnique();
        builder.HasIndex(s => s.Department);
    }
}