using Microsoft.AspNetCore.Identity;

namespace CraftParty.Domain.Entities;

public class RefreshToken : Entity
{
    public string IdentityUserId { get; set; }

    public IdentityUser IdentityUser { get; set; }
    
    public string Token { get; set; }
    
    public string JwtId { get; set; }
    
    public bool IsUsed { get; set; }
    
    public bool IsRevoked { get; set; }
    
    public DateTime ExpiryDate { get; set; }
}