namespace CraftParty.Application.Interfaces.Services;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}