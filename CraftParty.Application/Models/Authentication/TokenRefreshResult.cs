namespace CraftParty.Application.Models.Authentication;

public record TokenRefreshResult
{
    public string Token { get; init; }

    public string RefreshToken { get; init; }
}