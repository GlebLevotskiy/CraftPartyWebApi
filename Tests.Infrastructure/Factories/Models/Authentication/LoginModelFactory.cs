using CraftParty.Application.Models.Authentication;

namespace Tests.Infrastructure.Factories.Models.Authentication;

public static class LoginModelFactory
{
    public static LoginModel LoginModel(
        this ITestDataFactory factory,
        string email = null,
        string password = null)
    {
        return new LoginModel
        {
            Email = email,
            Password = password,
        };
    }
}