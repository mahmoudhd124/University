using Logic.Models.IdentityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logic.EntityConfigurations;

public class UserRefreshTokenConfiguration : IEntityTypeConfiguration<UserRefreshToken>
{
    public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.UserId).HasMaxLength(450);
        builder.Property(u => u.UserAgent).HasMaxLength(511);
        builder.Property(u => u.RefreshToken).HasMaxLength(64);

        builder
            .HasOne(u => u.User)
            .WithMany()
            .HasForeignKey(u => u.UserId);

        builder.HasIndex(u => new { u.UserId, u.UserAgent }).IsUnique();
    }
}