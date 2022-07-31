using System.Text;
using CraftParty.Infrastructure.Data;
using CraftParty.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace CraftParty.Api.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddAuthentication(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var jwtSettings = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
        var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);
        
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false, // To be updated
            ValidateAudience = false, // To be updated
            RequireExpirationTime = false,
            ValidateLifetime = true,
        };

        services.AddSingleton(tokenValidationParameters);
        
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(jwtOptions =>
        {
            jwtOptions.SaveToken = true;
            jwtOptions.TokenValidationParameters = tokenValidationParameters;
        });

        services.AddDefaultIdentity<IdentityUser>(userOptions =>
        {
            userOptions.SignIn.RequireConfirmedAccount = true;
        }).AddEntityFrameworkStores<ApplicationDbContext>();

        return services;
    }
}