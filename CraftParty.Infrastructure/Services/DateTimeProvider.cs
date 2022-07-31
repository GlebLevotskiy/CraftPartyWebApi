using CraftParty.Application.Interfaces.Services;

namespace CraftParty.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}