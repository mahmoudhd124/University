using Logic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logic.EntityConfigurations;

public class DoctorSubjectConfiguration : IEntityTypeConfiguration<DoctorSubject>
{
    public void Configure(EntityTypeBuilder<DoctorSubject> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasOne(x => x.Doctor)
            .WithMany(d => d.DoctorSubjects)
            .HasForeignKey(x => x.DoctorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.Subject)
            .WithOne(s => s.DoctorSubject)
            .HasForeignKey<DoctorSubject>(x => x.SubjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}