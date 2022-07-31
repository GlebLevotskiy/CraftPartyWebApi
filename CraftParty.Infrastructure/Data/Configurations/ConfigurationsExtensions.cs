using Microsoft.EntityFrameworkCore;

namespace CraftParty.Infrastructure.Data.Configurations;

public static class ConfigurationsExtensions
{
    public static void ApplyConfigurations(this ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new RefreshTokenConfiguration());
    }
}