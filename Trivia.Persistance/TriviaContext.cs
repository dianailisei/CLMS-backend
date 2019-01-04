using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Trivia.Domain.Entities;
using Trivia.Domain.Interfaces;

namespace Trivia.Persistance
{
    public class TriviaContext : DbContext, IReadRepository, IWriteRepository
    {
        public TriviaContext(DbContextOptions<TriviaContext> options)
            : base(options)
        {
            Database.Migrate();
            Database.EnsureCreated();
        }


        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : Entity
        {
            return Set<TEntity>().Where(e => e.Available == true).AsNoTracking();
        }

        public async Task<TEntity> FindByIdAsync<TEntity>(Guid id) where TEntity : Entity
        {
            return await Set<TEntity>().Where(e => e.Available == true).SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddNewAsync<TEntity>(TEntity entity) where TEntity : Entity
            => await Set<TEntity>().AddAsync(entity);

        public async Task DeleteByIdAsync<TEntity>(Guid id) where TEntity : Entity
        {
            var entity = await FindByIdAsync<TEntity>(id);
            if (entity != null)
            {
                var deleted = entity;
                deleted.Available = false;
                Entry(entity).CurrentValues.SetValues(deleted);
            }
        }

        public async Task UpdateAsync<TEntity>(Guid id, TEntity entity) where TEntity : Entity
        {
            TEntity exist = await Set<TEntity>().FindAsync(id);
            if (exist != null)
            {
                Entry(exist).CurrentValues.SetValues(entity);
            }
        }

        public async Task SaveAsync() => await SaveChangesAsync();
    }
}
