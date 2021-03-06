﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Schedule.Domain.Entities;
using Schedule.Domain.Interfaces;

namespace Schedule.Persistance
{
    public class ScheduleContext : DbContext, IReadRepository, IWriteRepository
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
        internal DbSet<Subject> Subjects { get; private set; }
        internal DbSet<Lecture> Lectures { get; private set; }

        /*protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=localhost;Database=dotnot;Trusted_Connection=True;");
        }
        */


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
