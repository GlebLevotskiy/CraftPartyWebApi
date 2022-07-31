using CraftParty.Application.Models.Authentication;
using ErrorOr;

namespace CraftParty.Application.Interfaces.Authentication.Validators;

public interface ITokenValidator
{
    Task<ErrorOr<Domain.Entities.RefreshToken>> Validate(RefreshTokenModel refreshTokenModel);
}