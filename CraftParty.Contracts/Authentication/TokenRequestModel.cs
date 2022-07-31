namespace CraftParty.Contracts.Authentication;

public record TokenRequestModel
{
    public string Token { get; init; }
    
    public string RefreshToken { get; init; }
}