using CraftParty.Application.Interfaces.Data;
using CraftParty.Application.Interfaces.Data.Repository;
using CraftParty.Infrastructure.Data.Repositories;

namespace CraftParty.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ApplicationDbContext _applicationDbContext;

    public UnitOfWork(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        UsersRepository = new UsersRepository(applicationDbContext);
        RefreshTokens = new RefreshTokensRepository(applicationDbContext);
    }

    public IUsersRepository UsersRepository { get; }
    
    public IRefreshTokenRepository RefreshTokens { get; }

    public async Task CompleteAsync()
    {
        await _applicationDbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _applicationDbContext.Dispose();
    }
}