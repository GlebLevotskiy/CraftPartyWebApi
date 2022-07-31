using CraftParty.Application.Interfaces.Authentication;
using CraftParty.Application.Interfaces.Authentication.Validators;
using CraftParty.Application.Interfaces.Authentication.Validators.RefreshToken;
using CraftParty.Application.Interfaces.Users;
using CraftParty.Application.Mapping;
using CraftParty.Application.Services.Authentication;
using CraftParty.Application.Services.Authentication.Validators;
using CraftParty.Application.Services.Authentication.Validators.RefreshToken;
using CraftParty.Application.Services.Users;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CraftParty.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(typeof(DependencyInjection).Assembly);
        services.AddAutoMapper(typeof(ApplicationMappingProfile));

        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IUsersRetrievingService, UsersRetrievingService>();
        services.AddScoped<IRefreshTokenValidator, RefreshTokenExistingValidator>();
        services.AddScoped<IRefreshTokenValidator, RefreshTokenExpirationValidator>();
        services.AddScoped<IRefreshTokenValidator, RefreshTokenIsUsedValidator>();
        services.AddScoped<IRefreshTokenValidator, RefreshTokenIsRevokedValidator>();
        services.AddScoped<IJwtSecurityTokenValidator, JwtSecurityTokenValidator>();
        services.AddScoped<ITokenExpiryDateClaimValidator, TokenExpiryDateClaimValidator>();
        services.AddScoped<ITokenJtiClaimValidator, TokenJtiClaimValidator>();
        services.AddScoped<ITokenValidator, TokenValidator>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();

        return services;
    }
}