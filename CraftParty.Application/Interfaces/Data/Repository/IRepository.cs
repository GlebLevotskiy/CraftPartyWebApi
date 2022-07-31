using CraftParty.Domain.Entities;

namespace CraftParty.Application.Interfaces.Data.Repository;

public interface IRepository<T>
    where T : Entity
{
    Task<IEnumerable<T>> GetAllAsync();

    Task<T> GetByIdAsync(Guid id);

    Task AddAsync(T entity);

    void Remove(T entity);

    void Update(T entity);
}