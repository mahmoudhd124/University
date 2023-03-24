using Logic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logic.EntityConfigurations;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.Property(d => d.NationalNumber).HasMaxLength(14).IsRequired();
        builder.HasIndex(d => d.NationalNumber).IsUnique();
    }
}