namespace CraftParty.Infrastructure.Common;

public class StringService
{
    public static string GenerateRandomString(int length)
    {
        var random = new Random();
        var chars = "QWERTYUIOPASDFGHJKLZXCVBNM0123456789";

        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}