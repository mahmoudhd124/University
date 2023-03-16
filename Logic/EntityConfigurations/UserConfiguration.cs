using Logic.Models.IdentityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logic.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Id).ValueGeneratedOnAdd();

        builder.Property(u => u.FirstName).HasMaxLength(63).IsRequired();
        builder.Property(u => u.LastName).HasMaxLength(63).IsRequired();
    }
}