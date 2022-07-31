namespace CraftParty.Application.Models.Authentication;

public record TokensDataModel
{
    public string JwtToken { get; set; }
    
    public string RefreshToken { get; set; }
}