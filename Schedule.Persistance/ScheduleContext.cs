using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Schedule.Domain.Entities;
using Schedule.Domain.Interfaces;

namespace Schedule.Persistance
{
    class ScheduleContext : DbContext, IRepository
    {
        public ScheduleContext(DbContextOptions<ScheduleContext> options)
            : base(options)
        {
            Database.Migrate();
            Database.EnsureCreated();
        }

        internal DbSet<Student> Students { get; private set; }
        internal DbSet<Laboratory> Laboratories { get; private set; }
        internal DbSet<Teacher> Teachers { get; private set; }

        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : Entity
        {
            return Set<TEntity>().AsNoTracking();
        }

        public async Task<TEntity> FindByIdAsync<TEntity>(Guid id) where TEntity : Entity
        {
            return await Set<TEntity>().SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddNewAsync<TEntity>(TEntity entity) where TEntity : Entity
            => await Set<TEntity>().AddAsync(entity);


        public async Task SaveAsync() => await SaveChangesAsync();
    }
}
