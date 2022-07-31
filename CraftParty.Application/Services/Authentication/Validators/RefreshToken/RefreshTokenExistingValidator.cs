using CraftParty.Application.Interfaces.Authentication.Validators.RefreshToken;

namespace CraftParty.Application.Services.Authentication.Validators.RefreshToken;

public class RefreshTokenExistingValidator : IRefreshTokenValidator
{
    public bool IsValid(Domain.Entities.RefreshToken refreshToken)
    {
        return refreshToken != null;
    }
}