using Logic.EntityConfigurations;
using Logic.Models;
using Logic.Models.IdentityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Logic.Data;

public class IdentityContext : IdentityDbContext<User, Role, string>
{
    public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<SubjectFiles> SubjectMaterials { get; set; }
    public DbSet<DoctorSubject> DoctorSubjects { get; set; }

    public IdentityContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new UserRefreshTokenConfiguration());
        builder.ApplyConfiguration(new RoleConfiguration());
        builder.ApplyConfiguration(new DoctorConfiguration());
        builder.ApplyConfiguration(new SubjectConfiguration());
        builder.ApplyConfiguration(new DoctorSubjectConfiguration());
        builder.ApplyConfiguration(new SubjectFileConfiguration());
        base.OnModelCreating(builder);
    }
}