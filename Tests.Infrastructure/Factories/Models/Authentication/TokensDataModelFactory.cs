using CraftParty.Application.Models.Authentication;

namespace Tests.Infrastructure.Factories.Models.Authentication;

public static class TokensDataModelFactory
{
    public static TokensDataModel TokensDataModel(
        this ITestDataFactory factory,
        string jwtToken = null,
        string refreshToken = null)
    {
        return new TokensDataModel
        {
            JwtToken = jwtToken,
            RefreshToken = refreshToken,
        };
    }
    
}