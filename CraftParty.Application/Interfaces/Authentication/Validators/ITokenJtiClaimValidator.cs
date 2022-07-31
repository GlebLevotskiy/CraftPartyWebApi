using System.Security.Claims;

namespace CraftParty.Application.Interfaces.Authentication.Validators;

public interface ITokenJtiClaimValidator
{
    bool IsValid(string refreshTokenJwtId, ClaimsPrincipal principal);
}