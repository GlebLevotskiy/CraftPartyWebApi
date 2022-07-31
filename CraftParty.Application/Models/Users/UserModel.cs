namespace CraftParty.Application.Models.Users;

public record UserModel
{
    public string Email { get; init; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
}