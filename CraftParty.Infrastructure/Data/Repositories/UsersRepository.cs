using CraftParty.Application.Interfaces.Data.Repository;
using CraftParty.Domain.Entities;
using CraftParty.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace CraftParty.Infrastructure.Data.Repositories;

public class UsersRepository : Repository<User>, IUsersRepository
{
    public UsersRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public override async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _dbSet.Where(u => u.ActivityStatus == EntityActivityStatus.Active)
            .AsNoTracking().ToListAsync();
    }

    public Task<User> GetUserByEmailAsync(string email)
    {
        return _dbSet.Include(u => u.IdentityUser).SingleOrDefaultAsync(user => user.Email == email);
    }
}