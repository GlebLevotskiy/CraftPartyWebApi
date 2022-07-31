using CraftParty.Application.Models.Authentication;

namespace Tests.Infrastructure.Factories.Models.Authentication;

public static class AuthenticationResultFactory 
{
    public static AuthenticationResult AuthenticationResult(
        this ITestDataFactory factory)
    {
        return new AuthenticationResult();
    }
}