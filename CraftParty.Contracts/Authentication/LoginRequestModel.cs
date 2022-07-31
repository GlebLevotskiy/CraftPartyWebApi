namespace CraftParty.Contracts.Authentication;

public record LoginRequestModel
{
    public string Email { get; init; }
     
    public string Password { get; init; }
}