using CraftParty.Domain.Entities;

namespace CraftParty.Application.Interfaces.Data.Repository;

public interface IUsersRepository : IRepository<User>
{
    Task<User> GetUserByEmailAsync(string email);
}