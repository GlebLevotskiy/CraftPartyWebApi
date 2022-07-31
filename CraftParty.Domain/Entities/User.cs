using Microsoft.AspNetCore.Identity;

namespace CraftParty.Domain.Entities;

public class User : Entity
{
    public string IdentityId { get; set; }
    
    public IdentityUser IdentityUser { get; set; } 
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Email { get; set; }
}