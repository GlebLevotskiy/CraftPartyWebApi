using Microsoft.IdentityModel.Tokens;

namespace CraftParty.Application.Interfaces.Authentication.Validators;

public interface IJwtSecurityTokenValidator
{
    bool IsValid(SecurityToken validatedToken);
}