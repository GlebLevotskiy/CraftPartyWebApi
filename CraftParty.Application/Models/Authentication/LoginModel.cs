namespace CraftParty.Application.Models.Authentication;

public record LoginModel
{
    public string Email { get; init; }
     
    public string Password { get; init; }
};