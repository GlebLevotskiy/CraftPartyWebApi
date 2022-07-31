using CraftParty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CraftParty.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>

{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Email).IsRequired();
        builder.Property(u => u.FirstName).IsRequired();
        builder.Property(u => u.LastName).IsRequired();
        
        builder.HasOne(u => u.IdentityUser)
            .WithOne()
            .HasForeignKey<User>(u => u.IdentityId);
    }
}