using CraftParty.Application.Models.Users;

namespace CraftParty.Application.Interfaces.Users;

public interface IUsersRetrievingService
{
    public Task<IEnumerable<UserModel>> GetUsers();
}