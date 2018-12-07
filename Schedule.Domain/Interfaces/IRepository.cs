using System;
using System.Linq;
using System.Threading.Tasks;
using Schedule.Domain.Entities;

namespace Schedule.Domain.Interfaces
{
    public interface IRepository
    {
        IQueryable<TEntity> GetAll<TEntity>()
            where TEntity : Entity;

        Task<TEntity> FindByIdAsync<TEntity>(Guid id)
            where TEntity : Entity;

        Task AddNewAsync<TEntity>(TEntity entity)
            where TEntity : Entity;

        Task DeleteByIdAsync<TEntity>(Guid id)
            where TEntity : Entity;

        Task UpdateAsync<TEntity>(Guid id, TEntity entity)
            where TEntity : Entity;

        Task SaveAsync();
    }
}
