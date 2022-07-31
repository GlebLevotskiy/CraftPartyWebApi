namespace CraftParty.Application.Models.Authentication;

public record AuthenticationResult
{
    public Guid Id { get; init; }
    
    public string FirstName { get; init; }
     
    public string LastName { get; init; }
     
    public string Email { get; init; }
     
    public string Token { get; init; }
     
    public string RefreshToken { get; init; }
}