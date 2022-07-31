using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CraftParty.Application.Interfaces.Authentication.Validators;

namespace CraftParty.Application.Services.Authentication.Validators;

public class TokenExpiryDateClaimValidator : ITokenExpiryDateClaimValidator
{
    public bool IsValid(ClaimsPrincipal principal)
    {
        var expiryDateClaim = principal.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Exp);

        return expiryDateClaim != null &&
            ConvertFromUnixTimestamp(long.Parse(expiryDateClaim.Value)) < DateTime.UtcNow;
    }

    private static DateTime ConvertFromUnixTimestamp(double timestamp)
    {
        DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        return origin.AddSeconds(timestamp);
    }
}