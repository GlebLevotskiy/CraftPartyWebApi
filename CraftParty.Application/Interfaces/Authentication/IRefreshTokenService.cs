using CraftParty.Application.Models.Authentication;
using ErrorOr;

namespace CraftParty.Application.Interfaces.Authentication;

public interface IRefreshTokenService
{
    Task<ErrorOr<TokenRefreshResult>> RefreshToken(RefreshTokenModel refreshTokenModel);
}