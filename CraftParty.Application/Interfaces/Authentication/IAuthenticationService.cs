using CraftParty.Application.Models.Authentication;
using ErrorOr;

namespace CraftParty.Application.Interfaces.Authentication;

public interface IAuthenticationService
{
    Task<ErrorOr<AuthenticationResult>> Login(LoginModel model);

    Task<ErrorOr<AuthenticationResult>> Register(RegisterModel model);
}