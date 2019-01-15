using System;
using System.Threading.Tasks;
using Trivia.Domain.Entities;

namespace Trivia.Domain.Interfaces
{
    public interface IWriteRepository
    {
        Task AddNewAsync<TEntity>(TEntity entity)
            where TEntity : Entity;

        Task DeleteByIdAsync<TEntity>(Guid id)
            where TEntity : Entity;

        Task UpdateAsync<TEntity>(Guid id, TEntity entity)
            where TEntity : Entity;

        Task SaveAsync();
    }
}
