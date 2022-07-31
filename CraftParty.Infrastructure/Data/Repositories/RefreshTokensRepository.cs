using CraftParty.Application.Interfaces.Data.Repository;
using CraftParty.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CraftParty.Infrastructure.Data.Repositories;

public class RefreshTokensRepository : Repository<RefreshToken>, IRefreshTokenRepository
{
    private readonly DbSet<RefreshToken> _dbSet;

    public RefreshTokensRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbSet = dbContext.RefreshTokens;
    }

    public Task<RefreshToken> GetByRefreshTokenAsync(string refreshToken)
    {
        return _dbSet.FirstOrDefaultAsync(rt => rt.Token == refreshToken);
    }
}