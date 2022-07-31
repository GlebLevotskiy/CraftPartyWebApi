using CraftParty.Domain.Entities;

namespace CraftParty.Application.Interfaces.Data.Repository;

public interface IRefreshTokenRepository : IRepository<RefreshToken>
{
    Task<RefreshToken> GetByRefreshTokenAsync(string refreshToken);
}