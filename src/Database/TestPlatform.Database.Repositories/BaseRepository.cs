namespace TestPlatform.Database.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Entities;
    using TestPlatform.Database.Repositories.Interfaces;

    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        public BaseRepository(TestPlatformDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.DbSet = this.DbContext.Set<TEntity>();
        }

        protected TestPlatformDbContext DbContext { get; }

        protected DbSet<TEntity> DbSet { get; private set; }

        public IQueryable<TEntity> GetAllAsQueryable()
        {
            return this.DbSet.AsQueryable();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool isDeletedFlag = false)
        {
            return await this.DbSet
                .Where(t => t.IsDeleted == isDeletedFlag)
                .AsNoTracking()
                .ToListAsync<TEntity>();
        }

        public IQueryable<TEntity> GetByIdAsQueryable(Guid id, bool isDeletedFlag = false)
        {
            var model = this.DbSet
                .Where(t => t.Id == id && t.IsDeleted == isDeletedFlag);

            return model;
        }

        public async Task<TEntity> GetByIdAsync(Guid id, bool isDeletedFlag = false)
        {
            var model = await this.DbSet
                .Where(t => t.IsDeleted == isDeletedFlag)
                .AsNoTracking()
                .SingleOrDefaultAsync(t => t.Id == id);

            return model;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            entity.CreatedDate = DateTime.Now;

            var entityEntry = await this.DbSet
                .AddAsync(entity)
                .AsTask();

            return entityEntry.Entity;
        }

        public TEntity Update(TEntity entity)
        {
            entity.ModifiedDate = DateTime.Now;
            var entry = this.DbContext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;

            return entry.Entity;
        }

        public TEntity HardDelete(TEntity entity)
        {
            var entry = this.DbContext.Remove(entity);

            return entry.Entity;
        }

        public TEntity Delete(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.DeletedDate = DateTime.Now;

            return this.Update(entity);
        }

        public TEntity Restore(TEntity entity)
        {
            entity.IsDeleted = false;
            entity.DeletedDate = null;
            entity.DeletedBy = null;

            return this.Update(entity);
        }

        public Task<int> SaveChangesAsync()
        {
            return this.DbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void DetachLocal(TEntity entity)
        {
            var local = this.DbSet
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(entity.Id));
            if (local != null)
            {
                this.DbContext.Entry(local).State = EntityState.Detached;
            }
            this.DbContext.Entry(entity).State = EntityState.Modified;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.DbContext?.Dispose();
            }
        }
    }
}
