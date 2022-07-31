using CraftParty.Application.Interfaces.Data.Repository;

namespace CraftParty.Application.Interfaces.Data;

public interface IUnitOfWork
{
    IUsersRepository UsersRepository { get; }
    
    IRefreshTokenRepository RefreshTokens { get; }

    Task CompleteAsync();
}