namespace CraftParty.Application.Models.Authentication;

public record RefreshTokenModel
{
    public string Token { get; init; }
    
    public string RefreshToken { get; init; }
}