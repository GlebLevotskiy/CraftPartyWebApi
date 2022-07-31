using System.Security.Claims;

namespace CraftParty.Application.Interfaces.Authentication.Validators;

public interface ITokenExpiryDateClaimValidator
{
    bool IsValid(ClaimsPrincipal principal);
}