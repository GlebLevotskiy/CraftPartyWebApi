using CraftParty.Application.Interfaces.Authentication.Validators.RefreshToken;

namespace CraftParty.Application.Services.Authentication.Validators.RefreshToken;

public class RefreshTokenIsUsedValidator : IRefreshTokenValidator
{
    public bool IsValid(Domain.Entities.RefreshToken refreshToken)
    {
        return !refreshToken.IsUsed;
    }
}