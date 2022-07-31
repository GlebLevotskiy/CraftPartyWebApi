namespace CraftParty.Infrastructure.Settings;

public class JwtSettings
{
    public string Secret { get; init; }
    
    public TimeSpan ExpiryTimeFrame { get; init; }
    
    public string Issuer { get; init; }
    
    public string Audience { get; init; }
}