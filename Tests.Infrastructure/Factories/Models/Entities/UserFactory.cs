using CraftParty.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Tests.Infrastructure.Factories.Models.Entities;

public static class UserFactory
{
    public static User User(
        this ITestDataFactory factory,
        string identityId = null,
        IdentityUser identityUser = null,
        string firstName = null,
        string lastName = null,
        string email = null)
    {
        return new User
        {
            IdentityId = identityId ?? identityUser?.Id ?? Guid.NewGuid().ToString(),
            IdentityUser = identityUser,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
        };
    }
}