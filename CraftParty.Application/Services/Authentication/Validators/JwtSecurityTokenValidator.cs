using System.IdentityModel.Tokens.Jwt;
using CraftParty.Application.Interfaces.Authentication.Validators;
using Microsoft.IdentityModel.Tokens;

namespace CraftParty.Application.Services.Authentication.Validators;

public class JwtSecurityTokenValidator : IJwtSecurityTokenValidator
{
    public bool IsValid(SecurityToken validatedToken)
    {
        return validatedToken is JwtSecurityToken jwtSecurityToken &&
            jwtSecurityToken.Header.Alg.Equals(
                 SecurityAlgorithms.HmacSha256,
                 StringComparison.InvariantCultureIgnoreCase);
    }
}