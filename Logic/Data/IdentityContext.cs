using Logic.EntityConfigurations;
using Logic.Models;
using Logic.Models.IdentityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Logic.Data;

public class IdentityContext : IdentityDbContext<User, Role, string>
{
    public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<SubjectFiles> SubjectMaterials { get; set; }
    public DbSet<DoctorSubject> DoctorSubjects { get; set; }
    public DbSet<Message> Messages { get; set; }

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
        builder.ApplyConfiguration(new MessageConfiguration());
        builder.ApplyUtcDateTimeConverter();
        base.OnModelCreating(builder);
    }
    
}

public static class UtcDateAnnotation
{
    private const string IsUtcAnnotation = "IsUtc";

    private static readonly ValueConverter UtcConverter =
        new ValueConverter<DateTime, DateTime>(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

    private static readonly ValueConverter<DateTime?, DateTime?> UtcNullableConverter =
        new ValueConverter<DateTime?, DateTime?>(v => v,
            v => v == null ? v : DateTime.SpecifyKind(v.Value, DateTimeKind.Utc));

    public static PropertyBuilder<TProperty> IsUtc<TProperty>(this PropertyBuilder<TProperty> builder,
        bool isUtc = true) =>
        builder.HasAnnotation(IsUtcAnnotation, isUtc);

    private static bool IsUtc(this IMutableProperty property) =>
        ((bool?) property.FindAnnotation(IsUtcAnnotation)?.Value) ?? true;

    /// <summary>
    /// Make sure this is called after configuring all your entities.
    /// </summary>
    public static void ApplyUtcDateTimeConverter(this ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (!property.IsUtc())
                {
                    continue;
                }

                if (property.ClrType == typeof(DateTime))
                {
                    property.SetValueConverter(UtcConverter);
                }

                if (property.ClrType == typeof(DateTime?))
                {
                    property.SetValueConverter(UtcNullableConverter);
                }
            }
        }
    }
}