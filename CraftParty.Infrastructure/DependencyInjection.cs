using CraftParty.Application.Interfaces.Authentication;
using CraftParty.Application.Interfaces.Data;
using CraftParty.Application.Interfaces.Data.Repository;
using CraftParty.Application.Interfaces.Services;
using CraftParty.Infrastructure.Common;
using CraftParty.Infrastructure.Data;
using CraftParty.Infrastructure.Data.Repositories;
using CraftParty.Infrastructure.Services;
using CraftParty.Infrastructure.Services.Authentication;
using CraftParty.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CraftParty.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        
        services.AddDbContext<ApplicationDbContext>(opt =>
            opt.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}