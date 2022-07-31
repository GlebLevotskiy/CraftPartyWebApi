using CraftParty.Application.Interfaces.Data.Repository;
using CraftParty.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CraftParty.Infrastructure.Data.Repositories;

public class Repository<T> : IRepository<T>
    where T : Entity
{
    protected readonly DbSet<T> _dbSet;

    public Repository(ApplicationDbContext dbContext)
    {
        _dbSet = dbContext.Set<T>();
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<T> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public virtual void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    public virtual void Update(T entity)
    {
        _dbSet.Update(entity);
    }
}