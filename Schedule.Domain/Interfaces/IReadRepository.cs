using System;
using System.Linq;
using System.Threading.Tasks;
using Schedule.Domain.Entities;

namespace Schedule.Domain.Interfaces
{
    public interface IReadRepository
    {
        IQueryable<TEntity> GetAll<TEntity>()
            where TEntity : Entity;

        Task<TEntity> FindByIdAsync<TEntity>(Guid id)
            where TEntity : Entity;
    }
}
