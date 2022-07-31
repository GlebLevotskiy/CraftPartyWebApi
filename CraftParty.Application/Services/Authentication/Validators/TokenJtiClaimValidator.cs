using System.Security.Claims;
using CraftParty.Application.Interfaces.Authentication.Validators;
using Microsoft.IdentityModel.JsonWebTokens;

namespace CraftParty.Application.Services.Authentication.Validators;

public class TokenJtiClaimValidator : ITokenJtiClaimValidator
{
    public bool IsValid(string refreshTokenJwtId, ClaimsPrincipal principal)
    {
        var jtiClaim = principal.Claims.SingleOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);

        return jtiClaim != null &&
            refreshTokenJwtId == jtiClaim.Value;
    }
}