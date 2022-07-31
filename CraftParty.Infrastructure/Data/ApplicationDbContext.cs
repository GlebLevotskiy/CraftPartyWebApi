using CraftParty.Domain.Entities;
using CraftParty.Infrastructure.Data.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CraftParty.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(RefreshTokenConfiguration).Assembly);
        base.OnModelCreating(builder);
    }
}