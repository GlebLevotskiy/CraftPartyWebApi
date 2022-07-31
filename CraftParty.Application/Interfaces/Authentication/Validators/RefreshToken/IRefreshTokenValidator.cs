namespace CraftParty.Application.Interfaces.Authentication.Validators.RefreshToken;

public interface IRefreshTokenValidator
{
    bool IsValid(Domain.Entities.RefreshToken refreshToken);
}