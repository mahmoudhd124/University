using Logic.Models.IdentityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logic.EntityConfigurations;

public class RoleConfiguration:IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(r => r.Id).ValueGeneratedOnAdd();
    }
}