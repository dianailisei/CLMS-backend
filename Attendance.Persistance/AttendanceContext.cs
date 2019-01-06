using System;
using System.Linq;
using System.Threading.Tasks;
using Attendance.Domain;
using Attendance.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.WindowsAzure.Storage.Table;

namespace Attendance.Persistance
{
    public class AttendanceContext : DbContext, IReadRepository, IWriteRepository
    {
        public AttendanceContext(DbContextOptions<AttendanceContext> options)
            : base(options)
        {
            Database.Migrate();
            Database.EnsureCreated();
        }

        internal DbSet<Presence> Presences { get; private set; }
        internal DbSet<Session> Sessions { get; private set; }

        /*protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=DESKTOP-99S221B;Database=Attendance;Trusted_Connection=True;");
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Presence>().HasOne(s => s.SessionEnrolled)
                .WithMany(a => a.Presences);
        }


        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : Entity
        {
            return Set<TEntity>().Where(e => e.Available).AsNoTracking();
        }

        public async Task<TEntity> FindByIdAsync<TEntity>(Guid id) where TEntity : Entity
        {
            return await Set<TEntity>().Where(e => e.Available).SingleOrDefaultAsync(e => e.Id == id);
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

        public async Task AttachObject<TEntity>(TEntity entity) where TEntity : Entity
        {
            Set<TEntity>().Attach(entity);
        }

        public async Task SaveAsync() => await SaveChangesAsync();
    }
}
