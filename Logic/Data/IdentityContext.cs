using Logic.EntityConfigurations;
using Logic.Models.IdentityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Logic.Data;

public class IdentityContext : IdentityDbContext<User, Role, string>
{
    public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

    public IdentityContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new UserRefreshTokenConfiguration());
        base.OnModelCreating(builder);
    }
}