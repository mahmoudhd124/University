using System.Text;
using Logic;
using Logic.Data;
using Logic.Helpers;
using Logic.Models.IdentityModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Api.Helpers;

public static class ProgramConfigurations
{
    public static void AddCustomConfiguration(this IServiceCollection services, ConfigurationManager configuration)
    {
        //add connections string
        var conStr = configuration.GetSection("connectionStrings")["default"];
        services.AddDbContext<IdentityContext>(opt => opt.UseSqlServer(conStr));

        //add Microsoft Identity
        services.AddDefaultIdentity<User>(opt => opt.SignIn.RequireConfirmedAccount = true)
            .AddRoles<Role>()
            .AddEntityFrameworkStores<IdentityContext>();

        //add autommaper
        services.AddAutoMapper(typeof(EntryPoint).Assembly);
        services.AddAutoMapper(typeof(Program).Assembly);

        //add MediatR
        services.AddMediatR(opt => { opt.RegisterServicesFromAssemblies(typeof(EntryPoint).Assembly); });

        //add cors
        services.AddCors(opt => opt.AddPolicy("allowLocalAtPort5173", builder =>
        {
            builder
                .WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .Build();
        }));

        //add helper classes configurations
        services.Configure<Jwt>(configuration.GetSection("Jwt"));
        services.Configure<Expiry>(configuration.GetSection("Expiry"));

        //add token configuration
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:key"]!)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        //require authenticated user
        services.AddAuthorization(options =>
        {
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        });

    }
}