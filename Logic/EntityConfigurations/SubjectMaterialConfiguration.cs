using Logic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logic.EntityConfigurations;

public class SubjectMaterialConfiguration : IEntityTypeConfiguration<SubjectMaterial>
{
    public void Configure(EntityTypeBuilder<SubjectMaterial> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Material).HasMaxLength(511).IsRequired();
        builder.Property(s => s.StoredName).HasMaxLength(254).IsRequired();
        builder.Property(s => s.Date).IsRequired();

        builder
            .HasOne(s => s.Subject)
            .WithMany(s => s.SubjectMaterials)
            .HasForeignKey(s => s.SubjectId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}